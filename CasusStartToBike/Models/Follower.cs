namespace CasusStartToBike.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Follower")]
    public partial class Follower
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int FollowerId { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] Date { get; set; }

        public virtual User User { get; set; }

        public virtual User User1 { get; set; }
    }
}
