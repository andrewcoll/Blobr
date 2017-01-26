using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Blobr
{
    public class AzureStorageWrapper : IAzureStorageWrapper
    {

        private readonly CloudBlobContainer container;

        /// <summary>
        /// Wrapper for Azure Storage
        /// </summary>
        /// <param name="accountName">Azure storage account name</param>
        /// <param name="keyValue">Azure storage account access key</param>
        /// <param name="containerName">Blob container to load, will be created if does not exist</param>
        public AzureStorageWrapper(string accountName, string keyValue, string containerName)
        {
            if(string.IsNullOrWhiteSpace(accountName))
            {
                throw new ArgumentNullException(nameof(accountName));
            }

            if(string.IsNullOrWhiteSpace(keyValue))
            {
                throw new ArgumentNullException(nameof(keyValue));
            }

            if(string.IsNullOrWhiteSpace(containerName))
            {
                throw new ArgumentNullException(nameof(containerName));
            }

            var storageCredentials = new StorageCredentials(accountName, keyValue);
            var cloudStorage = new CloudStorageAccount(storageCredentials, true);
            var blobClient = cloudStorage.CreateCloudBlobClient();
            this.container = blobClient.GetContainerReference(containerName);
        }


        /// <summary>
        /// Load data from a blob
        /// </summary>
        /// <param name="blobName">Name of blob to load</param>
        /// <returns>Blob data</returns>
        public async Task<string> LoadBlobDataAsync(string blobName)
        {
            if(string.IsNullOrWhiteSpace(blobName))
            {
                throw new ArgumentNullException(nameof(blobName));
            }

            if(!await this.container.ExistsAsync())
            {
                throw new BlobrLoadException($"Cannot retrieve '{blobName}' as specified container does not exist.");
            }

            var blob = this.container.GetBlobReference(blobName);

            if(!await blob.ExistsAsync())
            {
                throw new BlobrLoadException($"Cannot retried '{blobName}' as it does not exist.");
            }
            
            var data = string.Empty;

            using(var memStream = new MemoryStream())
            {
                await blob.DownloadToStreamAsync(memStream);
                data = Encoding.UTF8.GetString(memStream.ToArray());
            }

            return data;
        }

        

        /// <summary>
        /// Save data to a blob
        /// </summary>
        /// <param name="blobName">Blob to save to</param>
        /// <param name="data">Data to save</param>
        /// <param name="maxAttempts">Maximum number of attempts to save</param>
        /// <param name="retryInterval">Interval between attemps in milliseconds</param>
        /// <returns></returns>
        public async Task SaveBlobDataAsync(string blobName, string data, int maxAttempts = 5, int retryInterval = 3000)
        {
            if(string.IsNullOrWhiteSpace(blobName))
            {
                throw new ArgumentNullException(nameof(blobName));
            }

            if(string.IsNullOrWhiteSpace(data))
            {
                throw new ArgumentNullException(nameof(data));
            }

            if(maxAttempts < 1)
            {
                throw new ArgumentException("maxAttempts must be at least 1.");
            }

            await this.container.CreateIfNotExistsAsync();
            var blob = this.container.GetBlockBlobReference(blobName);

            while(maxAttempts > 0)
            {
                if(blob.Properties.LeaseState == LeaseState.Leased)
                {
                    await Task.Delay(retryInterval);
                    maxAttempts--;

                    if(maxAttempts == 0)
                    {
                        throw new BlobrSaveException("Failed to acquire lease on blob and max attemps exceeded.");
                    }

                    continue;
                }

                var lease = await blob.AcquireLeaseAsync(TimeSpan.FromSeconds(5));
                var accessCondition = AccessCondition.GenerateLeaseCondition(lease);
                
                await blob.UploadTextAsync(data, accessCondition, null, null);
                await blob.ReleaseLeaseAsync(accessCondition);
                break;
            }
        }
    }
}