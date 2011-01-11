using System;

namespace Perseus {
    /// <summary>
    /// Specifies whether applicable <see cref="Overload:Perseus.PString.Split"/> method overloads include
    /// or omit empty substrings from the return value as well as keep the delimiter at the end of each substring.
    /// </summary>
    [Flags]
    public enum SplitOptions {
        /// <summary>
        /// The return value includes array elements that contain an empty string and the delimiters are removed.
        /// </summary>
        None = 0,
        /// <summary>
        /// The return value does not include array elements that contain an empty string.
        /// </summary>
        RemoveEmptyEntries = 1,
        /// <summary>
        /// The strings in the array keep the delimiter at the end of the string.
        /// </summary>
        KeepDelimiter = 2,
        /// <summary>
        /// The delimeter gets its own index in the array.
        /// </summary>
        AddDelimiterToArray = 4,
        /// <summary>
        /// The return value does not include array elements if the string to split is empty.
        /// </summary>
        RemoveEmptySplitString = 8,
        /// <summary>
        /// The return value's array elements are trimmed of whitespace.
        /// </summary>
        Trim = 16,
        /// <summary>
        /// The return value's array elements are all unique.
        /// </summary>
        RemoveDuplicates = 32
    }
}
