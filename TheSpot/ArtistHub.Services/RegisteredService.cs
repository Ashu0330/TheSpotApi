
using ArtistHub.DAL.FactoryServices;
using ArtistHub.DAL.IFactoryServices;
using ArtistHub.DAL.Repository;
using ArtistHub.DAL.UOW;
using ArtistHub.Presentation.Helper;
using ArtistHub.Repository.RepositoryServices;
using ArtistHub.Services.IService;
using ArtistHub.Services.Service;
using Microsoft.Extensions.DependencyInjection;

namespace ArtistHub.Services
{
    public class RegisteredService
    {
        public static void RegisterServices(IServiceCollection service, string Connection)
        {
            service.AddScoped<IUOW, UOW>();
            service.AddScoped(typeof(IGenricRepository<>), typeof(GenricRepository<>));
            service.AddScoped<IFactoryService, FactoryService>();
            service.AddScoped(typeof(IRepositoryService<>), typeof(RepositoryService<>));
            service.AddSingleton(typeof(IConnectionService), item => new ConnectionService(Connection));
            service.AddScoped<IUserService, UserService>();
            service.AddScoped<IExploreService, ExploreService>();
            service.AddScoped<IAuthService, AuthService>();
            service.AddScoped<IArtistService, ArtistService>();
            service.AddScoped<ILoungeService, LoungeService>();
            service.AddScoped<IBookingService, BookingService>();
            service.AddScoped<IAdminService, AdminService>();
            service.AddScoped<IMasterService, MasterService>();
            service.AddScoped<ImageHelper>();
            service.AddHttpContextAccessor();
            service.AddScoped<JwtHelper>();
        }
    }
}
