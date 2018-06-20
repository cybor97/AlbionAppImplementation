using System;
using System.Threading.Tasks;
using uhttpsharp;
using Newtonsoft.Json;
using Albion.Data;
using System.Collections.Generic;
using SQLite;

namespace Albion.Server
{
    public class DataHandler : IHttpRequestHandler
    {
        static SQLiteConnection DBConnection => Program.DBConnection;

        public Task Handle(IHttpContext context, Func<Task> next)
        {
            var urlSegments = context.Request.Uri.OriginalString.Split('/');
            RequestsMap[urlSegments[0]](urlSegments[1], context);
            return Task.Factory.GetCompleted();
        }

        public Dictionary<string, Func<string, IHttpContext, HttpResponse>> RequestsMap = new Dictionary<string, Func<string, IHttpContext, HttpResponse>>(){
            {"Account", ProcessAccountRequest},
            {"Course",ProcessCourceRequest },
            {"Mark",ProcessMarkRequest },
            {"Statement",ProcessStatementRequest },
            {"Subscription",ProcessSubscriptionRequest}
        };

        static HttpResponse ProcessCourceRequest(string method, IHttpContext context)
        {
            switch (method)
            {
                case "GetCourses":
                    if (context.Request.Method == HttpMethods.Get)
                        return Reply(DBConnection.Table<Course>().ToList().Serialize());
                    break;
                case "SetCourse":
                    if (context.Request.Method == HttpMethods.Post)
                    {
                        DBConnection.InsertOrReplace(context.Request.Post.Raw.Deserialize<Course>());
                        return Reply();
                    }
                    break;
                case "RemoveCourse":
                    if (context.Request.Method == HttpMethods.Post)
                    {
                        DBConnection.Delete<Course>(context.Request.Post.Parsed.GetByName("ID"));
                        return Reply();
                    }
                    break;
            }
            return Reply(status: 404);
        }
        static HttpResponse ProcessAccountRequest(string method, IHttpContext context)
        {
            switch (method)
            {
                case "GetAccounts":
                    if (context.Request.Method == HttpMethods.Get)
                        return Reply(DBConnection.Table<Account>().ToList().Serialize());
                    break;
                case "SetAccount":
                    if (context.Request.Method == HttpMethods.Post)
                    {
                        DBConnection.InsertOrReplace(context.Request.Post.Raw.Deserialize<Account>());
                        return Reply();
                    }
                    break;
                case "RemoveAccount":
                    if (context.Request.Method == HttpMethods.Post)
                    {
                        DBConnection.Delete<Account>(context.Request.Post.Parsed.GetByName("ID"));
                        return Reply();
                    }
                    break;
            }
            return Reply(status: 404);
        }
        static HttpResponse ProcessMarkRequest(string method, IHttpContext context)
        {
            switch (method)
            {
                case "GetMark":
                    if (context.Request.Method == HttpMethods.Get)
                        return Reply(DBConnection.Table<Mark>().ToList().Serialize());
                    break;
                case "SetMark":
                    if (context.Request.Method == HttpMethods.Post)
                    {
                        DBConnection.InsertOrReplace(context.Request.Post.Raw.Deserialize<Mark>());
                        return Reply();
                    }
                    break;
                case "RemoveMark":
                    if (context.Request.Method == HttpMethods.Post)
                    {
                        DBConnection.Delete<Mark>(context.Request.Post.Parsed.GetByName("ID"));
                        return Reply();
                    }
                    break;
            }
            return Reply(status: 404);
        }
        static HttpResponse ProcessStatementRequest(string method, IHttpContext context)
        {
            switch (method)
            {
                case "GetCourses":
                    if (context.Request.Method == HttpMethods.Get)
                        return Reply(DBConnection.Table<Statement>().ToList().Serialize());
                    break;
                case "SetCourse":
                    if (context.Request.Method == HttpMethods.Post)
                    {
                        DBConnection.InsertOrReplace(context.Request.Post.Raw.Deserialize<Statement>());
                        return Reply();
                    }
                    break;
                case "RemoveCourse":
                    if (context.Request.Method == HttpMethods.Post)
                    {
                        DBConnection.Delete<Statement>(context.Request.Post.Parsed.GetByName("ID"));
                        return Reply();
                    }
                    break;
            }
            return Reply(status: 404);
        }
        static HttpResponse ProcessSubscriptionRequest(string method, IHttpContext context)
        {
            switch (method)
            {
                case "GetSubscriptions":
                    if (context.Request.Method == HttpMethods.Get)
                        return Reply(DBConnection.Table<Subscription>().ToList().Serialize());
                    break;
                case "SetSubscription":
                    if (context.Request.Method == HttpMethods.Post)
                    {
                        DBConnection.InsertOrReplace(context.Request.Post.Raw.Deserialize<Subscription>());
                        return Reply();
                    }
                    break;
                case "RemoveSubscription":
                    if (context.Request.Method == HttpMethods.Post)
                    {
                        DBConnection.Delete<Subscription>(context.Request.Post.Parsed.GetByName("ID"));
                        return Reply();
                    }
                    break;
            }
            return Reply(status: 404);
        }

        public static HttpResponse Reply(object response = null, int status = 200)
        {
            return new HttpResponse((HttpResponseCode)status, response == null ? "" : response is string ? (string)response : JsonConvert.SerializeObject(response), false);
        }
    }
}
