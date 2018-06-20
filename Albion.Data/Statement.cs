using SQLite;
using System;

namespace Albion.Data
{
    [Table("Statement")]
    public class Statement
    {
        [Column("ID")]
        [PrimaryKey]
        [AutoIncrement]
        public int ID { get; set; }

        [Column("StatementName")]
        public string StatementName { get; set; }

        [Column("StatementPeriodStart")]
        public DateTime StatementPeriodStart { get; set; }

        [Column("StatementPeriodEnd")]
        public DateTime StatementPeriodEnd { get; set; }
    }
}
