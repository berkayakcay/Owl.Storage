using System;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Owl.Storage.Interfaces;
using Owl.Storage.Services;
using Xunit;

namespace Owl.Storage.Tests
{
    public class ServiceCollectionTests
    {
        [Fact]
        public void AddAmazonS3StorageService_WithValidOptions_BeOfTypeAmazonS3StorageService()
        {
            // Arrange
            var serviceCollection = new ServiceCollection();

            // Act
            serviceCollection.AddAmazonS3StorageService(options =>
            {
                options.AccessKeyId = "dummy";
                options.SecretAccessKey = "dummy";
                options.Region = "eu-west-2";
                options.Bucket = "dev-bucket";
            });

            // Assert
            var serviceProvider = serviceCollection.BuildServiceProvider(false);

            var storageService = serviceProvider.GetService<IStorageService>();

            storageService.Should().BeOfType<AmazonS3StorageService>();
        }

        [Fact]
        public void AddAmazonS3StorageService_WithInvalidOptions_ThrowException()
        {
            // Arrange
            var serviceCollection = new ServiceCollection();

            // Act
            Action action = () => serviceCollection.AddAmazonS3StorageService(options =>
            {
                options.AccessKeyId = string.Empty;
                options.SecretAccessKey = string.Empty;
                options.Region = string.Empty;
                options.Bucket = string.Empty;
            });

            // Assert
            action.Should().Throw<Exception>();
        }
    }
}