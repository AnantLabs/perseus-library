using System;
using System.Collections.Generic;

namespace Perseus.Collections {
    public class CollectionChangedArgs<T> : EventArgs {
        public int Index;
        public T Item;

        public CollectionChangedArgs() {
            this.Index = -1;
            this.Item = default(T);
        }
        public CollectionChangedArgs(int index) {
            this.Index = index;
            this.Item = default(T);
        }
        public CollectionChangedArgs(int index, T item) {
            this.Index = index;
            this.Item = item;
        }
    }
}
