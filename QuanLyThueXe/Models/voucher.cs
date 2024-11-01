namespace QuanLyThueXe.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("voucher")]
    public partial class voucher
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public voucher()
        {
            Rentals = new HashSet<rental>();
        }

        [Key]
        public int voucher_id { get; set; }

        [StringLength(255)]
        public string name { get; set; }

        public int? discount { get; set; }

        public int? status { get; set; }

        public int? quantity { get; set; }

        [StringLength(255)]
        public string date_expire { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rental> Rentals { get; set; }
    }
}
