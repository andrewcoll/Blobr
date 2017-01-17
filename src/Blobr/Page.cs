using System;
using System.Collections.Generic;
using System.Linq;

namespace Blobr
{
    public class Page<T>
    {
        private ICollection<T> page;

        private Page(ICollection<T> items) 
        {
            if(items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            this.page = items;
        }

        /// <summary>
        /// Create a new page object
        /// </summary>
        /// <param name="items">Items in the page</param>
        /// <returns>Page object</returns>
        internal static Page<T> FromJson<T>(ICollection<T> items)
        {
            if(items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            var page = new Page<T>(items);

            return page;
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