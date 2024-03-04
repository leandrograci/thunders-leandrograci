using System.ComponentModel.DataAnnotations;

namespace Thunders.TaskGo.Domain.Entities
{
    public class EntityBase
    {        
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
