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
                if (context.Request.Uri.OriginalString == "/Auth/SignIn" || context.Request.Uri.OriginalString == "/Auth/SignUp")
                {
                    var account = Program.DBConnection.Table<Account>().FirstOrDefault(acc => acc.Email == context.Request.Post.Parsed.GetByName("Email"));
                    switch (context.Request.Uri.OriginalString)
                    {
                        case "/Auth/SignIn":
                            if (account != null && account.CheckPassword(context.Request.Post.Parsed.GetByName("Password")))
                                context.Response = this.Reply(Tools.ComputeHash(account.Email + account.Password));
                            else context.Response = this.Reply("Invalid email or password!", 403);

                            break;
                        case "/Auth/SignUp":
                            if (account == null)
                                context.Response = this.Reply("Sorry, account with this email already exists!", 400);
                            else
                            {
                                var newAccount = context.Request.Post.Raw.Deserialize<Account>();
                                newAccount.HashPassword();
                                Program.DBConnection.Insert(newAccount);
                                //DO NOT RETURN PASSWORDS!
                                newAccount.PasswordRaw = newAccount.PasswordHash = null;

                                context.Response = this.Reply(newAccount);
                            }
                            break;
                    }
                }
                else context.Response = this.Reply(status: 405);
            }
            else context.Response = new HttpResponse(HttpResponseCode.NotFound, "Path not found!", false);
            return Task.Factory.GetCompleted();
        }
    }

}
