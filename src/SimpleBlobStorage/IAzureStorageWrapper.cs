using System.Threading.Tasks;

public interface IAzureStorageWrapper
{
    Task<string> LoadBlobDataAsync(string blobName);
    Task SaveBlobDataAsync(string blobName, string data);
}