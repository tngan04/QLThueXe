using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyThueXe.Models
{
    [Table("vehicle")]
    public partial class vehicle
    {
        [Key]
        [Column("vehicle_id")]
        public int vehicle_id { get; set; }


        [StringLength(255)]
        public string name { get; set; }

        [StringLength(500)]
        public string image { get; set; }

        [StringLength(255)]
        public string type_vehicle { get; set; }

        public int? price { get; set; }

        public string description { get; set; }

        public int? quantity { get; set; }

        public int? number_vehicle { get; set; }

        [ForeignKey("Manufacturer")]
        [Column("manufacturer_id")]
        public int? manufacturer_id { get; set; } // Thay đổi từ int thành int?

        // Thuộc tính liên kết với Manufacturer
        public virtual Manufacturer Manufacturer { get; set; }


        [StringLength(50)]
        public string fuel_type { get; set; }

        // Xóa thuộc tính Manufacturer
        // public virtual Manufacturer Manufacturer { get; set; } // Mối quan hệ với bảng manufacturer

        public virtual ICollection<rental> Rentals { get; set; }

        public vehicle()
        {
            Rentals = new HashSet<rental>();
        }
    }
}
