using KasirApi.Api.Models;
using KasirApi.Core.Configs;
using Microsoft.Extensions.Options;
using System.Text;
using KasirApi.Api.Exceptions;

namespace KasirApi.Api.Middlewares
{
    public class ToolMiddleware
    {
        private readonly RequestDelegate _request;
        private readonly SecretConfigs _secretConfigs;

        public ToolMiddleware(
            RequestDelegate request,
            IOptions<SecretConfigs> secretConfigs
        )
        {
            _request = request;
            _secretConfigs = secretConfigs.Value;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var request = httpContext.Request;
            var path = request.Path.Value?.Split('/')[3];

            if (path != "tools")
            {
                await _request(httpContext);
                return;
            }

            request.EnableBuffering();

            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer);
            var bodyString = Encoding.UTF8.GetString(buffer);
            var body = bodyString.ToObject();

            if (body.Token != _secretConfigs.ToolsKey)
            {
                throw new UnauthorizedToolsException("You aren't allowed to access this endpoint.");
            }

            request.Body.Position = 0;
            await _request(httpContext);
        }
    }
}
