namespace CasusStartToBike.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Review")]
    public partial class Review
    {
        public int Id { get; set; }

        public int Rating { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public int MakerId { get; set; }

        public int? EventId { get; set; }

        public int? RouteId { get; set; }

        public virtual CycleEvent CycleEvent { get; set; }

        public virtual CycleRoute CycleRoute { get; set; }

        public virtual User User { get; set; }
    }
}
