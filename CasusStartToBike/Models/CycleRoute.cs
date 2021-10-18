namespace CasusStartToBike.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CycleRoute")]
    public partial class CycleRoute
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CycleRoute()
        {
            CycleEvent = new HashSet<CycleEvent>();
            Review = new HashSet<Review>();
            RouteLocation = new HashSet<RouteLocation>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string RouteName { get; set; }

        [StringLength(50)]
        public string RouteType { get; set; }

        public int Difficulty { get; set; }

        public bool IsPublic { get; set; }

        public int MakerId { get; set; }

        public int BadgeId { get; set; }

        public virtual Badge Badge { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CycleEvent> CycleEvent { get; set; }

        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Review> Review { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RouteLocation> RouteLocation { get; set; }
    }
}
