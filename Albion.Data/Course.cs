using SQLite;
using System;

namespace Albion.Data
{
    public class Course
    {
        [PrimaryKey]
        [Column("ID")]
        public string ID { get; set; } = Guid.NewGuid().ToString();

        [Column("CourseName")]
        public string CourseName { get; set; }

        [Column("Places")]
        public int Places { get; set; }

        [Column("Price")]
        public int Price { get; set; }

        [Column("CourseNote")]
        public string CourseNote { get; set; }
    }
}
