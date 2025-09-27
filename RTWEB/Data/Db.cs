using Microsoft.EntityFrameworkCore;
using ZPWEB.Models;

namespace ZPWEB.Data
{
    public class Db:DbContext
    {
        public static string ConnectionString = "Server=localhost;Database=zpweb;User Id=sa;Password=Test_123;Encrypt=False";

        //public static string ConnectionString = "Server=103.125.252.243;Database=demo;User Id=oct_demo;Password=hbswiplv4czmyjfqdexn;Encrypt=False";
        //TempData["Message"] = "✅ Save Successful";
        //TempData["Message"] = "❌ Save Successful";
        public Db()
        {

        }

        public Db(DbContextOptions<Db> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Instractor> Instractors { get; set; }
        public DbSet<Method> Methods { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Student> Students { get; set; }

    }
}
