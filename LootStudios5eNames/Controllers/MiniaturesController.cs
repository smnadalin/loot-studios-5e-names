using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LootStudios5eNames.Data;
using LootStudios5eNames.Models;
using OfficeOpenXml;

namespace LootStudios5eNames.Controllers
{
    public class MiniaturesController : Controller
    {
        private readonly LootStudios5eNamesContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public MiniaturesController(LootStudios5eNamesContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Miniatures
        public async Task<IActionResult> Index(int? page, string sortOrder)
        {
            int pageSize = 6; // Set your desired page size here
            int pageNumber = page ?? 1;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["LS_PackSortParm"] = String.IsNullOrEmpty(sortOrder) ? "-LS_Pack" : "";
            ViewData["LS_NameSortParm"] = sortOrder == "LS_Name" ? "-LS_Name" : "LS_Name";

            var miniatures = _context.Miniature.Select(m => m);

            switch (sortOrder)
            {
                case "-LS_Pack":
                    miniatures = miniatures.OrderByDescending(m => m.LS_Pack);
                    break;
                case "LS_Name":
                    miniatures = miniatures.OrderBy(m => m.LS_Name);
                    break;
                case "-LS_Name":
                    miniatures = miniatures.OrderByDescending(m => m.LS_Name);
                    break;
                default:
                    miniatures = miniatures.OrderBy(m => m.LS_Pack);
                    break;
            }

            var result = await miniatures
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            int totalItems = await _context.Miniature.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            ViewData["TotalPages"] = totalPages;
            ViewData["CurrentPage"] = pageNumber;

            return View(result);
        }


        [HttpGet]
        public ActionResult LoadDataFromExcel()
        {
            string filePath = Path.Combine(Environment.CurrentDirectory,"wwwroot", "App_Data", "loot-studios-5e-names.xlsx");
            var data = ReadDataFromExcel(filePath);
            InsertDataIntoSqlServer(data);

            return View();
        }

        

        private List<Miniature> ReadDataFromExcel(string filePath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var data = new List<Miniature>();
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets.FirstOrDefault();
                if (worksheet == null)
                {
                    // Handle the case when there are no worksheets
                    throw new Exception("No worksheets found in the Excel file.");
                }
                
                // Ensure that the expected column headings are present
                var expectedHeadings = new[] { "LS_Pack", "LS_Name", "LS_Role", "LS_Race", "LS_Class", "LS_Size", "LS_Image", "_5e_Name", "_5e_Type", "_5e_Size" };

                for (int col = 1; col <= worksheet.Dimension.Columns; col++)
                {
                    var actualHeading = worksheet.Cells[1, col].Value?.ToString();
                    if (!expectedHeadings.Contains(actualHeading))
                    {
                        // Handle error or throw an exception indicating a mismatch
                        throw new Exception($"Expected heading '{expectedHeadings[col - 1]}' not found in Excel file.");
                    }
                }

                for (int row = 2; row <= worksheet.Dimension.Rows; row++)
                {
                    string? LS_Pack = worksheet.Cells[row, 1].Value.ToString();
                    string? LS_Name = worksheet.Cells[row, 2].Value.ToString();
                    string? LS_Role = worksheet.Cells[row, 3].Value.ToString();
                    string? LS_Race = worksheet.Cells[row, 4].Value.ToString();
                    string? LS_Class = worksheet.Cells[row, 5].Value.ToString();
                    string? LS_Size = worksheet.Cells[row, 6].Value.ToString();
                    string? LS_Image = worksheet.Cells[row, 7].Value.ToString();
                    string? _5e_Name = worksheet.Cells[row, 8].Value.ToString();
                    string? _5e_Type = worksheet.Cells[row, 9].Value.ToString();
                    string? _5e_Size = worksheet.Cells[row, 10].Value.ToString();
                    
                    // Create a new entity and add it to the list
                    var entity = new Miniature { LS_Pack = LS_Pack, LS_Name = LS_Name, LS_Role = LS_Role, LS_Race = LS_Race, LS_Class = LS_Class, LS_Size = LS_Size, LS_Image = LS_Image, _5e_Name = _5e_Name, _5e_Size = _5e_Size, _5e_Type = _5e_Type };
                    data.Add(entity);
                }
            }
            return data;
        }

        private void InsertDataIntoSqlServer(List<Miniature> data)
        {
            using (var context = _context)
            {
                // Insert data into SQL Server
                context.Miniature.AddRange(data);
                context.SaveChanges();
            }
        }
    }
}
