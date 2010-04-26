using System;
using System.Collections.ObjectModel;

namespace Perseus.Collections {
    public class NotificationCollection<T> : Collection<T> {
        public event EventHandler<CollectionChangedArgs<T>> Changed;
        protected override void ClearItems() {
            base.ClearItems();
            if (this.Changed != null) {
                this.Changed(this, new CollectionChangedArgs<T>());
            }
        }
        protected override void InsertItem(int index, T item) {
            base.InsertItem(index, item);
            if (this.Changed != null) {
                this.Changed(this, new CollectionChangedArgs<T>(index, item));
            }
        }
        protected override void RemoveItem(int index) {
            base.RemoveItem(index);
            if (this.Changed != null) {
                this.Changed(this, new CollectionChangedArgs<T>(index));
            }
        }
        protected override void SetItem(int index, T item) {
            base.SetItem(index, item);
            if (this.Changed != null) {
                this.Changed(this, new CollectionChangedArgs<T>(index, item));
            }
        }
    }    
}
