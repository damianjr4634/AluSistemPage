using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace EsbaBlazorAppAuth.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });

        private static string _connectionString = "";
        
        public static void LoadConfig(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            
        }

        /*public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
         Console.WriteLine(options);
        }*/

        public ApplicationDbContext()
        {            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder                
                .UseLoggerFactory(MyLoggerFactory)
                .UseFirebird(_connectionString);
        }


        public async Task<List<T>> QueryAsync<T>(string sql, object? param = null) where T : class
        {
            var query = await Database.GetDbConnection().QueryAsync<T>(sql, param);
            return query.ToList();
        }

        public async Task<DataTable> QueryAsync(string sql)
        {
            var dt = new DataTable();
            using (var command = Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = sql;
                using (var reader = await command.ExecuteReaderAsync())
                {
                    dt.Load(reader);
                }
            }
            return dt;
        }

        public async Task<int> ExecuteAsync(string sql, object? param = null)
        {
            return await Database.GetDbConnection().ExecuteAsync(sql, param);
        }

        public async Task<T> ExecuteScalarAsync<T>(string sql, object? param = null)
        {
            return await Database.GetDbConnection().ExecuteScalarAsync<T>(sql, param);
        }

        public async Task<T> QuerySingleOrDefaultAsync<T>(string sql, object? param = null) where T : class
        {
            return await Database.GetDbConnection().QuerySingleOrDefaultAsync<T>(sql, param);
        }
        
        public async Task<T> QuerySingleValueOrDefaultAsync<T>(string sql, object? param = null)
        {
            return await Database.GetDbConnection().QuerySingleOrDefaultAsync<T>(sql, param);
        }
                
        public DbSet<CarreraGrupos> CarreraGrupos => Set<CarreraGrupos>();

        public DbSet<Carrera> Carrera => Set<Carrera>();
        
    }
}
