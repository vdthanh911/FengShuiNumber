using FengShuiNumber.Common.AppSetting;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace FengShuiNumber.Models
{
    public partial class FengShuiNumberDbContext : DbContext
    {
        private readonly IAppSetting _appSetting;
        public FengShuiNumberDbContext(IAppSetting appSetting)
        {
            _appSetting = appSetting;
        }

        public FengShuiNumberDbContext(DbContextOptions<FengShuiNumberDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MobileNumber> MobileNumbers { get; set; }
        public virtual DbSet<NetworkProvider> NetworkProviders { get; set; }
        public virtual DbSet<NetworkProviderPrefix> NetworkProviderPrefixes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connection_string = _appSetting.GetConnectionStringFromAppSetting("DefaultConnection");
                optionsBuilder.UseSqlServer(connection_string);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<MobileNumber>(entity =>
            {
                entity.ToTable("MobileNumber");

                entity.Property(e => e.MobileNumber1)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("MobileNumber");

                entity.HasOne(d => d.NetworkProvider)
                    .WithMany(p => p.MobileNumbers)
                    .HasForeignKey(d => d.NetworkProviderId)
                    .HasConstraintName("FK_MobileNumber_NetworkProvider");
            });

            modelBuilder.Entity<NetworkProviderPrefix>(entity =>
            {
                entity.ToTable("NetworkProviderPrefix");

                entity.Property(e => e.Prefix)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("Prefix");

                entity.HasOne(d => d.NetworkProvider)
                    .WithMany(p => p.NetworkProviderPrefixes)
                    .HasForeignKey(d => d.NetworkProviderId)
                    .HasConstraintName("FK_NetworkProviderPrefix_NetworkProvider");
            });

            modelBuilder.Entity<NetworkProvider>(entity =>
            {
                entity.ToTable("NetworkProvider");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
