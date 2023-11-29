using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LootStudios5eNames.Models;

namespace LootStudios5eNames.Data
{
    public class LootStudios5eNamesContext : DbContext
    {
        public LootStudios5eNamesContext (DbContextOptions<LootStudios5eNamesContext> options)
            : base(options)
        {
        }

        public DbSet<LootStudios5eNames.Models.Miniature> Miniature { get; set; } = default!;
    }
}
