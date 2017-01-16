using System;
using FakeItEasy;
using Microsoft.WindowsAzure.Storage.Blob;
using Xunit;
using SimpleBlobStorage;

namespace Tests
{
    public class StorageWrapperTests
    {
        [Fact]
        public void NullContainerThrowsException() 
        {
            Assert.Throws<ArgumentNullException>(() => { var sss = new StorageWrapper(null);});
        }

        [Fact]
        public void CanInitialiseStorageWrapper()
        {
            var cloudContainer = A.Fake<CloudBlobContainer>();
            var wrapper = new StorageWrapper(cloudContainer);

            Assert.NotNull(wrapper);
        }
    }
}
