using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SimpleBlobStorage
{
    public class StorageWrapper
    {
        private readonly IAzureStorageWrapper storageWrapper;

        public StorageWrapper(IAzureStorageWrapper storageWrapper)
        {
            if(storageWrapper == null)
            {
                throw new ArgumentNullException(nameof(storageWrapper));
            }    

            this.storageWrapper = storageWrapper;
        }


        /// <summary>
        /// Create a new page
        /// </summary>
        /// <param name="items">Items for the page</param>
        /// <returns>A page object</returns>
        public Page<T> CreatePage<T>(ICollection<T> items)
        {
            if(items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            var page = Page<T>.FromJson<T>(items);
            return page;
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
            var data = await this.storageWrapper.LoadBlobDataAsync(pageName);
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

            var serializedData = JsonConvert.SerializeObject(page.Items);
            await this.storageWrapper.SaveBlobDataAsync(pageName, serializedData);
        }
    }
}
