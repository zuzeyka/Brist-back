namespace Slush.Services.FileStorage
{
    public interface IFileStorageService
    {
        Task<String> SaveFile(String category, Guid attachedId, String fileName, Stream fileStream);
        Task<String> GetUrlToFile(String fileKey);
    }
}
