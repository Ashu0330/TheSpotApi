using System;
using System.Collections.Generic;
using ArtistHub.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ArtistHub.Domain.Context;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ArtistAvailability> ArtistAvailabilities { get; set; }

    public virtual DbSet<ArtistSample> ArtistSamples { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<CategoryMaster> CategoryMasters { get; set; }

    public virtual DbSet<Rating> Ratings { get; set; }

    public virtual DbSet<TblArtist> TblArtists { get; set; }

    public virtual DbSet<TblArtistMedium> TblArtistMedia { get; set; }

    public virtual DbSet<TblBooking> TblBookings { get; set; }

    public virtual DbSet<TblEvent> TblEvents { get; set; }

    public virtual DbSet<TblLounge> TblLounges { get; set; }

    public virtual DbSet<TblLoungeMedium> TblLoungeMedia { get; set; }

    public virtual DbSet<TblPayment> TblPayments { get; set; }

    public virtual DbSet<TblPoll> TblPolls { get; set; }

    public virtual DbSet<TblReview> TblReviews { get; set; }

    public virtual DbSet<TblRoleMaster> TblRoleMasters { get; set; }

    public virtual DbSet<TblTicket> TblTickets { get; set; }

    public virtual DbSet<TblUser> TblUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("musicdb");

        modelBuilder.Entity<ArtistAvailability>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ArtistAv__3214EC07570D05F9");

            entity.ToTable("ArtistAvailability", "dbo");

            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("available");
        });

        modelBuilder.Entity<ArtistSample>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ArtistSa__3214EC07EF93A0A3");

            entity.ToTable("ArtistSamples", "dbo");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FileUrl).HasMaxLength(500);
            entity.Property(e => e.Title).HasMaxLength(150);
            entity.Property(e => e.Type).HasMaxLength(50);
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Bookings__3214EC076D433FC1");

            entity.ToTable("Bookings", "dbo");

            entity.Property(e => e.BookingTime).HasMaxLength(50);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(20);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Categori__3214EC074A38AFC1");

            entity.ToTable("Categories", "dbo");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<CategoryMaster>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__19093A0BEE629403");

            entity.ToTable("CategoryMaster", "dbo");

            entity.Property(e => e.CategoryName).HasMaxLength(50);
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("isDeleted");
        });

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Ratings__3214EC07F421AFCF");

            entity.ToTable("Ratings", "dbo");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.RatingValue).HasColumnType("decimal(3, 2)");
        });

        modelBuilder.Entity<TblArtist>(entity =>
        {
            entity.HasKey(e => e.ArtistId).HasName("PK__Tbl_Arti__25706B509362A41B");

            entity.ToTable("Tbl_Artists", "dbo");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Instagram).HasMaxLength(500);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.IsVerified).HasDefaultValue(false);
            entity.Property(e => e.PricePerShow).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Rating)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(3, 2)");
            entity.Property(e => e.Spotify).HasMaxLength(500);
            entity.Property(e => e.TotalShows).HasDefaultValue(0);
            entity.Property(e => e.YouTube).HasMaxLength(500);
        });

        modelBuilder.Entity<TblArtistMedium>(entity =>
        {
            entity.HasKey(e => e.ArtistMediaId).HasName("PK__Tbl_Arti__169C555A6B57F8C3");

            entity.ToTable("Tbl_ArtistMedia", "dbo");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FileUrl).HasMaxLength(500);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.MediaCategory).HasMaxLength(50);
            entity.Property(e => e.Title).HasMaxLength(150);
        });

        modelBuilder.Entity<TblBooking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__Tbl_Book__73951AED226A365E");

            entity.ToTable("Tbl_Bookings", "dbo");

            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.BookingType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(20);
        });

        modelBuilder.Entity<TblEvent>(entity =>
        {
            entity.HasKey(e => e.EventId).HasName("PK__Tbl_Even__7944C810B571E221");

            entity.ToTable("Tbl_Events", "dbo");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.TicketPrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Title).HasMaxLength(150);
        });

        modelBuilder.Entity<TblLounge>(entity =>
        {
            entity.HasKey(e => e.LoungeId).HasName("PK__Tbl_Loun__50FD732C730B4D3E");

            entity.ToTable("Tbl_Lounges", "dbo");

            entity.Property(e => e.Address).HasMaxLength(300);
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.LoungeName).HasMaxLength(150);
            entity.Property(e => e.Rating)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(3, 2)");
        });

        modelBuilder.Entity<TblLoungeMedium>(entity =>
        {
            entity.HasKey(e => e.MediaId).HasName("PK__Tbl_Loun__B2C2B5CFB1BFC6BF");

            entity.ToTable("Tbl_LoungeMedia", "dbo");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.MediaType).HasMaxLength(20);
            entity.Property(e => e.MediaUrl).HasMaxLength(500);
        });

        modelBuilder.Entity<TblPayment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Tbl_Paym__9B556A38018D7AED");

            entity.ToTable("Tbl_Payments", "dbo");

            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Gateway).HasMaxLength(50);
            entity.Property(e => e.PaymentStatus).HasMaxLength(20);
            entity.Property(e => e.PaymentType).HasMaxLength(20);
        });

        modelBuilder.Entity<TblPoll>(entity =>
        {
            entity.HasKey(e => e.PollId).HasName("PK__Tbl_Poll__E1949E6A653A6000");

            entity.ToTable("Tbl_Polls", "dbo");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Question).HasMaxLength(300);
        });

        modelBuilder.Entity<TblReview>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__Tbl_Revi__74BC79CE267C735D");

            entity.ToTable("Tbl_Reviews", "dbo");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TargetType).HasMaxLength(20);
        });

        modelBuilder.Entity<TblRoleMaster>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Tbl_Role__8AFACE1A0AB20C03");

            entity.ToTable("Tbl_RoleMaster", "dbo");

            entity.HasIndex(e => e.RoleName, "UQ__Tbl_Role__8A2B61603D53DC7A").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<TblTicket>(entity =>
        {
            entity.HasKey(e => e.TicketId).HasName("PK__Tbl_Tick__712CC607E21A7700");

            entity.ToTable("Tbl_Tickets", "dbo");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<TblUser>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Tbl_User__1788CC4C96BA1495");

            entity.ToTable("Tbl_Users", "dbo");

            entity.HasIndex(e => e.Email, "UQ__Tbl_User__A9D10534B6CAAB43").IsUnique();

            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
