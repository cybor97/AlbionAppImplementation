using Albion.Data;
using System;
using System.Threading.Tasks;
using uhttpsharp;

namespace Albion.Server
{
    public class AuthHandler : IHttpRequestHandler
    {
        public Task Handle(IHttpContext context, Func<Task> next)
        {
            if (context.Request.Method == HttpMethods.Post)
            {
                foreach (var account in Program.DBConnection
                    .Table<Account>()
                     .Where(account => account.Email == context.Request.Post.Parsed.GetByName("Email")))
                {
                    if (account.CheckPassword(context.Request.Post.Parsed.GetByName("Password")))
                        context.Response = new HttpResponse(HttpResponseCode.Ok, Tools.ComputeHash(account.Email + account.Password), true);
                    break;
                }
            }
            return Task.Factory.GetCompleted();
        }
    }

}
