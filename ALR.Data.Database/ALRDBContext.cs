using ALR.Domain.Entities;
using ALR.Domain.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace ALR.Data.Database
{
    public class ALRDBContext : DbContext
    {
        public ALRDBContext(DbContextOptions options) : base(options)
        {
        }



        public DbSet<ProfileEntity> Profiles { get; set; }
        public DbSet<UserEntity> UserEntities { get; set; }
        public DbSet<UserTokenEntity> UserTokenEnties { get; set; }
        public DbSet<PostEntity> PostEnties { get; set; }
        public DbSet<RequestEntity> RequestEntities { get; set; }
        public DbSet<FeedbackEntity> FeedbackEntities { get; set; }
        public DbSet<ServiceEntity> ServiceEntities { get; set; }
        public DbSet<ServicesPackageEntity> ServicesPackages { get; set; }
        public DbSet<BillHistoryEntity> billHistoryEntities { get; set; }
        public DbSet<BoxChatEntity> BoxChatEntities { get; set; }
        public DbSet<BoxChatUserEntity> BoxChatUserEntities { get; set; }
        public DbSet<MessageEntity> MessageEntities { get; set; }
        public DbSet<BookingScheduleEntity> BookingScheduleEntities { get; set; }
        public DbSet<RoomEntity> RoomEntities { get; set; }
        public DbSet<MotelAddress> MotelAddresses { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Disable cascade delete for all relationships
            modelBuilder.Entity<BookingScheduleEntity>()
        .HasOne(e => e.tenant)
        .WithMany()
        .HasForeignKey(e => e.tenantId)
        .OnDelete(DeleteBehavior.Restrict);
            

        }
        protected ALRDBContext()
        {
        }
    }
}
