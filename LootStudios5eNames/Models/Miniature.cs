using System.ComponentModel.DataAnnotations;

namespace LootStudios5eNames.Models
{
    public class Miniature
    {
        public int Id { get; set; }
        [Display(Name = "Pack")]
        public string? LS_Pack { get; set; }
        [Display(Name = "Name")]
        public string? LS_Name { get; set; }
        [Display(Name = "Role")]
        public string? LS_Role { get; set; }
        [Display(Name = "Race")]
        public string? LS_Race { get; set; }
        [Display(Name = "Class")]
        public string? LS_Class { get; set; }
        [Display(Name = "Size")]
        public string? LS_Size { get; set; }
        [Display(Name = "Image")]
        public string? LS_Image { get; set; }
        [Display(Name = "5e Name")]
        public string? _5e_Name { get; set; }
        [Display(Name = "5e Type")]
        public string? _5e_Type { get; set; }
        [Display(Name = "5e Size")]
        public string? _5e_Size { get; set; }

        public Miniature()
        {

        }
    }
}
