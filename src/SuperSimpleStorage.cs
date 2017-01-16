using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Text;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;

namespace SimpleBlobStorage
{
    public class SimpleBlobStorage
    {
        private CloudBlobContainer container;

        public SimpleBlobStorage(CloudBlobContainer blobContainer)
        {
            if(blobContainer == null)
            {
                throw new ArgumentNullException(nameof(blobContainer));
            }    

            this.container = blobContainer;
        }


        /// <summary>
        /// Load a page from storage
        /// </summary>
        /// <param name="pageName">The name of the page</param>
        /// <returns>A page object</returns>
        public async Task<Page<T>> GetPageAsync<T>(string pageName)
        {
            if(string.IsNullOrWhiteSpace(pageName))
            {
                throw new ArgumentNullException(nameof(pageName));
            }

            return await LoadPageAsync<T>(pageName);
        }

        /// <summary>
        /// Loads a page
        /// </summary>
        /// <param name="pageName"></param>
        /// <returns></returns>
        private async Task<Page<T>> LoadPageAsync<T>(string pageName)
        {
            var blob = this.container.GetBlobReference(pageName);
            var data = string.Empty;

            using(var memStream = new MemoryStream())
            {
                await blob.DownloadToStreamAsync(memStream);
                data = Encoding.UTF8.GetString(memStream.ToArray());
            }

            var deserializedData = JsonConvert.DeserializeObject<List<T>>(data);

            return Page<T>.FromJson<T>(deserializedData);
        }


        /// <summary>
        /// Save changes to a page
        /// </summary>
        /// <param name="pageName">The name of the page</param>
        /// <param name="page">The page itself</param>
        /// <returns></returns>
        public async Task SavePageAsync<T>(string pageName, Page<T> page)
        {
            if(string.IsNullOrWhiteSpace(pageName))
            {
                throw new ArgumentNullException(nameof(pageName));
            }

            if(page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }

            var blob = this.container.GetBlockBlobReference(pageName);
            var serializedData = JsonConvert.SerializeObject(page);

            await blob.UploadTextAsync(serializedData);
        }
    }
}
