using System;
using System.Threading.Tasks;
using Amazon.S3;
using FluentAssertions;
using Owl.Storage.Exceptions;
using Owl.Storage.Models;
using Owl.Storage.Services;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace Owl.Storage.Tests
{


    public class AmazonS3StorageTest
    {
        [Fact]
        public void ReadAsync_WithEmptyRootAndPath_ThrowStorageException()
        {
            // Arrange
            var root = string.Empty;
            var path = string.Empty;
            var mockAmazonS3 = new Mock<IAmazonS3>();
            var mockOptions = new Mock<IOptions<AmazonS3StorageOptions>>();
            mockOptions.SetupGet(o => o.Value).Returns(new AmazonS3StorageOptions() { Bucket = "dev-bucket" });

            var amazonS3StorageService = new AmazonS3StorageService(mockAmazonS3.Object, mockOptions.Object);

            // Act
            Func<Task> action = async () => await amazonS3StorageService.ReadAsync(root, path);

            // Assert
            action.Should().ThrowExactly<StorageException>();
        }

        [Fact]
        public void WriteAsync_WithEmptyRootAndPath_ThrowStorageException()
        {
            // Arrange
            var root = string.Empty;
            var path = string.Empty;
            var mockAmazonS3 = new Mock<IAmazonS3>();
            var mockOptions = new Mock<IOptions<AmazonS3StorageOptions>>();
            mockOptions.SetupGet(o => o.Value).Returns(new AmazonS3StorageOptions() { Bucket = "dev-bucket" });

            var amazonS3StorageService = new AmazonS3StorageService(mockAmazonS3.Object, mockOptions.Object);

            // Act
            Func<Task> action = async () => await amazonS3StorageService.ReadAsync(root, path);

            // Assert
            action.Should().ThrowExactly<StorageException>();
        }

        [Fact]
        public void WriteAsync_WithLeadingSlashRoot_ThrowStorageException()
        {
            // Arrange
            var root = "/dummy";
            var path = "2020/10/10/dummy.jpg";
            var mockAmazonS3 = new Mock<IAmazonS3>();
            var mockOptions = new Mock<IOptions<AmazonS3StorageOptions>>();
            mockOptions.SetupGet(o => o.Value).Returns(new AmazonS3StorageOptions() { Bucket = "dev-bucket" });

            var amazonS3StorageService = new AmazonS3StorageService(mockAmazonS3.Object, mockOptions.Object);

            // Act
            Func<Task> action = async () => await amazonS3StorageService.ReadAsync(root, path);

            // Assert
            action.Should().ThrowExactly<StorageException>();
        }
    }
}