using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blobr;

namespace Blobr.Tests
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

        public Task SaveBlobDataAsync(string blobName, string data)
        {
            blobData.Add(blobName, data);
            return Task.FromResult(0);
        }
    }
}