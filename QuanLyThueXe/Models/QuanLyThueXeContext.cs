    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Linq;

    namespace QuanLyThueXe.Models
    {
        public partial class QuanLyThueXeContext : DbContext
        {
            public DbSet<RevenueStatistics> RevenueStatistics { get; set; }
            public DbSet<Manufacturer> Manufacturer { get; set; }
            public QuanLyThueXeContext()
                : base("name=QuanLyThueXeContext")
            {
            }

            public virtual DbSet<rental> Rentals { get; set; }
            public virtual DbSet<role> Roles { get; set; }
            public virtual DbSet<user> Users { get; set; }
            public virtual DbSet<vehicle> Vehicles { get; set; }
            public virtual DbSet<voucher> Vouchers { get; set; }
            public virtual DbSet<Revenue> Revenue { get; set; } // Đổi tên thành Revenue


            protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Revenue>()
           .ToTable("Revenue") // Chỉ định tên bảng
           .HasKey(r => r.revenue_id); // Thiết lập RevenueId là khóa chính

                modelBuilder.Entity<Revenue>()
                    .Property(r => r.Description)
                    .IsOptional(); // Nếu bạn muốn mô tả là tùy chọn

                modelBuilder.Entity<rental>()
                    .HasKey(r => r.rental_id); // Khóa chính cho Rental

                modelBuilder.Entity<rental>()
                    .Property(e => e.date_rental) // Giả sử bạn đã đổi tên từ date_rental
                    .IsRequired(); // Có thể thay đổi tùy thuộc vào yêu cầu của bạn

                modelBuilder.Entity<role>()
                    .HasMany(e => e.Users)
                    .WithRequired(e => e.role)
                    .WillCascadeOnDelete(false);

                modelBuilder.Entity<user>()
                    .HasMany(e => e.Rentals)
                    .WithRequired(e => e.user)
                    .WillCascadeOnDelete(false);

                modelBuilder.Entity<vehicle>()
                    .HasMany(e => e.Rentals)
                    .WithRequired(e => e.vehicle)
                    .WillCascadeOnDelete(false);

                modelBuilder.Entity<voucher>()
                    .Property(e => e.date_expire)
                    .IsUnicode(false);

          
            }
        }
    }
