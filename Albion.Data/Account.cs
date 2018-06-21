using Newtonsoft.Json;
using SQLite;
using System;
namespace Albion.Data
{
    [Table("Account")]
    public class Account
    {
        [Column("ID")]
        [PrimaryKey]
        public string ID { get; set; } = Guid.NewGuid().ToString();

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
        public string PasswordRaw { get; set; }

        [Ignore]
        [JsonIgnore]
        public string Password
        {
            get => null;
            set
            {
                PasswordHash = Tools.ComputeHash(value);
                PasswordRaw = value;
            }
        }

        public bool CheckPassword(string password)
        {
            return Tools.ComputeHash(password) == PasswordHash;
        }

        public void HashPassword()
        {
            PasswordHash = Tools.ComputeHash(PasswordRaw);
        }
    }
}
