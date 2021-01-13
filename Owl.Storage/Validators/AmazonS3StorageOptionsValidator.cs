using Microsoft.Extensions.Options;
using Owl.Storage.Models;

namespace Owl.Storage.Validators
{
    internal class AmazonS3StorageOptionsValidator : IValidateOptions<AmazonS3StorageOptions>
    {
        public ValidateOptionsResult Validate(string name, AmazonS3StorageOptions options)
        {
            return string.IsNullOrWhiteSpace(options.AccessKeyId) ||
                   string.IsNullOrWhiteSpace(options.SecretAccessKey) ||
                   string.IsNullOrWhiteSpace(options.Region) ||
                   string.IsNullOrWhiteSpace(options.Bucket)
                ? ValidateOptionsResult.Fail($"'{nameof(AmazonS3StorageOptions)}' section is invalid")
                : ValidateOptionsResult.Success;
        }
    }
}