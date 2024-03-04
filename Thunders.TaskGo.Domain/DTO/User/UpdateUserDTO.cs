namespace Thunders.TaskGo.Domain.DTO.User
{
    public class UpdateUserDTO
    {

        public Guid Id { get; set; }

        public string Password { get; set; }

        public bool Active { get; set; }
    }
}
