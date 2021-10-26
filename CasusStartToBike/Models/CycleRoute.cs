using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace CasusStartToBike.Models
{


    [Table("CycleRoute")]
    public partial class CycleRoute
    {
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

        [ForeignKey("User")]
        public int MakerId { get; set; }

        [ForeignKey("Badge")]
        public int? BadgeId { get; set; }

        public virtual Badge Badge { get; set; }
        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CycleEvent> CycleEvent { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Review> Review { get; set; }

        public virtual ICollection<RouteLocation> RouteLocation { get; set; }
    }
}
