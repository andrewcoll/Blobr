using System;
using System.Collections.Generic;
using Blobr;
using Xunit;

namespace Blobr.Tests
{
    public class PageTests
    {
        [Fact]
        public void EmptyNameThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
               var create = Page<string>.Create(string.Empty, new List<string>()); 
            });
        }

        [Fact]
        public void NullCollectionThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
               var create = Page<string>.Create<string>("testPage", null); 
            });
        }

        [Fact]
        public void NameIsSet()
        {
            var page = Page<string>.Create("testPage", new List<string>()); 
            Assert.Equal("testPage", page.Name);
        }

        [Fact]
        public void ItemsIsSet()
        {
            var items = new List<string>() { "a", "b", "c" };
            var page = Page<string>.Create("testPage", items); 
            Assert.Equal(items, page.Items);
        }

        [Fact]
        public void NullAddItemThrowsException()
        {
            var items = new List<string>() { "a", "b", "c" };
            var page = Page<string>.Create("testPage", items);

            Assert.Throws<ArgumentNullException>(() =>
            {
               page.AddItem(null);
            });
        }

        [Fact]
        public void CanAddItem()
        {
            var items = new List<string>() { "a", "b", "c" };
            var page = Page<string>.Create("testPage", items);
            page.AddItem("d");
            Assert.Contains("d", page.Items);
        }
    }
}