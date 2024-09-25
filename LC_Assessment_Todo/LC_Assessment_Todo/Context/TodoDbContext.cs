using LC_Assessment_Todo.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace LC_Assessment_Todo.Context
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
        {

        }

        public DbSet<TodoTask> Tasks { get; set; }
    }
}
