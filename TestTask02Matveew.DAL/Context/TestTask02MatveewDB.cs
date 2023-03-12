using Microsoft.EntityFrameworkCore;
using TestTask02Matveew.Domain;

namespace TestTask02Matveew.DAL.Context
{
    public class TestTask02MatveewDB : DbContext
    {
        public DbSet<Coordinate> Coordinates { get; set; }
        public TestTask02MatveewDB(DbContextOptions<TestTask02MatveewDB> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Coordinate>().HasCheckConstraint("CoordinateX", "CoordinateX");
            modelBuilder.Entity<Coordinate>()
                .ToTable(t => t.HasCheckConstraint("CoordinateX", "CoordinateX > 0 AND CoordinateX < 4"))
                .ToTable(t => t.HasCheckConstraint("CoordinateY", "CoordinateY > 0 AND CoordinateY < 4"))
                .ToTable(t => t.HasCheckConstraint("Client", "Client = 'X' OR Client = '0'"));
        }
    }
}
