using System.ComponentModel.DataAnnotations;

namespace TodoList.Models
{
    public class Status
    {
        [Key]
        public int? status_id { get; set; }
        public string? status_type { get; set; }
    }
}
