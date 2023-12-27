using FluentValidation.AspNetCore;
using PashaBank.API.Middlewares;
using PashaBank.Application;
using PashaBank.Infrastructure;
using System.Globalization;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        {
            builder.WebHost.UseKestrel(options =>
            {
                options.Limits.MaxRequestLineSize = 1048576;
                options.Limits.MaxRequestBufferSize = 1048576;
                options.Limits.MaxRequestBodySize = 100 * 1024 * 1024;
            })
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    .ConfigureAppConfiguration((hostingContext, config) =>
                    {
                        var env = hostingContext.HostingEnvironment;
                        config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
                        config.AddEnvironmentVariables();
                    })
                    .UseIISIntegration();

            IConfiguration configuration = builder.Configuration;
            builder.Services.AddControllers();
            builder.Services.AddFluentValidation(fv =>
            {
                fv.RegisterValidatorsFromAssemblyContaining(typeof(Program));
                fv.ValidatorOptions.LanguageManager.Culture = new CultureInfo("ka-GE");
                fv.ValidatorOptions.LanguageManager.Enabled = true;
                fv.LocalizationEnabled = true;
            });

            builder.Services.AddLocalization();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddApplicationLayer();
            builder.Services.AddInfrastructureLayer(configuration);

            builder.Services.AddAuthenticationServices(configuration);

            var origins = builder.Configuration.GetValue<string>("OriginsToAllow");

            builder.Services.AddCors();
        }

        var app = builder.Build();
        {

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            var serviceProvider = builder.Services.BuildServiceProvider();

            var context = serviceProvider.GetService<PashaBankDbContext>();

            if (context != null)
            {
                DbInitializer.InitializeDatabase(app.Services, context);
            }

            app.MapControllers();

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseRouting();

            app.UseHttpsRedirection();

            app.UseCors(builder =>
            {
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
            });

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Run();
        }
    }
}