using Microsoft.EntityFrameworkCore;
using Thunders.TaskGo.Domain.Entities;

namespace Thunders.TaskGo.Infra
{
    public class ThundersTaskGoDbContext : DbContext
    {
        public DbSet<UserEntity> User { get; set; }
        public DbSet<TaskItemEntity> TaskItem { get; set; }        

        public ThundersTaskGoDbContext(DbContextOptions<ThundersTaskGoDbContext> options)
        : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskItemEntity>()
                .HasOne(taskItem => taskItem.User)
                .WithMany(user => user.TaskItems)
                .HasForeignKey(taskItem => taskItem.UserId);
        }

    }
}   
