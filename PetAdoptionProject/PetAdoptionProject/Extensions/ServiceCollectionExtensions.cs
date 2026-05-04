using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Services;
using Application.Services.Interfaces;
using Domain.Factories;
using Domain.Strategies;
using Domain.Strategies.Interfaces;
using Infrastructure.Persistence;
using Infrastructure.Repositories;

namespace PetAdoptionProject.Extensions
{

    public static class ServiceCollectionExtensions
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            // Repositories
            services.AddScoped<IPetRepository, PetRepository>();
            services.AddScoped<IAdopterRepository, AdopterRepository>();
            services.AddScoped<IAdoptionRepository, AdoptionRepository>();


            // Services (Application layer)
            services.AddScoped<IPetAdoptionService, PetAdoptionService>();
            services.AddScoped<IPetManagementService, PetManagementService>();
            services.AddScoped<IAdopterService, AdopterService>();

            // Domain Factories
            services.AddScoped<IAdopterFactory, AdopterFactory>();

            // Strategy Pattern
            services.AddScoped<IAdoptionStrategy, DogAdoptionStrategy>();
            services.AddScoped<IAdoptionStrategy, CatAdoptionStrategy>();
            services.AddScoped<IAdoptionStrategy, HamsterAdoptionStrategy>();

            services.AddScoped<IAdoptionStrategyResolver, AdoptionStrategyResolver>();

            // Unit of Work
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
