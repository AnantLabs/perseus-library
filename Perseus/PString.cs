using System;
using System.Globalization;

namespace Perseus {
    public static partial class PString {
        #region Safe Substring
        /// <summary>
        /// Retrieves a substring from the given string. This substring starts at a specified character position and has a length of 1.
        /// </summary>
        /// <param name="s">The string to get a substring from.</param>
        /// <param name="index">The index of the start of the substring.</param>
        /// <returns>A <seealso cref="System.String"/> equivalent to the substring of length 1 that begins at Index of s.</returns>
        public static string CharAt(this string s, int index) {
            return s.Substring(index, 1);
        }
        /// <summary>
        /// Retrieves a substring from the given string. This substring starts and ends at specified character positions.
        /// </summary>
        /// <param name="s">The string to get a substring from.</param>
        /// <param name="startIndex">The index of the start of the substring.</param>
        /// <param name="endIndex">The index of the end of the substring.</param>
        /// <returns>>A <seealso cref="System.String"/> equivalent to the substring that begins at StartIndex and ends at EndIndex.</returns>
        public static string Cut(this string s, int startIndex, int endIndex) {
            return s.Substring(startIndex, endIndex - startIndex + 1);
        }
        /// <summary>
        /// Retrieves a substring from the given string. The substring starts at a specified character position and has a specified length.
        /// </summary>
        /// <param name="s">The string to get a substring from.</param>
        /// <param name="startIndex">The index of the start of the substring.</param>
        /// <param name="length">The number of characters in the substring.</param>
        /// <returns>A <seealso cref="System.String"/> equivalent to the substring of length Length that begins at StartIndex of the string.</returns>
        /// <remarks>This function is called safe because it has error checking and will return an empty string or a string 
        /// of a shorter length then specified if unable to retrieve a regular substring.</remarks>
        public static string SafeSubstring(this string s, int startIndex, int length) {
            startIndex = Math.Max(0, startIndex);
            length = Math.Max(0, length);

            if (s.Length < startIndex) {
                return string.Empty;
            }
            else if (s.Length < (startIndex + length)) {
                return s.Substring(startIndex);
            }
            else {
                return s.Substring(startIndex, length);
            }
        }
        /// <summary>
        /// Retrieves a substring from given string. The substring starts at a specified character position.
        /// <remarks>This function is called safe because it has error checking and will return an empty string 
        /// if unable to retrieve a regular substring.
        /// </remarks>
        /// </summary>
        /// <param name="s">The string to get a substring from.</param>
        /// <param name="startIndex">The index of the start of the substring.</param>
        /// <returns>A <see cref="System.String"/> equivalent to the substring that begins at StartIndex of the string.</returns>        
        public static string SafeSubstring(this string s, int startIndex) {
            startIndex = Math.Max(0, startIndex);

            if (s.Length < startIndex) {
                return string.Empty;
            }
            else {
                return s.Substring(startIndex);
            }
        }
        #endregion
        #region Starts With
        public static bool StartsWith(this string s, string[] values) {
            return PString.StartsWith(s, values, false, null);
        }
        public static bool StartsWith(this string s, string[] values, StringComparison comparisonType) {
            foreach (string value in values) {
                if (s.StartsWith(value, comparisonType)) {
                    return true;
                }
            }

            return false;
        }
        public static bool StartsWith(this string s, string[] values, bool ignoreCase, CultureInfo culture) {
            foreach (string value in values) {
                if (s.StartsWith(value, ignoreCase, culture)) {
                    return true;
                }
            }

            return false;
        }
        #endregion
        #region Ends With
        public static bool EndsWith(this string s, string[] values) {
            return PString.EndsWith(s, values, false, null);
        }
        public static bool EndsWith(this string s, string[] values, StringComparison comparisonType) {
            foreach (string value in values) {
                if (s.EndsWith(value, comparisonType)) {
                    return true;
                }
            }

            return false;
        }
        public static bool EndsWith(this string s, string[] values, bool ignoreCase, CultureInfo culture) {
            foreach (string value in values) {
                if (s.EndsWith(value, ignoreCase, culture)) {
                    return true;
                }
            }

            return false;
        }
        #endregion
        #region Find Index In
        /// <summary>
        /// Reports the index of the longest first occurrence of the specified string in the specified string <seealso cref="System.Array"/>.
        /// </summary>
        /// <param name="s">The <seealso cref="System.String"/> to check.</param>
        /// <param name="values">The <seealso cref="System.Array"/> to match to.</param>
        /// <returns>The index position in the <seealso cref="System.Array"/> of the specified <seealso cref="System.String"/> if it is found, or -1 if it is not.</returns>
        public static int FindIndexIn(this string s, string[] values) {
            return FindIndexIn(s, values, 0, false, null);
        }
        /// <summary>
        /// Reports the index of the longest first occurrence of the specified string in the specified string <seealso cref="System.Array"/>.
        /// </summary>
        /// <param name="s">The <seealso cref="System.String"/> to check.</param>
        /// <param name="startIndex">The index of the start of the specified <seealso cref="System.String"/>.</param>
        /// <param name="values">The <seealso cref="System.Array"/> to match to.</param>
        /// <returns>The index position in the <seealso cref="System.Array"/> of the specified <seealso cref="System.String"/> if it is found, or -1 if it is not.</returns>
        public static int FindIndexIn(this string s, string[] values, int startIndex) {            
            return FindIndexIn(s, values, startIndex, false, null);
        }
        public static int FindIndexIn(this string s, string[] values, int startIndex, StringComparison comparisonType) {
            int index = -1;
            
            for (int i = 0; i < values.Length; i++) {
                if (string.Compare(values[i], SafeSubstring(s, startIndex, values[i].Length), comparisonType) == 0) {
                    if (index == -1 || values[index].Length < values[i].Length) {
                        index = i;
                    }
                }
            }

            return index;
        }
        public static int FindIndexIn(this string s, string[] values, int startIndex, bool ignoreCase, CultureInfo culture) {
            int index = -1;

            if (culture == null) {
                culture = CultureInfo.CurrentCulture;
            }

            for (int i = 0; i < values.Length; i++) {                
                if (string.Compare(values[i], SafeSubstring(s, startIndex, values[i].Length), ignoreCase, culture) == 0) {
                    if (index == -1 || values[index].Length < values[i].Length) {
                        index = i;
                    }
                }
            }

            return index;
        }
        #endregion
        #region IsIn
        public static bool IsIn(this string s, string[] values) {
            return PString.IsIn(s, values, false);
        }
        public static bool IsIn(this string s, string[] values, bool ignoreCase) {
            return (PString.IndexIn(s, values, ignoreCase) >= 0);
        }
        #endregion
        #region IndexIn
        public static int IndexIn(this string s, string[] values) {
            return PString.IndexIn(s, values, false);
        }
        public static int IndexIn(this string s, string[] values, bool ignoreCase) {
            StringComparison sc = (ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
            for (int i = 0; i < values.Length; i++) {
                if (s.Is(values[i])) { return i; }
            }

            return -1;
        }
        #endregion
        public static bool ContainsAny(this string s, string[] values) {
            foreach (string str in values) {
                if (s.Contains(str)) {
                    return true;
                }
            }

            return false;
        }
        public static bool ContainsAny(this string s, char[] values) {
            foreach (char c in values) {
                if (s.IndexOf(c) >= 0) {
                    return true;
                }
            }

            return false;
        }
        #region Enclosed With
        public static bool EnclosedWith(this string s, string startValue, string endValue) {
            return PString.EnclosedWith(s, startValue, endValue, false, null);
        }
        public static bool EnclosedWith(this string s, string startValue, string endValue, StringComparison comparisonType) {
            if (s.StartsWith(startValue, comparisonType) && s.EndsWith(endValue, comparisonType)) {
                return true;
            }

            return false;
        }
        public static bool EnclosedWith(this string s, string startValue, string endValue, bool ignoreCase, CultureInfo culture) {
            if (s.StartsWith(startValue, ignoreCase, culture) && s.EndsWith(endValue, ignoreCase, culture)) {
                return true;
            }
            
            return false;
        }
        #endregion
        #region Length
        /// <summary>
        /// Gets the length of longest string in an array.
        /// </summary>
        /// <param name="s">A <seealso cref="System.Array"/> of strings.</param>
        /// <returns>The length of the longest string in the array.</returns>
        public static int MaxLength(string[] s) {
            int l = s[0].Length;
            for (int i = 1; i <= s.GetUpperBound(0); i++) {
                if (s[i].Length > l) { l = s[i].Length; }
            }
            return l;
        }
        /// <summary>
        /// Gets the length of shortest string in an array.
        /// </summary>
        /// <param name="s">A <seealso cref="System.Array"/> of strings.</param>
        /// <returns>The length of the shortest string in the array.</returns>
        public static int MinLength(string[] s) {
            int l = s[0].Length;
            for (int i = 1; i < s.Length; i++) {
                if (s[i].Length < l) { l = s[i].Length; }
            }
            return l;
        }
        /// <summary>
        /// Calculates the length of the string, excluding those between a SkipString.
        /// </summary>
        /// <param name="s">The string to get the length of.</param>
        /// <param name="Skip">The SkipString of the characters you want to skip over.</param>
        /// <returns></returns>
        public static int SkipLength(this string s, SkipStringStream skip) {
            int Length = 0;
            for (int i = 0; i < s.Length; i += skip.LastSkipLength) {
                if (skip.Skip(s, i) == false) {
                    Length++;
                }
            }
            return Length;
        }
        public static string[] RemoveLong(string[] s, int length) {
            for (int i = s.Length - 1; i >= 0; i--) {
                if (s[i].Length > length) {
                    PArray.RemoveItem(ref s, i);
                }
            }
            return s;
        }
        public static string[] RemoveShort(string[] s, int length) {
            for (int i = s.Length - 1; i >= 0; i--) {
                if (s[i].Length < length) {
                    PArray.RemoveItem(ref s, i);
                }
            }
            return s;
        }
        #endregion
        #region Is
        public static bool Is(this string strA, string strB) {
            return PString.Is(strA, strB, false);
        }
        public static bool Is(this string strA, string strB, bool ignoreCase) {
            StringComparison sc = (ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
            if (string.Compare(strA, strB, sc) == 0) {
                return true;
            }
            
            return false;            
        }
        public static bool IsNot(this string strA, string strB) {
            return PString.IsNot(strA, strB, false);            
        }
        public static bool IsNot(this string strA, string strB, bool ignoreCase) {
            return !PString.Is(strA, strB, ignoreCase);                        
        }
        public static bool IsEmpty(this string s) {
            return (PString.Is(s, string.Empty) || s == null);
        }
        public static bool IsNotEmpty(this string s) {
            return PString.IsNot(s, string.Empty);
        }
        #endregion
        #region Is Type
        public static bool IsBoolean(this string s) {
            if (s.Is("1") ||
                s.Is("0") ||
                s.Is(bool.TrueString, false) ||
                s.Is(bool.FalseString, false)) {
                return true;
            }
            
            return false;            
        }
        /// <summary>
        /// Determines if the string is numerical or not.
        /// </summary>
        /// <param name="s">A <seealso cref="System.String"/>.</param>
        /// <returns>True if the string is numerical; otherwise false.</returns>
        public static bool IsNumeric(this string s) {
            if (s.IsEmpty()) { return false; }

            bool isDec = false;
            int start;

            if (s.Substring(0, 1).Is("-")) {
                if (s.Length > 1) {
                    start = 1;
                }
                else {
                    return false;
                }
            }
            else {
                start = 0;
            }

            for (int i = start; i < s.Length; i++) {
                if (s.Substring(i, 1).Is(".")) {
                    if (isDec == false && s.Length > 1) {
                        isDec = true;
                    }
                    else {
                        return false;
                    }
                }
                else if (!PString.IsNumber(s[i])) {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// Determines if the string is numerical or not.
        /// </summary>
        /// <param name="s">A <seealso cref="System.String"/>.</param>
        /// <param name="allowDecimal">Allow decimals in the numeric value.</param>
        /// <param name="allowNegative">Allow the numeric value to be negative.</param>
        /// <returns>True if the string is numerical; otherwise false.</returns>
        public static bool IsNumeric(this string s, bool allowDecimal, bool allowNegative) {            
            if (s.IsEmpty()) { return false; }

            bool isDec = false;
            int start;

            if (s.Substring(0, 1).Is("-")) {
                if (allowNegative == true && s.Length > 1) {
                    start = 1;
                }
                else {
                    return false;
                }
            }
            else {
                start = 0;
            }

            for (int i = start; i < s.Length; i++) {
                if (s.Substring(i, 1) == ".") {
                    if (allowDecimal == true && isDec == false && s.Length > start + 1) {
                        isDec = true;
                    }
                    else {
                        return false;
                    }
                }
                else if (!PString.IsNumber(s[i])) {
                    return false;
                }
            }
            return true;
        }
        public static bool IsNumber(char c) {
            if (c < 48 || c > 57) {
                return false;
            }
            
            return true;
        }
        public static bool IsHex(this string s) {
            for (int i = 0; i < s.Length; i++) {
                if (!char.IsDigit(s, i) &&
                    s.Substring(i, 1).ToLower().IsNot("a") &&
                    s.Substring(i, 1).ToLower().IsNot("b") &&
                    s.Substring(i, 1).ToLower().IsNot("c") &&
                    s.Substring(i, 1).ToLower().IsNot("d") &&
                    s.Substring(i, 1).ToLower().IsNot("e") &&
                    s.Substring(i, 1).ToLower().IsNot("f")) {
                    return false;
                }
            }
            return true;
        }
        #endregion
        #region Set Value
        /// <summary>
        /// Sets the referenced string to the given value as well as returns it.
        /// </summary>
        /// <param name="s">The string to set the new value to.</param>
        /// <param name="Value">The value to set the string to.</param>
        /// <returns>Returns the value.</returns>
        [Obsolete]
        public static string SetValue(ref string s, string value) {
            s = value;
            return value;
        }
        #endregion
        #region Join
        /// <summary>
        /// Returns a <seealso cref=System.String"/> of all elements of the specified <seealso cref="System.Array"/> joined together.
        /// </summary>
        /// <param name="s">A <seealso cref=System.String"/> <seealso cref="System.Array"/> to join together.</param>
        /// <returns>A <seealso cref=System.String"/> of all elements of the specified <seealso cref="System.Array"/> joined together.</returns>
        public static string Join(this string[] s) {            
            return PString.Join(s, string.Empty, 0, 0);
        }
        /// <summary>
        /// Returns a <seealso cref=System.String"/> of all elements of the specified <seealso cref="System.Array"/> joined together.
        /// </summary>
        /// <param name="s">A <seealso cref=System.String"/> <seealso cref="System.Array"/> to join together.</param>
        /// <param name="Separator">A <seealso cref=System.String"/> to add inbetween each joined string.</param>
        /// <returns>A <seealso cref=System.String"/> of all elements of the specified <seealso cref="System.Array"/> joined together.</returns>
        public static string Join(this string[] s, string separator) {
            return PString.Join(s, separator, 0, 0);
        }
        /// <summary>
        /// Returns a <seealso cref=System.String"/> of a specified number of elements of the specified <seealso cref="System.Array"/> joined together.
        /// </summary>
        /// <param name="s">A <seealso cref=System.String"/> <seealso cref="System.Array"/> to join together.</param>
        /// <param name="StartIndex">The start index of the <seealso cref="System.Array"/>.</param>
        /// <param name="Count">The number of times to join strings.</param>
        /// <returns>A <seealso cref=System.String"/> of Count elements of the specified <seealso cref="System.Array"/> joined together.</returns>
        public static string Join(this string[] s, int startIndex, int count) {
            return PString.Join(s, string.Empty, startIndex, count);
        }
        /// <summary>
        /// Returns a <seealso cref=System.String"/> of a specified number of elements of the specified <seealso cref="System.Array"/> joined together.
        /// </summary>
        /// <param name="s">A <seealso cref=System.String"/> <seealso cref="System.Array"/> to join together.</param>
        /// <param name="StartIndex">The start index of the <seealso cref="System.Array"/>.</param>
        /// <param name="Separator">A <seealso cref=System.String"/> to add inbetween each joined string.</param>
        /// <param name="Count">The number of times to join strings.</param>
        /// <returns>A <seealso cref=System.String"/> of Count elements of the specified <seealso cref="System.Array"/> joined together.</returns>
        public static string Join(this string[] s, string separator, int startIndex, int count) {
            return String.Join(separator, s, startIndex, s.Length - startIndex);
            //string tmp = string.Empty;
            //int n = (count < 1 ? Math.Min(count, s.GetUpperBound(0)) : s.GetUpperBound(0));
            //for (int i = startIndex; i <= n; i++) {
            //    if (i == n) {
            //        tmp += s[i];
            //    }
            //    else {
            //        tmp += s[i] + separator;
            //    }
            //}
            //return tmp;
        }
        #endregion
        public static string SeparateWords(this string s) {
            return PString.SeparateWords(s, " ");
        }
        public static string SeparateWords(this string s, string separator) {
            if (s == string.Empty) {
                return string.Empty;
            }

            string result = s.Substring(0, 1);

            for (int i = 1; i < s.Length; i++) {
                string c = s[i].ToString();
                if (c == c.ToUpperInvariant()) {
                    result += separator;
                }
                result += c;
            }

            return result;
        }
    }
}
