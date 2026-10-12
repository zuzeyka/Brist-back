using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Slush.Services.FileStorage
{
    public class LocalFileStorageService : IFileStorageService
    {
        private static readonly HashSet<String> AllowedImageExtensions = new(StringComparer.OrdinalIgnoreCase)
        {
            ".jpg", ".jpeg", ".png", ".gif", ".webp"
        };

        private static readonly HashSet<String> AllowedVideoExtensions = new(StringComparer.OrdinalIgnoreCase)
        {
            ".mp4", ".webm", ".mov"
        };

        private const long MaxImageSizeBytes = 10 * 1024 * 1024;
        private const long MaxVideoSizeBytes = 200 * 1024 * 1024;

        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LocalFileStorageService(IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            _env = env;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<String> SaveFile(String category, Guid attachedId, String fileName, Stream fileStream)
        {
            var extension = Path.GetExtension(fileName);
            long maxSizeBytes;
            if (!String.IsNullOrEmpty(extension) && AllowedImageExtensions.Contains(extension))
            {
                maxSizeBytes = MaxImageSizeBytes;
            }
            else if (!String.IsNullOrEmpty(extension) && AllowedVideoExtensions.Contains(extension))
            {
                maxSizeBytes = MaxVideoSizeBytes;
            }
            else
            {
                throw new InvalidOperationException($"File type '{extension}' is not allowed.");
            }

            if (fileStream.Length > maxSizeBytes)
            {
                throw new InvalidOperationException($"File exceeds the maximum allowed size of {maxSizeBytes / (1024 * 1024)} MB.");
            }

            // The name written to disk is always generated, never the client-supplied
            // one — this rules out path traversal and collisions by construction.
            var safeFileName = $"{Guid.NewGuid()}{extension.ToLowerInvariant()}";
            var attachedFolder = attachedId.ToString();

            var fullDirectory = Path.Combine(_env.WebRootPath, "uploads", category, attachedFolder);
            Directory.CreateDirectory(fullDirectory);

            var fullPath = Path.Combine(fullDirectory, safeFileName);
            fileStream.Position = 0;
            using (var fileOutput = new FileStream(fullPath, FileMode.Create))
            {
                await fileStream.CopyToAsync(fileOutput);
            }

            return $"{category}/{attachedFolder}/{safeFileName}";
        }

        public Task<String> GetUrlToFile(String fileKey)
        {
            var request = _httpContextAccessor.HttpContext?.Request;
            var url = request != null
                ? $"{request.Scheme}://{request.Host}/uploads/{fileKey}"
                : $"/uploads/{fileKey}";

            return Task.FromResult(url);
        }
    }
}
