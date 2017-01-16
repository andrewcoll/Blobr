using System;
using System.Collections.Generic;
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
            var args = new List<object>() { new Uri("http://localhost") };
            var cloudContainer = A.Fake<CloudBlobContainer>(
            options => options.WithArgumentsForConstructor(args));
            var wrapper = new StorageWrapper(cloudContainer);

            Assert.NotNull(wrapper);
        }
    }
}
