using Albion.Data;
using SQLite;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using uhttpsharp;
using uhttpsharp.Handlers;
using uhttpsharp.Listeners;
using uhttpsharp.RequestProviders;

namespace Albion.Server
{
    static class Program
    {
        //TODO: Продумать архитектуру(если посчитаешь эту хреновой)
        //TODO: Создать остальные обработчики(если надо)
        private static HttpServer Server;
        //Чтобы не было конфликтов с одинаковыми названиями(на всякий случай)
        public static SQLiteConnection DBConnection { get; set; }

        public static bool Working => Server != null;

        public static void Main()
        {
            var pathToDb = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data.db");

            if (!File.Exists(pathToDb))
            {
                DBConnection = new SQLiteConnection(pathToDb);
                foreach (var current in new[]{
                    typeof(Account),
                    typeof(Course),
                    typeof(Mark),
                    typeof(Statement),
                    typeof(StatementRecord),
                    typeof(Subscription)
                })
                    DBConnection.CreateTable(current);
            }
            else
                DBConnection = new SQLiteConnection(pathToDb);

            if (!Working)
            {
                Server = new HttpServer(new HttpRequestProvider());

                Server.Use(new TcpListenerAdapter(new TcpListener(IPAddress.Any, 80)));
                Server.Use(new HttpRouter()
                .With("Auth", new AuthHandler())
                .With("Data", new DataHandler()));
                
                Server.Start();
            }

            while (true)
                Thread.Sleep(5000);
        }
    }
}
