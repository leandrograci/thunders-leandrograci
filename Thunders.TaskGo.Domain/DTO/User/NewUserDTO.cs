using System.ComponentModel.DataAnnotations;

namespace Thunders.TaskGo.Domain.DTO.User
{
    public class NewUserDTO
    {
        [Required(ErrorMessage = "O campo de Email é obrigatório.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo de Senha é obrigatório.")]
        public string Password { get; set; }

        public bool Active { get; set; }
    }
}
