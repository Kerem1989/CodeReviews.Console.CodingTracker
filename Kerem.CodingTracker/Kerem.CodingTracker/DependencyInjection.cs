using Kerem.CodingTracker.Domain.Interfaces;
using Kerem.CodingTracker.Features.CreateCodingSession;
using Kerem.CodingTracker.Features.EditCodingSession;
using Kerem.CodingTracker.Features.FindAllCodingSession;
using Kerem.CodingTracker.Infrastructure.Repositories;
using Kerem.CodingTracker.UI;
using Microsoft.Extensions.DependencyInjection;

namespace Kerem.CodingTracker ;

    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, string connectionString)                                                                                                            
        {               
            services.AddSingleton(new DapperDbContext(connectionString));
            services.AddSingleton<ICodingSessionRepository, CodingSessionRepository>();
            services.AddSingleton<CreateCodingSession>();
            services.AddSingleton<CountCodingSession>();
            services.AddSingleton<FindAllCodingSession>();
            services.AddSingleton<EditCodingSession>();
            services.AddSingleton<ConsoleMenu>();
            return services;
        }   
    }