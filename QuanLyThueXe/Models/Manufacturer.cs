namespace QuanLyThueXe.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Manufacturer")]
    public partial class Manufacturer
    {
        [Key]
        [Column("manufacturer_id")] // Đảm bảo rằng tên cột là chính xác
        public int manufacturer_id { get; set; }

        [StringLength(255)]
        public string name { get; set; } // Tên hãng xe

     

        public virtual ICollection<vehicle> Vehicles { get; set; } // Mối quan hệ với bảng vehicle
    }
}
