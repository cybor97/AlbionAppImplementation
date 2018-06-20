using SQLite;
using System;

namespace Albion.Data
{
    [Table("Mark")]
    public class Mark
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("ID")]
        public int ID { get; set; }

        [Column("Value")]
        public string Value { get; set; }

        [Column("MarkNote")]
        public string MarkNote { get; set; }

        [Column("MarkDate")]
        public DateTime MarkDate { get; set; } = DateTime.Now;
    }
}
