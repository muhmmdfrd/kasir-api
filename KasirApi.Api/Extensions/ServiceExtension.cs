using KasirApi.Core.Configs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System.IO.Compression;
using System.Net.Mime;
using System.Reflection;
using System.Text;
using Flozacode.Extensions.DirExtension;
using Flozacode.Repository;
using KasirApi.Api.Commons;
using KasirApi.Api.Constants;
using KasirApi.Api.Models;
using KasirApi.Core.Helpers;
using KasirApi.Core.Interfaces;
using KasirApi.Core.Services;
using KasirApi.Repository.Contexts;

namespace KasirApi.Api.Extensions
{
    public static class ServiceExtension
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddFlozaRepo();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IMemberService, MemberService>();
            services.AddScoped<IProductService, ProductService>();
        }

        public static void RegisterHelpers(this IServiceCollection services)
        {
            services.AddScoped<UserHelper>();
            services.AddScoped<TransactionHelper>();
            services.AddScoped<MemberHelper>();
            services.AddScoped<ProductHelper>();
        }
        
        public static void AddDbContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });
        }

        public static void RegisterAppSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtConfigs>(configuration.GetSection(nameof(JwtConfigs)));
            services.Configure<SecretConfigs>(configuration.GetSection(nameof(SecretConfigs)));
        }

        public static void ConfigureApiControllers(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(ValidateModelStateAttribute));
            })
            .ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var result = new ObjectResult(new ApiResponse<object>
                    {
                        Success = false,
                        Code = ResponseConstant.BAD_REQUEST_CODE,
                        Message = ResponseConstant.BAD_REQUEST_MESSAGE,
                        Data = context.ModelState.Values.SelectMany(c => c.Errors).Select(x => x.ErrorMessage)
                    });

                    result.ContentTypes.Add(MediaTypeNames.Application.Json);

                    return result;
                };
            })
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
            });
        }

        public static void ConfigureResponseCompression(this IServiceCollection services)
        {
            services.AddResponseCompression();
            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Fastest;
            });
        }

        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(CorsConstant.CORS_NAME, builder =>
                {
                    builder
                    .AllowAnyMethod()
                    .AllowAnyOrigin()
                    .SetIsOriginAllowed(_ => true)
                    .AllowAnyHeader();
                });
            });
        }

        public static void ConfigureControllerPrefix(this IServiceCollection services)
        {
            var routeAttribute = new RouteAttribute("api");
            var routePrefixConvention = new RoutePrefixConvention(routeAttribute);
            services.AddControllersWithViews(options => options.Conventions.Add(routePrefixConvention));
        }

        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }

        public static void ConfigureJwtAuthentication(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var jwtConfigs = serviceProvider.GetRequiredService<IOptions<JwtConfigs>>().Value;
            var key = Encoding.ASCII.GetBytes(jwtConfigs.TokenSecret);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtConfigs.Issuer,
                    ValidAudience = jwtConfigs.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero,
                };

                options.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = _ => Task.CompletedTask,
                    OnChallenge = _ => Task.CompletedTask
                };
            });
        }

        public static void AddVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            });
        }

        public static void ConfigureStaticFileOptions(this IServiceCollection services, IWebHostEnvironment environment)
        {
            var folder = Path.Combine(environment.WebRootPath, "Files");
            folder.DirExistOrCreate();
            var physicalProvider = new PhysicalFileProvider(folder);

            services.AddSingleton<IFileProvider>(physicalProvider);
            services.Configure<StaticFileOptions>(opts =>
            {
                opts.FileProvider = physicalProvider;
                opts.RequestPath = "/Files";
                opts.OnPrepareResponse = context =>
                {
                    context.Context.Response.Headers.Add("Cache-Control", "no-store");
                };
            });
            services.AddSingleton<StaticFileDetector>();
        }

        public static void AddJsonEnv(this WebApplicationBuilder builder)
        {
            if (builder.Environment.IsProduction())
            {
                builder
                    .Configuration
                    .AddJsonFile("appsettings.Production.json", true, true);
                
                return;
            }

            builder.Configuration.AddJsonFile("appsettings.Development.json", true, true);
        }

        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your token in the text input below"
                });

                var openApiRequirement = new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                };

                c.AddSecurityRequirement(openApiRequirement);
            });
        }
    }
}
