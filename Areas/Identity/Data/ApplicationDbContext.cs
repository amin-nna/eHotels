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
    }

    //We will define a data sets
    public DbSet<BienModel> Biens { get; set; }
    public DbSet<HotelChain> HotelChain { get; set; }
    public DbSet<CentralOffice> CentralOffice { get; set; }
    public DbSet<Hotel> Hotel { get; set; }
    public DbSet<HotelPhoneNumber> HotelPhoneNumber { get; set; }
    public DbSet<Models.Room> Room { get; set; }
    public DbSet<RoomAmenity> RoomAmenity { get; set; }
    public DbSet<RoomIssue> RoomIssue { get; set; }
    public DbSet<Booking> Booking { get; set; }
    public DbSet<Renting> Renting { get; set; }
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

