using System.ComponentModel.DataAnnotations;

namespace Thunders.TaskGo.Domain.DTO.TaskItem
{
    public class UpdateTaskItemDTO
    {

        [Required(ErrorMessage = "O código do usuário é obrigatório.")]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }



    }
}
