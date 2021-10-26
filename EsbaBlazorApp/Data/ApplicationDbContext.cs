using FirebirdSql.Data.FirebirdClient;
using EsbaBlazorApp.Data;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.Common;
using Microsoft.Extensions.Configuration;

namespace EsbaBlazorApp.Data
{
    class ApplicationDbContext : DbContext
    {
        private static string _connectionString = "";
        
        public static void LoadConfig(IConfiguration configuration)
        {
            _connectionString = configuration.GetValue<string>("Database:FbConnection");
            
        }

        static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });


        public ApplicationDbContext()
        {            
        }
        
        public DbSet<CarreraGrupos> CarreraGrupos => Set<CarreraGrupos>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder                
                .UseLoggerFactory(MyLoggerFactory)
                .UseFirebird(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
    /*class MyContext : DbContext
    {
        public MyContext(string connectionString)
            : base(new FbConnection(connectionString), true)
        { }

        public DbSet<CarreraGrupos> CarreraGrupos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
        }
    }*/
}