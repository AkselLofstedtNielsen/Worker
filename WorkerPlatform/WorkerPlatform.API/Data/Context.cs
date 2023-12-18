using Microsoft.EntityFrameworkCore;
using WorkerPlatform.API.Models;

namespace WorkerPlatform.API.Data
{
    public class Context : DbContext
    {
        public DbSet<Employee> Employees {get; set;}
        public DbSet<WorkField> WorkFields {get; set;}
        public DbSet<Manager> Managers {get; set;}

        
        
    }
}