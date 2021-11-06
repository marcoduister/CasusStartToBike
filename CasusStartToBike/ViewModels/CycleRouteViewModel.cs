using CasusStartToBike.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CasusStartToBike.ViewModels
{
    public class CycleRouteViewModel
    {
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

        public List<RouteLocation> RouteLocations { get; set; }
    }
}