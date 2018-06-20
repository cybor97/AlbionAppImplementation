using Newtonsoft.Json;
using SQLite;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Albion.Data
{
    public class Account
    {
        [Column("ID")]
        [PrimaryKey]
        [AutoIncrement]
        public int ID { get; set; }

        [Column("FullName")]
        public string FullName { get; set; }

        [Column("StudentFullName")]
        public string StudentFullName { get; set; }

        [Column("BirthDate")]
        public string BirthDate { get; set; }

        [Column("StudentBirthDate")]
        public string StudentBirthDate { get; set; }

        [Column("PhoneNumber")]
        public string PhoneNumber { get; set; }

        [Column("Email")]
        [Unique]
        public string Email { get; set; }

        [Column("Address")]
        public string Address { get; set; }

        [Column("AccessLevel")]
        public string AccessLevel { get; set; }

        [Column("PasswordHash")]
        public string PasswordHash { get; set; }

        [Ignore]
        [JsonIgnore]
        public string Password
        {
            get => null;
            set
            {
                PasswordHash = ComputeHash(value);
            }
        }

        public bool CheckPassword(string password)
        {
            return Tools.ComputeHash(password) == PasswordHash;
        }
    }
}
