using System;
using System.Threading.Tasks;
using Application.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.WWW.Middleware
{
    // [DB]: Middleware to allow us to easily update the database on app start.
    public class DatabaseUpdaterMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _connectionString;
        
        public DatabaseUpdaterMiddleware(RequestDelegate next,  string connectionString)
        {
            _next = next;
            _connectionString = connectionString;
        }
        
        public async Task InvokeAsync(HttpContext context, IDatabaseUpdater databaseUpdater)
        {
            databaseUpdater.UpdateDatabase(_connectionString);
            await _next(context);
        }
    }
    
    public static class UpdateDatabaseMiddlewareExtensions
    {
        public static IApplicationBuilder UseDataBaseUpdater(this IApplicationBuilder builder, string connectionString)
        {
            return builder.UseMiddleware<DatabaseUpdaterMiddleware>(connectionString);
        }
    }
}