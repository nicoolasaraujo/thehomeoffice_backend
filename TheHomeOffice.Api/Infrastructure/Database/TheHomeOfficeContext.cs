using System.IO;
using AutoMapper.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TheHomeOffice.Api.Domain.Models;

namespace TheHomeOffice.aApi.Infrastructure.Database
{
    public class TheHomeOfficeContext : DbContext
    {

        public TheHomeOfficeContext(DbContextOptions<TheHomeOfficeContext> options) : base(options)
        { }

        public DbSet<User> Users { get; set; }

        public Microsoft.Extensions.Configuration.IConfiguration Configuration { get; private set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            this.Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            optionsBuilder.UseNpgsql(this.Configuration.GetConnectionString("DefaultConnection"));
        }
    }
}
