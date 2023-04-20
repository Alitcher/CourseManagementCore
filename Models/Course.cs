using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AliciaCourseApi.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public int Lessons { get; set; }
        public decimal Price { get; set; }
        public string Subject { get; set; }
    }
}
