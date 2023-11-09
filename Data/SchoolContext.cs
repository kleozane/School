using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using School.Models;

namespace School.Data
{
    public class SchoolContext : DbContext
    {
        public SchoolContext (DbContextOptions<SchoolContext> options)
            : base(options)
        {
        }

        public DbSet<School.Models.Teacher> Teacher { get; set; } = default!;
        public DbSet<School.Models.Curriculum> Curriculums { get; set; } = default!;
        public DbSet<School.Models.Subject> Subjects { get; set; } = default!;
        public DbSet<School.Models.Student> Students { get; set; } = default!;
    }
}  
