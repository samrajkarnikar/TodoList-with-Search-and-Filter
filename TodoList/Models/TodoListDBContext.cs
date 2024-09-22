using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace TodoList.Models
{
    public class TodoListDBContext : DbContext
    {
        public TodoListDBContext(DbContextOptions<TodoListDBContext> options): base(options)
        {

        }

       protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-0UIUHS9; Initial Catalog=Todo_db; Trusted_Connection=True; MultipleActiveResultSets=True; TrustServerCertificate=true");
            base.OnConfiguring(optionsBuilder); ;
        
        }
        public DbSet<Todo> Todo { get; set; }

        public DbSet<Status> Status { get; set; }
    }

}

   
