using System;
using System.Collections.Generic;
using FakeItEasy;
using Xunit;
using SimpleBlobStorage;

namespace SimpleBlobStorage.Tests
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
            var wrapper = new StorageWrapper(new TestAzureStorageWrapper());
            Assert.NotNull(wrapper);
        }

        [Fact]
        public void CanCreatePage()
        {
            var items = new List<string>() { "a", "b", "c" };
            var wrapper = new StorageWrapper(new TestAzureStorageWrapper());

            var page = wrapper.CreatePage<string>(items);
            Assert.Equal(3, page.Items.Count);
        }
    }
}
