using Google.Protobuf.WellKnownTypes;
using Minio;
using Minio.DataModel;
using Minio.DataModel.Args;
using Minio.DataModel.Result;
using Minio.Exceptions;
using System.Reactive.Linq;
using System.Security.AccessControl;

namespace Slush.Services.Minio
{
    public class MinioService
    {
        private readonly IMinioClient _minioClient;

        public MinioService(IMinioClient minioClient)
        {
            _minioClient = minioClient;
        }

        public async Task<ListAllMyBucketsResult> ListBuckets()
        {
            var list = await _minioClient.ListBucketsAsync();

            return list;
        }

        public async Task<List<Item>> ListBucketFiles(String bucketName)
        {
            var files = new List<Item>();

            var listObjectsArgs = new ListObjectsArgs()
                .WithBucket(bucketName)
                .WithRecursive(true);

            try
            {
                var observable = _minioClient.ListObjectsAsync(listObjectsArgs);
                var items = await observable.ToList();
                files.AddRange(items);
            }
            catch (MinioException e)
            {
                Console.WriteLine($"Error occurred: {e.Message}");
            }

            return files;
        }

        public async Task<String> SaveFile(String bucketName, Guid attachedId, String imageName, Stream imageStream)
        {
            try
            {
                String uniqueName = $"{attachedId}/{imageName}";

                var args = new PutObjectArgs()
                    .WithBucket(bucketName)
                    .WithStreamData(imageStream)
                    .WithObject(uniqueName)
                    .WithObjectSize(imageStream.Length);


                await _minioClient.PutObjectAsync(args);

                return uniqueName;
            }
            catch (MinioException ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
                throw;
            }
        }

        public async Task<String> GetUrlToFile( String fileName)
        {
            var reqParams = new Dictionary<string, string>(StringComparer.Ordinal);

            PresignedGetObjectArgs args = new PresignedGetObjectArgs()
                .WithBucket("images")
                .WithObject(fileName)
                .WithHeaders(reqParams)
                .WithExpiry(604800);

            string url = await _minioClient.PresignedGetObjectAsync(args);

            Console.WriteLine(url);

            return url;
        }
    }
}
