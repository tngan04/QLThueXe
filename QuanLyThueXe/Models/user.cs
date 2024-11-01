namespace QuanLyThueXe.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class user
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public user()
        {
            Rentals = new HashSet<rental>();
        }

        [Key]
        public int user_id { get; set; }

        public int role_id { get; set; }

        [StringLength(255)]
        public string fullname { get; set; }

        [StringLength(255)]
        [EmailAddress(ErrorMessage = "Email không ?úng ??nh d?ng.")]
        public string email { get; set; }

        [StringLength(255)]
        public string phonenumber { get; set; }

        [StringLength(255)]
        public string gender { get; set; }

        [StringLength(255)]
        public string password { get; set; }
       

        [StringLength(255)]
        public string cccd { get; set; }
        [StringLength(255)]
        public string profile_picture { get; set; } // ???ng d?n ??n ?nh ng??i dùng

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rental> Rentals { get; set; }

        public virtual role role { get; set; }
    }
}
