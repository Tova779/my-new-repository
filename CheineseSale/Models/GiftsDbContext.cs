using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CheineseSale.Models;

public partial class GiftsDbContext : DbContext
{
    public GiftsDbContext()
    {
    }

    public GiftsDbContext(DbContextOptions<GiftsDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Baskat> Baskats { get; set; }

    public virtual DbSet<Catagory> Catagories { get; set; }

    public virtual DbSet<Donter> Donters { get; set; }

    public virtual DbSet<Gift> Gifts { get; set; }

    public virtual DbSet<GiftsImage> GiftsImages { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserGift> UserGifts { get; set; }

    public virtual DbSet<Winner> Winners { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("data source=DESKTOP-L9S4R74;initial catalog=Chinese_Sale;Integrated Security=SSPI;Persist Security Info=False;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Baskat>(entity =>
        {
            entity.ToTable("Baskat");

            entity.HasOne(d => d.Gift).WithMany(p => p.Baskats)
                .HasForeignKey(d => d.GiftId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Baskat_Gift");

            entity.HasOne(d => d.User).WithMany(p => p.Baskats)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Baskat_User");
        });

        modelBuilder.Entity<Catagory>(entity =>
        {
            entity.ToTable("Catagory");

            entity.Property(e => e.CatagoryId).HasColumnName("Catagory_id");
            entity.Property(e => e.CatagoryName)
                .HasMaxLength(50)
                .HasColumnName("Catagory_name");
        });

        modelBuilder.Entity<Donter>(entity =>
        {
            entity.ToTable("Donter");

            entity.Property(e => e.DonterId).HasColumnName("Donter_id");
            entity.Property(e => e.DonterFirstName)
                .HasMaxLength(50)
                .HasColumnName("Donter_first_name");
            entity.Property(e => e.DonterLastName)
                .HasMaxLength(50)
                .HasColumnName("Donter_Last_Name");
            entity.Property(e => e.DonterMail)
                .HasMaxLength(50)
                .HasColumnName("Donter_Mail");
            entity.Property(e => e.DonterPhon)
                .HasMaxLength(50)
                .HasColumnName("Donter_Phon");
        });

        modelBuilder.Entity<Gift>(entity =>
        {
            entity.ToTable("Gift");

            entity.Property(e => e.GiftId).HasColumnName("Gift_Id");
            entity.Property(e => e.CatagoryId).HasColumnName("Catagory_id");
            entity.Property(e => e.Description).HasMaxLength(50);
            entity.Property(e => e.DonterId).HasColumnName("Donter_Id");
            entity.Property(e => e.GiftName)
                .HasMaxLength(50)
                .HasColumnName("Gift_name");
            entity.Property(e => e.ImageId).HasColumnName("Image_Id");

            entity.HasOne(d => d.Catagory).WithMany(p => p.Gifts)
                .HasForeignKey(d => d.CatagoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Gift_Catagory");

            entity.HasOne(d => d.Donter).WithMany(p => p.Gifts)
                .HasForeignKey(d => d.DonterId)
                .HasConstraintName("FK_Gift_Donter");

            entity.HasOne(d => d.Image).WithMany(p => p.Gifts)
                .HasForeignKey(d => d.ImageId)
                .HasConstraintName("FK_Gift_Gifts_Images");
        });

        modelBuilder.Entity<GiftsImage>(entity =>
        {
            entity.HasKey(e => e.ImageId);

            entity.ToTable("Gifts_Images");

            entity.Property(e => e.ImageId).HasColumnName("Image_Id");
            entity.Property(e => e.ImageName)
                .HasMaxLength(50)
                .HasColumnName("Image_Name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.UserId).HasColumnName("User_Id");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(256);
            entity.Property(e => e.UserAdress).HasMaxLength(50);
            entity.Property(e => e.UserEmail)
                .HasMaxLength(50)
                .HasColumnName("User_Email");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .HasColumnName("User_Name");
            entity.Property(e => e.UserPhone).HasMaxLength(50);
            entity.Property(e => e.UserRole).HasMaxLength(50);
        });

        modelBuilder.Entity<UserGift>(entity =>
        {
            entity.ToTable("UserGift");

            entity.HasOne(d => d.Gift).WithMany(p => p.UserGifts)
                .HasForeignKey(d => d.GiftId)
                .HasConstraintName("FK_UserGift_Gift");

            entity.HasOne(d => d.User).WithMany(p => p.UserGifts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserGift_User");
        });

        modelBuilder.Entity<Winner>(entity =>
        {
            entity.ToTable("Winner");

            entity.Property(e => e.WinnerId).HasColumnName("Winner_id");
            entity.Property(e => e.GiftId).HasColumnName("Gift_Id");
            entity.Property(e => e.UserId).HasColumnName("User_Id");

            entity.HasOne(d => d.Gift).WithMany(p => p.Winners)
                .HasForeignKey(d => d.GiftId)
                .HasConstraintName("FK_Winner_Gift");

            entity.HasOne(d => d.User).WithMany(p => p.Winners)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Winner_User");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
