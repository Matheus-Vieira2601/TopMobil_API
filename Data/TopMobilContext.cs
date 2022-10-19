using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using TopMobil_API.Models;

namespace TopMobil_API.Data
{
    public class TopMobilContext: DbContext
    {
        protected readonly IConfiguration Configuration;
        public TopMobilContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server with connection string from app settings
            options.UseSqlServer(Configuration.GetConnectionString("StringConexaoSQLServer"));
        }

        public DbSet<Veiculo> Veiculo { get; set; }

        public DbSet<CadastroCliente> CadastroCliente { get; set; }

    }
}