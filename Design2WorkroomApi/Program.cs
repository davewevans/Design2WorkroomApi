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

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// ref: https://docs.microsoft.com/en-us/azure/active-directory/develop/quickstart-v2-aspnet-core-web-api
//services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddMicrosoftIdentityWebApi(Configuration.GetSection("AzureAdB2C"));

// ref: https://github.com/datalust/dotnet6-serilog-example
// ref: https://www.youtube.com/watch?v=ad0IhMFsxyw
builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console());
    //.WriteTo.Seq("http://localhost:5341"));

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

app.Run();
