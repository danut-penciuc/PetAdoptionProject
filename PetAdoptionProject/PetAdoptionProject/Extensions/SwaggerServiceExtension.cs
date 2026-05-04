using Microsoft.OpenApi;

namespace PetAdoptionProject.Extensions
{
    public static class SwaggerServiceExtensions
    {
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Pet Adoption API", Version = "v1" });
            });

            return services;
        }
    }
}
