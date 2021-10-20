using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace AspNetCoreProject.Infrastructure.MiddleWare
{
    public class TestMiddleWare
    {
        private readonly RequestDelegate next;
        private readonly ILogger<TestMiddleWare> logger;

        public TestMiddleWare(RequestDelegate Next, ILogger<TestMiddleWare> Logger)
        {
            next = Next;
            logger = Logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await next(context);
        }
    }
}
