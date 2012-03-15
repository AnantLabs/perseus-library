using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Perseus.Collections {
    public class ListEventArgs<T> : EventArgs {
        private bool _Canceled;
        private T _Item;

        public ListEventArgs(ListAction action)
            : this(action, default(T), default(T), -1, false, false) { }

        public ListEventArgs(ListAction action, bool canCancel)
            : this(action, default(T), default(T), -1, canCancel, false) { }

        public ListEventArgs(ListAction action, T item, int index)
            : this(action, default(T), item, index, false, false) { }

        public ListEventArgs(ListAction action, T item, int index, bool canCancel) 
            : this(action, default(T), item, index, canCancel, false) { }

        public ListEventArgs(ListAction action, T item, int index, bool canCancel, bool canUpdate) 
            : this(action, default(T), item, index, canCancel, canUpdate) { }

        public ListEventArgs(ListAction action, T oldItem, T item, int index)
            : this(action, oldItem, item, index, false, false) { }

        public ListEventArgs(ListAction action, T oldItem, T item, int index, bool canCancel)
            : this(action, oldItem, item, index, canCancel, false) { }

        public ListEventArgs(ListAction action, T oldItem, T item, int index, bool canCancel, bool canUpdate) {
            this.Action = action;
            this.OldItem = oldItem;
            this._Item = item;
            this.Index = index;
            this.CanCancel = canCancel;
            this._Canceled = false;
            this.CanUpdate = canUpdate;
        }

        public ListAction Action { get; protected set; }
        public bool Canceled {
            get { return this._Canceled; }
            set {
                if (value && !this.CanCancel) {
                    throw new Exception("Event cannot be canceled.");
                }

                this._Canceled = value;
            }
        }
        public bool CanCancel { get; protected set; }
        public T OldItem { get; protected set; }
        public bool CanUpdate { get; protected set; }
        public T Item { 
            get { return this._Item; }
            set {
                if (!this.CanUpdate) {
                    throw new Exception("Event item cannot be updated.");
                }

                this._Item = value; 
            }
        }
        public int Index { get; protected set; }
    }
}
