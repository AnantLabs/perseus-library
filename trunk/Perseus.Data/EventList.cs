using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

/* This code is heavily based on the class at http://www.codeproject.com/KB/cs/EventyList.aspx
 */
namespace Perseus.Collections {
    public delegate void ListChangedEventHandler<T>(EventList<T> sender, ListEventArgs<T> e);

    [DebuggerDisplay("Count = {Count}")]
    [Serializable()]
    public class EventList<T> : IList<T>, IList {
        private T[] _Items;
        private int _Size; // Current size of the list
        private int _Version; // Keep track of changes

        public EventList() {            
            this._Items = new T[0];
        }
        public EventList(int capacity) {
            if (capacity < 0) {
                throw new ArgumentOutOfRangeException();
            }
            this._Items = new T[capacity];
        }
        public EventList(IEnumerable<T> collection) {
            if (collection == null) {
                this._Items = new T[0];
                return;
            }

            ICollection<T> c = collection as ICollection<T>;
            if (c != null) {
                int count = c.Count;
                this._Items = new T[count];
                c.CopyTo(this._Items, 0);
                this._Size = count;
            }
            else {
                this._Size = 0;
                this._Items = new T[4];

                using (IEnumerator<T> en = collection.GetEnumerator()) {
                    while (en.MoveNext()) {
                        this.Add(en.Current);
                    }
                }
            }
        }

        #region Events
        public event ListChangedEventHandler<T> ItemAdded, ItemRemoved, ItemGotten, ItemSet, ListCleared, 
            BeforeItemAdded, BeforeItemRemove, BeforeItemSet, BeforeListCleared;

        protected virtual void OnItemAdded(ListEventArgs<T> e) {
            if (this.ItemAdded != null) {
                this.ItemAdded(this, e);
            }
        }
        protected virtual void OnItemRemoved(ListEventArgs<T> e) {
            if (this.ItemRemoved != null) {
                this.ItemRemoved(this, e);
            }
        }
        protected virtual void OnItemGotten(ListEventArgs<T> e) {
            if (this.ItemGotten != null) {
                this.ItemGotten(this, e);
            }
        }
        protected virtual void OnItemSet(ListEventArgs<T> e) {
            if (this.ItemSet != null) {
                this.ItemSet(this, e);
            }
        }
        protected virtual void OnListCleared(ListEventArgs<T> e) {
            if (this.ListCleared != null) {
                this.ListCleared(this, e);
            }
        }

        protected virtual void OnBeforeItemAdded(ListEventArgs<T> e) {
            if (this.BeforeItemAdded != null) {
                this.BeforeItemAdded(this, e);
            }
        }
        protected virtual void OnBeforeItemRemoved(ListEventArgs<T> e) {
            if (this.BeforeItemRemove != null) {
                this.BeforeItemRemove(this, e);
            }
        }
        protected virtual void OnBeforeItemSet(ListEventArgs<T> e) {
            if (this.BeforeItemSet != null) {
                this.BeforeItemSet(this, e);
            }
        }
        protected virtual void OnBeforeListCleared(ListEventArgs<T> e) {
            if (this.BeforeListCleared != null) {
                this.BeforeListCleared(this, e);
            }
        }
        #endregion

        #region Helpers
        private void EnsureCapacity(int capacity) {
            if (this._Items.Length < capacity) {
                // For performance, we increate the capicty by a decent amount so when 
                // adding items over a loop, the entire list doesn't need to be copied over and over
                int newCapacity = (this._Items.Length == 0 ? 4 : this._Items.Length * 2);

                if (newCapacity < capacity) {
                    newCapacity = capacity;
                }

                T[] newItems = new T[newCapacity];
                this._Items.CopyTo(newItems, 0);
                this._Items = newItems;
            }
        }
        private static bool IsCompatibleObject(object value) {
            return ((value is T) || (value == null && !typeof(T).IsValueType));
        }        
        #endregion

