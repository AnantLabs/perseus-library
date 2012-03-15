using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Perseus {
    /// <summary>
    /// Class to specify parts of a string to skip over when using them in procedures and functions.
    /// </summary>
    public class SkipStringStream {
        #region Property Variables
        private SkipItem[] _SkipItems; // The skip string to skip over.
        private string[] _IgnoreStrings; // String combinations to ignore. (example \" when a skip string could be ")
        private int _MinSkip; // The min number of times a SkipString has been found before starting to skip.
        private int _MaxSkip; // The max number of times a SkipString will skip before stoping.
        private int _LastSkipLength;
        #endregion
        #region Private Variables
        private int[] n; // The current skip count.
        private int _SkipStartEnd;
        #endregion
        #region Constructors
        public SkipStringStream(SkipItem skipitem) {
            Clear();
            AddSkipItem(skipitem);
        }
        public SkipStringStream(SkipItem[] skipitems) {
            Clear();
            for (int i = 0; i < skipitems.Length; i++) {
                AddSkipItem(skipitems[i]);
            }
        }
        public SkipStringStream() {
            Clear();
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets or sets the the ignore list's values.
        /// </summary>
        public string[] IgnoreStrings {
            get { return this._IgnoreStrings; }
            set { this._IgnoreStrings = value; }
        }
        /// <summary>
        /// Gets or sets the min number of start SkipStrings to find before skipping.
        /// </summary>
        public int MinSkip {
            get { return this._MinSkip; }
            set {
                this._MinSkip = Math.Max(-1, value);
                if (_MinSkip > this._MaxSkip && this._MaxSkip != -1) { this._MaxSkip = this._MinSkip; }
            }
        }
        /// <summary>
        /// Gets or sets the max number of start SkipStrings to find before stop skipping.
        /// </summary>
        public int MaxSkip {
            get { return this._MaxSkip; }
            set {
                this._MaxSkip = Math.Max(-1, value);
                if (_MaxSkip < this._MinSkip && this._MaxSkip != -1) { this._MinSkip = this._MaxSkip; }
            }
        }
        /// <summary>
        /// Gets the <seealso cref="Perseus.SkipItem"/> of the currently skipped string.
        /// </summary>
        public SkipItem CurrentSkip {
            get { return this._SkipItems[n[n.GetUpperBound(0)]]; }
        }
        public int Length {
            get { return n.Length; }
        }
        /// <summary>
        /// Gets all <seealso cref="Perseus.SkipItem"/> of this SkipStringStream.
        /// </summary>
        public SkipItem[] SkipItems {
            get { return this._SkipItems; }
        }
        public int LastSkipLength {
            get { return this._LastSkipLength; }
        }
        #endregion
        #region Public Methods
        public int AddSkipItem(SkipItem skipitem) {
            for (int i = 0; i < this._SkipItems.Length; i++) {
                if (string.Compare(_SkipItems[i].Start, skipitem.Start, StringComparison.Ordinal) == 0) {
                    this._SkipItems[i].AddEnd(skipitem.End);
                    return i;
                }
            }
            PArray.IncrementArrayAndSetValue<SkipItem>(ref this._SkipItems, skipitem);
            return this._SkipItems.GetUpperBound(0);
        }
        public void RemoveSkipItem(int index) {
            PArray.RemoveItem<SkipItem>(ref this._SkipItems, index);
        }
        public void RemoveSkipItem(string start, string end) {            
            // TODO: check all ends, only remove single end or entire if 1 end
            for (int i = this._SkipItems.GetUpperBound(0); i >= 0; i--) {
                if (this._SkipItems[i].Start.Is(start) && this._SkipItems[i].End.Is(end)) {                
                    PArray.RemoveItem<SkipItem>(ref this._SkipItems, i);
                }
            }
        }
        public int AddIgnoreString(string s) {
            PArray.IncrementArrayAndSetValue<string>(ref this._IgnoreStrings, s);
            return this._IgnoreStrings.GetUpperBound(0);
        }
        public void RemoveIgnoreString(int index) {
            PArray.RemoveItem<string>(ref this._IgnoreStrings, index);
        }
        /// <summary>
        /// Resets the current skip item count to 0.
        /// </summary>        
        public void Reset() {
            n = new int[0];
            this._SkipStartEnd = -1;
            this._LastSkipLength = 1;
        }
        /// <summary>
        /// Clears out all SkipString information.
        /// </summary>
        public void Clear() {
            this._SkipItems = new SkipItem[0];
            this._IgnoreStrings = new string[0];
            this._MinSkip = -1;
            this._MaxSkip = -1;
            this._LastSkipLength = 1;
            n = new int[0];
            this._SkipStartEnd = -1;
        }
        /// <summary>
        /// Check if a position in a string should be skipped.
        /// </summary>
        /// <param name="s">The string to check.</param>
        /// <param name="index">The position in the string.</param>
        /// <returns>Returns true if it should be skipped and false if not.</returns>
        public bool Skip(string s, int index) {
            if (_SkipItems.Length == 0) { return false; }

            if (!IsIgnore(s, index)) {

                int endindex;
                // If the string at the index is equal to the end string of the last skip string.
                if (n.Length > 0 && FindSkipItemEndIndex(out endindex, s, index, n[n.GetUpperBound(0)]) >= 0) {
                    this._LastSkipLength = this._SkipItems[n[n.GetUpperBound(0)]].Ends[endindex].Length;

                    if (IsMinOrEmpty()) {
                        PArray.DecrementArray<int>(ref n);
                        return false;
                    }
                    else if (_SkipStartEnd == n.Length && this._SkipItems[n[n.GetUpperBound(0)]].SkipEndString == false) {
                        this._SkipStartEnd = -1;
                        PArray.DecrementArray<int>(ref n);
                        return false;
                    }
                    else {
                        PArray.DecrementArray<int>(ref n);
                        return true;
                    }
                }
                else {
                    int itemindex = FindSkipItemIndex(s, index);
                    if (itemindex >= 0 && IsHighRank(itemindex) && !IsMax(itemindex)) {
                        this._LastSkipLength = this._SkipItems[itemindex].Start.Length;
                        PArray.IncrementArrayAndSetValue<int>(ref n, itemindex);
                        //potential bug where if there is ignore strings n.length wont be 1 and still the first!
                        if (IsMinOrEmpty()) {
                            return false;
                        }
                        else if (_SkipStartEnd == -1 && this._SkipItems[n[n.GetUpperBound(0)]].SkipStartString == false) {
                            this._SkipStartEnd = n.Length;
                            return false;
                        }
                        else if (_SkipStartEnd == -1 && this._SkipItems[n[n.GetUpperBound(0)]].SkipEndString == false) {
                            this._SkipStartEnd = n.Length;
                            return true;
                        }
                        else {
                            return true;
                        }
                    }
                }
            }
            else {
                return true;
            }

            this._LastSkipLength = 1;
            if (IsMinOrEmpty()) {
                return false;
            }
            else {
                return true;
            }
        }
        public bool Ignore(string s, int index) {
            return IsIgnore(s, index);
        }
        #endregion
        #region Private Methods
        private int FindSkipItemIndex(string s, int index) {
            int item = -1;
            for (int i = 0; i < this._SkipItems.Length; i++) {
                if (string.Compare(_SkipItems[i].Start,
                    PString.SafeSubstring(s, index, this._SkipItems[i].Start.Length),
                    StringComparison.Ordinal) == 0) {
                    if (item == -1 || this._SkipItems[item].Start.Length < this._SkipItems[i].Start.Length) {
                        item = i;
                    }
                }
            }
            return item;
        }
        private int FindSkipItemEndIndex(out int endindex, string s, int index, int skipitemindex) {
            int end = -1;
            for (int i = 0; i < this._SkipItems[skipitemindex].Ends.Length; i++) {
                if (string.Compare(_SkipItems[skipitemindex].Ends[i],
                    PString.SafeSubstring(s, index, this._SkipItems[skipitemindex].Ends[i].Length),
                    StringComparison.Ordinal) == 0) {
                    if (end == -1 || this._SkipItems[skipitemindex].Ends[end].Length < this._SkipItems[skipitemindex].Ends[i].Length) {
                        end = i;
                    }
                }
            }
            endindex = end;
            return end;
        }
        private bool IsIgnore(string s, int index) {
            for (int i = 0; i < this._IgnoreStrings.Length; i++) {
                for (int j = Math.Max(0, index - this._IgnoreStrings[i].Length + 1); j <= index; j++) {
                    if (string.Compare(PString.SafeSubstring(s, j, this._IgnoreStrings[i].Length),
                        this._IgnoreStrings[i],
                        StringComparison.Ordinal) == 0) {
                        return true;
                    }
                }
            }
            if (n.Length > 0 && CurrentSkip.IgnoreStrings.Length > 0) {
                for (int i = 0; i < CurrentSkip.IgnoreStrings.Length; i++) {
                    for (int j = Math.Max(0, index - CurrentSkip.IgnoreStrings[i].Length + 1); j <= index; j++) {
                        if (string.Compare(PString.SafeSubstring(s, j, CurrentSkip.IgnoreStrings[i].Length),
                            CurrentSkip.IgnoreStrings[i],
                            StringComparison.Ordinal) == 0) {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        private bool IsHighRank(int index) {
            if (n.Length == 0 || this._SkipItems[index].Rank >= this._SkipItems[n[n.GetUpperBound(0)]].Rank) {
                return true;
            }
            else {
                return false;
            }
        }
        private bool IsMax(int index) { // n.Length < vMaxSkip because current skip hasn't been added yet!
            if (_MaxSkip == -1) { return false; }
            else {
                int cnt = 0;
                for (int i = 0; i < n.Length; i++) { // Remove ignore strings from the counting!
                    if (!_SkipItems[n[i]].IgnoreMaxMin) { cnt++; }
                }
                if (!_SkipItems[index].IgnoreMaxMin) { cnt++; }

                return (cnt > this._MaxSkip);
            }
        }
        private bool IsMinOrEmpty() {
            if (n.Length == 0) { return true; }
            else if (_MinSkip == -1 || this._SkipItems[n[n.GetUpperBound(0)]].IgnoreMaxMin) { return false; }
            else {
                int cnt = this._MinSkip;
                int len = Math.Min(_MinSkip, n.Length);
                for (int i = 0; i < len; i++) { // Remove ignore strings from the counting!
                    if (_SkipItems[n[i]].IgnoreMaxMin) { cnt = i; break; } // Once ignore max min is found, it is past min
                }
                return (n.Length <= cnt);
            }
        }
        #endregion
    }
}
