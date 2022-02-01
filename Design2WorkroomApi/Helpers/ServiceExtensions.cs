using Design2WorkroomApi.Data;
using Design2WorkroomApi.Repository.Contracts;
using Design2WorkroomApi.Repository;
using Design2WorkroomApi.Services;
using Design2WorkroomApi.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Design2WorkroomApi.Helpers
{
    public static class ServiceExtensions
    {
        public static void ConfigureCorsPolicy(this IServiceCollection services)
        {
            services.AddCors(policy =>
            {
                policy.AddPolicy("CorsPolicy", options => options
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithExposedHeaders("totalAmountPages"));
            });
        }

        public static void ConfigureAddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
              options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }

        public static void ConfigureHelpers(this IServiceCollection services)
        {
            services.AddScoped<AppUserHelper>();
        }

        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IAppRolesProvider, AppRolesProvider>();
            services.AddScoped<IUploadService, UploadService>();
        }

        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IDesignerRepository, DesignerRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IWorkroomRepository, WorkroomRepository>();
            services.AddScoped<IDesignConceptRepository, DesignConceptRepository>();
            services.AddScoped<IEmailRepository, EmailRepository>();
            services.AddScoped<IInvitationRepository, InvitationRepository>();
            services.AddScoped<IB2CGraphClient, B2CGraphClient>();
            services.AddScoped<IWorkOrdersRepository, WorkOrdersRepository>();
        }
    }
}
