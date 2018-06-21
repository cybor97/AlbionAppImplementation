using System;
using System.Threading.Tasks;
using uhttpsharp;
using Albion.Data;
using System.Collections.Generic;
using SQLite;

namespace Albion.Server
{
    public class DataHandler : IHttpRequestHandler
    {
        static SQLiteConnection DBConnection => Program.DBConnection;

        public DataHandler()
        {
            RequestsMap = new Dictionary<string, Func<string, IHttpContext, HttpResponse>>(){
                {"Account", ProcessAccountRequest},
                {"Course", ProcessCourceRequest },
                {"Mark", ProcessMarkRequest },
                {"Statement", ProcessStatementRequest },
                {"Subscription", ProcessSubscriptionRequest}
            };
        }

        public Task Handle(IHttpContext context, Func<Task> next)
        {
            var urlSegments = context.Request.Uri.OriginalString.Split('/');
            context.Response = RequestsMap[urlSegments[2]](urlSegments[3], context);
            return Task.Factory.GetCompleted();
        }

        public Dictionary<string, Func<string, IHttpContext, HttpResponse>> RequestsMap;

        HttpResponse ProcessAccountRequest(string method, IHttpContext context)
        {
            switch (method)
            {
                case "GetAccounts":
                    if (context.Request.Method == HttpMethods.Get)
                    {
                        var accounts = DBConnection.Table<Account>().ToList();
                        foreach (var acc in accounts)
                            //Password hash is safer.. but there's just no reason to send it.
                            acc.PasswordHash = null;
                        return this.Reply(accounts.Serialize());
                    }
                    break;
                case "SetAccount":
                    if (context.Request.Method == HttpMethods.Post)
                    {
                        DBConnection.InsertOrReplace(context.Request.Post.Raw.Deserialize<Account>());
                        return this.Reply();
                    }
                    break;
                case "RemoveAccount":
                    if (context.Request.Method == HttpMethods.Post)
                    {
                        DBConnection.Delete<Account>(context.Request.Post.Parsed.GetByName("ID"));
                        return this.Reply();
                    }
                    break;
            }
            return this.Reply(status: 404);
        }
        HttpResponse ProcessCourceRequest(string method, IHttpContext context)
        {
            switch (method)
            {
                case "GetCourses":
                    if (context.Request.Method == HttpMethods.Get)
                        return this.Reply(DBConnection.Table<Course>().ToList().Serialize());
                    break;
                case "SetCourse":
                    if (context.Request.Method == HttpMethods.Post)
                    {
                        DBConnection.InsertOrReplace(context.Request.Post.Raw.Deserialize<Course>());
                        return this.Reply();
                    }
                    break;
                case "RemoveCourse":
                    if (context.Request.Method == HttpMethods.Post)
                    {
                        DBConnection.Delete<Course>(context.Request.Post.Parsed.GetByName("ID"));
                        return this.Reply();
                    }
                    break;
            }
            return this.Reply(status: 404);
        }
        HttpResponse ProcessMarkRequest(string method, IHttpContext context)
        {
            switch (method)
            {
                case "GetMark":
                    if (context.Request.Method == HttpMethods.Get)
                        return this.Reply(DBConnection.Table<Mark>().ToList().Serialize());
                    break;
                case "SetMark":
                    if (context.Request.Method == HttpMethods.Post)
                    {
                        DBConnection.InsertOrReplace(context.Request.Post.Raw.Deserialize<Mark>());
                        return this.Reply();
                    }
                    break;
                case "RemoveMark":
                    if (context.Request.Method == HttpMethods.Post)
                    {
                        DBConnection.Delete<Mark>(context.Request.Post.Parsed.GetByName("ID"));
                        return this.Reply();
                    }
                    break;
            }
            return this.Reply(status: 404);
        }
        HttpResponse ProcessStatementRequest(string method, IHttpContext context)
        {
            switch (method)
            {
                case "GetCourses":
                    if (context.Request.Method == HttpMethods.Get)
                        return this.Reply(DBConnection.Table<Statement>().ToList().Serialize());
                    break;
                case "SetCourse":
                    if (context.Request.Method == HttpMethods.Post)
                    {
                        DBConnection.InsertOrReplace(context.Request.Post.Raw.Deserialize<Statement>());
                        return this.Reply();
                    }
                    break;
                case "RemoveCourse":
                    if (context.Request.Method == HttpMethods.Post)
                    {
                        DBConnection.Delete<Statement>(context.Request.Post.Parsed.GetByName("ID"));
                        return this.Reply();
                    }
                    break;
            }
            return this.Reply(status: 404);
        }
        HttpResponse ProcessSubscriptionRequest(string method, IHttpContext context)
        {
            switch (method)
            {
                case "GetSubscriptions":
                    if (context.Request.Method == HttpMethods.Get)
                        return this.Reply(DBConnection.Table<Subscription>().ToList().Serialize());
                    break;
                case "SetSubscription":
                    if (context.Request.Method == HttpMethods.Post)
                    {
                        DBConnection.InsertOrReplace(context.Request.Post.Raw.Deserialize<Subscription>());
                        return this.Reply();
                    }
                    break;
                case "RemoveSubscription":
                    if (context.Request.Method == HttpMethods.Post)
                    {
                        DBConnection.Delete<Subscription>(context.Request.Post.Parsed.GetByName("ID"));
                        return this.Reply();
                    }
                    break;
            }
            return this.Reply(status: 404);
        }
    }
}
