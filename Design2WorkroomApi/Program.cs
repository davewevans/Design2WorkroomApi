using Design2WorkroomApi.Helpers;
using Design2WorkroomApi.Repository;
using Design2WorkroomApi.Repository.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using System.Reflection;
using AspNetCoreRateLimit;
using Microsoft.AspNetCore.OData;
using Serilog;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// ref: https://docs.microsoft.com/en-us/azure/active-directory/develop/quickstart-v2-aspnet-core-web-api
//services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddMicrosoftIdentityWebApi(Configuration.GetSection("AzureAdB2C"));

// ref: https://github.com/datalust/dotnet6-serilog-example
// ref: https://www.youtube.com/watch?v=ad0IhMFsxyw
//builder.Host.UseSerilog((ctx, lc) => lc
//    .WriteTo.Console());

// The following line enables Application Insights telemetry collection.
builder.Services.AddApplicationInsightsTelemetry();

const string APP_NAME = "D2W";
Design2WorkroomApi.Helpers.LoggerConfigurationExtensions.SetupLoggerConfiguration(APP_NAME);

//builder.Host.UseSerilog((ctx, loggerConfiguration) =>
//    loggerConfiguration.WriteTo.File("Logs/log.txt")
//        logger
//        )
//.lc.ConfigureBaseLogging(APP_NAME, AppVersionInfo.GetBuildInfo());
//.lc.AddApplicationInsightsLogging(services, hostBuilderContext.Configuration)

builder.Host.UseSerilog((hostBuilderContext, services, loggerConfiguration) =>
{
    loggerConfiguration.WriteTo.File("Logs/log.txt");
    loggerConfiguration.ConfigureBaseLogging(APP_NAME);
    loggerConfiguration.AddApplicationInsightsLogging(services, hostBuilderContext.Configuration);
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
      .AddMicrosoftIdentityWebApi(options =>
      {
          builder.Configuration.Bind("AzureAdB2C", options);
          options.TokenValidationParameters.NameClaimType = "name";
      },
        options => { builder.Configuration.Bind("AzureAdB2C", options); });

builder.Services.AddControllers().AddOData(option 
        => option.Select().Filter().OrderBy());

builder.Services.ConfigureCorsPolicy();

builder.Services.ConfigureAddDbContext(builder.Configuration);

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

builder.Services.ConfigureHelpers();

builder.Services.ConfigureServices();

builder.Services.ConfigureRepositories();

// For sending emails via Postmark
builder.Services.AddScoped<IPostmarkEmailSender, EmailSender>();

builder.Services.AddMemoryCache();

builder.Services.Configure<IpRateLimitOptions>((options) =>
{
    options.GeneralRules = new List<RateLimitRule>()
    {
        // Limits API requests to prevent DDOS attacks
        new RateLimitRule()
        {
            Endpoint = "*",
            Limit = 20,
            Period = "2m"
        }
    };
});

builder.Services.AddInMemoryRateLimiting();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// TODO
// XML comments for swagger ??? 
// Configure swagger options ??
//
// ref: https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-2.2&tabs=visual-studio
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "D2W API",
        Version = "v1",
        Description = "",
        // contact
        // license
        // extensions
        // terms of service                   
    });

    string xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    string xmlCommentsPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);
    try
    {
        c.IncludeXmlComments(xmlCommentsPath);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.ToString());
    }
    
});

var app = builder.Build();

app.UseSerilogRequestLogging();

app.UseIpRateLimiting();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "D2W API v1"));
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();


try
{
    Log.Information("Starting web host");
    app.Run();
    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}