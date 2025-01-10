using Microsoft.Extensions.DependencyInjection;
//using Products.Application.Behaviors;
using System.Reflection;

namespace Products.Application.Extensions;

public static class ServiceCollectionExtension
{

    public static void AddApplication(this IServiceCollection services)
    {
        Assembly? applicationAssembly = Assembly.GetExecutingAssembly();

        services.AddAutoMapper(applicationAssembly);

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(applicationAssembly);

            //cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ProductQueryHandlerSelectorBehavior<,>));
        });
    }
}
