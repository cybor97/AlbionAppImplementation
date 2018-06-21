using SQLite;
using System;

namespace Albion.Data
{
    public class StatementRecord
    {
        [Column("ID")]
        [PrimaryKey]
        public string ID { get; set; } = Guid.NewGuid().ToString();

        [Column("StatementID")]
        public int StatementID { get; set; }

        [Column("MarkID")]
        public int MarkID { get; set; }
    }
}