        #region IList<T> Members
        public int IndexOf(T item) {
            return Array.IndexOf(this._Items, item, 0, this._Size);
        }
        public void Insert(int index, T item) {
            if (index > this._Size) {
                throw new ArgumentOutOfRangeException();
            }

            var e = new ListEventArgs<T>(ListAction.Add, item, index, true, true);
            this.OnBeforeItemAdded(e);
            if (e.Canceled) {
                return;
            }

            if (this._Size == this._Items.Length) {
                this.EnsureCapacity(this._Size + 1);
            }

            // Don't shift if item is on the end
            if (index < this._Size) {
                Array.Copy(this._Items, index, this._Items, index + 1, this._Size - index);
            }

            this._Items[index] = e.Item;
            ++this._Size;
            
            ++this._Version;

            e = new ListEventArgs<T>(ListAction.Add, e.Item, index);
            this.OnItemAdded(e);
        }
        public void RemoveAt(int index) {
            if ((uint)index >= (uint)this._Size) {
                throw new ArgumentOutOfRangeException();
            }

            T oldItem = this._Items[index];

            var e = new ListEventArgs<T>(ListAction.Remove, oldItem, index, true);
            this.OnBeforeItemRemoved(e);
            if (e.Canceled) {
                return;
            }
            
            --this._Size;
            if (index < this._Size) {
                Array.Copy(this._Items, index + 1, this._Items, index, this._Size - index);
            }

            this._Items[this._Size] = default(T);

            ++this._Version;

            e = new ListEventArgs<T>(ListAction.Remove, oldItem, index);
            this.OnItemRemoved(e);
        }
        public T this[int index] {
            get {
                if ((uint)index >= (uint)this._Size) {
                    throw new ArgumentOutOfRangeException();
                }

                T item = this._Items[index];

                var e = new ListEventArgs<T>(ListAction.Get, item, index);
                this.OnItemGotten(e);

                return item;
            }
            set {
                if ((uint)index >= (uint)this._Size) {
                    throw new ArgumentOutOfRangeException();
                }

                T oldItem = this._Items[index];

                var e = new ListEventArgs<T>(ListAction.Set, oldItem, value, index, true, true);
                this.OnItemSet(e);
                if (e.Canceled) {
                    return;
                }

                this._Items[index] = e.Item;
                ++this._Version;

                e = new ListEventArgs<T>(ListAction.Set, oldItem, e.Item, index);
                this.OnItemSet(e);
            }
        }
        #endregion

        #region ICollection<T> Members
        public void Add(T item) {
            var e = new ListEventArgs<T>(ListAction.Add, item, this._Size, true, true);
            this.OnBeforeItemAdded(e);
            if (e.Canceled) {
                return;
            }

            if (this._Size >= this._Items.Length) {
                this.EnsureCapacity(this._Size + 1);
            }

            this._Items[this._Size++] = e.Item;

            ++this._Version;

            e = new ListEventArgs<T>(ListAction.Add, e.Item, this._Size - 1);
            this.OnItemAdded(e);
        }
        public void Clear() {
            var e = new ListEventArgs<T>(ListAction.Clear, true);
            this.OnBeforeListCleared(e);
            if (e.Canceled) {
                return;
            }
            
            this._Items = new T[this._Size];
            this._Size = 0;
            
            ++this._Version;

            e = new ListEventArgs<T>(ListAction.Clear);
            this.OnListCleared(e);
        }
        public bool Contains(T item) {
            if ((Object)item == null) {
                for (int i = 0; i < this._Size; ++i) {
                    if ((Object)this._Items[i] == null) {
                        return true;
                    }
                }

                return false;
            }
            
            EqualityComparer<T> c = EqualityComparer<T>.Default;
            for (int i = 0; i < this._Size; ++i) {
                if (c.Equals(this._Items[i], item))
                    return true;
            }

            return false;
        }
        public void CopyTo(T[] array, int index) {
            Array.Copy(this._Items, 0, array, index, this._Size);
        }
        public int Count { get { return this._Size; } }
        public bool IsReadOnly { get { return false; } }
        public bool Remove(T item) {
            int index = IndexOf(item);

            if (index >= 0) {
                var e = new ListEventArgs<T>(ListAction.Remove, item, index, true);
                this.OnBeforeItemRemoved(e);
                if (e.Canceled) {
                    return false;
                }
                                
                this.RemoveAt(index);

                e = new ListEventArgs<T>(ListAction.Remove, item, index);
                this.OnItemRemoved(e);

                return true;
            }

            return false;
        }
        #endregion

