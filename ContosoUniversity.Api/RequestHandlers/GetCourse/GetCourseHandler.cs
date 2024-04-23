using ContosoUniversity.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.RequestHandlers.GetCourse;

public class GetCourseHandler : IRequestHandler<GetCourseRequest, List<GetCourseResponse>>
{
    private readonly ContosoDbContext _contosoDbContext;

    public GetCourseHandler(ContosoDbContext contosoDbContext)
    {
        _contosoDbContext = contosoDbContext;
    }

    public async Task<List<GetCourseResponse>> Handle(GetCourseRequest request, CancellationToken cancellationToken)
    {
        var courseQuery = _contosoDbContext.Courses
            .AsNoTracking()
            .AsQueryable();

        if (request.CourseId.HasValue)
        {
            courseQuery = courseQuery.Where(m => m.Id == request.CourseId.Value);
        }

        if (request.Credits.HasValue)
        {
            courseQuery = courseQuery.Where(m => m.Credits >= request.Credits.Value);
        }

        return await courseQuery
            .Select(m => new GetCourseResponse
            {
                UniversityId = m.UniversityId,
                CourseId = m.Id,
                CourseTitle = m.Title,
                CourseDescription = m.Description,
                Credits = m.Credits
            }).ToListAsync(cancellationToken);
    }
}