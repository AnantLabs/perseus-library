using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Perseus {
    public class PArray {
        public static void ResizeArrayTo<T>(ref T[] a, int size) {
            Array.Resize<T>(ref a, size);
        }
        public static void IncrementArray<T>(ref T[] a) {
            if (a == null) {
                a = new T[1];
            }
            else {
                Array.Resize<T>(ref a, a.Length + 1);
            }
        }
        public static void IncrementArrayAndSetValue<T>(ref T[] a, T value) {
            if (a == null) {
                a = new T[1];
                a[0] = value;
            }
            else {
                Array.Resize<T>(ref a, a.Length + 1);
                a[a.GetUpperBound(0)] = value;
            }
        }
        public static void IncrementArray<T>(ref T[] a, int amount) {
            if (a == null) {
                a = new T[amount];
            }
            else {
                Array.Resize<T>(ref a, a.Length + amount);
            }
        }
        public static void DecrementArray<T>(ref T[] a) {
            if (a.Length == 0) {
                throw new IndexOutOfRangeException();
            }
            else if (a.Length == 1) {
                if (a[0] is IDisposable) {
                    ((IDisposable)a[0]).Dispose();
                }
                a = new T[0];
            }
            else {
                Array.Resize<T>(ref a, a.GetUpperBound(0));
            }
        }
        public static void RemoveItem<T>(ref T[] a, int index) {
            if (a.Length == 0) {
                throw new IndexOutOfRangeException();
            }
            else if (a.Length == 1) {
                if (a[0] is IDisposable) {
                    ((IDisposable)a[0]).Dispose();
                }
                a = new T[0];
            }
            else {
                T[] tmp = new T[a.GetUpperBound(0)];
                for (int i = 0; i < a.GetUpperBound(0); i++) {
                    if (i < index)
                        tmp[i] = a[i];
                    else if (i == index) {
                        if (a[0] is IDisposable) {
                            ((IDisposable)a[0]).Dispose();
                        }
                        tmp[i] = a[i + 1];
                    }
                    else if (i > index)
                        tmp[i] = a[i + 1];
                }
                a = tmp;
            }
        }
        public static void InsertItem<T>(ref T[] a, int index) {
            if (a == null) {
                a = new T[1];
            }
            else {
                T[] tmp = new T[Math.Max(a.Length + 1, index + 1)];
                int len = Math.Min(tmp.GetUpperBound(0), a.GetUpperBound(0)) + 1;
                for (int i = 0; i <= len; i++) {
                    if (i < index)
                        tmp[i] = a[i];
                    else if (i > index)
                        tmp[i] = a[i - 1];
                }
                a = tmp;
            }
        }
        /// <summary>
        /// Removes all items in the referenced array from a start index for a number of times.
        /// </summary>
        /// <param name="a">The array to remove items from.</param>
        /// <param name="index">The start index from which to start removing the items from.</param>
        /// <param name="Length">How many items to remove.</param>
        public static void RemoveItems<T>(ref T[] a, int index, int length) {
            if (index + length > a.GetUpperBound(0) || index < 0) {
                throw new IndexOutOfRangeException();
            }

            T[] tmp = new T[a.Length - length];
            for (int i = 0; i <= a.GetUpperBound(0) - length; i++) {
                if (i < index)
                    tmp[i] = a[i];
                else if (i >= index && i < index + length) {
                    if (a[0] is IDisposable) {
                        ((IDisposable)a[0]).Dispose();
                    }
                    tmp[i] = a[i + length];
                }
                else if (i > index + length)
                    tmp[i] = a[i + length];
            }
            a = tmp;
        }
        public static void Concat<T>(ref T[] s1, T[] s2) {
            if (s2 == null) { return; }
            else if (s1 == null) {
                s1 = s2;
            }
            else {
                int olen = s1.Length;
                PArray.ResizeArrayTo<T>(ref s1, s1.Length + s2.Length);
                for (int i = olen; i < s1.Length; i++) {
                    s1[i] = s2[i - olen];
                }
            }
        }
    }
}
