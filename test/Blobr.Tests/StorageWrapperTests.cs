﻿using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Blobr;

namespace Blobr.Tests
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

            var page = wrapper.CreatePage<string>("testPage", items);
            Assert.Equal(3, page.Items.ToList().Count);
        }

        [Fact]
        public async void CanSavePage()
        {
            var storage = new TestAzureStorageWrapper();
            var items = new List<string>() { "a", "b", "c" };
            var wrapper = new StorageWrapper(storage);
            var page = wrapper.CreatePage<string>("testPage", items);
            await wrapper.SavePageAsync(page);
            Assert.Equal(1, storage.blobData.Count);
        }

        [Fact]
        public async void CanGetExistingPage()
        {
            var storage = new TestAzureStorageWrapper();
            var items = new List<string>() { "a", "b", "c" };
            var wrapper = new StorageWrapper(storage);
            var page = wrapper.CreatePage<string>("testPage", items);
            await wrapper.SavePageAsync(page);
            
            var retrieved = await wrapper.GetPageAsync<string>("testPage");
            Assert.Equal(3, retrieved.Items.ToList().Count);
        }
    }
}
