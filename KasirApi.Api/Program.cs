using KasirApi.Api.Extensions;
using Serilog;

// Service
var builder = WebApplication.CreateBuilder(args);

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Host.UseSerilog((context, services, configuration) => {
    configuration.ReadFrom.Configuration(context.Configuration);
});

builder.AddJsonEnv();

var services = builder.Services;
services.AddSwagger();
services.AddVersioning();
services.ConfigureResponseCompression();
services.ConfigureApiControllers();
services.ConfigureControllerPrefix();
services.AddDbContext(builder.Configuration);
services.RegisterServices();
services.RegisterHelpers();
services.RegisterAppSettings(builder.Configuration);
services.ConfigureCors();
services.ConfigureJwtAuthentication();
services.AddEndpointsApiExplorer();
services.ConfigureStaticFileOptions(builder.Environment);
services.ConfigureAutoMapper();
services.AddHttpContextAccessor();

// App builder
var app = builder.Build();
app.RegisterCors();
app.RegisterSwagger();
app.RegisterMiddlewares();
app.RegisterStaticFile();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
