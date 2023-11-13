using _19290273_ERENCAN_TEKIN.Models;
using Microsoft.EntityFrameworkCore;

namespace _19290273_ERENCAN_TEKIN.Data
{
    public class TasksAPIDbContext : DbContext
    {
        public TasksAPIDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ToDoTasks> Tasks { get; set; }
    }
}
