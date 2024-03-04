using Microsoft.EntityFrameworkCore;
using Thunders.TaskGo.Domain.Entities;
using Thunders.TaskGo.Infra;

namespace Thunders.TaskGo.Infra.Repositories
{
    public interface ITaskItemRepository
    {
        Task<IEnumerable<TaskItemEntity>> GetTaskItemsAsync();
        Task<TaskItemEntity> GetTaskItemByIdAsync(Guid taskItemId);
        Task AddAsync(TaskItemEntity taskItem);
        Task<TaskItemEntity> UpdateAsync(TaskItemEntity taskItem);
        Task DeleteAsync(TaskItemEntity taskItem);
    }

    public class TaskItemRepository : ITaskItemRepository
    {
        private readonly ThundersTaskGoDbContext _dbContext;
        public TaskItemRepository(ThundersTaskGoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(TaskItemEntity taskItem)
        {
            _dbContext.TaskItem.Add(taskItem);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(TaskItemEntity taskItem)
        {
            _dbContext.TaskItem.Remove(taskItem);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<TaskItemEntity> GetTaskItemByIdAsync(Guid taskItemId)
        {
            return await _dbContext.TaskItem.FirstOrDefaultAsync(taskItem => taskItem.Id == taskItemId);
        }

        public async Task<IEnumerable<TaskItemEntity>> GetTaskItemsAsync()
        {
            return await _dbContext.TaskItem.Include(c => c.User).ToListAsync();
        }

        public async Task<TaskItemEntity> UpdateAsync(TaskItemEntity taskItem)
        {            
            _dbContext.TaskItem.Update(taskItem);
            _dbContext.Entry(taskItem).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return taskItem;
        }
    }
}
