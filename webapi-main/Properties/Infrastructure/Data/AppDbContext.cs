using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<Property> Properties => Set<Property>();
    public DbSet<RoomType> RoomTypes => Set<RoomType>();
    public DbSet<Reservation> Reservations => Set<Reservation>();

    protected override void OnModelCreating( ModelBuilder modelBuilder )
    {
        modelBuilder.Entity<Reservation>()
            .HasOne( r => r.RoomType )
            .WithMany()
            .HasForeignKey( r => r.RoomTypeId )
            .OnDelete( DeleteBehavior.Restrict );

        modelBuilder.Entity<Reservation>()
            .HasOne( r => r.Property )
            .WithMany()
            .HasForeignKey( r => r.PropertyId )
            .OnDelete( DeleteBehavior.Restrict );

        modelBuilder.Entity<Reservation>()
            .Property( r => r.Total )
            .HasPrecision( 18, 2 );

        modelBuilder.Entity<RoomType>()
            .Property( r => r.DailyPrice )
            .HasPrecision( 18, 2 );

        modelBuilder.Entity<RoomType>()
            .Property( r => r.Services )
            .HasConversion(
                v => string.Join( ',', v ),
                v => v.Split( ',', StringSplitOptions.RemoveEmptyEntries ).ToList() );

        modelBuilder.Entity<RoomType>()
            .Property( r => r.Amenities )
            .HasConversion(
                v => string.Join( ',', v ),
                v => v.Split( ',', StringSplitOptions.RemoveEmptyEntries ).ToList() );

        modelBuilder.Entity<Property>()
            .HasIndex( p => p.City )
            .HasDatabaseName( "IX_Properties_City" );

        modelBuilder.Entity<Reservation>()
            .HasIndex( r => new { r.ArrivalDate, r.DepartureDate } )
            .HasDatabaseName( "IX_Reservations_Dates" );
    }

    public AppDbContext( DbContextOptions<AppDbContext> options )
        : base( options )
    {
    }
}
