using MediatR;

namespace ContosoUniversity.RequestHandlers.CreateCourse;

public class CreateCourseRequest : IRequest<CreateCourseResponse>
{
    public int? UniversityId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int? Credits { get; set; }
}