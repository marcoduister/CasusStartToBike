namespace CasusStartToBike.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Badge")]
    public partial class Badge
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Badge()
        {
            CycleEvent = new HashSet<CycleEvent>();
            CycleRoute = new HashSet<CycleRoute>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string BadgeName { get; set; }

        [Required]
        [StringLength(255)]
        public string BadgeImage { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CycleEvent> CycleEvent { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CycleRoute> CycleRoute { get; set; }
    }
}
