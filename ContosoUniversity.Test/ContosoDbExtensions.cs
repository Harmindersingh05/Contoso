using ContosoUniversity.Data;

namespace ContosoUniversity.Test
{
    public static class ContosoDbExtensions
    {
        public static ContosoDbContext WithCourse(this ContosoDbContext context, Action<CourseEntity>? entity = null)
        {
            var course = new CourseEntity()
            {
                Id = 1,
                Credits = 5,
                Description = "Description Course 1",
                Title = "Title Course 1",
            };

            entity?.Invoke(course);
            context.Courses.Add(course);
            context.SaveChanges();

            return context;
        }
    }
}
