using ContosoUniversity.RequestHandlers.GetCourse;
using Shouldly;
namespace ContosoUniversity.Test.GetCourseTests
{
    [TestClass]
    public class GetCourseValidationTest
    {
        [TestMethod]
        public void GetCourseValidation_NegativeCredits_ShouldReturnError()
        {
            var validator = new GetCourseValidation();
            var request = new GetCourseRequest()
            {
                Credits = -1
            };

            var results = validator.Validate(request);

            results.ShouldNotBeNull();
            results.Errors[0].PropertyName.ShouldBe(nameof(GetCourseRequest.Credits));
            results.Errors[0].ErrorMessage.ShouldBe("'Credits' must be greater than '0'.");
        }

        [TestMethod]
        public void GetCourseValidation_ExcessiveCredits_ShouldReturnError()
        {
            var validator = new GetCourseValidation();
            var request = new GetCourseRequest()
            {
                Credits = 26
            };

            var results = validator.Validate(request);

            results.ShouldNotBeNull();
            results.Errors[0].PropertyName.ShouldBe(nameof(GetCourseRequest.Credits));
            results.Errors[0].ErrorMessage.ShouldBe("'Credits' must be less than '25'.");
        }

        [TestMethod]
        public void GetCourseValidation_ValidCredits_ShouldPass()
        {
            var validator = new GetCourseValidation();
            var request = new GetCourseRequest()
            {
                Credits = 5
            };

            var results = validator.Validate(request);

            results.ShouldNotBeNull();
            results.Errors.Count.ShouldBe(0);
        }
    }
}
