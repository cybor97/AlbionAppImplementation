using Newtonsoft.Json;
using SQLite;
using System;

namespace Albion.Data
{
    [Table("Mark")]
    public class Mark
    {
        [PrimaryKey]
        [Column("ID")]
        public string ID { get; set; } = Guid.NewGuid().ToString();

        [Column("AccountID")]
        public string AccountID { get; set; }

        [Column("CourseID")]
        public string CourseID { get; set; }

        [Column("Value")]
        public int MarkValue { get; set; }

        [Column("MarkNote")]
        public string MarkNote { get; set; }

        [Column("MarkDate")]
        public DateTime MarkDate { get; set; } = DateTime.Now;

        private Account _student;
        [Ignore]
        [JsonIgnore]
        public Account Student
        {
            get
            {
                return _student;
            }
            set
            {
                _student = value;
                AccountID = _student?.ID;
            }
        }
        private Course _course;
        [Ignore]
        [JsonIgnore]
        public Course Course
        {
            get
            {
                return _course;
            }
            set
            {
                _course = value;
                CourseID = _course?.ID;
            }
        }
    }
}
