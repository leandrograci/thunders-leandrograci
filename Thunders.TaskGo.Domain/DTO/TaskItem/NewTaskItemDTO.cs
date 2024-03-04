using System.ComponentModel.DataAnnotations;

namespace Thunders.TaskGo.Domain.DTO.TaskItem
{
    public class NewTaskItemDTO
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public Guid UserId { get; set; }
    }
}
