using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Thunders.TaskGo.Domain.DTO.TaskItem;
using Thunders.TaskGo.Service.Services;


[ApiController]
[Route("api/[controller]/[action]")]
public class TaskItemController : Controller
{
    private readonly ITaskItemService _taskItemService;

    public TaskItemController(ITaskItemService taskItemService)
    {
        _taskItemService = taskItemService;
    }

    [HttpGet]
    [Authorize(Policy = "BearerPolicy")]
    public async Task<IActionResult> GetTaskItems()
    {
        var response = await _taskItemService.GetTaskItemsAsync();

        return Ok(response);
    }


    [HttpPost]
    [Authorize(Policy = "BearerPolicy")]
    [SwaggerOperation(Summary = "Cria uma nova Tarefa", Description = "Cria uma nova Tarefa com base nos dados fornecidos.")]
    [SwaggerResponse(201, "Criado com sucesso", typeof(NewTaskItemDTO))]
    [SwaggerResponse(400, "Requisição inválida", typeof(void))]
    public async Task<IActionResult> AddTaskItem(NewTaskItemDTO model)
    {
        if (ModelState.IsValid)        
            await _taskItemService.AddTaskItemAsync(model);        

        return Ok(model);
    }
  
    [HttpPut]
    [Authorize(Policy = "BearerPolicy")]
    [SwaggerOperation(Summary = "Atualiza uma tarefa existente", Description = "Atualiza os detalhes de uma tarefa existente com base no ID.")]
    [SwaggerResponse(204, "Atualizado com sucesso", typeof(UpdateTaskItemDTO))]
    [SwaggerResponse(400, "Requisição inválida", typeof(void))]
    [SwaggerResponse(404, "Não encontrado", typeof(void))]
    public async Task<IActionResult> UpdateTaskItem(UpdateTaskItemDTO model)
    {
        if (ModelState.IsValid)
            await _taskItemService.UpdateTaskItemAsync(model);

        return Ok(model);
    }

    [HttpDelete]
    [Authorize(Policy = "BearerPolicy")]
    [SwaggerOperation(Summary = "Remove uma tarefa existente", Description = "Remove uma tarefa existente com base no ID.")]
    [SwaggerResponse(204, "Removido com sucesso", typeof(void))]
    [SwaggerResponse(400, "Requisição inválida", typeof(void))]
    [SwaggerResponse(404, "Não encontrado", typeof(void))]
    public async Task<IActionResult> DeleteTaskItem(Guid taskItemId)
    {
        if (ModelState.IsValid)
            await _taskItemService.DeleteTaskItemAsync(taskItemId);

        return Ok("Tarefa removida com sucesso!");
    }
}