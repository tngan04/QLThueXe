using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace QuanLyThueXe.Models
{
    public partial class QuanLyThueXeContext : DbContext
    {
        public QuanLyThueXeContext()
            : base("name=QuanLyThueXeContext")
        {
        }

        public virtual DbSet<rental> rentals { get; set; }
        public virtual DbSet<role> roles { get; set; }
        public virtual DbSet<user> users { get; set; }
        public virtual DbSet<vehicle> vehicles { get; set; }
        public virtual DbSet<voucher> vouchers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<rental>()
                .Property(e => e.date_rental)
                .IsUnicode(false);

            modelBuilder.Entity<role>()
                .HasMany(e => e.users)
                .WithRequired(e => e.role)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e.rentals)
                .WithRequired(e => e.user)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<vehicle>()
                .HasMany(e => e.rentals)
                .WithRequired(e => e.vehicle)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<voucher>()
                .Property(e => e.date_expire)
                .IsUnicode(false);
        }
    }
}
