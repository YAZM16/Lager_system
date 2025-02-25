using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace My_Project_03.Models;

public partial class VehiclePartsManagementContext : DbContext
{
    public VehiclePartsManagementContext()
    {
    }

    public VehiclePartsManagementContext(DbContextOptions<VehiclePartsManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<Inventory> Inventories { get; set; }
    public virtual DbSet<Manufacturer> Manufacturers { get; set; }
    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<OrderItem> OrderItems { get; set; }
    public virtual DbSet<Parts> Parts { get; set; }
    public virtual DbSet<VehicleModel> VehicleModels { get; set; }
    public virtual DbSet<VehicleType> VehicleTypes { get; set; }
    public virtual DbSet<Cart> Carts { get; set; }
    public virtual DbSet<User> Users { get; set; }  // Added Users

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=DESKTOP-8SDDV0E\\SQLEXPRESS;Database=VehiclePartsManagement;Integrated Security=True;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Categories
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Categori__19093A0BC78AE0D5");
            entity.Property(e => e.CategoryName).HasMaxLength(100);

            entity.HasOne(d => d.ParentCategory)
                .WithMany(p => p.InverseParentCategory)
                .HasForeignKey(d => d.ParentCategoryId)
                .HasConstraintName("FK_Category_ParentCategory");
        });

        // Inventory
        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__inventor__3213E83FDAC03CD5");
            entity.ToTable("inventory");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.LastUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("last_updated");
            entity.Property(e => e.PartId).HasColumnName("part_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Parts)
                .WithMany(p => p.Inventories)
                .HasForeignKey(d => d.PartId)
                .HasConstraintName("FK__inventory__parts___5CD6CB2B");
        });

        // Manufacturers
        modelBuilder.Entity<Manufacturer>(entity =>
        {
            entity.HasKey(e => e.ManufacturerId).HasName("PK__Manufact__357E5CC159511DC7");
            entity.Property(e => e.CountryOfOrigin).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        // Orders
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__C3905BCFD7C81FFF");
            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(10, 2)");
        });

        // Order Items
        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__order_it__3213E83F2E31D8EC");
            entity.ToTable("order_items");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.InventoryId).HasColumnName("inventory_id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.UnitPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("unit_price");

            entity.HasOne(d => d.Inventory)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.InventoryId)
                .HasConstraintName("FK__order_ite__inven__628FA481");
        });

        // Parts
        modelBuilder.Entity<Parts>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__parts__3213E83F279A2BFE");
            entity.ToTable("Parts");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CompatibilityNotes).HasColumnType("text").HasColumnName("compatibility_notes");
            entity.Property(e => e.Name).HasMaxLength(200).IsUnicode(false).HasColumnName("name");
            entity.Property(e => e.PartNumber).HasMaxLength(50).IsUnicode(false).HasColumnName("part_number");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)").HasColumnName("price");
            entity.Property(e => e.VehicleType).HasMaxLength(20).IsUnicode(false).HasColumnName("vehicle_type");
        });

        // Vehicle Models
        modelBuilder.Entity<VehicleModel>(entity =>
        {
            entity.HasKey(e => e.ModelId).HasName("PK__VehicleM__E8D7A12C5B3DE157");
            entity.Property(e => e.ModelName).HasMaxLength(100);
            entity.Property(e => e.YearRange).HasMaxLength(20);

            entity.HasOne(d => d.Manufacturer)
                .WithMany(p => p.VehicleModels)
                .HasForeignKey(d => d.ManufacturerId)
                .HasConstraintName("FK_VehicleModels_Manufacturers");
        });

        // Vehicle Types
        modelBuilder.Entity<VehicleType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__vehicle___3213E83FFE13BE29");
            entity.ToTable("vehicle_types");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description).HasColumnType("text").HasColumnName("description");
            entity.Property(e => e.TypeName).HasMaxLength(50).IsUnicode(false).HasColumnName("type_name");
        });

        // Carts
        modelBuilder.Entity<Cart>(entity =>
        {
            entity.ToTable("Cart");
            entity.HasOne(c => c.Parts).WithMany(p => p.Carts).HasForeignKey(c => c.PartId);
        });

        // Users (Fix for Primary Key Issue)
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.UserId);  // Defines primary key
            entity.HasIndex(u => u.Username).IsUnique();  // Ensures username is unique
            entity.HasIndex(u => u.Email).IsUnique();  // Ensures email is unique
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
