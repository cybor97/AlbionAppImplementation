using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Albion.Data
{
    public static class Tools
    {
        public static T Deserialize<T>(this byte[] data)
        {
            return JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(data));
        }

        public static string Serialize(this object data)
        {
            return JsonConvert.SerializeObject(data);
        }

        public static List<T> ToList<T>(this TableQuery<T> data)
        {
            return new List<T>(data);
        }

        public static string ComputeHash(string password)
        {
            return Convert.ToBase64String(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(password)));
        }
    }
}
