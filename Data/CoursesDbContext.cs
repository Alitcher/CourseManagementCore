using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AliciaCourseApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AliciaCourseApi.Data
{
    public class CoursesDbContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public CoursesDbContext(DbContextOptions<CoursesDbContext> options) : base(options) { }
    }
}
