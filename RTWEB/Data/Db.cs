using Microsoft.EntityFrameworkCore;

namespace ZPWEB.Data
{
    public class Db:DbContext
    {
        public static string ConnectionString = "Server=localhost;Database=zpweb;User Id=sa;Password=Test_123;Encrypt=False";

        //public static string ConnectionString = "Server=103.125.252.243;Database=demo;User Id=oct_demo;Password=hbswiplv4czmyjfqdexn;Encrypt=False";

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

        //public DbSet<Domain> Domains { get; set; }
        
    }
}
