using GameBook.Server.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace GameBook.Server.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemCategory> ItemCategories { get; set; }
        public DbSet<ConsumableItem> ConsumableItems { get; set; }
        public DbSet<Interactible> Interactibles { get; set; }
        public DbSet<InteractiblesOption> InteractiblesOptions { get; set; }
        public DbSet<InteractOption> InteractOptions { get; set; }
        public DbSet<InteractiblesItem> InteractiblesItems { get; set; }
        public DbSet<Dialog> Dialogs { get; set; }
        public DbSet<DialogResponse> DialogResponses { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<LocationContent> LocationContent { get; set; }
        public DbSet<LocationPath> LocationPaths { get; set; }
        public DbSet<RequiredItems> RequiredItems { get; set; }
        public DbSet<End> End { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=database.db");
        }
    }
}
