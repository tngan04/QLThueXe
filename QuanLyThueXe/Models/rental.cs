namespace QuanLyThueXe.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("rental")]
    public partial class rental
    {
        [Key]
        public int rental_id { get; set; }

        public int user_id { get; set; }

        public int vehicle_id { get; set; }

        public int? voucher_id { get; set; }

        [Column(TypeName = "date")]
        public DateTime date_rental { get; set; }

        public int? number_day { get; set; }

        public decimal amount { get; set; }

        public int? status { get; set; }

        public int? number_vehicle { get; set; }

        public decimal? tiendatcocxe { get; set; }

        // Mối quan hệ với bảng User
        [ForeignKey("user_id")]
        public virtual user user { get; set; }

        // Mối quan hệ với bảng Vehicle
        [ForeignKey("vehicle_id")]
        public virtual vehicle vehicle { get; set; }

        // Mối quan hệ với bảng Voucher
        [ForeignKey("voucher_id")]
        public virtual voucher voucher { get; set; }
    }
}
