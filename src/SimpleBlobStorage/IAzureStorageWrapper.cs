using System.Threading.Tasks;

public interface IAzureStorageWrapper
{
    Task<string> LoadBlobDataAsync(string blobName);
    void SaveBlobDataAsync(string blobName, string data);
}