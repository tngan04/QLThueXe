using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace QuanLyThueXe.Models
{

    public class ApplicationDbContext : DbContext
    {
        public DbSet<Revenue> Revenues { get; set; }

        public ApplicationDbContext() : base("QuanLyThueXeEntities") // Thay đổi tên chuỗi kết nối
        {
        }
    }
}