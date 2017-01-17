using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleBlobStorage.Tests
{
    public class TestAzureStorageWrapper : IAzureStorageWrapper
    {
        internal readonly Dictionary<string, string> blobData;

        public TestAzureStorageWrapper()
        {
            this.blobData = new Dictionary<string, string>();
        }

        public async Task<string> LoadBlobDataAsync(string blobName)
        {
            return blobData[blobName];
        }

        public void SaveBlobDataAsync(string blobName, string data)
        {
            blobData.Add(blobName, data);
        }
    }
}