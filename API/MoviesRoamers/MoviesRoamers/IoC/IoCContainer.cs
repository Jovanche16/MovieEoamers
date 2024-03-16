using Microsoft.Extensions.Configuration;
using MoviesRoamers.Data.Repositories.Common;
using MoviesRoamers.Data.Repositories.Interface;
using MoviesRoamers.Services.Common;
using MoviesRoamers.Services.Interface;

namespace MovieRoamers.IoC
{
    public class IoCContainer
    {
        public static IServiceCollection Configure(IServiceCollection services, IConfiguration configuration)
        {
            // Register your services here
            services.AddSingleton<IConfiguration>(configuration);
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IMoviesService, MoviesService>();
            services.AddScoped<IWatchListService, WatchListService>();



            // Register your Repositories here
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<IMoviesRepository, MoviesRepository>();
            services.AddScoped<IWatchListRepository, WatchListRepository>();

            return services;
        }

    }
}
