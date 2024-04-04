using KasirApi.Api.Middlewares;
using Microsoft.Extensions.FileProviders;

namespace KasirApi.Api.Extensions
{
    public static class AppBuilderExtension
    {
        public static void RegisterMiddlewares(this WebApplication app)
        {
            app.UseMiddleware<HttpLoggerMiddleware>(); // Place HttpLoggerMiddleware first!
            app.UseMiddleware<ExceptionMiddleware>();
        }

        public static void RegisterSwagger(this WebApplication app)
        {
            if (!app.Environment.IsDevelopment()) return;
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        public static void RegisterCors(this WebApplication app)
        {
            app.UseCors(options =>
            {
                options.SetIsOriginAllowed(_ => true);
                options.AllowAnyHeader();
                options.AllowAnyMethod();
                options.AllowCredentials();
            });
        }

        public static void RegisterStaticFile(this WebApplication app)
        {
            app.UseStaticFiles(new StaticFileOptions
            {
                RequestPath = new PathString("/files"),
                FileProvider = new PhysicalFileProvider(app.Environment.WebRootPath),
            });
        }
    }
}
