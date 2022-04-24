namespace QuanLyThueXe.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("vehicle")]
    public partial class vehicle
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public vehicle()
        {
            rentals = new HashSet<rental>();
        }

        [Key]
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rental> rentals { get; set; }
    }
}
