using System;
using System.Collections.Generic;
using System.Linq;

namespace Blobr
{
    public class Page<T>
    {
        private readonly string pageName;

        private readonly ICollection<T> page;

        private Page(string pageName, ICollection<T> items) 
        {
            if(items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            this.page = items;
            this.pageName = pageName;
        }

        /// <summary>
        /// Create a new page object
        /// </summary>
        /// <param name="items">Items in the page</param>
        /// <returns>Page object</returns>
        internal static Page<T> Create<T>(string pageName, ICollection<T> items)
        {
            if(items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            if(string.IsNullOrWhiteSpace(pageName))
            {
                throw new ArgumentNullException(nameof(pageName));
            }

            var page = new Page<T>(pageName, items);

            return page;
        }

        /// <summary>
        /// Name of the page
        /// </summary>
        /// <returns>The page name</returns>
        public string Name
        {
            get
            {
                return this.pageName;
            }
        }

        /// <summary>
        /// Add a new item to a page
        /// </summary>
        /// <param name="item">The item to add</param>        
        public void AddItem(T item)
        {
            if(item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            this.page.Add(item);
        }

        /// <summary>
        /// Get the items in this page
        /// </summary>
        /// <returns>A read-only collection of items</returns>
        public IQueryable<T> Items
        {
            get
            {
                return this.page.AsQueryable();
            }
        }
    }
}