namespace ContosoUniversity.RequestHandlers.GetCourse;

public class GetCourseResponse
{
    public int? UniversityId { get; set; }
    public int? CourseId { get; set; }
    public string? CourseTitle { get; set; }
    public string? CourseDescription { get; set; }
    public int? Credits { get; set; }
}