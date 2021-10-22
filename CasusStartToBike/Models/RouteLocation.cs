namespace CasusStartToBike.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RouteLocation")]
    public partial class RouteLocation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RouteLocation()
        {
            RouteLocation1 = new HashSet<RouteLocation>();
        }

        public int Id { get; set; }

        public int RouteId { get; set; }

        [Required]
        public string Location { get; set; }

        public int? LastLocationId { get; set; }

        public virtual CycleRoute CycleRoute { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RouteLocation> RouteLocation1 { get; set; }

        public virtual RouteLocation RouteLocation2 { get; set; }
    }
}
