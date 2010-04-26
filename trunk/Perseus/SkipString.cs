using System;
using System.Collections.Generic;
using System.Drawing;

namespace Perseus {
    public class SkipString {
        #region Property Variables
        private SkipItem[] _SkipItems; // The information of the strings to skip over.
        private string[] _IgnoreStrings; // String combinations to ignore. (example \" when a skip string could be ")        
        private int _MinSkip; // The min number of times a SkipString has been found before starting to skip.
        private int _MaxSkip; // The max number of times a SkipString will skip before stoping.
        private string _CheckString; // The string to check for skips;
        private int[] _InnerIgnoreStrings;
        #endregion
        #region Private Variables
        private Point[] sp;
        private bool _IsInit;
        #endregion
        #region Constructors
        public SkipString() {
            Clear();
        }
        public SkipString(string checkstring) {
            SetupSkipStrng(checkstring);
        }
        public SkipString(SkipItem skipitem) {
            SetupSkipStrng(string.Empty);
            AddSkipItem(skipitem);
        }
        public SkipString(SkipItem[] skipitems) {
            SetupSkipStrng(string.Empty);
            for (int i = 0; i < skipitems.Length; i++) {
                AddSkipItem(skipitems[i]);
            }
        }
        public SkipString(string checkstring, SkipItem skipitem) {
            SetupSkipStrng(checkstring);
            AddSkipItem(skipitem);
        }
        public SkipString(string checkstring, SkipItem[] skipitems) {
            SetupSkipStrng(checkstring);
            for (int i = 0; i < skipitems.Length; i++) {
                AddSkipItem(skipitems[i]);
            }
        }
        private void SetupSkipStrng(string check) {
            this._CheckString = check;
            this._SkipItems = new SkipItem[0];
            this._IgnoreStrings = new string[0];
            this._InnerIgnoreStrings = new int[0];
            this._MinSkip = -1;
            this._MaxSkip = -1;
            this._IsInit = false;
        }
        #endregion
        #region Properties
        public string[] IgnoreStrings {
            get { return this._IgnoreStrings; }
            set { this._IgnoreStrings = value; this._IsInit = false; }
        }
        public int MinSkip {
            get { return this._MinSkip; }
            set {
                this._MinSkip = Math.Max(-1, value);
                if (_MinSkip > this._MaxSkip && this._MaxSkip != -1) { this._MaxSkip = this._MinSkip; }
                this._IsInit = false;
            }
        }
        public int MaxSkip {
            get { return this._MaxSkip; }
            set {
                this._MaxSkip = Math.Max(-1, value);
                if (_MaxSkip < this._MinSkip && this._MaxSkip != -1) { this._MinSkip = this._MaxSkip; }
                this._IsInit = false;
            }
        }
        public SkipItem[] SkipItems {
            get { return this._SkipItems; }
            set {
                this._SkipItems = value;
                this._IsInit = false;
            }
        }
        public string CheckString {
            get { return this._CheckString; }
            set { this._CheckString = value; this._IsInit = false; }
        }
        #endregion
        #region Public Methods
        public int AddSkipItem(SkipItem skipitem) {
            for (int i = 0; i < this._SkipItems.Length; i++) {
                if (_SkipItems[i].Start == skipitem.Start) {
                    this._SkipItems[i].AddEnd(skipitem.End);
                    this._IsInit = false;
                    return i;
                }
            }
            PArray.IncrementArrayAndSetValue<SkipItem>(ref this._SkipItems, skipitem);
            this._IsInit = false;
            return this._SkipItems.GetUpperBound(0);
        }
        public void RemoveSkipItem(int index) {
            PArray.RemoveItem<SkipItem>(ref this._SkipItems, index);
            this._IsInit = false;
        }
        public void RemoveSkipItem(string start, string end) {
            for (int i = this._SkipItems.GetUpperBound(0); i >= 0; i--) {
                if (_SkipItems[i].Start == start && this._SkipItems[i].Start == end) {
                    PArray.RemoveItem<SkipItem>(ref this._SkipItems, i);
                    this._IsInit = false;
                }
            }
        }
        public int AddIgnoreString(string ignorestring) {
            PArray.IncrementArrayAndSetValue<string>(ref this._IgnoreStrings, ignorestring);
            this._IsInit = false;
            return this._IgnoreStrings.GetUpperBound(0);
        }
        public void RemoveIgnoreString(int index) {
            PArray.RemoveItem<string>(ref this._IgnoreStrings, index);
            this._IsInit = false;
        }
        public void Clear() {
            this._SkipItems = new SkipItem[0];
            this._IgnoreStrings = new string[0];
            this._InnerIgnoreStrings = new int[0];
            this._MinSkip = -1;
            this._MaxSkip = -1;
            this._CheckString = string.Empty;
            this.sp = new Point[0];
            this._IsInit = false;
        }

        public bool Skip(int index) {
            if (!_IsInit) { Initialize(); }
            for (int i = 0; i <= sp.GetUpperBound(0); i++) {
                if (index >= sp[i].X && index <= sp[i].Y) { return true; }
            }
            return false;
        }
        public bool Ignore(int index) {
            if (!_IsInit) { Initialize(); }
            if (IsIgnore(index, new int[0])) {
                return true;
            }
            foreach (int i in this._InnerIgnoreStrings) {
                if (i == index) { return true; }
            }
            return false;
        }

