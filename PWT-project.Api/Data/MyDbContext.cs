using Microsoft.EntityFrameworkCore;
using PWT_project.Api.Models;

namespace PWT_project.Api.Data
{
    public class MyDbContext : DbContext
    {
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Vare>().ToTable("varer");
			modelBuilder.Entity<Vare>().HasNoKey();
			modelBuilder.Entity<Beholdning>().ToTable("beholdning");
			modelBuilder.Entity<Beholdning>().HasNoKey();


			base.OnModelCreating(modelBuilder);
		}

		public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options) { }

        public DbSet<Vare> Vare { get; set; }
		public DbSet<Beholdning> Beholdning { get; set; }

    }
}