using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasusStartToBike.Models
{


    [Table("Account")]
    public partial class Account
    {
        [Key, ForeignKey("User")]
        [Column(Order = 0)]
        public int UserId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string LastName { get; set; }

        [Key]
        [Column(Order = 3, TypeName = "date")]
        public DateTime Birthdate { get; set; }

        [StringLength(255)]
        public string ProfileImage { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(50)]
        public string Residence { get; set; }

        public int? Distance { get; set; }
        [Required]
        public virtual User User { get; set; }
    }
}
