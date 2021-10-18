using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace CasusStartToBike.Models
{
    public partial class STBDModel : DbContext
    {
        public STBDModel()
            : base("name=STBDModel")
        {
        }

        public virtual DbSet<Badge> Badge { get; set; }
        public virtual DbSet<CycleEvent> CycleEvent { get; set; }
        public virtual DbSet<CycleRoute> CycleRoute { get; set; }
        public virtual DbSet<Follower> Follower { get; set; }
        public virtual DbSet<Review> Review { get; set; }
        public virtual DbSet<RouteLocation> RouteLocation { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Account> Account { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Badge>()
                .Property(e => e.BadgeName)
                .IsUnicode(false);

            modelBuilder.Entity<Badge>()
                .Property(e => e.BadgeImage)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Badge>()
                .HasMany(e => e.CycleEvent)
                .WithRequired(e => e.Badge)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Badge>()
                .HasMany(e => e.CycleRoute)
                .WithRequired(e => e.Badge)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CycleEvent>()
                .Property(e => e.EventName)
                .IsUnicode(false);

            modelBuilder.Entity<CycleEvent>()
                .Property(e => e.Location)
                .IsUnicode(false);

            modelBuilder.Entity<CycleEvent>()
                .HasMany(e => e.Review)
                .WithOptional(e => e.CycleEvent)
                .HasForeignKey(e => e.EventId);

            modelBuilder.Entity<CycleRoute>()
                .Property(e => e.RouteName)
                .IsUnicode(false);

            modelBuilder.Entity<CycleRoute>()
                .Property(e => e.RouteType)
                .IsUnicode(false);

            modelBuilder.Entity<CycleRoute>()
                .HasMany(e => e.CycleEvent)
                .WithRequired(e => e.CycleRoute)
                .HasForeignKey(e => e.RouteId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CycleRoute>()
                .HasMany(e => e.Review)
                .WithOptional(e => e.CycleRoute)
                .HasForeignKey(e => e.RouteId);

            modelBuilder.Entity<CycleRoute>()
                .HasMany(e => e.RouteLocation)
                .WithRequired(e => e.CycleRoute)
                .HasForeignKey(e => e.RouteId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Follower>()
                .Property(e => e.Date)
                .IsFixedLength();

            modelBuilder.Entity<Review>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<RouteLocation>()
                .Property(e => e.Location)
                .IsUnicode(false);

            modelBuilder.Entity<RouteLocation>()
                .HasMany(e => e.RouteLocation1)
                .WithOptional(e => e.RouteLocation2)
                .HasForeignKey(e => e.LastLocationId);

            modelBuilder.Entity<User>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.CycleEvent)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.MakerId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.CycleRoute)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.MakerId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Follower)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Follower1)
                .WithRequired(e => e.User1)
                .HasForeignKey(e => e.FollowerId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Review)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.MakerId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Account)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.ProfileImage)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.Residence)
                .IsUnicode(false);
        }
    }
}
