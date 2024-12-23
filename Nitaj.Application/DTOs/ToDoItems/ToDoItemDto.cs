using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Nitaj.Application.DTOs.ToDoItems
{
    public class ToDoItemDto
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [DefaultValue(false)]
        public bool IsCompleted { get; set; } = false;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
