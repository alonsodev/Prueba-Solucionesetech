using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Solucionesetech.DataAccess.Models
{
    public partial class TRAVELSContext : DbContext
    {
        public TRAVELSContext()
        {
        }

        public TRAVELSContext(DbContextOptions<TRAVELSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AvailableTravel> AvailableTravels { get; set; }
        public virtual DbSet<Destination> Destinations { get; set; }
        public virtual DbSet<Origin> Origins { get; set; }
        public virtual DbSet<Travel> Travels { get; set; }
        public virtual DbSet<Traveler> Travelers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-0KJB45B;Database=TRAVELS;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AvailableTravel>(entity =>
            {
                entity.ToTable("AvailableTravel");

                entity.Property(e => e.AvailableTravelId).HasColumnName("available_travel_id");

                entity.Property(e => e.Capacity).HasColumnName("capacity");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("code");

                entity.Property(e => e.DestinationId).HasColumnName("destination_id");

                entity.Property(e => e.OriginId).HasColumnName("origin_id");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("price");

                entity.HasOne(d => d.Destination)
                    .WithMany(p => p.AvailableTravels)
                    .HasForeignKey(d => d.DestinationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AvailableTravel_Destination");

                entity.HasOne(d => d.Origin)
                    .WithMany(p => p.AvailableTravels)
                    .HasForeignKey(d => d.OriginId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AvailableTravel_Origin");
            });

            modelBuilder.Entity<Destination>(entity =>
            {
                entity.ToTable("Destination");

                entity.Property(e => e.DestinationId).HasColumnName("destination_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Origin>(entity =>
            {
                entity.ToTable("Origin");

                entity.Property(e => e.OriginId).HasColumnName("origin_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Travel>(entity =>
            {
                entity.ToTable("Travel");

                entity.Property(e => e.TravelId).HasColumnName("travel_id");

                entity.Property(e => e.AvailableTravelId).HasColumnName("available_travel_id");

                entity.Property(e => e.TravelerId).HasColumnName("traveler_id");

                entity.HasOne(d => d.AvailableTravel)
                    .WithMany(p => p.Travels)
                    .HasForeignKey(d => d.AvailableTravelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Travel_AvailableTravel");

                entity.HasOne(d => d.Traveler)
                    .WithMany(p => p.Travels)
                    .HasForeignKey(d => d.TravelerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Travel_Traveler");
            });

            modelBuilder.Entity<Traveler>(entity =>
            {
                entity.ToTable("Traveler");

                entity.Property(e => e.TravelerId).HasColumnName("traveler_id");

                entity.Property(e => e.Address)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("address");

                entity.Property(e => e.IdentificationDocument)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("identification_document");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("phone");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
