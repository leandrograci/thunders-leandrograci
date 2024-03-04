using System.Text.Json.Serialization;

namespace Thunders.TaskGo.Domain.Entities;

public class UserEntity : EntityBase
{
    public string Password { get; set; }    

    [JsonIgnore]
    public List<TaskItemEntity> TaskItems { get; set; }
}
