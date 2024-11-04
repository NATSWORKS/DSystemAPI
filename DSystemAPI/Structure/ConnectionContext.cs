using DSystemAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace DSystemAPI.Structure
{
    public class ConnectionContext : DbContext
    {
        public DbSet<TaskModel> Tasks { get; set; }

        /*
        ========================
        OnConfiguring
        ------------------------
        Configuração de conexão.
        ========================
        */
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
