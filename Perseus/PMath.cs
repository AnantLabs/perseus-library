using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Perseus {
    public static class PMath {
        #region Degrees and Radians
        private const double DegreesToRadians = Math.PI / 180d;
        private const double RadiansToDegrees = 180d / Math.PI;

        public static double ToRadians(this int degrees) {
            return degrees * PMath.DegreesToRadians;
        }
        public static double ToRadians(this float degrees) {
            return degrees * PMath.DegreesToRadians;
        }
        public static double ToRadians(this double degrees) {
            return degrees * PMath.DegreesToRadians;
        }

        public static double ToDegrees(this int radians) {
            return radians * PMath.RadiansToDegrees;
        }
        public static double ToDegrees(this float radians) {
            return radians * PMath.RadiansToDegrees;
        }
        public static double ToDegrees(this double radians) {
            return radians * PMath.RadiansToDegrees;
        }
        #endregion
        #region Max Min
        public static byte MaxMin(this byte value, byte min, byte max) {
            if (value < min) { return min; }
            else if (value > max) { return max; }
            else { return value; }
        }
        public static short MaxMin(this short value, short min, short max) {
            if (value < min) { return min; }
            else if (value > max) { return max; }
            else { return value; }
        }
        public static int MaxMin(this int value, int min, int max) {
            if (value < min) { return min; }
            else if (value > max) { return max; }
            else { return value; }
        }
        public static float MaxMin(this float value, float min, float max) {
            if (value < min) { return min; }
            else if (value > max) { return max; }
            else { return value; }
        }
        public static double MaxMin(this double value, double min, double max) {
            if (value < min) { return min; }
            else if (value > max) { return max; }
            else { return value; }
        }
        public static decimal MaxMin(this decimal value, decimal min, decimal max) {
            if (value < min) { return min; }
            else if (value > max) { return max; }
            else { return value; }
        }
        #endregion
        #region SetValue
        /// <summary>
        /// Sets the given referenced integer to the given value.
        /// </summary>
        /// <param name="n">The integer to set the value to.</param>
        /// <param name="Value">The value to set the integer to.</param>
        /// <returns>Returns the value.</returns>        
        public static int SetValue(ref int n, int value) {
            n = value;
            return value;
        }
        /// <summary>
        /// Sets the given referenced integer to the given value if it is greater then the given lowest value.
        /// </summary>
        /// <param name="n">The integer to set the value to.</param>
        /// <param name="Value">The value to set the integer to.</param>
        /// <param name="Low">The value must be greater then this one to get set to the integer.</param>
        /// <returns>Returns the value.</returns>
        public static int SetValueAboveLow(ref int n, int value, int low) {
            if (value > low) {
                n = value;
            }
            return value;
        }
        /// <summary>
        /// Sets the given referenced integer to the given value if it is less then the given highest value.
        /// </summary>
        /// <param name="n">The integer to set the value to.</param>
        /// <param name="Value">The value to set the integer to.</param>
        /// <param name="High">The value must be less then this one to get set to the integer.</param>
        /// <returns>Returns the value.</returns>
        public static int SetValueBelowHigh(ref int n, int value, int high) {
            if (value < high) {
                n = value;
            }
            return value;
        }
        #endregion        
        #region String Rounding
        public static string AddLeadingZeros(string s, int length) {
            if (s.Length >= length) {
                return s;
            }
            else {
                return s.PadLeft(length, '0');
            }
        }
        public static string AddLeadingZeros(int n, int length) {
            string s = n.ToString();
            return AddLeadingZeros(s, length);
        }
        public static string AddTrailingDecimals(string s, int decimals) {
            if (decimals < 0) { throw new ArgumentOutOfRangeException("decimals"); }
            else if (decimals == 0) { return s; }

            int tmp = s.LastIndexOf(".", StringComparison.Ordinal);
            if (tmp == -1) {
                s += ".";
                tmp = s.Length + decimals;
            }
            else {
                tmp = decimals + tmp + 1;
            }

            return s.PadRight(tmp, '0');
        }
        public static string AddTrailingDecimals(float n, int decimals) {
            string s = n.ToString();
            return PMath.AddTrailingDecimals(s, decimals);
        }
        public static string AddTrailingDecimals(double n, int decimals) {
            string s = n.ToString();
            return PMath.AddTrailingDecimals(s, decimals);
        }

        public static string StringRound(this float value, int decimals) {
            return PMath.StringRound(System.Convert.ToDouble(value), decimals);
        }
        public static string StringRound(this double value, int decimals) {
            double tmp = Math.Round(value, decimals, MidpointRounding.AwayFromZero);
            return PMath.AddTrailingDecimals(tmp, decimals);
        }
        public static string StringRound(this decimal value, int decimals) {
            double tmp = (double)Math.Round(value, decimals, MidpointRounding.AwayFromZero);
            return PMath.AddTrailingDecimals(tmp, decimals);
        }
        #endregion
        #region Is Between
        public static bool IsBetween(this byte value, byte low, byte high) {
            if (value >= low && value <= high) {
                return true;
            }

            return false;
        }
        public static bool IsBetween(this short value, short low, short high) {
            if (value >= low && value <= high) {
                return true;
            }

            return false;
        }
        public static bool IsBetween(this int value, int low, int high) {
            if (value >= low && value <= high) {
                return true;
            }
            
            return false;
        }
        public static bool IsBetween(this float value, float low, float high) {
            if (value >= low && value <= high) {
                return true;
            }

            return false;
        }
        public static bool IsBetween(this double value, double low, double high) {
            if (value >= low && value <= high) {
                return true;
            }

            return false;
        }
        public static bool IsBetween(this decimal value, decimal low, decimal high) {
            if (value >= low && value <= high) {
                return true;
            }

            return false;
        }        
        #endregion
    }
}
