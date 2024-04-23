using ContosoUniversity.Data;
using MediatR;

namespace ContosoUniversity.RequestHandlers.CreateCourse;

public class CreateCourseHandler : IRequestHandler<CreateCourseRequest,CreateCourseResponse>
{
    private readonly ContosoDbContext _contosoDbContext;

    public CreateCourseHandler(ContosoDbContext contosoDbContext)
    {
        _contosoDbContext = contosoDbContext;
    }

    public async Task<CreateCourseResponse> Handle(CreateCourseRequest request, CancellationToken cancellationToken)
    {
        var course = new CourseEntity
        {
            Id = 2, // Internal Id Generator?
            UniversityId = request.UniversityId!.Value,
            Description = request.Description!,
            Credits = request.Credits!.Value,
            Title = request.Title!,
        };

        await _contosoDbContext.Courses.AddAsync(course, cancellationToken);
        await _contosoDbContext.SaveChangesAsync(cancellationToken);

        return new CreateCourseResponse
        {
            CourseId = course.Id
        };
    }
}