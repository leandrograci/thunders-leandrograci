using Microsoft.EntityFrameworkCore;
using Thunders.TaskGo.Domain.Entities;

namespace Thunders.TaskGo.Infra.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserEntity>> GetUsersAsync();
        Task<UserEntity> GetUserByIdAsync(Guid userId);
        Task AddAsync(UserEntity user);
        Task<UserEntity> UpdateAsync(UserEntity user);
    }
    public class UserRepository : IUserRepository
    {
        private readonly ThundersTaskGoDbContext _dbContext;

        public UserRepository(ThundersTaskGoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(UserEntity user)
        {
            _dbContext.User.Add(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserEntity>> GetUsersAsync()
        {
            return await _dbContext.User.ToListAsync();
        }

        public async Task<UserEntity> GetUserByIdAsync(Guid userId)
        {
            return await _dbContext.User.FirstOrDefaultAsync(user => user.Id == userId);
        }

        public async Task<UserEntity> UpdateAsync(UserEntity user)
        {
            _dbContext.User.Update(user);
            _dbContext.Entry(user).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return user;
        }
    }
}
