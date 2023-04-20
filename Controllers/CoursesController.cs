using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using AliciaCourseApi.Models;
using AliciaCourseApi.Data;

namespace AliciaCourseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly CoursesDbContext _context;

        public CoursesController(CoursesDbContext context)
        {
            _context = context;
        }

        // ... Your API endpoints will be added here
        // GET: api/courses?subject=subjectName
        [HttpGet]
        public ActionResult<IEnumerable<Course>> GetCourses([FromQuery] string subject)
        {
            if (!string.IsNullOrEmpty(subject))
            {
                return _context.Courses.Where(c => c.Subject == subject).ToList();
            }

            return _context.Courses.ToList();
        }

        // POST: api/courses/add
        [HttpPost("add")]
        public ActionResult<Course> AddCourse([FromBody] Course course)
        {
            _context.Courses.Add(course);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetCourses), new { subject = course.Subject }, course);
        }

        // PUT: api/courses/update
        [HttpPut("update")]
        public IActionResult UpdateCourse([FromBody] Course updatedCourse)
        {
            var course = _context.Courses.FirstOrDefault(c => c.Id == updatedCourse.Id);

            if (course == null)
            {
                return NotFound();
            }

            course.ItemName = updatedCourse.ItemName;
            course.Lessons = updatedCourse.Lessons;
            course.Price = updatedCourse.Price;
            course.Subject = updatedCourse.Subject;

            _context.Courses.Update(course);
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/courses/delete
        [HttpDelete("delete")]
        public IActionResult DeleteCourse([FromBody] Course courseToDelete)
        {
            var course = _context.Courses.FirstOrDefault(c => c.Id == courseToDelete.Id);

            if (course == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(course);
            _context.SaveChanges();

            return NoContent();
        }

        // POST: api/courses/apply_discount
        [HttpPost("apply_discount")]
        public ActionResult<IEnumerable<Course>> ApplyDiscount([FromBody] DiscountRequest discountRequest)
        {
            var subject = discountRequest.Subject;
            var discountPercentage = discountRequest.DiscountPercentage;

            var courses = _context.Courses.Where(c => c.Subject == subject).ToList();
            var discountedCourses = new List<DiscountedCourse>();

            foreach (var course in courses)
            {
                var discountedPrice = course.Price * (1 - discountPercentage / 100);
                discountedCourses.Add(new DiscountedCourse
                {
                    Id = course.Id,
                    ItemName = course.ItemName,
                    Lessons = course.Lessons,
                    OriginalPrice = course.Price,
                    DiscountedPrice = discountedPrice,
                    Subject = course.Subject
                });
            }

            return Ok(discountedCourses);
        }
    }
    public class DiscountRequest
    {
        public string Subject { get; set; }
        public decimal DiscountPercentage { get; set; }
    }

    public class DiscountedCourse : Course
    {
        public decimal OriginalPrice { get; set; }
        public decimal DiscountedPrice { get; set; }
    }

}