        public string GetSkippedString {
            get {
                if (!_IsInit) { Initialize(); }
                string s = string.Empty;
                for (int i = 0; i < sp.Length; i++) {
                    s += PString.Cut(_CheckString, sp[i].X, sp[i].Y);
                }
                return s;
            }
        }
        public string GetNonSkippedString {
            get {
                if (!_IsInit) { Initialize(); }

                string s = string.Empty;
                int start = 0;

                for (int i = 0; i < sp.Length; i++) {
                    s += PString.Cut(_CheckString, start, sp[i].X - 1);
                    start = sp[i].Y + 1;
                }
                s += PString.Cut(_CheckString, start, this._CheckString.Length - 1);

                return s;
            }
        }
        #endregion
        #region Private Methods
        private void Initialize() {
            sp = new Point[0];

            if (_SkipItems.Length == 0) { return; }

            int[] n = new int[0];
            int tmpmin = this._MinSkip;

            for (int i = 0; i < this._CheckString.Length; i++) {
                if (!IsIgnore(i, n)) {

                    int endindex;
                    if (n.Length > 0 && FindSkipItemEndIndex(out endindex, i, n[n.GetUpperBound(0)]) >= 0) {

                        if (n.Length <= tmpmin && sp.Length > 0 && sp[sp.GetUpperBound(0)].Y == -1) {
                            // Check for skip end string inclusion
                            if (_SkipItems[n[n.GetUpperBound(0)]].SkipEndString == false) {
                                sp[sp.GetUpperBound(0)].Y = i - 1;
                            }
                            else {
                                sp[sp.GetUpperBound(0)].Y = i + this._SkipItems[n[n.GetUpperBound(0)]].Ends[endindex].Length - 1;
                            }
                            tmpmin = this._MinSkip;
                        }

                        PArray.DecrementArray<int>(ref n);
                    }
                    else {
                        int itemindex = FindSkipItemIndex(i);
                        if (itemindex >= 0 && IsHighRank(n, itemindex) && !IsMax(n, itemindex)) {
                            PArray.IncrementArrayAndSetValue<int>(ref n, itemindex);

                            if (!IsMin(n) && (sp.Length == 0 || sp[sp.GetUpperBound(0)].Y >= 0)) {
                                tmpmin = n.Length;

                                PArray.IncrementArray<Point>(ref sp);
                                if (_SkipItems[itemindex].SkipStartString == false) {
                                    sp[sp.GetUpperBound(0)] = new Point(i + this._SkipItems[n[0]].Start.Length, -1);
                                }
                                else {
                                    sp[sp.GetUpperBound(0)] = new Point(i, -1);
                                }
                            }

                            // Subtract 1 because of i++
                            i += this._SkipItems[itemindex].Start.Length - 1;
                        }
                    }
                }
            }

            if (sp.Length > 0 && sp[sp.GetUpperBound(0)].Y == -1) { sp[sp.GetUpperBound(0)].Y = this._CheckString.Length - 1; }
            this._IsInit = true;
        }
        private bool IsIgnore(int index, int[] skipIndex) {
            for (int i = 0; i <= this._IgnoreStrings.GetUpperBound(0); i++) {
                for (int j = Math.Max(0, index - this._IgnoreStrings[i].Length + 1); j <= index; j++) {
                    if (PString.SafeSubstring(_CheckString, j, this._IgnoreStrings[i].Length) == this._IgnoreStrings[i]) {
                        return true;
                    }
                }
            }
            if (skipIndex.Length > 0 && this._SkipItems[skipIndex[skipIndex.GetUpperBound(0)]].IgnoreStrings.Length > 0) {
                for (int i = 0; i < this._SkipItems[skipIndex[skipIndex.GetUpperBound(0)]].IgnoreStrings.Length; i++) {
                    for (int j = Math.Max(0, index - this._SkipItems[skipIndex[skipIndex.GetUpperBound(0)]].IgnoreStrings[i].Length + 1); j <= index; j++) {
                        if (PString.SafeSubstring(_CheckString, j, this._SkipItems[skipIndex[skipIndex.GetUpperBound(0)]].IgnoreStrings[i].Length) == this._SkipItems[skipIndex[skipIndex.GetUpperBound(0)]].IgnoreStrings[i]) {
                            PArray.IncrementArrayAndSetValue(ref this._InnerIgnoreStrings, index);
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        private int FindSkipItemIndex(int index) {
            int item = -1;
            for (int i = 0; i < this._SkipItems.Length; i++) {
                if (_SkipItems[i].Start == PString.SafeSubstring(_CheckString, index, this._SkipItems[i].Start.Length)) {
                    if (item == -1 || this._SkipItems[item].Start.Length < this._SkipItems[i].Start.Length) {
                        item = i;
                    }
                }
            }
            return item;
        }
        private int FindSkipItemEndIndex(out int endindex, int index, int skipitemindex) {
            int end = -1;
            for (int i = 0; i < this._SkipItems[skipitemindex].Ends.Length; i++) {
                if (_SkipItems[skipitemindex].Ends[i] == PString.SafeSubstring(_CheckString, index, this._SkipItems[skipitemindex].Ends[i].Length)) {
                    if (end == -1 || this._SkipItems[skipitemindex].Ends[end].Length < this._SkipItems[skipitemindex].Ends[i].Length) {
                        end = i;
                    }
                }
            }
            endindex = end;
            return end;
        }
        private bool IsHighRank(int[] n, int index) {
            if (n.Length == 0 || this._SkipItems[index].Rank >= this._SkipItems[n[n.GetUpperBound(0)]].Rank) {
                return true;
            }
            else {
                return false;
            }
        }
        private bool IsMax(int[] n, int index) {
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
        private bool IsMin(int[] n) {
            if (_MinSkip == -1 || this._SkipItems[n[n.GetUpperBound(0)]].IgnoreMaxMin) { return false; }
            else { return (n.Length <= this._MinSkip); }
        }
        #endregion
    }
}
