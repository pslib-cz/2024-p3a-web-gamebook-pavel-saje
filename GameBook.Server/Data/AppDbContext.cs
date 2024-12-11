using GameBook.Server.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace GameBook.Server.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Items> Items { get; set; }
        public DbSet<ItemCategories> ItemCategories { get; set; }
        public DbSet<ConsumableItems> ConsumableItems { get; set; }
        public DbSet<Interactibles> Interactibles { get; set; }
        public DbSet<InteractibleOptions> InteractibleOptions { get; set; }
        public DbSet<OptionsEnum> OptionsEnum { get; set; }
        public DbSet<InteractiblesItems> InteractiblesItems { get; set; }
        public DbSet<NpcDialog> NpcDialog { get; set; }
        public DbSet<NpcDialogResponses> NpcDialogResponses { get; set; }
        public DbSet<Locations> Locations { get; set; }
        public DbSet<LocationContent> LocationContent { get; set; }
        public DbSet<LocationPaths> LocationPaths { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=database.db");
        }
    }
}
