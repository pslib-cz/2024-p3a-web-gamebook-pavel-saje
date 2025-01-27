using GameBook.Server.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace GameBook.Server.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<DataConsumableItem> ConsumableItem { get; set; }
        public DbSet<DataInteractible> Interactible { get; set; }
        public DbSet<DataInteractiblesItem> InteractiblesItem { get; set; }
        public DbSet<DataItem> Item { get; set; }
        public DbSet<DataItemCategory> ItemCategory { get; set; }
        public DbSet<DataDialog> Dialog { get; set; }
        public DbSet<DataDialogResponse> DialogResponse { get; set; }
        public DbSet<DataInteractiblesOption> InteractiblesOption { get; set; }
        public DbSet<DataInteractOption> InteractOption { get; set; }
        public DbSet<DataLocation> Location { get; set; }
        public DbSet<DataLocationContent> LocationContent { get; set; }
        public DbSet<DataLocationPath> LocationPath { get; set; }
        public DbSet<DataRequiredItems> RequiredItems { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<DataDialog> DataDialogs { get; set; }
        public DbSet<DataDialogResponse> DataDialogResponses { get; set; }

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
