using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoList.Models
{
    public class Todo
    {
        [Key]

        [Display(Name = "ID")] 
        public int? id { get; set; }

        [Display(Name = "Title")]
        public string? title { get; set; }

        [Display(Name = "Status")]
        public int status_id { get; set; }

        [Display(Name = "Status ID")]
        [ForeignKey("status_id")]
        public Status? Status { get; set; }

      


    }
}
