using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using eHotels.Models;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Twilio.TwiML.Voice;

namespace eHotels.Areas.Identity.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    //This class will be responsile of declaring the Tables and inject the connection string 
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
        builder.Entity<RoomAmenities>()
                .HasKey(ra => new { ra.RoomNumber, ra.Amenity });
        builder.Entity<RoomIssues>()
                .HasKey(ra => new { ra.RoomNumber, ra.Problem });
    }

    //We will define a data sets
    public DbSet<HotelChains> HotelChain { get; set; }
    public DbSet<CentralOffices> CentralOffice { get; set; }
    public DbSet<Hotels> Hotel { get; set; }
    public DbSet<HotelPhoneNumbers> HotelPhoneNumber { get; set; }
    public DbSet<Models.Rooms> Room { get; set; }
    public DbSet<RoomAmenities> RoomAmenity { get; set; }
    public DbSet<RoomIssues> RoomIssue { get; set; }
    public DbSet<Bookings> Booking { get; set; }
    public DbSet<Rentings> Renting { get; set; }
    //public DbSet<ImageModel> ImagesBiens { get; set; }

}

public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(u => u.FirstName).HasMaxLength(255);
        builder.Property(u => u.LastName).HasMaxLength(255);
        builder.Property(u => u.SIN).HasMaxLength(255);
        builder.Property(u => u.Street).HasMaxLength(255);
        builder.Property(u => u.City).HasMaxLength(255);
        builder.Property(u => u.Province).HasMaxLength(255);
        builder.Property(u => u.PostalCode).HasMaxLength(255);

    }
}

