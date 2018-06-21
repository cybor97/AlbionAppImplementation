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

        [Column("Value")]
        public string Value { get; set; }

        [Column("MarkNote")]
        public string MarkNote { get; set; }

        [Column("MarkDate")]
        public DateTime MarkDate { get; set; } = DateTime.Now;
    }
}
