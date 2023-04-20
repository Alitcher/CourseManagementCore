using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AliciaCourseApi.Models
{
    public class CourseSubject
    {
        public string Name { get; set; }
        public List<Course> Courses { get; set; }
    }
}
