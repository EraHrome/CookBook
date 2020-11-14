using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CookBookServer.Code.Automapper
{
    public static class AutomapperServiceExtention
    {
        public static IServiceCollection AddMapper(this IServiceCollection services, string namespaseFirstPart)
        {
            var assembliesToScane = AppDomain.CurrentDomain.GetAssemblies().Where(a => a.FullName.StartsWith(namespaseFirstPart)).ToArray();
            var allTypes = assembliesToScane.SelectMany(a => a.ExportedTypes).ToArray();

            var profiles = allTypes
                    .Where(t => typeof(Profile).GetTypeInfo().IsAssignableFrom(t.GetTypeInfo()) && !t.GetTypeInfo().IsAbstract)
                    .ToArray();

            var config = new MapperConfiguration(cfg => {
                foreach (var profile in profiles)
                {
                    cfg.AddProfile(profile);
                }
            });
            services.AddSingleton<IMapper, Mapper>(c => new Mapper(config));
            return services;
        }
    }
}
