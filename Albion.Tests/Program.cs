using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;

namespace Albion.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            ExecuteTests();
            while (true) Thread.Sleep(100);
        }

        static async void ExecuteTests()
        {
            Console.WriteLine("Checking status code...");
            Assert.AreEqual(HttpStatusCode.OK, (await new HttpClient().GetAsync("http://localhost/Data/Account/GetAccounts")).StatusCode);

            Console.WriteLine("Tests OK!");
        }
    }
}
