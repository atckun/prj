using System.ComponentModel.DataAnnotations;

namespace AbstractProject.Abstractions.Models
{
    public class ItemModel
    {
        [Required]
        [MaxLength(length: 256)]
        public string Title { get; set; }
        
        [Required]
        [MaxLength(length: 4096)]
        public string Description { get; set; }
    }
}