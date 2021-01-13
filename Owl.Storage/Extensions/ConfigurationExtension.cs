using System;
using Owl.Storage.Models;

namespace Microsoft.Extensions.Configuration
{
    public static class ConfigurationExtension
    {
        /// <summary>
        /// Gets amazon s3 storage options from configuration
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static IConfigurationSection GetAmazonS3StorageOptionsSection(this IConfiguration configuration)
        {
            if (configuration == default)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            var section = configuration.GetSection(nameof(AmazonS3StorageOptions));
            if (!section.Exists())
            {
                throw new ArgumentException($"'{nameof(AmazonS3StorageOptions)}' section doesn't exist", nameof(configuration));
            }

            return section;
        }
    }
}