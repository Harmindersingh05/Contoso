using ContosoUniversity.RequestHandlers.CreateCourse;
using ContosoUniversity.RequestHandlers.GetCourse;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ContosoUniversity.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CourseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetCourses([FromQuery]int? credits)
        {
            var courses = await _mediator.Send(new GetCourseRequest
            {
                Credits = credits
            });
            return Ok(courses);
        }

        [HttpGet("{courseId}")]
        public async Task<IActionResult> Get(int? courseId)
        {
            var courses = await _mediator.Send(new GetCourseRequest
            {
                CourseId = courseId
            });

            if (!courses.Any()) return NotFound();

            return Ok(courses.First());
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateCourseRequest course)
        {
            var createCourseResponse = await _mediator.Send(course);
            return Ok(createCourseResponse);
        }
    }
}