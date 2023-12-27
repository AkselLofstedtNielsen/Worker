using Microsoft.EntityFrameworkCore;
using WorkerPlatform.API.Models;

namespace WorkerPlatform.API.Data
{
    public class Context : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<WorkField> WorkFields { get; set; }
        public DbSet<Manager> Managers { get; set; }

        public string DbPath { get; }

        public Context(DbContextOptions<Context> options) : base(options)
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Combine(path, "workerplatform.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");

    }
}