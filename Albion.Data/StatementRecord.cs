using SQLite;

namespace Albion.Data
{
    public class StatementRecord
    {
        [Column("ID")]
        [PrimaryKey]
        [AutoIncrement]
        public int ID { get; set; }

        [Column("StatementID")]
        public int StatementID { get; set; }

        [Column("MarkID")]
        public int MarkID { get; set; }
    }
}
