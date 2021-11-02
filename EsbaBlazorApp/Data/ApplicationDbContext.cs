using System.Data.Common;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using FirebirdSql.Data.FirebirdClient;
using EsbaBlazorApp.Data;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Dapper;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace EsbaBlazorApp.Data
{
    class ApplicationDbContext : IdentityDbContext<IdentityUser>
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
            base.OnModelCreating(modelBuilder);
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