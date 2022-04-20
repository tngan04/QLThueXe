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

        [StringLength(255)]
        public string date_rental { get; set; }

        public int? number_day { get; set; }

        public int? amount { get; set; }

        public int? status { get; set; }

        public virtual user user { get; set; }

        public virtual vehicle vehicle { get; set; }

        public virtual voucher voucher { get; set; }
    }
}
