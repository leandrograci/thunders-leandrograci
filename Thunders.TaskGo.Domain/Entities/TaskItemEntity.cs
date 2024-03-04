using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Thunders.TaskGo.Domain.Entities
{
    public class TaskItemEntity : EntityBase
    {   
        public string Description { get; set; }

        public DateTime Start { get; set; } = DateTime.Now;

        public DateTime End { get; set; }
        public Guid UserId { get; set; }        

        [ForeignKey("UserId")]
        [JsonIgnore]
        public UserEntity User { get; set; }

   
    }
}
