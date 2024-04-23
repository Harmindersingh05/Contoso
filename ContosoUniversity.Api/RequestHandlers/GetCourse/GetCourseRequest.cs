using MediatR;

namespace ContosoUniversity.RequestHandlers.GetCourse;

public class GetCourseRequest : IRequest<List<GetCourseResponse>>
{
    public int? CourseId { get; set; }
    public int? Credits { get; set; }
}