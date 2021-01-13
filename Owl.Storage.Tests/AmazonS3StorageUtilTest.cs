using System;
using FluentAssertions;
using Owl.Storage.Exceptions;
using Owl.Storage.Utils;
using Xunit;

namespace Owl.Storage.Tests
{
    public class AmazonS3StorageUtilTest
    {
        [Theory]
        [InlineData("")]
        [InlineData("///")]
        [InlineData("\\\\")]
        [InlineData("/dummy.txt")]
        [InlineData("dummy$.txt")]
        public void test(string path)
        {
            // Arrange
            /* path */

            // Act
            Action action = () => AmazonS3StorageUtil.ValidatePath(path);

            // Assert
            action.Should().ThrowExactly<StorageException>();
        }
    }
}