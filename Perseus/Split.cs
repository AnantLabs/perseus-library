using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Perseus {
    public static partial class PString {        
        #region Single Separator
        /// <summary>
        /// Returns a <seealso cref="System.String"/> array containing the substrings in this string that are delimited by
        /// the specified <seealso cref="System.String"/>.
        /// </summary>
        /// <param name="s">The string to split.</param>
        /// <param name="Separator">A string that delimits the substrings in this string.</param>
        /// <returns>An array whose elements contain the substrings in this string that are delimited
        /// by the strings separator.</returns>
        public static string[] Split(this string s, string separator) {
            return s.Split(new string[] { separator }, StringSplitOptions.None);
        }
        /// <summary>
        /// Returns a <seealso cref="System.String"/> array containing the substrings in this string that are delimited by
        /// the specified <seealso cref="System.String"/>.
        /// </summary>
        /// <param name="s">The string to split.</param>
        /// <param name="Separator">A string that delimits the substrings in this string.</param>
        /// <param name="Count">The maximum number of substrings to return.</param>
        /// <returns>An array whose elements contain the substrings in this string that are delimited
        /// by the strings separator.</returns>
        public static string[] Split(this string s, string separator, int count) {
            return s.Split(new string[] { separator }, count, StringSplitOptions.None);
        }
        /// <summary>
        /// Returns a <seealso cref="System.String"/> array containing the substrings in this string that are delimited by
        /// the specified <seealso cref="System.String"/>.
        /// </summary>
        /// <param name="s">The string to split</param>
        /// <param name="Separator">A string that delimits the substrings in this string.</param>
        /// <param name="Skip">The <seealso cref="Perseus.SkipString"/> to define what parts of the string to skip over when splitting.</param>
        /// <returns>An array whose elements contain the substrings in this string that are delimited
        /// by the strings separator.</returns>
        public static string[] Split(this string s, string separator, SkipStringStream skip) {
            return SplitBasic(s, new string[] { separator }, skip);
        }
        /// <summary>
        /// Returns a <seealso cref="System.String"/> array containing the substrings in this string that are delimited by
        /// the specified <seealso cref="System.String"/>.
        /// </summary>
        /// <param name="s">The string to split.</param>
        /// <param name="Separator">A string that delimits the substrings in this string.</param>
        /// <param name="Options">Specifies the <seealso cref="Perseus.SplitOptions"/> to 
        /// use when splitting the string.</param>
        /// <returns>An array whose elements contain the substrings in this string that are delimited
        /// by the strings separator.</returns>
        public static string[] Split(this string s, string separator, SplitOptions options) {
            return SplitOptionsBase(s, new string[] { separator }, options);
        }
        /// <summary>
        /// Returns a <seealso cref="System.String"/> array containing the substrings in this string that are delimited by
        /// the specified <seealso cref="System.String"/>.
        /// </summary>
        /// <param name="s">The string to split.</param>
        /// <param name="Separator">A string that delimits the substrings in this string.</param>
        /// <param name="Count">The maximum number of substrings to return.</param>
        /// <param name="Skip">The <seealso cref="Perseus.SkipString"/> to define what parts of the string to skip over when splitting.</param>
        /// <returns>An array whose elements contain the substrings in this string that are delimited
        /// by the strings separator.</returns>
        public static string[] Split(this string s, string separator, int count, SkipStringStream skip) {
            return SplitBasic(s, new string[] { separator }, count, skip);
        }
        /// <summary>
        /// Returns a <seealso cref="System.String"/> array containing the substrings in this string that are delimited by
        /// the specified <seealso cref="System.String"/>.
        /// </summary>
        /// <param name="s">The string to split.</param>
        /// <param name="Separator">A string that delimits the substrings in this string.</param>
        /// <param name="Count">The maximum number of substrings to return.</param>
        /// <param name="Options">Specifies the <seealso cref="Perseus.SplitOptions"/> to 
        /// use when splitting the string.</param>
        /// <returns>An array whose elements contain the substrings in this string that are delimited
        /// by the strings separator.</returns>
        public static string[] Split(this string s, string separator, int count, SplitOptions options) {
            return SplitOptionsCount(s, new string[] { separator }, count, options);
        }
        /// <summary>
        /// Returns a <seealso cref="System.String"/> array containing the substrings in this string that are delimited by
        /// the specified <seealso cref="System.String"/>.
        /// </summary>
        /// <param name="s">The string to split.</param>
        /// <param name="Separator">A string that delimits the substrings in this string.</param>
        /// <param name="Skip">The <seealso cref="Perseus.SkipString"/> to define what parts of the string to skip over when splitting.</param>
        /// <param name="Options">Specifies the <seealso cref="Perseus.SplitOptions"/> to 
        /// use when splitting the string.</param>
        /// <returns>An array whose elements contain the substrings in this string that are delimited
        /// by the strings separator.</returns>
        public static string[] Split(this string s, string separator, SkipStringStream skip, SplitOptions options) {
            return SplitOptionsSkip(s, new string[] { separator }, skip, options);
        }
        /// <summary>
        /// Returns a <seealso cref="System.String"/> array containing the substrings in this string that are delimited by
        /// the specified <seealso cref="System.String"/>.
        /// </summary>
        /// <param name="s">The string to split.</param>
        /// <param name="Separator">A string that delimits the substrings in this string.</param>
        /// <param name="Count">The maximum number of substrings to return.</param>
        /// <param name="Skip">The <seealso cref="Perseus.SkipString"/> to define what parts of the string to skip over when splitting.</param>
        /// <param name="Options">Specifies the <seealso cref="Perseus.SplitOptions"/> to 
        /// use when splitting the string.</param>
        /// <returns>An array whose elements contain the substrings in this string that are delimited
        /// by the strings separator.</returns>
        public static string[] Split(this string s, string separator, int count, SkipStringStream skip, SplitOptions options) {
            return SplitOptionsCountSkip(s, new string[] { separator }, count, skip, options);
        }
        #endregion
        #region Multi Separator
        /// <summary>
        /// Returns a <seealso cref="System.String"/> array containing the substrings in this string that are delimited by
        /// elements of a specified <seealso cref="System.String"/> array.
        /// </summary>
        /// <param name="s">The string to split.</param>
        /// <param name="Separator">An array of strings that delimit the substrings in this string, an empty
        /// array containing no delimiters, or null.</param>
        /// <returns>An array whose elements contain the substrings in this string that are delimited
        /// by one or more strings in separator.</returns>
        public static string[] Split(this string s, string[] separator) {
            return s.Split(separator, StringSplitOptions.None);
        }
        /// <summary>
        /// Returns a <seealso cref="System.String"/> array containing the substrings in this string that are delimited by
        /// elements of a specified System.String array.
        /// </summary>
        /// <param name="s">The string to split.</param>
        /// <param name="Separator">An array of strings that delimit the substrings in this string, an empty
        /// array containing no delimiters, or null.</param>
        /// <param name="Count">The maximum number of substrings to return.</param>
        /// <returns>An array whose elements contain the substrings in this string that are delimited
        /// by one or more strings in separator.</returns>
        public static string[] Split(this string s, string[] separator, int count) {
            return s.Split(separator, count, StringSplitOptions.None);
        }        
        /// <summary>
        /// Returns a <seealso cref="System.String"/> array containing the substrings in this string that are delimited by
        /// elements of a specified <seealso cref="System.String"/> array.
        /// </summary>
        /// <param name="s">The string to split.</param>
        /// <param name="Separator">An array of strings that delimit the substrings in this string, an empty
        /// array containing no delimiters, or null.</param>
        /// <param name="Skip">The <seealso cref="Perseus.SkipString"/> to define what parts of the string to skip over when splitting.</param>
        /// <returns>An array whose elements contain the substrings in this string that are delimited
        /// by one or more strings in separator.</returns>
        public static string[] Split(this string s, string[] separator, SkipStringStream skip) {
            return SplitBasic(s, separator, skip);
        }
        /// <summary>
        /// Returns a <seealso cref="System.String"/> array containing the substrings in this string that are delimited by
        /// elements of a specified <seealso cref="System.String"/> array.
        /// </summary>
        /// <param name="s">The string to split.</param>
        /// <param name="Separator">An array of strings that delimit the substrings in this string, an empty
        /// array containing no delimiters, or null.</param>
        /// <param name="Options">Specifies the <seealso cref="Perseus.SplitOptions"/> to 
        /// use when splitting the string.</param>
        /// <returns>An array whose elements contain the substrings in this string that are delimited
        /// by one or more strings in separator.</returns>
        public static string[] Split(this string s, string[] separator, SplitOptions options) {
            return SplitOptionsBase(s, separator, options);
        }
        /// <summary>
        /// Returns a <seealso cref="System.String"/> array containing the substrings in this string that are delimited by
        /// elements of a specified <seealso cref="System.String"/> array.
        /// </summary>
        /// <param name="s">The string to split.</param>
        /// <param name="Separator">An array of strings that delimit the substrings in this string, an empty
        /// array containing no delimiters, or null.</param>
        /// <param name="Count">The maximum number of substrings to return.</param>
        /// <param name="Skip">The <seealso cref="Perseus.SkipString"/> to define what parts of the string to skip over when splitting.</param>
        /// <returns>An array whose elements contain the substrings in this string that are delimited
        /// by one or more strings in separator.</returns>
        public static string[] Split(this string s, string[] separator, int count, SkipStringStream Skip) {
            return SplitBasic(s, separator, count, Skip);
        }
        /// <summary>
        /// Returns a <seealso cref="System.String"/> array containing the substrings in this string that are delimited by
        /// elements of a specified System.String array.
        /// </summary>
        /// <param name="s">The string to split.</param>
        /// <param name="Separator">An array of strings that delimit the substrings in this string, an empty
        /// array containing no delimiters, or null.</param>
        /// <param name="Count">The maximum number of substrings to return.</param>
        /// <param name="Options">Specifies the <seealso cref="Perseus.SplitOptions"/> to 
        /// use when splitting the string.</param>
        /// <returns>An array whose elements contain the substrings in this string that are delimited
        /// by one or more strings in separator.</returns>
        public static string[] Split(this string s, string[] separator, int count, SplitOptions options) {
            return SplitOptionsCount(s, separator, count, options);
        }
        /// <summary>
        /// Returns a <seealso cref="System.String"/> array containing the substrings in this string that are delimited by
        /// elements of a specified <seealso cref="System.String"/> array.
        /// </summary>
        /// <param name="s">The string to split.</param>
        /// <param name="Separator">An array of strings that delimit the substrings in this string, an empty
        /// array containing no delimiters, or null.</param>
        /// <param name="Skip">The <seealso cref="Perseus.SkipString"/> to define what parts of the string to skip over when splitting.</param>
        /// <param name="Options">Specifies the <seealso cref="Perseus.SplitOptions"/> to 
        /// use when splitting the string.</param>
        /// <returns>An array whose elements contain the substrings in this string that are delimited
        /// by one or more strings in separator.</returns>
        public static string[] Split(this string s, string[] separator, SkipStringStream skip, SplitOptions options) {
            return SplitOptionsSkip(s, separator, skip, options);
        }
        /// <summary>
        /// Returns a <seealso cref="System.String"/> array containing the substrings in this string that are delimited by
        /// elements of a specified <seealso cref="System.String"/> array.
        /// </summary>
        /// <param name="s">The string to split.</param>
        /// <param name="Separator">An array of strings that delimit the substrings in this string, an empty
        /// array containing no delimiters, or null.</param>
        /// <param name="Count">The maximum number of substrings to return.</param>
        /// <param name="Skip">The <seealso cref="Perseus.SkipString"/> to define what parts of the string to skip over when splitting.</param>
        /// <param name="Options">Specifies the <seealso cref="Perseus.SplitOptions"/> to 
        /// use when splitting the string.</param>
        /// <returns>An array whose elements contain the substrings in this string that are delimited
        /// by one or more strings in separator.</returns>
        public static string[] Split(this string s, string[] separator, int count, SkipStringStream skip, SplitOptions options) {
            return SplitOptionsCountSkip(s, separator, count, skip, options);
        }
        #endregion

        #region Split Options
        private static string[] SplitOptionsCountSkip(string s, string[] Separator, int Count, SkipStringStream Skip, SplitOptions Options) {
            bool Trim = ((Options | SplitOptions.Trim) == Options);

            if (IsEmpty(s, Trim, Options)) {
                return new string[0];
            }

            bool Remove = ((Options | SplitOptions.RemoveEmptyEntries) == Options);

            if (Bit.IsIn(Options, SplitOptions.AddDelimiterToArray)) {
                if (Trim && Remove) {
                    return SplitTrimAddDelimiterRemoveEmpty(s, Separator, Count, Skip);
                }
                else if (Trim) {
                    return SplitTrimAddDelimiter(s, Separator, Count, Skip);
                }
                else if (Remove) {
                    return SplitAddDelimiterRemoveEmpty(s, Separator, Count, Skip);
                }
                else {
                    return SplitAddDelimiter(s, Separator, Count, Skip);
                }
            }
            else if (Bit.IsIn(Options, SplitOptions.KeepDelimiter)) {
                if (Trim && Remove) {
                    return SplitTrimKeepDelimiterRemoveEmpty(s, Separator, Count, Skip);
                }
                else if (Trim) {
                    return SplitTrimKeepDelimiter(s, Separator, Count, Skip);
                }
                else if (Remove) {
                    return SplitKeepDelimiterRemoveEmpty(s, Separator, Count, Skip);
                }
                else {
                    return SplitKeepDelimiter(s, Separator, Count, Skip);
                }
            }
            else {
                if (Trim && Remove) {
                    return SplitTrimRemoveEmpty(s, Separator, Count, Skip);
                }
                else if (Trim) {
                    return SplitTrim(s, Separator, Count, Skip);
                }
                else if (Remove) {
                    return SplitRemoveEmpty(s, Separator, Count, Skip);
                }
                else {
                    return SplitBasic(s, Separator, Count, Skip);
                }
            }
        }
        private static string[] SplitOptionsCount(string s, string[] Separator, int Count, SplitOptions Options) {
            bool Trim = ((Options | SplitOptions.Trim) == Options);

            if (IsEmpty(s, Trim, Options)) {
                return new string[0];
            }

            bool Remove = ((Options | SplitOptions.RemoveEmptyEntries) == Options);

            if (Bit.IsIn(Options, SplitOptions.AddDelimiterToArray)) {
                if (Trim && Remove) {
                    return SplitTrimAddDelimiterRemoveEmpty(s, Separator, Count);
                }
                else if (Trim) {
                    return SplitTrimAddDelimiter(s, Separator, Count);
                }
                else if (Remove) {
                    return SplitAddDelimiterRemoveEmpty(s, Separator, Count);
                }
                else {
                    return SplitAddDelimiter(s, Separator, Count);
                }
            }
            else if (Bit.IsIn(Options, SplitOptions.KeepDelimiter)) {
                if (Trim && Remove) {
                    return SplitTrimKeepDelimiterRemoveEmpty(s, Separator, Count);
                }
                else if (Trim) {
                    return SplitTrimKeepDelimiter(s, Separator, Count);
                }
                else if (Remove) {
                    return SplitKeepDelimiterRemoveEmpty(s, Separator, Count);
                }
                else {
                    return SplitKeepDelimiter(s, Separator, Count);
                }
            }
            else {
                if (Trim) {
                    StringSplitOptions sso = ToSplitOptions(Options);
                    string[] tmp = s.Split(Separator, Count, sso);
                    if ((sso | StringSplitOptions.RemoveEmptyEntries) == sso) {
                        for (int i = tmp.GetUpperBound(0); i >= 0; i--) {
                            tmp[i] = tmp[i].Trim();
                            if (tmp[i].IsEmpty()) {
                                PArray.RemoveItem<string>(ref tmp, i);
                            }
                        }
                    }
                    else {
                        for (int i = 0; i <= tmp.GetUpperBound(0); i++) {
                            tmp[i] = tmp[i].Trim();
                        }
                    }
                    return tmp;
                }
                else {
                    return s.Split(Separator, Count, ToSplitOptions(Options));
                }
            }
        }
        private static string[] SplitOptionsSkip(string s, string[] Separator, SkipStringStream Skip, SplitOptions Options) {
            bool Trim = ((Options | SplitOptions.Trim) == Options);

            if (IsEmpty(s, Trim, Options)) {
                return new string[0];
            }

            bool Remove = ((Options | SplitOptions.RemoveEmptyEntries) == Options);

            if (Bit.IsIn(Options, SplitOptions.AddDelimiterToArray)) {
                if (Trim && Remove) {
                    return SplitTrimAddDelimiterRemoveEmpty(s, Separator, Skip);
                }
                else if (Trim) {
                    return SplitTrimAddDelimiter(s, Separator, Skip);
                }
                else if (Remove) {
                    return SplitAddDelimiterRemoveEmpty(s, Separator, Skip);
                }
                else {
                    return SplitAddDelimiter(s, Separator, Skip);
                }
            }
            else if (Bit.IsIn(Options, SplitOptions.KeepDelimiter)) {
                if (Trim && Remove) {
                    return SplitTrimKeepDelimiterRemoveEmpty(s, Separator, Skip);
                }
                else if (Trim) {
                    return SplitTrimKeepDelimiter(s, Separator, Skip);
                }
                else if (Remove) {
                    return SplitKeepDelimiterRemoveEmpty(s, Separator, Skip);
                }
                else {
                    return SplitKeepDelimiter(s, Separator, Skip);
                }
            }
            else {
                if (Trim && Remove) {
                    return SplitTrimRemoveEmpty(s, Separator, Skip);
                }
                else if (Trim) {
                    return SplitTrim(s, Separator, Skip);
                }
                else if (Remove) {
                    return SplitRemoveEmpty(s, Separator, Skip);
                }
                else {
                    return SplitBasic(s, Separator, Skip);
                }
            }
        }
        private static string[] SplitOptionsBase(string s, string[] Separator, SplitOptions Options) {
            bool Trim = ((Options | SplitOptions.Trim) == Options);

            if (IsEmpty(s, Trim, Options)) {
                return new string[0];
            }

            bool Remove = ((Options | SplitOptions.RemoveEmptyEntries) == Options);

            if (Bit.IsIn(Options, SplitOptions.AddDelimiterToArray)) {
                if (Trim && Remove) {
                    return SplitTrimAddDelimiterRemoveEmpty(s, Separator);
                }
                else if (Trim) {
                    return SplitTrimAddDelimiter(s, Separator);
                }
                else if (Remove) {
                    return SplitAddDelimiterRemoveEmpty(s, Separator);
                }
                else {
                    return SplitAddDelimiter(s, Separator);
                }
            }
            else if (Bit.IsIn(Options, SplitOptions.KeepDelimiter)) {
                if (Trim && Remove) {
                    return SplitTrimKeepDelimiterRemoveEmpty(s, Separator);
                }
                else if (Trim) {
                    return SplitTrimKeepDelimiter(s, Separator);
                }
                else if (Remove) {
                    return SplitKeepDelimiterRemoveEmpty(s, Separator);
                }
                else {
                    return SplitKeepDelimiter(s, Separator);
                }
            }
            else {
                if (Trim) {
                    StringSplitOptions sso = ToSplitOptions(Options);
                    string[] tmp = s.Split(Separator, sso);
                    if ((sso | StringSplitOptions.RemoveEmptyEntries) == sso) {
                        for (int i = tmp.GetUpperBound(0); i >= 0; i--) {
                            tmp[i] = tmp[i].Trim();
                            if (tmp[i].IsEmpty()) {
                                PArray.RemoveItem<string>(ref tmp, i);
                            }
                        }
                    }
                    else {
                        for (int i = 0; i <= tmp.GetUpperBound(0); i++) {
                            tmp[i] = tmp[i].Trim();
                        }
                    }
                    return tmp;
                }
                else {
                    return s.Split(Separator, ToSplitOptions(Options));
                }
            }
        }

        private static bool IsEmpty(string s, bool trim, SplitOptions options) {
            if ((trim ? (s.Trim().IsEmpty()) : (s.IsEmpty())) &&
                Bit.IsIn(options, SplitOptions.RemoveEmptySplitString)) {
                return true;
            }
            else {
                return false;
            }
        }
        #endregion

        #region Normal
        private static string[] SplitTrimAddDelimiterRemoveEmpty(string s, string[] Separator) {

            string[] strArray = new string[] { };
            int cnt = 0;
            int index = 0;
            int len = s.Length - MinLength(Separator) + 1;

            for (int i = 0; i <= len; i++) {
                if (i == len) {
                    if (s.Trim().IsNotEmpty()) {
                        PArray.IncrementArray(ref strArray);
                        strArray[strArray.GetUpperBound(0)] = s.Trim();
                    }
                } // If not being skipped and is a seperator
                else if (PMath.SetValue(ref index, FindIndexIn(s, Separator, i - cnt)) >= 0) {
                    string tmp = PString.SafeSubstring(s, 0, i - cnt);
                    if (tmp.Trim().IsNotEmpty()) {
                        PArray.IncrementArray<string>(ref strArray);
                        strArray[strArray.GetUpperBound(0)] = tmp.Trim();
                        PArray.IncrementArray<string>(ref strArray);
                        strArray[strArray.GetUpperBound(0)] = Separator[index];
                    }
                    s = PString.SafeSubstring(s, i - cnt + Separator[index].Length);

                    i += Separator[index].Length - 1;
                    cnt += tmp.Length + Separator[index].Length;
                }
            }
            return strArray;
        }
        private static string[] SplitTrimKeepDelimiterRemoveEmpty(string s, string[] Separator) {
            string[] strArray = new string[] { };
            int cnt = 0;
            int index = 0;
            int len = s.Length - MinLength(Separator) + 1;

            for (int i = 0; i <= len; i++) {
                if (i == len) {
                    if (s.Trim().IsNotEmpty()) {
                        PArray.IncrementArray(ref strArray);
                        strArray[strArray.GetUpperBound(0)] = s.Trim();
                    }
                } // If not being skipped and is a seperator
                else if (PMath.SetValue(ref index, FindIndexIn(s, Separator, i - cnt)) >= 0) {
                    string tmp = PString.SafeSubstring(s, 0, i - cnt);
                    if (tmp.Trim().IsNotEmpty()) {
                        PArray.IncrementArray<string>(ref strArray);
                        strArray[strArray.GetUpperBound(0)] = tmp.Trim() + Separator[index];
                    }
                    s = PString.SafeSubstring(s, i - cnt + Separator[index].Length);

                    i += Separator[index].Length - 1;
                    cnt += tmp.Length + Separator[index].Length;
                }
            }
            return strArray;
        }
        private static string[] SplitTrimRemoveEmpty(string s, string[] Separator) {
            string[] strArray = new string[] { };
            int cnt = 0;
            int index = 0;
            int len = s.Length - MinLength(Separator) + 1;

            for (int i = 0; i <= len; i++) {
                if (i == len) {
                    if (s.Trim().IsNotEmpty()) {
                        PArray.IncrementArray(ref strArray);
                        strArray[strArray.GetUpperBound(0)] = s.Trim();
                    }
                } // If not being skipped and is a seperator
                else if (PMath.SetValue(ref index, FindIndexIn(s, Separator, i - cnt)) >= 0) {
                    string tmp = PString.SafeSubstring(s, 0, i - cnt);
                    if (tmp.Trim().IsNotEmpty()) {
                        PArray.IncrementArray<string>(ref strArray);
                        strArray[strArray.GetUpperBound(0)] = tmp.Trim();
                    }
                    s = PString.SafeSubstring(s, i - cnt + Separator[index].Length);
                    i += Separator[index].Length - 1;
                    cnt += tmp.Length + Separator[index].Length;
                }
            }
            return strArray;
        }

        private static string[] SplitTrimAddDelimiter(string s, string[] Separator) {
            string[] strArray = new string[] { };
            int cnt = 0;
            int index = 0;
            int len = s.Length - MinLength(Separator) + 1;

            for (int i = 0; i <= len; i++) {
                if (i == len) {
                    PArray.IncrementArray(ref strArray);
                    strArray[strArray.GetUpperBound(0)] = s.Trim();
                } // If not being skipped and is a seperator
                else if (PMath.SetValue(ref index, FindIndexIn(s, Separator, i - cnt)) >= 0) {
                    string tmp = PString.SafeSubstring(s, 0, i - cnt);

                    PArray.IncrementArray<string>(ref strArray);
                    strArray[strArray.GetUpperBound(0)] = tmp.Trim();
                    PArray.IncrementArray<string>(ref strArray);
                    strArray[strArray.GetUpperBound(0)] = Separator[index];

                    s = PString.SafeSubstring(s, i - cnt + Separator[index].Length);
                    i += Separator[index].Length - 1;
                    cnt += tmp.Length + Separator[index].Length;
                }
            }
            return strArray;
        }
        private static string[] SplitTrimKeepDelimiter(string s, string[] Separator) {
            string[] strArray = new string[] { };
            int cnt = 0;
            int index = 0;
            int len = s.Length - MinLength(Separator) + 1;

            for (int i = 0; i <= len; i++) {
                if (i == len) {
                    PArray.IncrementArray(ref strArray);
                    strArray[strArray.GetUpperBound(0)] = s.Trim();
                } // If not being skipped and is a seperator
                else if (PMath.SetValue(ref index, FindIndexIn(s, Separator, i - cnt)) >= 0) {
                    string tmp = PString.SafeSubstring(s, 0, i - cnt);

                    PArray.IncrementArray<string>(ref strArray);
                    strArray[strArray.GetUpperBound(0)] = tmp.Trim() + Separator[index];

                    s = PString.SafeSubstring(s, i - cnt + Separator[index].Length);

                    i += Separator[index].Length - 1;
                    cnt += tmp.Length + Separator[index].Length;
                }
            }
            return strArray;
        }
        private static string[] SplitTrim(string s, string[] Separator) {
            string[] strArray = new string[] { };
            int cnt = 0;
            int index = 0;
            int len = s.Length - MinLength(Separator) + 1;

            for (int i = 0; i <= len; i++) {
                if (i == len) {
                    PArray.IncrementArray(ref strArray);
                    strArray[strArray.GetUpperBound(0)] = s.Trim();
                } // If not being skipped and is a seperator
                else if (PMath.SetValue(ref index, FindIndexIn(s, Separator, i - cnt)) >= 0) {
                    string tmp = PString.SafeSubstring(s, 0, i - cnt);

                    PArray.IncrementArray<string>(ref strArray);
                    strArray[strArray.GetUpperBound(0)] = tmp.Trim();

                    s = PString.SafeSubstring(s, i - cnt + Separator[index].Length);

                    i += Separator[index].Length - 1;
                    cnt += tmp.Length + Separator[index].Length;
                }
            }
            return strArray;
        }

        private static string[] SplitAddDelimiterRemoveEmpty(string s, string[] Separator) {
            string[] strArray = new string[] { };
            int cnt = 0;
            int index = 0;
            int len = s.Length - MinLength(Separator) + 1;

            for (int i = 0; i <= len; i++) {
                if (i == len) {
                    if (s.IsNotEmpty()) {
                        PArray.IncrementArray(ref strArray);
                        strArray[strArray.GetUpperBound(0)] = s;
                    }
                } // If not being skipped and is a seperator
                else if (PMath.SetValue(ref index, FindIndexIn(s, Separator, i - cnt)) >= 0) {
                    string tmp = PString.SafeSubstring(s, 0, i - cnt);
                    if (tmp.IsNotEmpty()) {
                        PArray.IncrementArray<string>(ref strArray);
                        strArray[strArray.GetUpperBound(0)] = tmp;
                        PArray.IncrementArray<string>(ref strArray);
                        strArray[strArray.GetUpperBound(0)] = Separator[index];
                    }
                    s = PString.SafeSubstring(s, i - cnt + Separator[index].Length);

                    i += Separator[index].Length - 1;
                    cnt += tmp.Length + Separator[index].Length;
                }
            }
            return strArray;
        }
        private static string[] SplitKeepDelimiterRemoveEmpty(string s, string[] Separator) {
            string[] strArray = new string[] { };
            int cnt = 0;
            int index = 0;
            int len = s.Length - MinLength(Separator) + 1;

            for (int i = 0; i <= len; i++) {
                if (i == len) {
                    if (s.IsNotEmpty()) {
                        PArray.IncrementArray(ref strArray);
                        strArray[strArray.GetUpperBound(0)] = s;
                    }
                } // If not being skipped and is a seperator
                else if (PMath.SetValue(ref index, FindIndexIn(s, Separator, i - cnt)) >= 0) {
                    string tmp = PString.SafeSubstring(s, 0, i - cnt);
                    if (tmp.IsNotEmpty()) {
                        PArray.IncrementArray<string>(ref strArray);
                        strArray[strArray.GetUpperBound(0)] = tmp + Separator[index];
                    }
                    s = PString.SafeSubstring(s, i - cnt + Separator[index].Length);

                    i += Separator[index].Length - 1;
                    cnt += tmp.Length + Separator[index].Length;
                }
            }
            return strArray;
        }
        private static string[] SplitRemoveEmpty(string s, string[] Separator) {
            string[] strArray = new string[] { };
            int cnt = 0;
            int index = 0;
            int len = s.Length - MinLength(Separator) + 1;

            for (int i = 0; i <= len; i++) {
                if (i == len) {
                    if (s.IsNotEmpty()) {
                        PArray.IncrementArray(ref strArray);
                        strArray[strArray.GetUpperBound(0)] = s;
                    }
                } // If not being skipped and is a seperator
                else if (PMath.SetValue(ref index, FindIndexIn(s, Separator, i - cnt)) >= 0) {
                    string tmp = PString.SafeSubstring(s, 0, i - cnt);
                    if (tmp.IsNotEmpty()) {
                        PArray.IncrementArray<string>(ref strArray);
                        strArray[strArray.GetUpperBound(0)] = tmp;
                    }
                    s = PString.SafeSubstring(s, i - cnt + Separator[index].Length);

                    i += Separator[index].Length - 1;
                    cnt += tmp.Length + Separator[index].Length;
                }
            }
            return strArray;
        }

        private static string[] SplitAddDelimiter(string s, string[] Separator) {
            string[] strArray = new string[] { };
            int cnt = 0;
            int index = 0;
            int len = s.Length - MinLength(Separator) + 1;

            for (int i = 0; i <= len; i++) {
                if (i == len) {
                    PArray.IncrementArray(ref strArray);
                    strArray[strArray.GetUpperBound(0)] = s;
                } // If not being skipped and is a seperator
                else if (PMath.SetValue(ref index, FindIndexIn(s, Separator, i - cnt)) >= 0) {
                    string tmp = PString.SafeSubstring(s, 0, i - cnt);

                    PArray.IncrementArray<string>(ref strArray);
                    strArray[strArray.GetUpperBound(0)] = tmp;
                    PArray.IncrementArray<string>(ref strArray);
                    strArray[strArray.GetUpperBound(0)] = Separator[index];

                    s = PString.SafeSubstring(s, i - cnt + Separator[index].Length);

                    i += Separator[index].Length - 1;
                    cnt += tmp.Length + Separator[index].Length;
                }
            }
            return strArray;
        }
        private static string[] SplitKeepDelimiter(string s, string[] Separator) {
            string[] strArray = new string[] { };
            int cnt = 0;
            int index = 0;
            int len = s.Length - MinLength(Separator) + 1;

            for (int i = 0; i <= len; i++) {
                if (i == len) {
                    PArray.IncrementArray(ref strArray);
                    strArray[strArray.GetUpperBound(0)] = s;
                } // If not being skipped and is a seperator
                else if (PMath.SetValue(ref index, FindIndexIn(s, Separator, i - cnt)) >= 0) {
                    string tmp = PString.SafeSubstring(s, 0, i - cnt);

                    PArray.IncrementArray<string>(ref strArray);
                    strArray[strArray.GetUpperBound(0)] = tmp + Separator[index];

                    s = PString.SafeSubstring(s, i - cnt + Separator[index].Length);

                    i += Separator[index].Length - 1;
                    cnt += tmp.Length + Separator[index].Length;
                }
            }
            return strArray;
        }
        private static string[] SplitBasic(string s, string[] Separator) {
            string[] strArray = new string[] { };
            int cnt = 0;
            int index = 0;
            int len = s.Length - MinLength(Separator) + 1;

            for (int i = 0; i <= len; i++) {
                if (i == len) {
                    PArray.IncrementArray(ref strArray);
                    strArray[strArray.GetUpperBound(0)] = s;
                } // If not being skipped and is a seperator
                else if (PMath.SetValue(ref index, FindIndexIn(s, Separator, i - cnt)) >= 0) {
                    string tmp = PString.SafeSubstring(s, 0, i - cnt);

                    PArray.IncrementArray<string>(ref strArray);
                    strArray[strArray.GetUpperBound(0)] = tmp;

                    s = PString.SafeSubstring(s, i - cnt + Separator[index].Length);

                    i += Separator[index].Length - 1;
                    cnt += tmp.Length + Separator[index].Length;
                }
            }
            return strArray;
        }
        #endregion
        #region Count
        private static string[] SplitTrimAddDelimiterRemoveEmpty(string s, string[] Separator, int Count) {
            if (Count < 0) {
                throw new ArgumentOutOfRangeException("count", "count" + " cannot be less than zero.");
                throw new ArgumentOutOfRangeException("count", "count" + " cannot be less than zero.");
            }
            else if (Count == 0 || s.IsEmpty()) {
                return new string[] { };
            }
            else if (Count == 1) {
                return new string[] { s };
            }
            else {
                string[] strArray = new string[] { };
                int cnt = 0;
                int index = 0;
                int len = s.Length - MinLength(Separator) + 1;

                for (int i = 0; i <= len; i++) {
                    if (i == len) {
                        if (s.Trim().IsNotEmpty()) {
                            PArray.IncrementArray(ref strArray);
                            strArray[strArray.GetUpperBound(0)] = s.Trim();
                        }
                    } // If not being skipped and is a seperator
                    else if (PMath.SetValue(ref index, FindIndexIn(s, Separator, i - cnt)) >= 0) {
                        string tmp = PString.SafeSubstring(s, 0, i - cnt);
                        if (tmp.Trim().IsNotEmpty()) {
                            PArray.IncrementArray<string>(ref strArray);
                            strArray[strArray.GetUpperBound(0)] = tmp.Trim();
                            PArray.IncrementArray<string>(ref strArray);
                            strArray[strArray.GetUpperBound(0)] = Separator[index];
                        }
                        s = PString.SafeSubstring(s, i - cnt + Separator[index].Length);
                        if (strArray.Length + 1 == Count) { // If the count has been reached
                            if (s.Trim().IsNotEmpty()) {
                                PArray.IncrementArray(ref strArray);
                                strArray[strArray.GetUpperBound(0)] = s.Trim();
                            }
                            return strArray;
                        }
                        else {
                            i += Separator[index].Length - 1;
                            cnt += tmp.Length + Separator[index].Length;
                        }
                    }
                }
                return strArray;
            }
        }
        private static string[] SplitTrimKeepDelimiterRemoveEmpty(string s, string[] Separator, int Count) {
            if (Count < 0) {
                throw new ArgumentOutOfRangeException("count", "count" + " cannot be less than zero.");
            }
            else if (Count == 0 || s.IsEmpty()) {
                return new string[] { };
            }
            else if (Count == 1) {
                return new string[] { s };
            }
            else {
                string[] strArray = new string[] { };
                int cnt = 0;
                int index = 0;
                int len = s.Length - MinLength(Separator) + 1;

                for (int i = 0; i <= len; i++) {
                    if (i == len) {
                        if (s.Trim().IsNotEmpty()) {
                            PArray.IncrementArray(ref strArray);
                            strArray[strArray.GetUpperBound(0)] = s.Trim();
                        }
                    } // If not being skipped and is a seperator
                    else if (PMath.SetValue(ref index, FindIndexIn(s, Separator, i - cnt)) >= 0) {
                        string tmp = PString.SafeSubstring(s, 0, i - cnt);
                        if (tmp.Trim().IsNotEmpty()) {
                            PArray.IncrementArray<string>(ref strArray);
                            strArray[strArray.GetUpperBound(0)] = tmp.Trim() + Separator[index];
                        }
                        s = PString.SafeSubstring(s, i - cnt + Separator[index].Length);
                        if (strArray.Length + 1 == Count) { // If the count has been reached
                            if (s.Trim().IsNotEmpty()) {
                                PArray.IncrementArray(ref strArray);
                                strArray[strArray.GetUpperBound(0)] = s.Trim();
                            }
                            return strArray;
                        }
                        else {
                            i += Separator[index].Length - 1;
                            cnt += tmp.Length + Separator[index].Length;
                        }
                    }
                }
                return strArray;
            }
        }
        private static string[] SplitTrimRemoveEmpty(string s, string[] Separator, int Count) {
            if (Count < 0) {
                throw new ArgumentOutOfRangeException("count", "count" + " cannot be less than zero.");
            }
            else if (Count == 0 || s.IsEmpty()) {
                return new string[] { };
            }
            else if (Count == 1) {
                return new string[] { s };
            }
            else {
                string[] strArray = new string[] { };
                int cnt = 0;
                int index = 0;
                int len = s.Length - MinLength(Separator) + 1;

                for (int i = 0; i <= len; i++) {
                    if (i == len) {
                        if (s.Trim().IsNotEmpty()) {
                            PArray.IncrementArray(ref strArray);
                            strArray[strArray.GetUpperBound(0)] = s.Trim();
                        }
                    } // If not being skipped and is a seperator
                    else if (PMath.SetValue(ref index, FindIndexIn(s, Separator, i - cnt)) >= 0) {
                        string tmp = PString.SafeSubstring(s, 0, i - cnt);
                        if (tmp.Trim().IsNotEmpty()) {
                            PArray.IncrementArray<string>(ref strArray);
                            strArray[strArray.GetUpperBound(0)] = tmp.Trim();
                        }
                        s = PString.SafeSubstring(s, i - cnt + Separator[index].Length);
                        if (strArray.Length + 1 == Count) { // If the count has been reached
                            if (s.Trim().IsNotEmpty()) {
                                PArray.IncrementArray(ref strArray);
                                strArray[strArray.GetUpperBound(0)] = s.Trim();
                            }
                            return strArray;
                        }
                        else {
                            i += Separator[index].Length - 1;
                            cnt += tmp.Length + Separator[index].Length;
                        }
                    }
                }
                return strArray;
            }
        }

        private static string[] SplitTrimAddDelimiter(string s, string[] Separator, int Count) {
            if (Count < 0) {
                throw new ArgumentOutOfRangeException("count", "count" + " cannot be less than zero.");
            }
            else if (Count == 0 || s.IsEmpty()) {
                return new string[] { };
            }
            else if (Count == 1) {
                return new string[] { s };
            }
            else {
                string[] strArray = new string[] { };
                int cnt = 0;
                int index = 0;
                int len = s.Length - MinLength(Separator) + 1;

                for (int i = 0; i <= len; i++) {
                    if (i == len) {
                        PArray.IncrementArray(ref strArray);
                        strArray[strArray.GetUpperBound(0)] = s.Trim();
                    } // If not being skipped and is a seperator
                    else if (PMath.SetValue(ref index, FindIndexIn(s, Separator, i - cnt)) >= 0) {
                        string tmp = PString.SafeSubstring(s, 0, i - cnt);

                        PArray.IncrementArray<string>(ref strArray);
                        strArray[strArray.GetUpperBound(0)] = tmp.Trim();
                        PArray.IncrementArray<string>(ref strArray);
                        strArray[strArray.GetUpperBound(0)] = Separator[index];

                        s = PString.SafeSubstring(s, i - cnt + Separator[index].Length);
                        if (strArray.Length + 1 == Count) { // If the count has been reached
                            PArray.IncrementArray(ref strArray);
                            strArray[strArray.GetUpperBound(0)] = s.Trim();
                            return strArray;
                        }
                        else {
                            i += Separator[index].Length - 1;
                            cnt += tmp.Length + Separator[index].Length;
                        }
                    }
                }
                return strArray;
            }
        }
        private static string[] SplitTrimKeepDelimiter(string s, string[] Separator, int Count) {
            if (Count < 0) {
                throw new ArgumentOutOfRangeException("count", "count" + " cannot be less than zero.");
            }
            else if (Count == 0 || s.IsEmpty()) {
                return new string[] { };
            }
            else if (Count == 1) {
                return new string[] { s };
            }
            else {
                string[] strArray = new string[] { };
                int cnt = 0;
                int index = 0;
                int len = s.Length - MinLength(Separator) + 1;

                for (int i = 0; i <= len; i++) {
                    if (i == len) {
                        PArray.IncrementArray(ref strArray);
                        strArray[strArray.GetUpperBound(0)] = s.Trim();
                    } // If not being skipped and is a seperator
                    else if (PMath.SetValue(ref index, FindIndexIn(s, Separator, i - cnt)) >= 0) {
                        string tmp = PString.SafeSubstring(s, 0, i - cnt);

                        PArray.IncrementArray<string>(ref strArray);
                        strArray[strArray.GetUpperBound(0)] = tmp.Trim() + Separator[index];

                        s = PString.SafeSubstring(s, i - cnt + Separator[index].Length);
                        if (strArray.Length + 1 == Count) { // If the count has been reached
                            PArray.IncrementArray(ref strArray);
                            strArray[strArray.GetUpperBound(0)] = s.Trim();
                            return strArray;
                        }
                        else {
                            i += Separator[index].Length - 1;
                            cnt += tmp.Length + Separator[index].Length;
                        }
                    }
                }
                return strArray;
            }
        }
        private static string[] SplitTrim(string s, string[] Separator, int Count) {
            if (Count < 0) {
                throw new ArgumentOutOfRangeException("count", "count" + " cannot be less than zero.");
            }
            else if (Count == 0 || s.IsEmpty()) {
                return new string[] { };
            }
            else if (Count == 1) {
                return new string[] { s };
            }
            else {
                string[] strArray = new string[] { };
                int cnt = 0;
                int index = 0;
                int len = s.Length - MinLength(Separator) + 1;

                for (int i = 0; i <= len; i++) {
                    if (i == len) {
                        PArray.IncrementArray(ref strArray);
                        strArray[strArray.GetUpperBound(0)] = s.Trim();
                    } // If not being skipped and is a seperator
                    else if (PMath.SetValue(ref index, FindIndexIn(s, Separator, i - cnt)) >= 0) {
                        string tmp = PString.SafeSubstring(s, 0, i - cnt);

                        PArray.IncrementArray<string>(ref strArray);
                        strArray[strArray.GetUpperBound(0)] = tmp.Trim();

                        s = PString.SafeSubstring(s, i - cnt + Separator[index].Length);
                        if (strArray.Length + 1 == Count) { // If the count has been reached
                            PArray.IncrementArray(ref strArray);
                            strArray[strArray.GetUpperBound(0)] = s.Trim();
                            return strArray;
                        }
                        else {
                            i += Separator[index].Length - 1;
                            cnt += tmp.Length + Separator[index].Length;
                        }
                    }
                }
                return strArray;
            }
        }

        private static string[] SplitAddDelimiterRemoveEmpty(string s, string[] Separator, int Count) {
            if (Count < 0) {
                throw new ArgumentOutOfRangeException("count", "count" + " cannot be less than zero.");
            }
            else if (Count == 0 || s.IsEmpty()) {
                return new string[] { };
            }
            else if (Count == 1) {
                return new string[] { s };
            }
            else {
                string[] strArray = new string[] { };
                int cnt = 0;
                int index = 0;
                int len = s.Length - MinLength(Separator) + 1;

                for (int i = 0; i <= len; i++) {
                    if (i == len) {
                        if (s.IsNotEmpty()) {
                            PArray.IncrementArray(ref strArray);
                            strArray[strArray.GetUpperBound(0)] = s;
                        }
                    } // If not being skipped and is a seperator
                    else if (PMath.SetValue(ref index, FindIndexIn(s, Separator, i - cnt)) >= 0) {
                        string tmp = PString.SafeSubstring(s, 0, i - cnt);
                        if (tmp.IsNotEmpty()) {
                            PArray.IncrementArray<string>(ref strArray);
                            strArray[strArray.GetUpperBound(0)] = tmp;
                            PArray.IncrementArray<string>(ref strArray);
                            strArray[strArray.GetUpperBound(0)] = Separator[index];
                        }
                        s = PString.SafeSubstring(s, i - cnt + Separator[index].Length);
                        if (strArray.Length + 1 == Count) { // If the count has been reached
                            if (s.IsNotEmpty()) {
                                PArray.IncrementArray(ref strArray);
                                strArray[strArray.GetUpperBound(0)] = s;
                            }
                            return strArray;
                        }
                        else {
                            i += Separator[index].Length - 1;
                            cnt += tmp.Length + Separator[index].Length;
                        }
                    }
                }
                return strArray;
            }
        }
        private static string[] SplitKeepDelimiterRemoveEmpty(string s, string[] Separator, int Count) {
            if (Count < 0) {
                throw new ArgumentOutOfRangeException("count", "count" + " cannot be less than zero.");
            }
            else if (Count == 0 || s.IsEmpty()) {
                return new string[] { };
            }
            else if (Count == 1) {
                return new string[] { s };
            }
            else {
                string[] strArray = new string[] { };
                int cnt = 0;
                int index = 0;
                int len = s.Length - MinLength(Separator) + 1;

                for (int i = 0; i <= len; i++) {
                    if (i == len) {
                        if (s.IsNotEmpty()) {
                            PArray.IncrementArray(ref strArray);
                            strArray[strArray.GetUpperBound(0)] = s;
                        }
                    } // If not being skipped and is a seperator
                    else if (PMath.SetValue(ref index, FindIndexIn(s, Separator, i - cnt)) >= 0) {
                        string tmp = PString.SafeSubstring(s, 0, i - cnt);
                        if (tmp.IsNotEmpty()) {
                            PArray.IncrementArray<string>(ref strArray);
                            strArray[strArray.GetUpperBound(0)] = tmp + Separator[index];
                        }
                        s = PString.SafeSubstring(s, i - cnt + Separator[index].Length);
                        if (strArray.Length + 1 == Count) { // If the count has been reached
                            if (s.IsNotEmpty()) {
                                PArray.IncrementArray(ref strArray);
                                strArray[strArray.GetUpperBound(0)] = s;
                            }
                            return strArray;
                        }
                        else {
                            i += Separator[index].Length - 1;
                            cnt += tmp.Length + Separator[index].Length;
                        }
                    }
                }
                return strArray;
            }
        }
        private static string[] SplitRemoveEmpty(string s, string[] Separator, int Count) {
            if (Count < 0) {
                throw new ArgumentOutOfRangeException("count", "count" + " cannot be less than zero.");
            }
            else if (Count == 0 || s.IsEmpty()) {
                return new string[] { };
            }
            else if (Count == 1) {
                return new string[] { s };
            }
            else {
                string[] strArray = new string[] { };
                int cnt = 0;
                int index = 0;
                int len = s.Length - MinLength(Separator) + 1;

                for (int i = 0; i <= len; i++) {
                    if (i == len) {
                        if (s.IsNotEmpty()) {
                            PArray.IncrementArray(ref strArray);
                            strArray[strArray.GetUpperBound(0)] = s;
                        }
                    } // If not being skipped and is a seperator
                    else if (PMath.SetValue(ref index, FindIndexIn(s, Separator, i - cnt)) >= 0) {
                        string tmp = PString.SafeSubstring(s, 0, i - cnt);
                        if (tmp.IsNotEmpty()) {
                            PArray.IncrementArray<string>(ref strArray);
                            strArray[strArray.GetUpperBound(0)] = tmp;
                        }
                        s = PString.SafeSubstring(s, i - cnt + Separator[index].Length);
                        if (strArray.Length + 1 == Count) { // If the count has been reached
                            if (s.IsNotEmpty()) {
                                PArray.IncrementArray(ref strArray);
                                strArray[strArray.GetUpperBound(0)] = s;
                            }
                            return strArray;
                        }
                        else {
                            i += Separator[index].Length - 1;
                            cnt += tmp.Length + Separator[index].Length;
                        }
                    }
                }
                return strArray;
            }
        }

        private static string[] SplitAddDelimiter(string s, string[] Separator, int Count) {
            if (Count < 0) {
                throw new ArgumentOutOfRangeException("count", "count" + " cannot be less than zero.");
            }
            else if (Count == 0 || s.IsEmpty()) {
                return new string[] { };
            }
            else if (Count == 1) {
                return new string[] { s };
            }
            else {
                string[] strArray = new string[] { };
                int cnt = 0;
                int index = 0;
                int len = s.Length - MinLength(Separator) + 1;

                for (int i = 0; i <= len; i++) {
                    if (i == len) {
                        PArray.IncrementArray(ref strArray);
                        strArray[strArray.GetUpperBound(0)] = s;
                    } // If not being skipped and is a seperator
                    else if (PMath.SetValue(ref index, FindIndexIn(s, Separator, i - cnt)) >= 0) {
                        string tmp = PString.SafeSubstring(s, 0, i - cnt);

                        PArray.IncrementArray<string>(ref strArray);
                        strArray[strArray.GetUpperBound(0)] = tmp;
                        PArray.IncrementArray<string>(ref strArray);
                        strArray[strArray.GetUpperBound(0)] = Separator[index];

                        s = PString.SafeSubstring(s, i - cnt + Separator[index].Length);
                        if (strArray.Length + 1 == Count) { // If the count has been reached
                            PArray.IncrementArray(ref strArray);
                            strArray[strArray.GetUpperBound(0)] = s;
                            return strArray;
                        }
                        else {
                            i += Separator[index].Length - 1;
                            cnt += tmp.Length + Separator[index].Length;
                        }
                    }
                }
                return strArray;
            }
        }
        private static string[] SplitKeepDelimiter(string s, string[] Separator, int Count) {
            if (Count < 0) {
                throw new ArgumentOutOfRangeException("count", "count" + " cannot be less than zero.");
            }
            else if (Count == 0 || s.IsEmpty()) {
                return new string[] { };
            }
            else if (Count == 1) {
                return new string[] { s };
            }
            else {
                string[] strArray = new string[] { };
                int cnt = 0;
                int index = 0;
                int len = s.Length - MinLength(Separator) + 1;

                for (int i = 0; i <= len; i++) {
                    if (i == len) {
                        PArray.IncrementArray(ref strArray);
                        strArray[strArray.GetUpperBound(0)] = s;
                    } // If not being skipped and is a seperator
                    else if (PMath.SetValue(ref index, FindIndexIn(s, Separator, i - cnt)) >= 0) {
                        string tmp = PString.SafeSubstring(s, 0, i - cnt);

                        PArray.IncrementArray<string>(ref strArray);
                        strArray[strArray.GetUpperBound(0)] = tmp + Separator[index];

                        s = PString.SafeSubstring(s, i - cnt + Separator[index].Length);
                        if (strArray.Length + 1 == Count) { // If the count has been reached
                            PArray.IncrementArray(ref strArray);
                            strArray[strArray.GetUpperBound(0)] = s;
                            return strArray;
                        }
                        else {
                            i += Separator[index].Length - 1;
                            cnt += tmp.Length + Separator[index].Length;
                        }
                    }
                }
                return strArray;
            }
        }
        private static string[] SplitBasic(string s, string[] Separator, int Count) {
            if (Count < 0) {
                throw new ArgumentOutOfRangeException("count", "count" + " cannot be less than zero.");
            }
            else if (Count == 0 || s.IsEmpty()) {
                return new string[] { };
            }
            else if (Count == 1) {
                return new string[] { s };
            }
            else {
                string[] strArray = new string[] { };
                int cnt = 0;
                int index = 0;
                int len = s.Length - MinLength(Separator) + 1;

                for (int i = 0; i <= len; i++) {
                    if (i == len) {
                        PArray.IncrementArray(ref strArray);
                        strArray[strArray.GetUpperBound(0)] = s;
                    } // If not being skipped and is a seperator
                    else if (PMath.SetValue(ref index, FindIndexIn(s, Separator, i - cnt)) >= 0) {
                        string tmp = PString.SafeSubstring(s, 0, i - cnt);

                        PArray.IncrementArray<string>(ref strArray);
                        strArray[strArray.GetUpperBound(0)] = tmp;

                        s = PString.SafeSubstring(s, i - cnt + Separator[index].Length);
                        if (strArray.Length + 1 == Count) { // If the count has been reached
                            PArray.IncrementArray(ref strArray);
                            strArray[strArray.GetUpperBound(0)] = s;
                            return strArray;
                        }
                        else {
                            i += Separator[index].Length - 1;
                            cnt += tmp.Length + Separator[index].Length;
                        }
                    }
                }
                return strArray;
            }
        }
        #endregion
        #region Skip
        private static string[] SplitTrimAddDelimiterRemoveEmpty(string s, string[] Separator, SkipStringStream Skip) {
            string[] strArray = new string[] { };
            int cnt = 0;
            int index = 0;
            int len = s.Length - MinLength(Separator) + 1;
            bool check;
            for (int i = 0; i <= len; i += Skip.LastSkipLength) {
                check = Skip.Skip(s, i - cnt);

                if (i == len) {
                    if (s.Trim().IsNotEmpty()) {
                        PArray.IncrementArray(ref strArray);
                        strArray[strArray.GetUpperBound(0)] = s.Trim();
                    }
                } // If not being skipped and is a seperator
                else if (check == false && PMath.SetValue(ref index, FindIndexIn(s, Separator, i - cnt)) >= 0) {
                    string tmp = PString.SafeSubstring(s, 0, i - cnt);
                    if (tmp.Trim().IsNotEmpty()) {
                        PArray.IncrementArray<string>(ref strArray);
                        strArray[strArray.GetUpperBound(0)] = tmp.Trim();
                        PArray.IncrementArray<string>(ref strArray);
                        strArray[strArray.GetUpperBound(0)] = Separator[index];
                    }
                    s = PString.SafeSubstring(s, i - cnt + Separator[index].Length);

                    i += Separator[index].Length - 1;
                    cnt += tmp.Length + Separator[index].Length;
                }
            }
            return strArray;
        }
        private static string[] SplitTrimKeepDelimiterRemoveEmpty(string s, string[] Separator, SkipStringStream Skip) {
            string[] strArray = new string[] { };
            int cnt = 0;
            int index = 0;
            int len = s.Length - MinLength(Separator) + 1;
            bool check;
            for (int i = 0; i <= len; i += Skip.LastSkipLength) {
                check = Skip.Skip(s, i - cnt);

                if (i == len) {
                    if (s.Trim().IsNotEmpty()) {
                        PArray.IncrementArray(ref strArray);
                        strArray[strArray.GetUpperBound(0)] = s.Trim();
                    }
                } // If not being skipped and is a seperator
                else if (check == false && PMath.SetValue(ref index, FindIndexIn(s, Separator, i - cnt)) >= 0) {
                    string tmp = PString.SafeSubstring(s, 0, i - cnt);
                    if (tmp.Trim().IsNotEmpty()) {
                        PArray.IncrementArray<string>(ref strArray);
                        strArray[strArray.GetUpperBound(0)] = tmp.Trim() + Separator[index];
                    }
                    s = PString.SafeSubstring(s, i - cnt + Separator[index].Length);

                    i += Separator[index].Length - 1;
                    cnt += tmp.Length + Separator[index].Length;
                }
            }
            return strArray;
        }
        private static string[] SplitTrimRemoveEmpty(string s, string[] Separator, SkipStringStream Skip) {
            string[] strArray = new string[] { };
            int cnt = 0;
            int index = 0;
            int len = s.Length - MinLength(Separator) + 1;
            bool check;
            for (int i = 0; i <= len; i += Skip.LastSkipLength) {
                check = Skip.Skip(s, i - cnt);

                if (i == len) {
                    if (s.Trim().IsNotEmpty()) {
                        PArray.IncrementArray(ref strArray);
                        strArray[strArray.GetUpperBound(0)] = s.Trim();
                    }
                } // If not being skipped and is a seperator
                else if (check == false && PMath.SetValue(ref index, FindIndexIn(s, Separator, i - cnt)) >= 0) {
                    string tmp = PString.SafeSubstring(s, 0, i - cnt);
                    if (tmp.Trim().IsNotEmpty()) {
                        PArray.IncrementArray<string>(ref strArray);
                        strArray[strArray.GetUpperBound(0)] = tmp.Trim();
                    }
                    s = PString.SafeSubstring(s, i - cnt + Separator[index].Length);
                    i += Separator[index].Length - 1;
                    cnt += tmp.Length + Separator[index].Length;
                }
            }
            return strArray;
        }

        private static string[] SplitTrimAddDelimiter(string s, string[] Separator, SkipStringStream Skip) {
            string[] strArray = new string[] { };
            int cnt = 0;
            int index = 0;
            int len = s.Length - MinLength(Separator) + 1;
            bool check;
            for (int i = 0; i <= len; i += Skip.LastSkipLength) {
                check = Skip.Skip(s, i - cnt);

                if (i == len) {
                    PArray.IncrementArray(ref strArray);
                    strArray[strArray.GetUpperBound(0)] = s.Trim();
                } // If not being skipped and is a seperator
                else if (check == false && PMath.SetValue(ref index, FindIndexIn(s, Separator, i - cnt)) >= 0) {
                    string tmp = PString.SafeSubstring(s, 0, i - cnt);

                    PArray.IncrementArray<string>(ref strArray);
                    strArray[strArray.GetUpperBound(0)] = tmp.Trim();
                    PArray.IncrementArray<string>(ref strArray);
                    strArray[strArray.GetUpperBound(0)] = Separator[index];

                    s = PString.SafeSubstring(s, i - cnt + Separator[index].Length);
                    i += Separator[index].Length - 1;
                    cnt += tmp.Length + Separator[index].Length;
                }
            }
            return strArray;
        }
        private static string[] SplitTrimKeepDelimiter(string s, string[] Separator, SkipStringStream Skip) {
            string[] strArray = new string[] { };
            int cnt = 0;
            int index = 0;
            int len = s.Length - MinLength(Separator) + 1;
            bool check;
            for (int i = 0; i <= len; i += Skip.LastSkipLength) {
                check = Skip.Skip(s, i - cnt);

                if (i == len) {
                    PArray.IncrementArray(ref strArray);
                    strArray[strArray.GetUpperBound(0)] = s.Trim();
                } // If not being skipped and is a seperator
                else if (check == false && PMath.SetValue(ref index, FindIndexIn(s, Separator, i - cnt)) >= 0) {
                    string tmp = PString.SafeSubstring(s, 0, i - cnt);

                    PArray.IncrementArray<string>(ref strArray);
                    strArray[strArray.GetUpperBound(0)] = tmp.Trim() + Separator[index];

                    s = PString.SafeSubstring(s, i - cnt + Separator[index].Length);

                    i += Separator[index].Length - 1;
                    cnt += tmp.Length + Separator[index].Length;
                }
            }
            return strArray;
        }
        private static string[] SplitTrim(string s, string[] Separator, SkipStringStream Skip) {
            string[] strArray = new string[] { };
            int cnt = 0;
            int index = 0;
            int len = s.Length - MinLength(Separator) + 1;
            bool check;
            for (int i = 0; i <= len; i += Skip.LastSkipLength) {
                check = Skip.Skip(s, i - cnt);

                if (i == len) {
                    PArray.IncrementArray(ref strArray);
                    strArray[strArray.GetUpperBound(0)] = s.Trim();
                } // If not being skipped and is a seperator
                else if (check == false && PMath.SetValue(ref index, FindIndexIn(s, Separator, i - cnt)) >= 0) {
                    string tmp = PString.SafeSubstring(s, 0, i - cnt);

                    PArray.IncrementArray<string>(ref strArray);
                    strArray[strArray.GetUpperBound(0)] = tmp.Trim();

                    s = PString.SafeSubstring(s, i - cnt + Separator[index].Length);

                    i += Separator[index].Length - 1;
                    cnt += tmp.Length + Separator[index].Length;
                }
            }
            return strArray;
        }

        private static string[] SplitAddDelimiterRemoveEmpty(string s, string[] Separator, SkipStringStream Skip) {
            string[] strArray = new string[] { };
            int cnt = 0;
            int index = 0;
            int len = s.Length - MinLength(Separator) + 1;
            bool check;
            for (int i = 0; i <= len; i += Skip.LastSkipLength) {
                check = Skip.Skip(s, i - cnt);

                if (i == len) {
                    if (s.IsNotEmpty()) {
                        PArray.IncrementArray(ref strArray);
                        strArray[strArray.GetUpperBound(0)] = s;
                    }
                } // If not being skipped and is a seperator
                else if (check == false && PMath.SetValue(ref index, FindIndexIn(s, Separator, i - cnt)) >= 0) {
                    string tmp = PString.SafeSubstring(s, 0, i - cnt);
                    if (tmp.IsNotEmpty()) {
                        PArray.IncrementArray<string>(ref strArray);
                        strArray[strArray.GetUpperBound(0)] = tmp;
                        PArray.IncrementArray<string>(ref strArray);
                        strArray[strArray.GetUpperBound(0)] = Separator[index];
                    }
                    s = PString.SafeSubstring(s, i - cnt + Separator[index].Length);

                    i += Separator[index].Length - 1;
                    cnt += tmp.Length + Separator[index].Length;
                }
            }
            return strArray;
        }
        private static string[] SplitKeepDelimiterRemoveEmpty(string s, string[] Separator, SkipStringStream Skip) {
            string[] strArray = new string[] { };
            int cnt = 0;
            int index = 0;
            int len = s.Length - MinLength(Separator) + 1;
            bool check;
            for (int i = 0; i <= len; i += Skip.LastSkipLength) {
                check = Skip.Skip(s, i - cnt);

                if (i == len) {
                    if (s.IsNotEmpty()) {
                        PArray.IncrementArray(ref strArray);
                        strArray[strArray.GetUpperBound(0)] = s;
                    }
                } // If not being skipped and is a seperator
                else if (check == false && PMath.SetValue(ref index, FindIndexIn(s, Separator, i - cnt)) >= 0) {
                    string tmp = PString.SafeSubstring(s, 0, i - cnt);
                    if (tmp.IsNotEmpty()) {
                        PArray.IncrementArray<string>(ref strArray);
                        strArray[strArray.GetUpperBound(0)] = tmp + Separator[index];
                    }
                    s = PString.SafeSubstring(s, i - cnt + Separator[index].Length);

                    i += Separator[index].Length - 1;
                    cnt += tmp.Length + Separator[index].Length;
                }
            }
            return strArray;
        }
        private static string[] SplitRemoveEmpty(string s, string[] Separator, SkipStringStream Skip) {
            string[] strArray = new string[] { };
            int cnt = 0;
            int index = 0;
            int len = s.Length - MinLength(Separator) + 1;
            bool check;
            for (int i = 0; i <= len; i += Skip.LastSkipLength) {
                check = Skip.Skip(s, i - cnt);

                if (i == len) {
                    if (s.IsNotEmpty()) {
                        PArray.IncrementArray(ref strArray);
                        strArray[strArray.GetUpperBound(0)] = s;
                    }
                } // If not being skipped and is a seperator
                else if (check == false && PMath.SetValue(ref index, FindIndexIn(s, Separator, i - cnt)) >= 0) {
                    string tmp = PString.SafeSubstring(s, 0, i - cnt);
                    if (tmp.IsNotEmpty()) {
                        PArray.IncrementArray<string>(ref strArray);
                        strArray[strArray.GetUpperBound(0)] = tmp;
                    }
                    s = PString.SafeSubstring(s, i - cnt + Separator[index].Length);

                    i += Separator[index].Length - 1;
                    cnt += tmp.Length + Separator[index].Length;
                }
            }
            return strArray;
        }

        private static string[] SplitAddDelimiter(string s, string[] separator, SkipStringStream skip) {
            string[] strArray = new string[] { };
            int cnt = 0;
            int index = 0;
            int len = s.Length - MinLength(separator) + 1;
            bool check;
            for (int i = 0; i <= len; i += skip.LastSkipLength) {
                check = skip.Skip(s, i - cnt);

                if (i == len) {
                    PArray.IncrementArray(ref strArray);
                    strArray[strArray.GetUpperBound(0)] = s;
                } // If not being skipped and is a seperator
                else if (check == false && PMath.SetValue(ref index, FindIndexIn(s, separator, i - cnt)) >= 0) {
                    string tmp = PString.SafeSubstring(s, 0, i - cnt);

                    PArray.IncrementArray<string>(ref strArray);
                    strArray[strArray.GetUpperBound(0)] = tmp;
                    PArray.IncrementArray<string>(ref strArray);
                    strArray[strArray.GetUpperBound(0)] = separator[index];

                    s = PString.SafeSubstring(s, i - cnt + separator[index].Length);

                    i += separator[index].Length - 1;
                    cnt += tmp.Length + separator[index].Length;
                }
            }
            return strArray;
        }
        private static string[] SplitKeepDelimiter(string s, string[] separator, SkipStringStream skip) {
            string[] strArray = new string[] { };
            int cnt = 0;
            int index = 0;
            int len = s.Length - MinLength(separator) + 1;
            bool check;
            for (int i = 0; i <= len; i += skip.LastSkipLength) {
                check = skip.Skip(s, i - cnt);

                if (i == len) {
                    PArray.IncrementArray(ref strArray);
                    strArray[strArray.GetUpperBound(0)] = s;
                } // If not being skipped and is a seperator
                else if (check == false && PMath.SetValue(ref index, FindIndexIn(s, separator, i - cnt)) >= 0) {
                    string tmp = PString.SafeSubstring(s, 0, i - cnt);

                    PArray.IncrementArray<string>(ref strArray);
                    strArray[strArray.GetUpperBound(0)] = tmp + separator[index];

                    s = PString.SafeSubstring(s, i - cnt + separator[index].Length);

                    i += separator[index].Length - 1;
                    cnt += tmp.Length + separator[index].Length;
                }
            }
            return strArray;
        }
        private static string[] SplitBasic(string s, string[] separator, SkipStringStream skip) {
            string[] strArray = new string[] { };
            int cnt = 0;
            int index = 0;
            int len = s.Length - MinLength(separator) + 1;
            bool check;
            for (int i = 0; i <= len; i += skip.LastSkipLength) {
                check = skip.Skip(s, i - cnt);

                if (i == len) {
                    PArray.IncrementArray(ref strArray);
                    strArray[strArray.GetUpperBound(0)] = s;
                } // If not being skipped and is a seperator
                else if (check == false && PMath.SetValue(ref index, FindIndexIn(s, separator, i - cnt)) >= 0) {
                    string tmp = PString.SafeSubstring(s, 0, i - cnt);

                    PArray.IncrementArray<string>(ref strArray);
                    strArray[strArray.GetUpperBound(0)] = tmp;

                    s = PString.SafeSubstring(s, i - cnt + separator[index].Length);

                    i += separator[index].Length - 1;
                    cnt += tmp.Length + separator[index].Length;
                }
            }
            return strArray;
        }
        #endregion
        #region Count Skip
        private static string[] SplitTrimAddDelimiterRemoveEmpty(string s, string[] separator, int count, SkipStringStream skip) {
            if (count < 0) {
                throw new ArgumentOutOfRangeException("count", "count" + " cannot be less than zero.");
            }
            else if (count == 0 || s.IsEmpty()) {
                return new string[] { };
            }
            else if (count == 1) {
                return new string[] { s };
            }
            else {
                string[] strArray = new string[] { };
                int cnt = 0;
                int index = 0;
                int len = s.Length - MinLength(separator) + 1;
                bool check;
                for (int i = 0; i <= len; i += skip.LastSkipLength) {
                    check = skip.Skip(s, i - cnt);

                    if (i == len) {
                        if (s.Trim().IsNotEmpty()) {
                            PArray.IncrementArray(ref strArray);
                            strArray[strArray.GetUpperBound(0)] = s.Trim();
                        }
                    } // If not being skipped and is a seperator
                    else if (check == false && PMath.SetValue(ref index, FindIndexIn(s, separator, i - cnt)) >= 0) {
                        string tmp = PString.SafeSubstring(s, 0, i - cnt);
                        if (tmp.Trim().IsNotEmpty()) {
                            PArray.IncrementArray<string>(ref strArray);
                            strArray[strArray.GetUpperBound(0)] = tmp.Trim();
                            PArray.IncrementArray<string>(ref strArray);
                            strArray[strArray.GetUpperBound(0)] = separator[index];
                        }
                        s = PString.SafeSubstring(s, i - cnt + separator[index].Length);
                        if (strArray.Length + 1 == count) { // If the count has been reached
                            if (s.Trim().IsNotEmpty()) {
                                PArray.IncrementArray(ref strArray);
                                strArray[strArray.GetUpperBound(0)] = s.Trim();
                            }
                            return strArray;
                        }
                        else {
                            i += separator[index].Length - 1;
                            cnt += tmp.Length + separator[index].Length;
                        }
                    }
                }
                return strArray;
            }
        }
        private static string[] SplitTrimKeepDelimiterRemoveEmpty(string s, string[] separator, int count, SkipStringStream skip) {
            if (count < 0) {
                throw new ArgumentOutOfRangeException("count", "count" + " cannot be less than zero.");
            }
            else if (count == 0 || s.IsEmpty()) {
                return new string[] { };
            }
            else if (count == 1) {
                return new string[] { s };
            }
            else {
                string[] strArray = new string[] { };
                int cnt = 0;
                int index = 0;
                int len = s.Length - MinLength(separator) + 1;
                bool check;
                for (int i = 0; i <= len; i += skip.LastSkipLength) {
                    check = skip.Skip(s, i - cnt);

                    if (i == len) {
                        if (s.Trim().IsNotEmpty()) {
                            PArray.IncrementArray(ref strArray);
                            strArray[strArray.GetUpperBound(0)] = s.Trim();
                        }
                    } // If not being skipped and is a seperator
                    else if (check == false && PMath.SetValue(ref index, FindIndexIn(s, separator, i - cnt)) >= 0) {
                        string tmp = PString.SafeSubstring(s, 0, i - cnt);
                        if (tmp.Trim().IsNotEmpty()) {
                            PArray.IncrementArray<string>(ref strArray);
                            strArray[strArray.GetUpperBound(0)] = tmp.Trim() + separator[index];
                        }
                        s = PString.SafeSubstring(s, i - cnt + separator[index].Length);
                        if (strArray.Length + 1 == count) { // If the count has been reached
                            if (s.Trim().IsNotEmpty()) {
                                PArray.IncrementArray(ref strArray);
                                strArray[strArray.GetUpperBound(0)] = s.Trim();
                            }
                            return strArray;
                        }
                        else {
                            i += separator[index].Length - 1;
                            cnt += tmp.Length + separator[index].Length;
                        }
                    }
                }
                return strArray;
            }
        }
        private static string[] SplitTrimRemoveEmpty(string s, string[] separator, int count, SkipStringStream skip) {
            if (count < 0) {
                throw new ArgumentOutOfRangeException("count", "count" + " cannot be less than zero.");
            }
            else if (count == 0 || s.IsEmpty()) {
                return new string[] { };
            }
            else if (count == 1) {
                return new string[] { s };
            }
            else {
                string[] strArray = new string[] { };
                int cnt = 0;
                int index = 0;
                int len = s.Length - MinLength(separator) + 1;
                bool check;
                for (int i = 0; i <= len; i += skip.LastSkipLength) {
                    check = skip.Skip(s, i - cnt);

                    if (i == len) {
                        if (s.Trim().IsNotEmpty()) {
                            PArray.IncrementArray(ref strArray);
                            strArray[strArray.GetUpperBound(0)] = s.Trim();
                        }
                    } // If not being skipped and is a seperator
                    else if (check == false && PMath.SetValue(ref index, FindIndexIn(s, separator, i - cnt)) >= 0) {
                        string tmp = PString.SafeSubstring(s, 0, i - cnt);
                        if (tmp.Trim().IsNotEmpty()) {
                            PArray.IncrementArray<string>(ref strArray);
                            strArray[strArray.GetUpperBound(0)] = tmp.Trim();
                        }
                        s = PString.SafeSubstring(s, i - cnt + separator[index].Length);
                        if (strArray.Length + 1 == count) { // If the count has been reached
                            if (s.Trim().IsNotEmpty()) {
                                PArray.IncrementArray(ref strArray);
                                strArray[strArray.GetUpperBound(0)] = s.Trim();
                            }
                            return strArray;
                        }
                        else {
                            i += separator[index].Length - 1;
                            cnt += tmp.Length + separator[index].Length;
                        }
                    }
                }
                return strArray;
            }
        }

        private static string[] SplitTrimAddDelimiter(string s, string[] separator, int count, SkipStringStream skip) {
            if (count < 0) {
                throw new ArgumentOutOfRangeException("count", "count" + " cannot be less than zero.");
            }
            else if (count == 0 || s.IsEmpty()) {
                return new string[] { };
            }
            else if (count == 1) {
                return new string[] { s };
            }
            else {
                string[] strArray = new string[] { };
                int cnt = 0;
                int index = 0;
                int len = s.Length - MinLength(separator) + 1;
                bool check;
                for (int i = 0; i <= len; i += skip.LastSkipLength) {
                    check = skip.Skip(s, i - cnt);

                    if (i == len) {
                        PArray.IncrementArray(ref strArray);
                        strArray[strArray.GetUpperBound(0)] = s.Trim();
                    } // If not being skipped and is a seperator
                    else if (check == false && PMath.SetValue(ref index, FindIndexIn(s, separator, i - cnt)) >= 0) {
                        string tmp = PString.SafeSubstring(s, 0, i - cnt);

                        PArray.IncrementArray<string>(ref strArray);
                        strArray[strArray.GetUpperBound(0)] = tmp.Trim();
                        PArray.IncrementArray<string>(ref strArray);
                        strArray[strArray.GetUpperBound(0)] = separator[index];

                        s = PString.SafeSubstring(s, i - cnt + separator[index].Length);
                        if (strArray.Length + 1 == count) { // If the count has been reached
                            PArray.IncrementArray(ref strArray);
                            strArray[strArray.GetUpperBound(0)] = s.Trim();
                            return strArray;
                        }
                        else {
                            i += separator[index].Length - 1;
                            cnt += tmp.Length + separator[index].Length;
                        }
                    }
                }
                return strArray;
            }
        }
        private static string[] SplitTrimKeepDelimiter(string s, string[] separator, int count, SkipStringStream skip) {
            if (count < 0) {
                throw new ArgumentOutOfRangeException("count", "count" + " cannot be less than zero.");
            }
            else if (count == 0 || s.IsEmpty()) {
                return new string[] { };
            }
            else if (count == 1) {
                return new string[] { s };
            }
            else {
                string[] strArray = new string[] { };
                int cnt = 0;
                int index = 0;
                int len = s.Length - MinLength(separator) + 1;
                bool check;
                for (int i = 0; i <= len; i += skip.LastSkipLength) {
                    check = skip.Skip(s, i - cnt);

                    if (i == len) {
                        PArray.IncrementArray(ref strArray);
                        strArray[strArray.GetUpperBound(0)] = s.Trim();
                    } // If not being skipped and is a seperator
                    else if (check == false && PMath.SetValue(ref index, FindIndexIn(s, separator, i - cnt)) >= 0) {
                        string tmp = PString.SafeSubstring(s, 0, i - cnt);

                        PArray.IncrementArray<string>(ref strArray);
                        strArray[strArray.GetUpperBound(0)] = tmp.Trim() + separator[index];

                        s = PString.SafeSubstring(s, i - cnt + separator[index].Length);
                        if (strArray.Length + 1 == count) { // If the count has been reached
                            PArray.IncrementArray(ref strArray);
                            strArray[strArray.GetUpperBound(0)] = s.Trim();
                            return strArray;
                        }
                        else {
                            i += separator[index].Length - 1;
                            cnt += tmp.Length + separator[index].Length;
                        }
                    }
                }
                return strArray;
            }
        }
        private static string[] SplitTrim(string s, string[] separator, int count, SkipStringStream skip) {
            if (count < 0) {
                throw new ArgumentOutOfRangeException("count", "count" + " cannot be less than zero.");
            }
            else if (count == 0 || s.IsEmpty()) {
                return new string[] { };
            }
            else if (count == 1) {
                return new string[] { s };
            }
            else {
                string[] strArray = new string[] { };
                int cnt = 0;
                int index = 0;
                int len = s.Length - MinLength(separator) + 1;
                bool check;
                for (int i = 0; i <= len; i += skip.LastSkipLength) {
                    check = skip.Skip(s, i - cnt);

                    if (i == len) {
                        PArray.IncrementArray(ref strArray);
                        strArray[strArray.GetUpperBound(0)] = s.Trim();
                    } // If not being skipped and is a seperator
                    else if (check == false && PMath.SetValue(ref index, FindIndexIn(s, separator, i - cnt)) >= 0) {
                        string tmp = PString.SafeSubstring(s, 0, i - cnt);

                        PArray.IncrementArray<string>(ref strArray);
                        strArray[strArray.GetUpperBound(0)] = tmp.Trim();

                        s = PString.SafeSubstring(s, i - cnt + separator[index].Length);
                        if (strArray.Length + 1 == count) { // If the count has been reached
                            PArray.IncrementArray(ref strArray);
                            strArray[strArray.GetUpperBound(0)] = s.Trim();
                            return strArray;
                        }
                        else {
                            i += separator[index].Length - 1;
                            cnt += tmp.Length + separator[index].Length;
                        }
                    }
                }
                return strArray;
            }
        }

        private static string[] SplitAddDelimiterRemoveEmpty(string s, string[] separator, int count, SkipStringStream skip) {
            if (count < 0) {
                throw new ArgumentOutOfRangeException("count", "count" + " cannot be less than zero.");
            }
            else if (count == 0 || s.IsEmpty()) {
                return new string[] { };
            }
            else if (count == 1) {
                return new string[] { s };
            }
            else {
                string[] strArray = new string[] { };
                int cnt = 0;
                int index = 0;
                int len = s.Length - MinLength(separator) + 1;
                bool check;
                for (int i = 0; i <= len; i += skip.LastSkipLength) {
                    check = skip.Skip(s, i - cnt);

                    if (i == len) {
                        if (s.IsNotEmpty()) {
                            PArray.IncrementArray(ref strArray);
                            strArray[strArray.GetUpperBound(0)] = s;
                        }
                    } // If not being skipped and is a seperator
                    else if (check == false && PMath.SetValue(ref index, FindIndexIn(s, separator, i - cnt)) >= 0) {
                        string tmp = PString.SafeSubstring(s, 0, i - cnt);
                        if (tmp.IsNotEmpty()) {
                            PArray.IncrementArray<string>(ref strArray);
                            strArray[strArray.GetUpperBound(0)] = tmp;
                            PArray.IncrementArray<string>(ref strArray);
                            strArray[strArray.GetUpperBound(0)] = separator[index];
                        }
                        s = PString.SafeSubstring(s, i - cnt + separator[index].Length);
                        if (strArray.Length + 1 == count) { // If the count has been reached
                            if (s.IsNotEmpty()) {
                                PArray.IncrementArray(ref strArray);
                                strArray[strArray.GetUpperBound(0)] = s;
                            }
                            return strArray;
                        }
                        else {
                            i += separator[index].Length - 1;
                            cnt += tmp.Length + separator[index].Length;
                        }
                    }
                }
                return strArray;
            }
        }
        private static string[] SplitKeepDelimiterRemoveEmpty(string s, string[] separator, int count, SkipStringStream skip) {
            if (count < 0) {
                throw new ArgumentOutOfRangeException("count", "count" + " cannot be less than zero.");
            }
            else if (count == 0 || s.IsEmpty()) {
                return new string[] { };
            }
            else if (count == 1) {
                return new string[] { s };
            }
            else {
                string[] strArray = new string[] { };
                int cnt = 0;
                int index = 0;
                int len = s.Length - MinLength(separator) + 1;
                bool check;
                for (int i = 0; i <= len; i += skip.LastSkipLength) {
                    check = skip.Skip(s, i - cnt);

                    if (i == len) {
                        if (s.IsNotEmpty()) {
                            PArray.IncrementArray(ref strArray);
                            strArray[strArray.GetUpperBound(0)] = s;
                        }
                    } // If not being skipped and is a seperator
                    else if (check == false && PMath.SetValue(ref index, FindIndexIn(s, separator, i - cnt)) >= 0) {
                        string tmp = PString.SafeSubstring(s, 0, i - cnt);
                        if (tmp.IsNotEmpty()) {
                            PArray.IncrementArray<string>(ref strArray);
                            strArray[strArray.GetUpperBound(0)] = tmp + separator[index];
                        }
                        s = PString.SafeSubstring(s, i - cnt + separator[index].Length);
                        if (strArray.Length + 1 == count) { // If the count has been reached
                            if (s.IsNotEmpty()) {
                                PArray.IncrementArray(ref strArray);
                                strArray[strArray.GetUpperBound(0)] = s;
                            }
                            return strArray;
                        }
                        else {
                            i += separator[index].Length - 1;
                            cnt += tmp.Length + separator[index].Length;
                        }
                    }
                }
                return strArray;
            }
        }
        private static string[] SplitRemoveEmpty(string s, string[] separator, int count, SkipStringStream skip) {
            if (count < 0) {
                throw new ArgumentOutOfRangeException("count", "count" + " cannot be less than zero.");
            }
            else if (count == 0 || s.IsEmpty()) {
                return new string[] { };
            }
            else if (count == 1) {
                return new string[] { s };
            }
            else {
                string[] strArray = new string[] { };
                int cnt = 0;
                int index = 0;
                int len = s.Length - MinLength(separator) + 1;
                bool check;
                for (int i = 0; i <= len; i += skip.LastSkipLength) {
                    check = skip.Skip(s, i - cnt);

                    if (i == len) {
                        if (s.IsNotEmpty()) {
                            PArray.IncrementArray(ref strArray);
                            strArray[strArray.GetUpperBound(0)] = s;
                        }
                    } // If not being skipped and is a seperator
                    else if (check == false && PMath.SetValue(ref index, FindIndexIn(s, separator, i - cnt)) >= 0) {
                        string tmp = PString.SafeSubstring(s, 0, i - cnt);
                        if (tmp.IsNotEmpty()) {
                            PArray.IncrementArray<string>(ref strArray);
                            strArray[strArray.GetUpperBound(0)] = tmp;
                        }
                        s = PString.SafeSubstring(s, i - cnt + separator[index].Length);
                        if (strArray.Length + 1 == count) { // If the count has been reached
                            if (s.IsNotEmpty()) {
                                PArray.IncrementArray(ref strArray);
                                strArray[strArray.GetUpperBound(0)] = s;
                            }
                            return strArray;
                        }
                        else {
                            i += separator[index].Length - 1;
                            cnt += tmp.Length + separator[index].Length;
                        }
                    }
                }
                return strArray;
            }
        }

        private static string[] SplitAddDelimiter(string s, string[] Separator, int count, SkipStringStream skip) {
            if (count < 0) {
                throw new ArgumentOutOfRangeException("count", "count" + " cannot be less than zero.");
            }
            else if (count == 0 || s.IsEmpty()) {
                return new string[] { };
            }
            else if (count == 1) {
                return new string[] { s };
            }
            else {
                string[] strArray = new string[] { };
                int cnt = 0;
                int index = 0;
                int len = s.Length - MinLength(Separator) + 1;
                bool check;
                for (int i = 0; i <= len; i += skip.LastSkipLength) {
                    check = skip.Skip(s, i - cnt);

                    if (i == len) {
                        PArray.IncrementArray(ref strArray);
                        strArray[strArray.GetUpperBound(0)] = s;
                    } // If not being skipped and is a seperator
                    else if (check == false && PMath.SetValue(ref index, FindIndexIn(s, Separator, i - cnt)) >= 0) {
                        string tmp = PString.SafeSubstring(s, 0, i - cnt);

                        PArray.IncrementArray<string>(ref strArray);
                        strArray[strArray.GetUpperBound(0)] = tmp;
                        PArray.IncrementArray<string>(ref strArray);
                        strArray[strArray.GetUpperBound(0)] = Separator[index];

                        s = PString.SafeSubstring(s, i - cnt + Separator[index].Length);
                        if (strArray.Length + 1 == count) { // If the count has been reached
                            PArray.IncrementArray(ref strArray);
                            strArray[strArray.GetUpperBound(0)] = s;
                            return strArray;
                        }
                        else {
                            i += Separator[index].Length - 1;
                            cnt += tmp.Length + Separator[index].Length;
                        }
                    }
                }
                return strArray;
            }
        }
        private static string[] SplitKeepDelimiter(string s, string[] separator, int count, SkipStringStream skip) {
            if (count < 0) {
                throw new ArgumentOutOfRangeException("count", "count" + " cannot be less than zero.");
            }
            else if (count == 0 || s.IsEmpty()) {
                return new string[] { };
            }
            else if (count == 1) {
                return new string[] { s };
            }
            else {
                string[] strArray = new string[] { };
                int cnt = 0;
                int index = 0;
                int len = s.Length - MinLength(separator) + 1;
                bool check;
                for (int i = 0; i <= len; i += skip.LastSkipLength) {
                    check = skip.Skip(s, i - cnt);

                    if (i == len) {
                        PArray.IncrementArray(ref strArray);
                        strArray[strArray.GetUpperBound(0)] = s;
                    } // If not being skipped and is a seperator
                    else if (check == false && PMath.SetValue(ref index, FindIndexIn(s, separator, i - cnt)) >= 0) {
                        string tmp = PString.SafeSubstring(s, 0, i - cnt);

                        PArray.IncrementArray<string>(ref strArray);
                        strArray[strArray.GetUpperBound(0)] = tmp + separator[index];

                        s = PString.SafeSubstring(s, i - cnt + separator[index].Length);
                        if (strArray.Length + 1 == count) { // If the count has been reached
                            PArray.IncrementArray(ref strArray);
                            strArray[strArray.GetUpperBound(0)] = s;
                            return strArray;
                        }
                        else {
                            i += separator[index].Length - 1;
                            cnt += tmp.Length + separator[index].Length;
                        }
                    }
                }
                return strArray;
            }
        }
        private static string[] SplitBasic(string s, string[] separator, int count, SkipStringStream skip) {
            if (count < 0) {
                throw new ArgumentOutOfRangeException("count", "count" + " cannot be less than zero.");
            }
            else if (count == 0 || s.IsEmpty()) {
                return new string[] { };
            }
            else if (count == 1) {
                return new string[] { s };
            }
            else {
                string[] strArray = new string[] { };
                int cnt = 0;
                int index = 0;
                int len = s.Length - MinLength(separator) + 1;
                bool check;
                for (int i = 0; i <= len; i += skip.LastSkipLength) {
                    check = skip.Skip(s, i - cnt);

                    if (i == len) {
                        PArray.IncrementArray(ref strArray);
                        strArray[strArray.GetUpperBound(0)] = s;
                    } // If not being skipped and is a seperator
                    else if (check == false && PMath.SetValue(ref index, FindIndexIn(s, separator, i - cnt)) >= 0) {
                        string tmp = PString.SafeSubstring(s, 0, i - cnt);

                        PArray.IncrementArray<string>(ref strArray);
                        strArray[strArray.GetUpperBound(0)] = tmp;

                        s = PString.SafeSubstring(s, i - cnt + separator[index].Length);
                        if (strArray.Length + 1 == count) { // If the count has been reached
                            PArray.IncrementArray(ref strArray);
                            strArray[strArray.GetUpperBound(0)] = s;
                            return strArray;
                        }
                        else {
                            i += separator[index].Length - 1;
                            cnt += tmp.Length + separator[index].Length;
                        }
                    }
                }
                return strArray;
            }
        }
        #endregion
        #region Misc
        private static StringSplitOptions ToSplitOptions(SplitOptions options) {
            if (Bit.IsIn(options, SplitOptions.RemoveEmptyEntries)) {
                return StringSplitOptions.RemoveEmptyEntries;
            }
            else {
                return StringSplitOptions.None;
            }
        }
        #endregion        
    }
}
