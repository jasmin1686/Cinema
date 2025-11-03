using Mapster;
using Cinema.Models;
using Cinema.ViewModels;

namespace Cinema.Configurations
{
    public static class MapsterConfig
    {
        public static void RegisterMapsterConfig(this IServiceCollection services)
        {
            TypeAdapterConfig<ApplicationUser, ApplicationUserVM>
                    .NewConfig()
                    .Map(d => d.FullName, s => $"{s.Name} ")
                    .TwoWays();
        }
    }
}
