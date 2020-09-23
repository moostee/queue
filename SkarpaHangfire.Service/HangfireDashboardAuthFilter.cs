using Hangfire.Dashboard;
using System;

namespace SkarpaHangfire.Service
{
    public class HangfireDashboardAuthFilter : IDashboardAuthorizationFilter
    {
        private bool _isAuthorized;
        public bool Authorize(DashboardContext context)
        {
            //var httpContext = context.GetHttpContext();
            ////httpContext.Request.
            //if (!httpContext.Request.Headers.ContainsKey("Authorization"))
            //{
            //    context.Response.StatusCode = 401;
            //    Console.WriteLine("Authorization is required");
            //    context.Response.WriteAsync("Authorization is required");
            //    return false;
            //}
            //try
            //{
            //    var authHeader = AuthenticationHeaderValue.Parse(httpContext.Request.Headers["Authorization"]);
            //    var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
            //    var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':');
            //    var username = credentials[0];
            //    var password = credentials[1];
            //    _isAuthorized = IsAuthenticated(username, password);
            //}
            //catch
            //{
            //    context.Response.StatusCode = 401;
            //    context.Response.WriteAsync("Invalid authorization header");
            //    return false;
            //}
            //if (!_isAuthorized)
            //{
            //    context.Response.StatusCode = 401;
            //    context.Response.WriteAsync("Invalid username/ password");
            //    return false;
            //}
            // Allow all authenticated users to see the Dashboard (potentially dangerous).
            return true;
        }

        public bool IsAuthenticated(string username, string password)
        {
            var basicAuthUserName = "syarpaadmin";
            var basicAuthPassword = "syarpa123";
            //Check that username and password are correct
            return username.Equals(basicAuthUserName, StringComparison.InvariantCultureIgnoreCase)
               && password.Equals(basicAuthPassword);
        }
    }

}

