using SQLite;
using System;
using System.Globalization;

namespace Albion.Data
{
    public class Subscription
    {
        const int FROZEN = -2, INACTIVE = -1, ACTIVE = 0, FINISHED = 1;

        [PrimaryKey]
        [Column("ID")]
        public string ID { get; set; } = Guid.NewGuid().ToString();

        [Column("AccountID")]
        public string AccountID { get; set; }

        [Column("CourseID")]
        public string CourseID { get; set; }

        [Column("StartDate")]
        public string StartDate { get; set; } = DateTime.Now.ToString(CultureInfo.InvariantCulture.DateTimeFormat);

        [Column("FinishDate")]
        public string FinishDate { get; set; }

        [Column("State")]
        public int State { get; set; } = INACTIVE;

        [Column("ContractNumber")]
        public string ContractNumber { get; set; }
    }
}
