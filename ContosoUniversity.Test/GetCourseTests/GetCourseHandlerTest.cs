using ContosoUniversity.RequestHandlers.GetCourse;
using Shouldly;


namespace ContosoUniversity.Test.GetCourseTests
{
    [TestClass]
    public class GetCourseHandlerTest : UnitTestBase
    {

        [TestMethod]
        public async Task GetCourses_NoFilters_ShouldReturnAllCourses()
        {
            var universityId = 1;
            var course1Id = 1;
            var course1Credit = 5;
            var course1Title = "Bachelor of Construction";

            var course2Id = 2;
            var course2Credit = 7;
            var course2Title = "Bachelor of Architecture";

            ContosoDbContext
                .WithCourse(entity =>
                {
                    entity.Id = course1Id;
                    entity.Title = course1Title;
                    entity.Credits = course1Credit;
                    entity.UniversityId = universityId;
                }).
                WithCourse(entity =>
                {
                    entity.Id = course2Id;
                    entity.Title = course2Title;
                    entity.Credits = course2Credit;
                    entity.UniversityId = universityId;
                });

            var request = new GetCourseRequest()
            {
                Credits = null,
                CourseId = null
            };

            var results = await new GetCourseHandler(ContosoDbContext).Handle(request,default);

            results = results.Where(m => m.UniversityId == universityId).ToList();
            results.ShouldNotBeNull();
            results.Count.ShouldBe(2);

            var course1 = results.SingleOrDefault(m => m.CourseId == course1Id);
            course1.ShouldNotBeNull();
            course1.CourseTitle.ShouldBe(course1Title);
            course1.Credits.ShouldBe(course1Credit);

            var course2 = results.SingleOrDefault(m => m.CourseId == course2Id);
            course2.ShouldNotBeNull();
            course2.CourseTitle.ShouldBe(course2Title);
            course2.Credits.ShouldBe(course2Credit);

        }

        [TestMethod]
        public async Task GetCourses_FilterByCredits()
        {
            var universityId = 2;
            var course1Id = 1;
            var course1Credit = 5;
            var course1Title = "Bachelor of Construction";

            var course2Id = 2;
            var course2Credit = 7;
            var course2Title = "Bachelor of Architecture";

            ContosoDbContext
                .WithCourse(entity =>
                {
                    entity.Id = course1Id;
                    entity.Title = course1Title;
                    entity.Credits = course1Credit;
                    entity.UniversityId = universityId;

                }).
                WithCourse(entity =>
                {
                    entity.Id = course2Id;
                    entity.Title = course2Title;
                    entity.Credits = course2Credit;
                    entity.UniversityId = universityId;
                });

            var request = new GetCourseRequest()
            {
                Credits = 6,
                CourseId = null
            };

            var results = await new GetCourseHandler(ContosoDbContext).Handle(request, default);
            results = results.Where(m => m.UniversityId == universityId).ToList();
            results.ShouldNotBeNull();
            results.Count.ShouldBe(1);

            var course2 = results.SingleOrDefault(m => m.CourseId == course2Id);
            course2.ShouldNotBeNull();
            course2.CourseTitle.ShouldBe(course2Title);
            course2.Credits.ShouldBe(course2Credit);
        }


        [TestMethod]
        public async Task GetCourses_FilterByCreditAndCourse()
        {
            var universityId = 3;
            var course1Id = 1;
            var course1Credit = 5;
            var course1Title = "Bachelor of Construction";

            var course2Id = 2;
            var course2Credit = 7;
            var course2Title = "Bachelor of Architecture";

            ContosoDbContext
                .WithCourse(entity =>
                {
                    entity.Id = course1Id;
                    entity.Title = course1Title;
                    entity.Credits = course1Credit;
                    entity.UniversityId = universityId;

                }).
                WithCourse(entity =>
                {
                    entity.Id = course2Id;
                    entity.Title = course2Title;
                    entity.Credits = course2Credit;
                    entity.UniversityId = universityId;
                });

            var request = new GetCourseRequest()
            {
                Credits = course1Credit,
                CourseId = course1Id
            };

            var results = await new GetCourseHandler(ContosoDbContext).Handle(request, default);
            results = results.Where(m => m.UniversityId == universityId).ToList();
            results.ShouldNotBeNull();
            results.Count.ShouldBe(1);

            var course1 = results.SingleOrDefault(m => m.CourseId == course1Id);
            course1.ShouldNotBeNull();
            course1.CourseTitle.ShouldBe(course1Title);
            course1.Credits.ShouldBe(course1Credit);
        }
    }
}
