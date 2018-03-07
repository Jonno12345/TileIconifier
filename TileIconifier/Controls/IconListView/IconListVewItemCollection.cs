using System;
using System.Collections;
using System.Collections.Generic;

namespace TileIconifier.Controls.IconListView
{
    class IconListVewItemCollection : IList<IconListViewItem>
    {
        private List<IconListViewItem> _items;
        private IconListView _owner;

        internal IconListVewItemCollection(IconListView owner)
        {
            _items = new List<IconListViewItem>();
            _owner = owner;
        }

        public IconListViewItem this[int index]
        {
            get
            {
                if (index < 0 || index >= _items.Count)
                {
                    throw new ArgumentOutOfRangeException(nameof(index), index, null);
                }
                return _items[index];
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        public int Count
        {
            get
            {
                return _items.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        ///     Setup an item before it is added.
        /// </summary>
        /// <param name="item">Item that will be added.</param>
        private void SetupForAdd(IconListViewItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            if (item.ListView != null)
            {
                throw new InvalidOperationException("The item is already contained in another IconListView.");
            }
            item.ListView = _owner;
        }

        /// <summary>
        ///     Setup an item before it is removed.
        /// </summary>
        /// <param name="item">Item that will be removed.</param>
        private void SetupForRemove(IconListViewItem item)
        {
            //A null item is accepted
            if (item != null)
            {
                _owner.OnItemRemovingInternal(item.Index);
                item.ListView = null;
            }
        }

        public void Add(IconListViewItem item)
        {
            SetupForAdd(item);
            _items.Add(item);
            _owner.OnItemAddedInternal(true);
        }

        public void AddRange(List<IconListViewItem> items)
        {
            if(items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            var newItems = new IconListViewItem[items.Count];
            for(var i = 0; i < items.Count; i++)
            {
                newItems[i] = items[i];
            }
            AddRange(newItems);
        }

        public void AddRange(IconListViewItem[] items)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }
            for (var i = 0; i < items.Length; i++)
            {
                SetupForAdd(items[i]);
                _items.Add(items[i]);
                _owner.OnItemAddedInternal(i == items.Length - 1);
            }
        }

        public void Clear()
        {
            if (Count == 0)
            {
                return;
            }
            for (var i = _items.Count - 1; i >= 0; i--)
            {
                SetupForRemove(_items[i]);
                _items.RemoveAt(i);
                _owner.OnItemRemovedInternal(i == 0);
            }
        }

        public bool Contains(IconListViewItem item)
        {
            return _items.Contains(item);
        }

        public void CopyTo(IconListViewItem[] array, int arrayIndex)
        {
            _items.CopyTo(array, arrayIndex);
        }

        public IEnumerator<IconListViewItem> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        public int IndexOf(IconListViewItem item)
        {
            return _items.IndexOf(item);
        }

        public void Insert(int index, IconListViewItem item)
        {
            SetupForAdd(item);
            _items.Insert(index, item);
            _owner.OnItemAddedInternal(true);
        }

        public bool Remove(IconListViewItem item)
        {
            SetupForRemove(item);
            var result = _items.Remove(item);
            if (result)
            {
                _owner.OnItemRemovedInternal(true);
            }
            return result;
        }

        public void RemoveAt(int index)
        {
            //Accept invalid indexes
            if (index >= 0 && index < _items.Count)
            {
                var item = _items[index];
                SetupForRemove(item);
                _items.RemoveAt(index);
                _owner.OnItemRemovedInternal(true);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }
    }
}
