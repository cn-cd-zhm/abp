﻿using System;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Volo.Abp.MongoDB;
using Volo.Abp.MongoDB.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AbpMongoDbServiceCollectionExtensions
    {
        public static IServiceCollection AddMongoDbContext<TMongoDbContext>(this IServiceCollection services, Action<IMongoDbContextRegistrationOptionsBuilder> optionsBuilder = null) //Created overload instead of default parameter
            where TMongoDbContext : AbpMongoDbContext
        {
            var options = new MongoDbContextRegistrationOptions(typeof(TMongoDbContext));
            optionsBuilder?.Invoke(options);

            foreach (var dbContextType in options.ReplacedDbContextTypes)
            {
                services.Replace(ServiceDescriptor.Transient(dbContextType, typeof(TMongoDbContext)));
            }

            new MongoDbRepositoryRegistrar(options)
                .AddRepositories(services);

            return services;
        }
    }
}
