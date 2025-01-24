using GameBook.Server.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace GameBook.Server.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<DataItem> Items { get; set; }
        public DbSet<DataItemCategory> ItemCategories { get; set; }
        public DbSet<DataConsumableItem> ConsumableItems { get; set; }
        public DbSet<DataInteractible> Interactibles { get; set; }
        public DbSet<DataInteractiblesOption> InteractiblesOptions { get; set; }
        public DbSet<DataInteractOption> InteractOptions { get; set; }
        public DbSet<DataInteractiblesItem> InteractiblesItems { get; set; }
        public DbSet<DataDialog> Dialogs { get; set; }
        public DbSet<DataDialogResponse> DialogResponses { get; set; }
        public DbSet<DataLocation> Locations { get; set; }
        public DbSet<DataLocationContent> LocationContent { get; set; }
        public DbSet<DataLocationPath> LocationPaths { get; set; }
        public DbSet<DataRequiredItems> RequiredItems { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=database.db");
        }
    }
}
