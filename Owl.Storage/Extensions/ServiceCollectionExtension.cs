using System;
using Amazon;
using Amazon.S3;
using Owl.Storage.Interfaces;
using Owl.Storage.Models;
using Owl.Storage.Services;
using Owl.Storage.Validators;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddAmazonS3StorageService(this IServiceCollection services, Action<AmazonS3StorageOptions> configureOptions)
        {
            if (services == default)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (configureOptions == default)
            {
                throw new ArgumentNullException(nameof(configureOptions));
            }

            services.AddSingleton<IValidateOptions<AmazonS3StorageOptions>, AmazonS3StorageOptionsValidator>();
            services.Configure(configureOptions);
            using (var provider = services.BuildServiceProvider(true))
            {
                var options = provider.GetRequiredService<IOptions<AmazonS3StorageOptions>>().Value;
                return services.AddAmazonS3StorageService(options);
            }
        }

        public static IServiceCollection AddAmazonS3StorageService(this IServiceCollection services, IConfigurationSection config)
        {
            if (services == default)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (config == default)
            {
                throw new ArgumentNullException(nameof(config));
            }

            services.AddSingleton<IValidateOptions<AmazonS3StorageOptions>, AmazonS3StorageOptionsValidator>();
            services.Configure<AmazonS3StorageOptions>(config);
            using (var provider = services.BuildServiceProvider(true))
            {
                var options = provider.GetRequiredService<IOptions<AmazonS3StorageOptions>>().Value;
                return services.AddAmazonS3StorageService(options);
            }
        }

        public static IServiceCollection AddAmazonS3StorageService(this IServiceCollection services, IConfigurationSection config, Action<BinderOptions> configureBinder)
        {
            if (services == default)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (config == default)
            {
                throw new ArgumentNullException(nameof(config));
            }

            if (configureBinder == default)
            {
                throw new ArgumentNullException(nameof(configureBinder));
            }

            services.AddSingleton<IValidateOptions<AmazonS3StorageOptions>, AmazonS3StorageOptionsValidator>();
            services.Configure<AmazonS3StorageOptions>(config, configureBinder);
            using (var provider = services.BuildServiceProvider(true))
            {
                var options = provider.GetRequiredService<IOptions<AmazonS3StorageOptions>>().Value;
                return services.AddAmazonS3StorageService(options);
            }
        }

        private static IServiceCollection AddAmazonS3StorageService(this IServiceCollection services, AmazonS3StorageOptions options)
        {
            services.AddSingleton<IAmazonS3, AmazonS3Client>(_ => new AmazonS3Client(options.AccessKeyId, options.SecretAccessKey, RegionEndpoint.GetBySystemName(options.Region)));
            services.AddSingleton<IStorageService, AmazonS3StorageService>();
            return services;
        }
    }
}