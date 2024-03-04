using Thunders.TaskGo.Infra.Repositories;
using Thunders.TaskGo.Service.Services;

namespace Thunders.TaskGo.Web.Startup.Extensions
{
    public static class ConfigureServicesExtension
    {
        public static void AddServices(this IServiceCollection services)
        {        
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
        
            services.AddScoped<ITaskItemService, TaskItemService>();
            services.AddScoped<ITaskItemRepository, TaskItemRepository>();        
        }
    }
}
