namespace BoardingHouse.Entities.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DbContexEntities : DbContext
    {
        public DbContexEntities()
            : base("name=DbContexEntities")
        {
        }

        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<Error> Errors { get; set; }
        public virtual DbSet<MoreInfomation> MoreInfomations { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<PostType> PostTypes { get; set; }
        public virtual DbSet<Province> Provinces { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<RoomType> RoomTypes { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<SystemSetting> SystemSettings { get; set; }
        public virtual DbSet<Ward> Wards { get; set; }
        public virtual DbSet<AuditLog> AuditLogs { get; set; }
        public virtual DbSet<PaymentCard> PaymentCards { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>()
                .Property(e => e.Alias)
                .IsUnicode(false);

            modelBuilder.Entity<PostType>()
                .Property(e => e.Alias)
                .IsUnicode(false);
        }
    }
}