        #region IEnumerable<T> Members
        public IEnumerator<T> GetEnumerator() {
            return new Enumerator(this);
        }
        #endregion

        #region IEnumerable Members
        IEnumerator IEnumerable.GetEnumerator() {
            return new Enumerator(this);
        }
        #endregion

        #region IList Members
        int IList.Add(object item) {
            this.Add((T)item);

            return this.Count - 1;
        }
        bool IList.Contains(object item) {
            if (EventList<T>.IsCompatibleObject(item)) {
                return this.Contains((T)item);
            }

            return false;
        }
        int IList.IndexOf(object item) {
            if (EventList<T>.IsCompatibleObject(item)) {
                return this.IndexOf((T)item);
            }

            return -1;
        }
        void IList.Insert(int index, object item) {
            this.Insert(index, (T)item);
        }
        bool IList.IsFixedSize { get { return false; } }
        void IList.Remove(object item) {
            if (EventList<T>.IsCompatibleObject(item)) {
                this.Remove((T)item);
            }
        }
        object IList.this[int index] {
            get { return this[index]; }
            set {
                if (!EventList<T>.IsCompatibleObject(value)) {
                    throw new ArgumentException("Wrong value type: \"" + value.GetType().ToString() + "\"!");
                }
                this[index] = (T)value;
            }
        }
        #endregion

        #region ICollection Members
        void ICollection.CopyTo(Array array, int index) {
            if (array.Rank != 1) {
                throw new ArgumentException("Multi dimensional arrays are not supported.");
            }

            try {
                Array.Copy(this._Items, 0, array, index, this._Size);
            }
            catch (ArrayTypeMismatchException) {
                throw new ArgumentException("Incompatible array types");
            }
        }
        bool ICollection.IsSynchronized { get { return false; } }
        object ICollection.SyncRoot { get { return null; } }
        #endregion

        #region The Enumerator
        [Serializable()]
        public struct Enumerator : IEnumerator<T>, IEnumerator {
            private EventList<T> _List;
            private int _Index;
            private int _Version;
            private T _Current;

            internal Enumerator(EventList<T> list) {
                this._List = list;
                this._Index = 0;
                this._Version = list._Version;
                this._Current = default(T);
            }

            public bool MoveNext() {
                if (this._Version != this._List._Version) {
                    throw new InvalidOperationException("Collection has changed!");
                }

                if (this._Index < this._List._Size) {
                    this._Current = this._List._Items[this._Index++];
                    return true;
                }

                this._Index = this._List._Size + 1;
                this._Current = default(T);

                return false;
            }

            public T Current { get { return this._Current; } }

            Object IEnumerator.Current {
                get {
                    if (this._Index == 0 || this._Index == this._List._Size + 1) {
                        throw new InvalidOperationException("What's up?");
                    }

                    return this.Current;
                }
            }

            void IEnumerator.Reset() {
                if (this._Version != this._List._Version) {
                    throw new InvalidOperationException("Collection has changed!");
                }

                this._Index = 0;
                this._Current = default(T);
            }

            public void Dispose() {
                // Intentionally left blank
            }

        }
        #endregion
    }

    public enum ListAction {
        Add,
        Remove,
        Get,
        Set,
        Clear
    }
}
