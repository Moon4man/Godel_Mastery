using FileStorage.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FileStorage.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<MetaInformation> MetaInformation { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=FileStorage;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<MetaInformation>(MetaInfoConfigure);
            builder.ApplyConfiguration(new DbSeeds());
            base.OnModelCreating(builder);
        }

        public void MetaInfoConfigure(EntityTypeBuilder<MetaInformation> builder)
        {
            builder
                .Property(p => p.FileName)
                .HasMaxLength(100)
                .IsRequired();

            builder
                .Property(p => p.FileExtension)
                .HasMaxLength(10)
                .IsRequired();

            builder
                .Property(p => p.FileSize)
                .IsRequired();

            builder
                .Property(p => p.FileCreationDate)
                .HasMaxLength(15)
                .IsRequired();

            builder
                .Property(p => p.LastAccessTime)
                .HasMaxLength(15)
                .IsRequired();

            builder
                .Property(p => p.DownloadNumber)
                .IsRequired();
        }
    }
}
