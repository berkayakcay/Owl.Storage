using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Options;
using Owl.Storage.Constants;
using Owl.Storage.Exceptions;
using Owl.Storage.Interfaces;
using Owl.Storage.Models;
using Owl.Storage.Utils;

namespace Owl.Storage.Services
{
    public class AmazonS3StorageService : IStorageService
    {
        private readonly IAmazonS3 _amazonS3;
        private readonly IOptions<AmazonS3StorageOptions> _options;

        public AmazonS3StorageService(IAmazonS3 amazonS3, IOptions<AmazonS3StorageOptions> options)
        {
            _amazonS3 = amazonS3;
            _options = options;
        }

        public async Task<byte[]> ReadAsync(string root, string path, CancellationToken cancellationToken = default)
        {
            var fullpath = AmazonS3StorageUtil.GetFullPath(root, path);
            AmazonS3StorageUtil.ValidatePath(fullpath);

            try
            {
                var getObjectRequest = new GetObjectRequest
                {
                    BucketName = _options.Value.Bucket,
                    Key = fullpath,
                };

                var getObjectResponse = await _amazonS3.GetObjectAsync(getObjectRequest, cancellationToken).ConfigureAwait(false);
                if (getObjectResponse.HttpStatusCode == HttpStatusCode.OK)
                {
                    await using var responseStream = getObjectResponse.ResponseStream;
                    await using (var memoryStream = new MemoryStream())
                    {
                        await responseStream.CopyToAsync(memoryStream, cancellationToken).ConfigureAwait(false);
                        await responseStream.FlushAsync(cancellationToken);
                        return memoryStream.ToArray();
                    }
                }

                return null;
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                throw new StorageException($"{StorageExceptionMessages.COULD_NOT_READ} - An error occurred with the code {amazonS3Exception.ErrorCode} and message '{amazonS3Exception.Message}' when reading an object", amazonS3Exception);
            }
        }

        public async Task<bool> WriteAsync(string root, string path, Stream stream, CancellationToken cancellationToken = default)
        {
            var fullpath = AmazonS3StorageUtil.GetFullPath(root, path);
            AmazonS3StorageUtil.ValidatePath(fullpath);

            try
            {
                var putObjectRequest = new PutObjectRequest()
                {
                    BucketName = _options.Value.Bucket,
                    Key = fullpath,
                    InputStream = stream
                };

                var putObjectResponse = await _amazonS3.PutObjectAsync(putObjectRequest, cancellationToken);
                if (putObjectResponse.HttpStatusCode == HttpStatusCode.OK)
                {
                    return true;
                }

                return false;
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                throw new StorageException($"{StorageExceptionMessages.COULD_NOT_WRITE} - An error occurred with the code {amazonS3Exception.ErrorCode} and message '{amazonS3Exception.Message}' when writing an object", amazonS3Exception);
            }
        }
    }
}