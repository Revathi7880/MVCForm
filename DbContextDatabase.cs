using dotNetProject.Models;
using Microsoft.EntityFrameworkCore;

namespace dotNetProject
{
    public class DbContextDatabase : DbContext
    {
        public DbContextDatabase(DbContextOptions<DbContextDatabase> options) : base(options) { }
        public DbSet<FormDataModal> UserData { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FormDataModal>().ToTable("form_data", "public");
            modelBuilder.Entity<FormDataModal>().HasKey(f => f.UserId);
        }
    }
}
