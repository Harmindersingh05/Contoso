using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Data;

public class ContosoDbContext : DbContext
{
    public DbSet<CourseEntity> Courses { get; set; }
    
    public ContosoDbContext(DbContextOptions<ContosoDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CourseEntity>().ToTable("Course").HasKey(m => new { m.UniversityId, m.Id });
    }
}