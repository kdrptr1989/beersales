using BeerSales.Api.Interface;
using System.Reflection;

namespace BeerSales.Api
{
    public static class ConfigureWeb
    {
        public static void UseWeb(this IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseEndpoints(typeof(WebMarker));
        }

        private static void UseEndpoints(this IApplicationBuilder app, Type typeMarker)
        {
            var endpointTypes = GetEndpointTypesFromAssemblyContaining(typeMarker);

            foreach (var endpointType in endpointTypes)
            {
                endpointType.GetMethod(nameof(IEndpoint.DefineEndpoint))!
                    .Invoke(null, new object[] { app });
            }
        }

        private static IEnumerable<TypeInfo> GetEndpointTypesFromAssemblyContaining(Type typeMarker)
        {
            var endpointTypes = typeMarker.Assembly.DefinedTypes
                .Where(x => !x.IsAbstract && !x.IsInterface &&
                            typeof(IEndpoint).IsAssignableFrom(x));
            return endpointTypes;
        }
    }
}
