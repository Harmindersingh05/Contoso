using FluentValidation;

namespace ContosoUniversity.RequestHandlers.CreateCourse;

public class CreateCourseValidation : AbstractValidator<CreateCourseRequest>
{
    public CreateCourseValidation()
    {
        RuleFor(m => m.UniversityId).NotEmpty();
        RuleFor(m => m.Title).NotEmpty();
        RuleFor(m => m.Description).NotEmpty();
        When(x => x.Credits != null, () =>
        {
            RuleFor(m => m.Credits)
                .NotEmpty()
                .GreaterThan(0)
                .LessThan(25);
        });
    }
}