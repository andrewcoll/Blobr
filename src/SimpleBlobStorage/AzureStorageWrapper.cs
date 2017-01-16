using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;

namespace SimpleBlobStorage
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
            this.container.CreateIfNotExistsAsync();
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

            var blob = this.container.GetBlobReference(blobName);
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
        public async void SaveBlobDataAsync(string blobName, string data)
        {
            if(string.IsNullOrWhiteSpace(blobName))
            {
                throw new ArgumentNullException(nameof(blobName));
            }

            if(string.IsNullOrWhiteSpace(data))
            {
                throw new ArgumentNullException(nameof(data));
            }

            var blob = this.container.GetBlockBlobReference(blobName);
            await blob.UploadTextAsync(data);
        }
    }
}