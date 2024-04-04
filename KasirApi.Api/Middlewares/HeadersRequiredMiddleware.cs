using KasirApi.Api.Constants;
using KasirApi.Api.Models;
using Microsoft.AspNetCore.Http;

namespace KasirApi.Api.Middlewares
{
    public class HeadersRequiredMiddleware
    {
        private readonly RequestDelegate _next;

        public HeadersRequiredMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.Value == "/swagger")
            {
                context.Request.Headers["Module"] = "Swagger";
                context.Request.Headers["Version"] = "1.0";
            }

            var module = context.Request.Headers["Module"].FirstOrDefault();

            if (module == null)
            {
                await ShowErrorResponseAsync(context);
                return;
            }

            var version = context.Request.Headers["Version"].FirstOrDefault();

            if (version == null)
            {
                await ShowErrorResponseAsync(context);
                return;
            }
            
            await _next(context);
        }

        private static async Task ShowErrorResponseAsync(HttpContext context)
        {
            context.Response.ContentType = ContentTypeConstant.JSON;
            var response = new ApiResponse<object>().Fail(ResponseConstant.REQUIRED_HEADERS, ResponseConstant.REQUIRED_HEADERS_CODE).ToString();
            await context.Response.WriteAsync(response);
        }
    }
}
