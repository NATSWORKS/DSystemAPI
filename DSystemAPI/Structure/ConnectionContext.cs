using DSystemAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace DSystemAPI.Structure
{
    public class ConnectionContext : DbContext
    {
        public DbSet<TaskModel> Tasks { get; set; }

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
             => optionsBuilder.UseNpgsql(
                 "Server=localhost;" +
                 "Port=5432;Database=taskDb" +
                 "User Id=postgres;" +
                 "Password=102030");*/
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer(
         "Data Source=DESKTOP-QVNBIP4;" +
         "Database=master;" +
         "Integrated Security=SSPI;" +
         "TrustServerCertificate=True;" +
         "User ID=sa;" +
         "Password=102030;");
    }
}
