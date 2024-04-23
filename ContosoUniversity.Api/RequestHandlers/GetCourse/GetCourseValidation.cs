using FluentValidation;

namespace ContosoUniversity.RequestHandlers.GetCourse;

public class GetCourseValidation: AbstractValidator<GetCourseRequest>
{
    public GetCourseValidation()
    {
        When(x => x.Credits != null, () =>
        {
            RuleFor(m => m.Credits)
                .NotEmpty()
                .GreaterThan(0)
                .LessThan(25);
        });
    }
}