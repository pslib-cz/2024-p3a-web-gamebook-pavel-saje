using GameBook.Server.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace GameBook.Server.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<DataConsumableItem> ConsumableItems { get; set; }
        public DbSet<DataInteractible> Interactibles { get; set; }
        public DbSet<DataInteractiblesItem> InteractiblesItems { get; set; }
        public DbSet<DataItem> Items { get; set; }
        public DbSet<DataItemCategory> ItemCategories { get; set; }
        public DbSet<DataDialog> Dialogs { get; set; }
        public DbSet<DataDialogResponse> DialogResponses { get; set; }
        public DbSet<DataInteractiblesOption> InteractiblesOptions { get; set; }
        public DbSet<DataInteractOption> InteractOptions { get; set; }
        public DbSet<DataLocation> Locations { get; set; }
        public DbSet<DataLocationContent> LocationContents { get; set; }
        public DbSet<DataLocationPath> LocationPaths { get; set; }
        public DbSet<DataRequiredItems> RequiredItems { get; set; }
        public DbSet<DataEnd> Ends { get; set; }
        public DbSet<DataNpc> Npcs { get; set; }
        public DbSet<DataWeapon> Weapons { get; set; }
        public DbSet<DataInteractiblesNpc> InteractiblesNpcs { get; set; }
        public DbSet<DataTrade> Trade { get; set; }
        public DbSet<DataTrades> Trades { get; set; }
        public DbSet<DataBuy> Buy { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DataDialog>()
                .HasMany(d => d.DialogResponses)
                .WithOne(dr => dr.Dialog)
                .HasForeignKey(dr => dr.DialogID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DataDialogResponse>()
                .HasOne(dr => dr.NextDialog)
                .WithMany()
                .HasForeignKey(dr => dr.NextDialogID)
                .OnDelete(DeleteBehavior.Restrict);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=database.db");
        }
    }
}
