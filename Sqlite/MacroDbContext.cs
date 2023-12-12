using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macro.Sqlite;
internal class MacroDbContext : DbContext {
    public MacroDbContext(DbContextOptions opstions) : base(opstions) { }

    public DbContext Context => this;

    public DbSet<Image> Images { get; set; }
    public DbSet<Histogram> Histograms { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Image>()
            .HasKey(i => i.Id);
        modelBuilder.Entity<Histogram>()
            .HasKey(h => h.Id);

        modelBuilder.Entity<Image>()
            .HasOne(i => i.Histogram)
            .WithOne(h => h.Image)
            .HasForeignKey<Image>(i => i.HistogramId)
            .HasForeignKey<Histogram>(h => h.ImageId);
    }
}
