using System.Threading.Tasks;

namespace Blobr
{
    public interface IAzureStorageWrapper
    {
        Task<string> LoadBlobDataAsync(string blobName);
        Task SaveBlobDataAsync(string blobName, string data);
    }
}
