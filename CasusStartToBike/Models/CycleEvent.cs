namespace CasusStartToBike.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CycleEvent")]
    public partial class CycleEvent
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CycleEvent()
        {
            Review = new HashSet<Review>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string EventName { get; set; }

        public DateTime EventStart { get; set; }

        public DateTime EventEnd { get; set; }

        [Required]
        [StringLength(50)]
        public string Location { get; set; }

        public int Difficulty { get; set; }

        public bool IsArchived { get; set; }

        public bool IsPublic { get; set; }

        public int MakerId { get; set; }

        public int BadgeId { get; set; }

        public int RouteId { get; set; }

        public virtual Badge Badge { get; set; }

        public virtual CycleRoute CycleRoute { get; set; }

        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Review> Review { get; set; }
    }
}
