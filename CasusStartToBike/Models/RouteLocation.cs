namespace CasusStartToBike.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("RouteLocation")]
    public partial class RouteLocation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RouteLocation() { }

        public int Id { get; set; }

        [Required]
        public string Location { get; set; }

        [ForeignKey("CycleRoute")]
        [Required]
        public int RouteId { get; set; }

        [ForeignKey("routeLocation")]
        public int? LastLocationId { get; set; }

        public virtual CycleRoute CycleRoute { get; set; }

        public virtual RouteLocation routeLocation { get; set; }
    }
}
