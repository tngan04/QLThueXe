using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QuanLyThueXe.Models
{
    public class RevenueStatistics
    {
        [Key]
        [Column("revenue_id")]  // Đảm bảo Entity Framework ánh xạ tới đúng tên cột trong cơ sở dữ liệu
        public int revenue_id { get; set; }

        [Column("amount")]
        public decimal? Amount { get; set; }

        [Column("date")]
        public DateTime Date { get; set; }

        [Column("description")]
        public string Description { get; set; }
    }
}