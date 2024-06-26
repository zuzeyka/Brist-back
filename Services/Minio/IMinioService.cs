using Minio.DataModel;
using Minio.DataModel.Result;

namespace Slush.Services.Minio
{
    public interface IMinioService
    {
        Task<ListAllMyBucketsResult> ListBuckets();
        Task<List<Item>> ListBucketFiles(String bucketName);
        Task<String> SaveFile(String bucketName, Guid attachedId, String imageName, Stream imageStream);
        Task<List<String>> GetUrlToFiles();
        Task<String> GetUrlToFile(String fileName);
    }
}
