using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Perseus {
    /// <summary>
    /// Stores information particular to individual SkipStrings.
    /// </summary>
    public class SkipItem {
        #region Property Members
        private string _Start;
        private string[] _End;
        private int _Rank;
        private bool _IgnoreMaxMin;
        private bool _SkipEndString;
        private bool _SkipStartString;
        private string[] _IgnoreStrings;
        #endregion
        #region Constructors
        /// <summary>
        /// SkipInfo constructor.
        /// </summary>
        /// <param name="start">String to start a skip.</param>
        /// <param name="end">String to end a skip.</param>
        /// <param name="rank">Specifies a rank. No further skips will take place until an end skip is found with an equal or higher rank.</param>
        /// <param name="ignoreMaxMin">Specifies whether the skip string should ignore the specified max min skip values.</param>
        public SkipItem(string start, string end, int rank, bool ignoreMaxMin) {
            _Start = start;
            _End = new string[] { end };
            _Rank = rank;
            _IgnoreMaxMin = ignoreMaxMin;
            _SkipStartString = true;
            _SkipEndString = true;
            _IgnoreStrings = new string[0];
        }
        /// <summary>
        /// SkipInfo constructor.
        /// </summary>
        /// <param name="Start">String to start a skip.</param>
        /// <param name="Ends">Strings to end a skip.</param>
        /// <param name="Rank">Specifies a rank. No further skips will take place until an end skip is found with an equal or higher rank.</param>
        /// <param name="IgnoreMaxMin">Specifies whether the skip string should ignore the specified max min skip values.</param>
        public SkipItem(string Start, string[] Ends, int Rank, bool IgnoreMaxMin) {
            _Start = Start;
            _End = Ends;
            _Rank = Rank;
            _IgnoreMaxMin = IgnoreMaxMin;
            _SkipStartString = true;
            _SkipEndString = true;
            _IgnoreStrings = new string[0];
        }
        /// <summary>
        /// Initializes a new instance of the Perseus.SkipInfo class.
        /// </summary>
        /// <param name="Start">String to start a skip.</param>
        /// <param name="End">String to end a skip.</param>
        /// <param name="Rank">Specifies a rank. No further skips will take place until an end skip is found with an equal or higher rank.</param>
        public SkipItem(string Start, string End, int Rank) {
            _Start = Start;
            _End = new string[] { End };
            _Rank = Rank;
            _IgnoreMaxMin = false;
            _SkipStartString = true;
            _SkipEndString = true;
            _IgnoreStrings = new string[0];
        }
        /// <summary>
        /// Initializes a new instance of the Perseus.SkipInfo class.
        /// </summary>
        /// <param name="Start">String to start a skip.</param>
        /// <param name="Ends">Strings to end a skip.</param>
        /// <param name="Rank">Specifies a rank. No further skips will take place until an end skip is found with an equal or higher rank.</param>
        public SkipItem(string Start, string[] Ends, int Rank) {
            _Start = Start;
            _End = Ends;
            _Rank = Rank;
            _IgnoreMaxMin = false;
            _SkipStartString = true;
            _SkipEndString = true;
            _IgnoreStrings = new string[0];
        }
        /// <summary>
        /// Initializes a new instance of the Perseus.SkipInfo class.
        /// </summary>
        /// <param name="Start">String to start a skip.</param>
        /// <param name="End">String to end a skip.</param>
        /// <param name="IgnoreMaxMin">Specifies whether the skip string should ignore the skip strings max min skip values.</param>
        public SkipItem(string Start, string End, bool IgnoreMaxMin) {
            _Start = Start;
            _End = new string[] { End };
            _Rank = 0;
            _IgnoreMaxMin = IgnoreMaxMin;
            _SkipStartString = true;
            _SkipEndString = true;
            _IgnoreStrings = new string[0];
        }
        /// <summary>
        /// Initializes a new instance of the Perseus.SkipInfo class.
        /// </summary>
        /// <param name="Start">String to start a skip.</param>
        /// <param name="Ends">Strings to end a skip.</param>
        /// <param name="IgnoreMaxMin">Specifies whether the skip string should ignore the skip strings max min skip values.</param>
        public SkipItem(string Start, string[] Ends, bool IgnoreMaxMin) {
            _Start = Start;
            _End = Ends;
            _Rank = 0;
            _IgnoreMaxMin = IgnoreMaxMin;
            _SkipEndString = true;
            _IgnoreStrings = new string[0];
        }
        /// <summary>
        /// Initializes a new instance of the Perseus.SkipInfo class.
        /// </summary>
        /// <param name="start">String to start a skip.</param>
        /// <param name="end">String to end a skip.</param>
        /// <param name="skipStartString">Skip the start strings?</param>
        /// <param name="skipEndString">Skip the end string?</param>
        public SkipItem(string start, string end, bool skipStartString, bool skipEndString) {
            _Start = start;
            _End = new string[] { end };
            _Rank = 0;
            _IgnoreMaxMin = false;
            _SkipStartString = skipStartString;
            _SkipEndString = skipEndString;
            _IgnoreStrings = new string[0];
        }
        /// <summary>
        /// Initializes a new instance of the Perseus.SkipInfo class.
        /// </summary>
        /// <param name="start">String to start a skip.</param>
        /// <param name="end">Strings to end a skip.</param>
        /// <param name="skipStartString">Skip the start strings?</param>
        /// <param name="skipEndString">Skip the end string?</param>
        public SkipItem(string start, string[] ends, bool skipStartString, bool skipEndString) {
            _Start = start;
            _End = ends;
            _Rank = 0;
            _IgnoreMaxMin = false;
            _SkipStartString = skipStartString;
            _SkipEndString = skipEndString;
            _IgnoreStrings = new string[0];
        }
        /// <summary>
        /// Initializes a new instance of the Perseus.SkipInfo class.
        /// </summary>
        /// <param name="start">String to start a skip.</param>
        /// <param name="end">String to end a skip.</param>
        /// <param name="rank">Specifies a rank. No further skips will take place until an end skip is found with an equal or higher rank.</param>
        /// <param name="skipStartString">Skip the start strings?</param>
        /// <param name="skipEndString">Skip the end string?</param>
        public SkipItem(string start, string end, int rank, bool skipStartString, bool skipEndString) {
            _Start = start;
            _End = new string[] { end };
            _Rank = rank;
            _IgnoreMaxMin = false;
            _SkipStartString = skipStartString;
            _SkipEndString = skipEndString;
            _IgnoreStrings = new string[0];
        }
        /// <summary>
        /// Initializes a new instance of the Perseus.SkipInfo class.
        /// </summary>
        /// <param name="start">String to start a skip.</param>
        /// <param name="end">Strings to end a skip.</param>
        /// <param name="rank">Specifies a rank. No further skips will take place until an end skip is found with an equal or higher rank.</param>
        /// <param name="skipStartString">Skip the start strings?</param>
        /// <param name="skipEndString">Skip the end string?</param>
        public SkipItem(string start, string[] ends, int rank, bool skipStartString, bool skipEndString) {
            _Start = start;
            _End = ends;
            _Rank = rank;
            _IgnoreMaxMin = false;
            _SkipStartString = skipStartString;
            _SkipEndString = skipEndString;
            _IgnoreStrings = new string[0];
        }
        /// <summary>
        /// Initializes a new instance of the Perseus.SkipInfo class.
        /// </summary>
        /// <param name="start">String to start a skip.</param>
        /// <param name="end">String to end a skip.</param>
        /// <param name="rank">Specifies a rank. No further skips will take place until an end skip is found with an equal or higher rank.</param>
        /// <param name="ignoreMaxMin">Specifies whether the skip string should ignore the skip strings max min skip values.</param>
        /// <param name="skipStartString">Skip the start strings?</param>
        /// <param name="skipEndString">Skip the end string?</param>
        public SkipItem(string start, string end, int rank, bool ignoreMaxMin, bool skipStartString, bool skipEndString) {
            _Start = start;
            _End = new string[] { end };
            _Rank = rank;
            _IgnoreMaxMin = ignoreMaxMin;
            _SkipStartString = skipStartString;
            _SkipEndString = skipEndString;
            _IgnoreStrings = new string[0];
        }
        /// <summary>
        /// Initializes a new instance of the Perseus.SkipInfo class.
        /// </summary>
        /// <param name="start">String to start a skip.</param>
        /// <param name="end">Strings to end a skip.</param>
        /// <param name="rank">Specifies a rank. No further skips will take place until an end skip is found with an equal or higher rank.</param>
        /// <param name="ignoreMaxMin">Specifies whether the skip string should ignore the skip strings max min skip values.</param>
        /// <param name="skipStartString">Skip the start strings?</param>
        /// <param name="skipEndString">Skip the end string?</param>
        public SkipItem(string start, string[] ends, int rank, bool ignoreMaxMin, bool skipStartString, bool skipEndString) {
            _Start = start;
            _End = ends;
            _Rank = rank;
            _IgnoreMaxMin = ignoreMaxMin;
            _SkipStartString = skipStartString;
            _SkipEndString = skipEndString;
            _IgnoreStrings = new string[0];
        }
        /// <summary>
        /// Initializes a new instance of the Perseus.SkipInfo class.
        /// </summary>
        /// <param name="start">String to start a skip.</param>
        /// <param name="end">String to end a skip.</param>
        public SkipItem(string start, string end) {
            _Start = start;
            _End = new string[] { end };
            _Rank = 0;
            _IgnoreMaxMin = false;
            _SkipStartString = true;
            _SkipEndString = true;
            _IgnoreStrings = new string[0];
        }
        /// <summary>
        /// Initializes a new instance of the Perseus.SkipInfo class.
        /// </summary>
        /// <param name="start">String to start a skip.</param>
        /// <param name="ends">Strings to end a skip.</param>
        public SkipItem(string start, string[] ends) {
            _Start = start;
            _End = ends;
            _Rank = 0;
            _IgnoreMaxMin = false;
            _SkipStartString = true;
            _SkipEndString = true;
            _IgnoreStrings = new string[0];
        }
        /// <summary>
        /// Initializes a new instance of the Perseus.SkipInfo class.
        /// </summary>
        public SkipItem() {
            _Start = "";
            _End = new string[0];
            _Rank = 0;
            _IgnoreMaxMin = false;
            _SkipStartString = true;
            _SkipEndString = true;
            _IgnoreStrings = new string[0];
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets or sets the start value of the SkipInfo.
        /// </summary>
        public String Start {
            get { return _Start; }
            set { _Start = value; }
        }
        /// <summary>
        /// Gets or sets the first end value of the SkipInfo.
        /// </summary>
        public String End {
            get { return _End[0]; }
            set { _End[0] = value; }
        }
        /// <summary>
        /// Gets or sets the end values of the SkipInfo.
        /// </summary>
        public String[] Ends {
            get { return _End; }
            set { _End = value; }
        }
        /// <summary>
        /// Gets or sets the rank of this skip info.
        /// </summary>
        public int Rank {
            get { return _Rank; }
            set { _Rank = value; }
        }
        /// <summary>
        /// Gets or sets whether this skip info should ignore the skip strings max min skip values.
        /// </summary>
        public bool IgnoreMaxMin {
            get { return _IgnoreMaxMin; }
            set { _IgnoreMaxMin = value; }
        }
        /// <summary>
        /// Gets or sets whether this skip item should not skip on its start string.
        /// </summary>
        public bool SkipStartString {
            get { return _SkipStartString; }
            set { _SkipStartString = value; }
        }
        /// <summary>
        /// Gets or sets whether this skip item should not skip on its end string.
        /// </summary>
        public bool SkipEndString {
            get { return _SkipEndString; }
            set { _SkipEndString = value; }
        }
        /// <summary>
        /// Gets or sets the the ignore list's values.
        /// </summary>
        public string[] IgnoreStrings {
            get { return _IgnoreStrings; }
            set { _IgnoreStrings = value; }
        }
        #endregion
        #region Methods
        /// <summary>
        /// Checks if the SkipInfo is empty.
        /// </summary>
        /// <returns>Returns true if it is and false if not.</returns>
        public bool IsEmpty() {
            if (_End.Length == 0 && this._Start.Is(string.Empty)) { return true; }
            else { return false; }
        }
        /// <summary>
        /// Adds an end value to the SkipInfo.
        /// </summary>
        /// <param name="end"></param>
        public void AddEnd(string end) {
            PArray.IncrementArrayAndSetValue<string>(ref _End, end);
        }
        /// <summary>
        /// Determines whether the specified System.Object is equal to the current System.Object.
        /// </summary>
        /// <param name="obj">The System.Object to compare with the current System.Object.</param>
        /// <returns>Returns true if it the specified System.Object is equal to the current System.Object and false if not.</returns>
        private new bool Equals(object obj) {
            return base.Equals(obj);
        }
        /// <summary>
        /// Determines whether s1 and s2 is equal to the start and end strings of this SkipInfo.
        /// </summary>
        /// <param name="start">The string to compare with the start string.</param>
        /// <param name="end">The string to compare with the end string.</param>
        /// <returns>Returns true if they do equal and false if not.</returns>
        public bool Equals(string start, string end) {
            if (this._Start.Is(start)) {
                for (int i = 0; i < _End.Length; i++) {
                    if (this._End[i].Is(end)) { return true; }
                }
            }
            return false;
        }
        public int AddIgnoreString(string s) {
            PArray.IncrementArrayAndSetValue<string>(ref _IgnoreStrings, s);
            return _IgnoreStrings.GetUpperBound(0);
        }
        #endregion
    }
}
