using SQLite;

namespace Albion.Data
{
    public class Course
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("ID")]
        public int ID { get; set; }

        [Column("CourseName")]
        public string CourseName { get; set; }

        [Column("Places")]
        public string Places { get; set; }

        [Column("Price")]
        public string Price { get; set; }

        [Column("CourseNote")]
        public string CourseNote { get; set; }
    }
}
