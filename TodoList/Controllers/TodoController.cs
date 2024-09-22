using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TodoList.DTOs;
using TodoList.Models;
using AutoMapper;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace TodoList.Controllers
{
    public class TodoController : Controller
    {
        private readonly TodoListDBContext _context;
        private readonly IMapper _mapper;

        public TodoController(TodoListDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IActionResult Index(string search, string statusFilter)
        {
            var statusList = _context.Status
                            .Select(s => new
                            {
                                s.status_id,
                                s.status_type
                            })
                            .ToList();

            ViewBag.StatusList = new SelectList(statusList, "status_id", "status_type");

            ViewData["CurrentSearch"] = search;
            ViewData["CurrentStatusFilter"] = statusFilter;

            var statusOptions = new List<SelectListItem>
    {
        new SelectListItem { Value = "", Text = "All", Selected = string.IsNullOrEmpty(statusFilter) },
        new SelectListItem { Value = "completed", Text = "Completed", Selected = statusFilter == "completed" },
        new SelectListItem { Value = "processing", Text = "Processing", Selected = statusFilter == "processing" },
        new SelectListItem { Value = "pending", Text = "Pending", Selected = statusFilter == "pending" }
    };

            
            ViewBag.StatusOptions = statusOptions;

            // Query to join Todo and Status and project it to the ViewModel
            var query = from s in _context.Status
                        join t in _context.Todo
                        on s.status_id equals t.status_id
                        select new TodoListViewModel
                        {
                            status_id = s.status_id,
                            status_type = s.status_type,
                            title = t.title,
                            id = t.id,
                        };

            // Apply filtering by search term
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(t => t.title.Contains(search) || t.status_type.Contains(search));
            }

            //Filtering
            if (!string.IsNullOrEmpty(statusFilter))
            {
              
                query = query.Where(t => t.status_type == statusFilter);
            }


            var todoList = query.ToList();
            return View(todoList);
        }

        public IActionResult Create()
        {
            var statusList = _context.Status
                            .Select(s => new
                            {
                                s.status_id,
                                s.status_type
                            })
                            .ToList();

            ViewBag.StatusList = new SelectList(statusList, "status_id", "status_type");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Todo todo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(todo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(todo);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var todo = await _context.Todo.FindAsync(id);

            if (todo == null)
            {
                return NotFound();
            }

            var statusList = _context.Status
                            .Select(s => new
                            {
                                s.status_id,
                                s.status_type
                            })
                            .ToList();

            ViewBag.StatusList = new SelectList(statusList, "status_id", "status_type", todo.status_id);
            return View(todo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Todo todo)
        {
            if (id != todo.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            { 
                {
                    _context.Update(todo);
                    await _context.SaveChangesAsync();
                }
                
                return RedirectToAction(nameof(Index));
            }
            var statusList = _context.Status
                            .Select(s => new
                            {
                                s.status_id,
                                s.status_type
                            })
                            .ToList();
            ViewBag.StatusList = new SelectList(statusList, "status_id", "status_type", todo.status_id);
            return View(todo);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var todo = await _context.Todo.FindAsync(id);

            if (todo == null)
            {
                return NotFound();
            }
            return View(todo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var todo = await _context.Todo.FindAsync(id);

            if (todo == null)
            {
                return NotFound();
            }

            _context.Todo.Remove(todo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TodoExists(int id)
        {
            return _context.Todo.Any(e => e.id == id);
        }
    }
}
