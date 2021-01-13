using Owl.Storage.Constants;
using Owl.Storage.Exceptions;

namespace Owl.Storage.Utils
{
    public static class AmazonS3StorageUtil
    {
        /// <summary>
        /// Get full path
        /// </summary>
        /// <param name="root"></param>
        /// <param name="path"></param>
        public static string GetFullPath(string root, string path)
        {
            return System.IO.Path.Combine(root, path);
        }

        /// <summary>
        /// Validates path
        /// </summary>
        /// <param name="path"></param>
        /// <exception cref="StorageException"></exception>
        public static void ValidatePath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new StorageException($"{StorageExceptionMessages.INVALID_PATH} - {nameof(path)} : {path} can not be null or empty");
            }

            // TODO : could have more validation
        }
    }
}