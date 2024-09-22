using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;


namespace TodoList.DTOs
{
    public class TodoListViewModel
    {
        [Display(Name = "ID")]
        public int? id { get; set; }
        [Display(Name = "Status ID")]
        public int? status_id { get; set; }

        [Display(Name = "Title")]
        public string? title { get; set; } = string.Empty;

        [Display(Name = "Status")]
        public string? status_type { get; set; } = string.Empty;

        public List<SelectListItem> StatusList { get; set; }

    }
}
  