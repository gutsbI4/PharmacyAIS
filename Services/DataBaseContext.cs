using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PharmacyAIS.Models;

namespace PharmacyAIS.Services;

public partial class DataBaseContext : DbContext
{
    public DataBaseContext()
    {
    }

    public DataBaseContext(DbContextOptions<DataBaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customer { get; set; }

    public virtual DbSet<Department> Department { get; set; }

    public virtual DbSet<Employee> Employee { get; set; }

    public virtual DbSet<Manufacturer> Manufacturer { get; set; }

    public virtual DbSet<Order> Order { get; set; }

    public virtual DbSet<OrderStatus> OrderStatus { get; set; }

    public virtual DbSet<Product> Product { get; set; }

    public virtual DbSet<ProductOrder> ProductOrder { get; set; }

    public virtual DbSet<Role> Role { get; set; }

    public virtual DbSet<Supplie> Supplie { get; set; }

    public virtual DbSet<SupplieProduct> SupplieProduct { get; set; }

    public virtual DbSet<Supplier> Supplier { get; set; }

    public virtual DbSet<Unit> Unit { get; set; }

    public virtual DbSet<User> User { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-D045G8S; Initial Catalog=PharmacyAIS; Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.ContactInfo).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.Location).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.ContactInfo).HasMaxLength(50);
            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Position).HasMaxLength(50);
            entity.Property(e => e.Salary).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Department).WithMany(p => p.Employee)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK_Employee_Department");
        });

        modelBuilder.Entity<Manufacturer>(entity =>
        {
            entity.Property(e => e.ManufacturerId)
                .ValueGeneratedNever()
                .HasColumnName("ManufacturerID");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.OrderDate).HasColumnType("datetime");
            entity.Property(e => e.SaleDate).HasColumnType("datetime");
            entity.Property(e => e.StatusId).HasColumnName("StatusID");
            entity.Property(e => e.TotalCost).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Customer).WithMany(p => p.Order)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_Order_Customer");

            entity.HasOne(d => d.Status).WithMany(p => p.Order)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK_Order_OrderStatus");
        });

        modelBuilder.Entity<OrderStatus>(entity =>
        {
            entity.Property(e => e.OrderStatusId).HasColumnName("OrderStatusID");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.Dosage).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Image).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UnitId).HasColumnName("UnitID");

            entity.HasOne(d => d.Manufacturer).WithMany(p => p.Product)
                .HasForeignKey(d => d.ManufacturerId)
                .HasConstraintName("FK_Product_Manufacturer");

            entity.HasOne(d => d.Unit).WithMany(p => p.Product)
                .HasForeignKey(d => d.UnitId)
                .HasConstraintName("FK_Product_Unit");
        });

        modelBuilder.Entity<ProductOrder>(entity =>
        {
            entity.HasKey(e => new { e.OrderId, e.ProductId });

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Order).WithMany(p => p.ProductOrder)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_ProductOrder_Order");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductOrder)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_ProductOrder_Product");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Supplie>(entity =>
        {
            entity.Property(e => e.SupplieId).HasColumnName("SupplieID");
            entity.Property(e => e.SupplierId).HasColumnName("SupplierID");
            entity.Property(e => e.TotalCost).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Supplie)
                .HasForeignKey(d => d.SupplierId)
                .HasConstraintName("FK_Supplie_Supplier");
        });

        modelBuilder.Entity<SupplieProduct>(entity =>
        {
            entity.HasKey(e => new { e.SupplieId, e.ProductId });

            entity.Property(e => e.SupplieId).HasColumnName("SupplieID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Product).WithMany(p => p.SupplieProduct)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_SupplieProduct_Product");

            entity.HasOne(d => d.Supplie).WithMany(p => p.SupplieProduct)
                .HasForeignKey(d => d.SupplieId)
                .HasConstraintName("FK_SupplieProduct_Supplie");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.Property(e => e.SupplierId).HasColumnName("SupplierID");
            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.ContactInfo).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Unit>(entity =>
        {
            entity.Property(e => e.UnitId).HasColumnName("UnitID");
            entity.Property(e => e.Name).HasMaxLength(20);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.Image).HasMaxLength(80);
            entity.Property(e => e.Password).HasMaxLength(20);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.Username).HasMaxLength(15);

            entity.HasOne(d => d.Employee).WithMany(p => p.User)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_User_Employee");

            entity.HasOne(d => d.Role).WithMany(p => p.User)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_User_Role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
