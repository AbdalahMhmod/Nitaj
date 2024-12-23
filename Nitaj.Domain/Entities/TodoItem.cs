using System.ComponentModel.DataAnnotations;

namespace Nitaj.Domain.Entities
{
    public class TodoItem
    {
        // Id (int, Primary Key)
        // Title (string, required)
        // Description (string, optional)
        // IsCompleted (bool, default: false)
        // CreatedDate (DateTime, default: current date)
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(255, ErrorMessage = "Title cannot be longer than 255 characters.")]
        public string Title { get; set; }
        [StringLength(1000, ErrorMessage = "Description cannot be longer than 1000 characters.")]
        public string Description { get; set; }
        [Required]
        public bool IsCompleted { get; set; } = false;
        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }

}
