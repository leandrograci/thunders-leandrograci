using Thunders.TaskGo.Domain.DTO.TaskItem;
using Thunders.TaskGo.Domain.Entities;
using Thunders.TaskGo.Domain.Exceptions;
using Thunders.TaskGo.Infra.Repositories;

namespace Thunders.TaskGo.Service.Services;

public interface ITaskItemService
{
    Task<IEnumerable<TaskItemEntity>> GetTaskItemsAsync();
    Task<TaskItemEntity> GetTaskItemByIdAsync(Guid taskItemId);
    Task AddTaskItemAsync(NewTaskItemDTO taskItem);
    Task<TaskItemEntity> UpdateTaskItemAsync(UpdateTaskItemDTO taskItem);
    Task DeleteTaskItemAsync(Guid taskItemId);

}

public class TaskItemService : ITaskItemService
{
    private readonly ITaskItemRepository _taskItemRepository;    

    public TaskItemService(ITaskItemRepository taskItemRepository)
    {
        _taskItemRepository = taskItemRepository;    
    }

    public async Task<IEnumerable<TaskItemEntity>> GetTaskItemsAsync()
    {
        return await _taskItemRepository.GetTaskItemsAsync();
    }

    public async Task<TaskItemEntity> GetTaskItemByIdAsync(Guid taskItemId)
    {
        return await _taskItemRepository.GetTaskItemByIdAsync(taskItemId);
    }

    public async Task AddTaskItemAsync(NewTaskItemDTO taskItem)
    {
        if (taskItem.End <= taskItem.Start)
            throw new BusinessException("A data e hora final da tarefa deve ser maior que a data inicio");

        var newTaskItem = new TaskItemEntity() {     
              Id = new Guid(),
              Name = taskItem.Name, 
              Description = taskItem.Description,
              Start = taskItem.Start,
              End = taskItem.End,
              UserId = taskItem.UserId
        };
        await _taskItemRepository.AddAsync(newTaskItem);
    }

    public async Task<TaskItemEntity> UpdateTaskItemAsync(UpdateTaskItemDTO taskItem)
    {
        if (taskItem.End <= taskItem.Start)
            throw new BusinessException("A data e hora final da tarefa deve ser maior que a data inicio");

        var getTaskItem = await _taskItemRepository.GetTaskItemByIdAsync(taskItem.Id);
        if(getTaskItem == null)
            throw new BusinessException("Tarefa não encontrada!");

        getTaskItem.Name = taskItem.Name;
        getTaskItem.Description = taskItem.Description;
        getTaskItem.Start = taskItem.Start;
        getTaskItem.End = taskItem.End;

        return await _taskItemRepository.UpdateAsync(getTaskItem);
    }

    public async Task DeleteTaskItemAsync(Guid taskItemId)
    {
        var buscaTaskItem = await _taskItemRepository.GetTaskItemByIdAsync(taskItemId);
        if (buscaTaskItem == null)
            throw new BusinessException("Tarefa não encontrada!");

        await _taskItemRepository.DeleteAsync(buscaTaskItem);
    }
}
