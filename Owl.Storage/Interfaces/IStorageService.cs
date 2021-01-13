using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Owl.Storage.Interfaces
{
    public interface IStorageService
    {
        /// <summary>
        /// Reads given path
        /// </summary>
        /// <param name="root">root path containet/bucket</param>
        /// <param name="path">path with filename and extension</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<byte[]> ReadAsync(string root, string path, CancellationToken cancellationToken = default);

        /// <summary>
        /// Writes content to given path
        /// </summary>
        /// <param name="root">root path containet/bucket</param>
        /// <param name="path">path with filename and extension</param>
        /// <param name="stream"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> WriteAsync(string root, string path, Stream stream, CancellationToken cancellationToken = default);
    }
}