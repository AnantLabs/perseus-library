using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Perseus {
    /// <summary>
    /// Specifies a bit mask.
    /// </summary>
    public enum BitMask {
        /// <summary>
        /// Specefies the low and high order bits are not set.
        /// </summary>
        Empty,
        /// <summary>
        /// Specefies the low order bit is set.
        /// </summary>
        LowOrderBit,
        /// <summary>
        /// Specefies the high order bit is set.
        /// </summary>
        HighOrderBit,
        /// <summary>
        /// Specefies the low and high order bits are set.
        /// </summary>
        HighAndLowOrderBit
    }
    /// <summary>
    /// Specifies the length of a bit pattern.
    /// </summary>
    public enum BitLength : int {
        /// <summary>
        /// Specefies the bit pattern is 4 values long. 
        /// </summary>
        Bit4 = 4,
        /// <summary>
        /// Specefies the bit pattern is 8 values long. 
        /// </summary>
        Bit8 = 8,
        /// <summary>
        /// Specefies the bit pattern is 16 values long. 
        /// </summary>
        Bit16 = 16,
        /// <summary>
        /// Specefies the bit pattern is 32 values long. 
        /// </summary>
        Bit32 = 32,
        /// <summary>
        /// Specefies the bit pattern is 64 values long. 
        /// </summary>
        Bit64 = 64
    }
    public static class Bit {
        #region Constants
        internal const ulong HIGH_ORDER_BIT_64 = 9223372036854775808;
        internal const ulong HIGH_ORDER_BIT_32 = 2147483648;
        internal const ulong HIGH_ORDER_BIT_16 = 32768;
        internal const ulong HIGH_ORDER_BIT_8 = 128;
        internal const ulong HIGH_ORDER_BIT_4 = 8;
        #endregion

        #region Flag Operations (long)
        /// <summary>
        /// Sets the given masked bits to 'on'.
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="Mask">A mask of bits.</param>
        public static void SetOn(ref long Flag, long Mask) {
            Flag |= Mask;
        }
        /// <summary>
        /// Sets the given masked bits to 'off'.
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="Mask">A mask of bits.</param>
        public static void SetOff(ref long Flag, long Mask) {
            Flag &= ~Mask;
        }
        /// <summary>
        /// Toggles the given masked bits in Flags.
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="Mask">A mask of bits.</param>
        public static void Toggle(ref long Flag, long Mask) {
            Flag ^= Mask;
        }
        /// <summary>
        /// Sets non intersecting bits to 'off'.
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="Mask">A mask of bits.</param>
        public static void GetIntersects(ref  long Flag, long Mask) {
            Flag &= Mask;
        }
        /// <summary>
        /// Sets intersecting bits to 'off'.
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="Mask">A mask of bits.</param>
        public static void GetDifference(ref long Flag, long Mask) {
            Flag |= Mask;
            Flag ^= Mask;
        }
        /// <summary>
        /// Determines if the given masked bits are "on".
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="Mask">A mask of bits.</param>
        /// <returns>True if the given masked bits are "on"; otherwise false.</returns>
        public static bool IsIn(long Flag, long Mask) {
            return (Flag | Mask) == Flag;
        }
        /// <summary>
        /// Determines if any of the given masked bits are "on".
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="Mask">A mask of bits.</param>
        /// <returns>True if any of the given masked bits are "on"; otherwise false.</returns>
        public static bool AnyIsIn(long Flag, long Mask) {
            if ((Flag &= Mask) == 0) { return false; }
            else { return true; }
        }
        /// <summary>
        /// Determines which bits are "on".
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <returns>An array of values; one for each bit that is "on".</returns>
        public static long[] GetFlags(long Flag) {
            if (Flag <= 0) {
                return new long[] { 0 };
            }
            long[] cFlag = new long[0];
            while (true) {
                long c = (long)Math.Floor((double)(Flag + 1) / 2);
                long x = 1;
                while (true) {
                    if (c > x) {
                        x = x * 2;
                    }
                    else {
                        PArray.IncrementArray(ref cFlag);
                        cFlag[cFlag.GetUpperBound(0)] = x;
                        Flag -= x;
                        break;
                    }
                }
                if (Flag == 0) {
                    break;
                }
            }
            return cFlag;
        }
        /// <summary>
        /// Determines if the <seealso cref="Perseus.BitWise.BitMask"/> intersects with the given flag.
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="BitLength">The bit length</param>
        /// <param name="BitMask">A <seealso cref="Perseus.BitWise.BitMask"/> enumeration representing a bit mask.</param>
        /// <returns>True if the mask of bits intersects; otherwise false.</returns>
        public static bool IsBitOn(long Flag, BitLength BitLength, BitMask BitMask) {
            if (BitMask == BitMask.LowOrderBit) {
                return IsBitOn(Flag, 1);
            }

            ulong Mask = 0;
            //uint Flag = (uint) Value;
            switch (BitLength) {
                case BitLength.Bit4:
                    Mask = HIGH_ORDER_BIT_4;
                    break;
                case BitLength.Bit8:
                    Mask = HIGH_ORDER_BIT_8;
                    break;
                case BitLength.Bit16:
                    Mask = HIGH_ORDER_BIT_16;
                    break;
                case BitLength.Bit32:
                    Mask = HIGH_ORDER_BIT_32;
                    break;
                case BitLength.Bit64:
                    Mask = HIGH_ORDER_BIT_64;
                    break;
                default:
                    throw new Exception("Illegal enum value: " + BitLength.ToString());
            }

            if (BitMask == BitMask.HighOrderBit) {
                return IsBitOn(Flag, Mask);
            }
            else if (BitMask == BitMask.HighAndLowOrderBit) {
                ++Mask;
                return IsBitOn(Flag, Mask);
            }
            else if (BitMask == BitMask.Empty) {
                ++Mask;
                return !IsBitOn(Flag, Mask);
            }
            else {
                throw new Exception("Illegal enum value: " + BitMask.ToString());
            }
        }
        /// <summary>
        /// Determines if the <seealso cref="Perseus.BitWise.BitMask"/> intersects with the given flag.
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="BitMask">A <seealso cref="Perseus.BitWise.BitMask"/> enumeration representing a bit mask.</param>
        /// <returns>True if the mask of bits intersects; otherwise false.</returns>
        public static bool IsBitOn(long Flag, BitMask BitMask) {
            if (BitMask == BitMask.LowOrderBit) {
                return IsBitOn(Flag, 1);
            }

            ulong Mask = HIGH_ORDER_BIT_64;

            if (BitMask == BitMask.HighOrderBit) {
                return IsBitOn(Flag, Mask);
            }
            else if (BitMask == BitMask.HighAndLowOrderBit) {
                ++Mask;
                return IsBitOn(Flag, Mask);
            }
            else if (BitMask == BitMask.Empty) {
                ++Mask;
                return !IsBitOn(Flag, Mask);
            }
            else {
                throw new Exception("Illegal enum value: " + BitMask.ToString());
            }
        }
        /// <summary>
        /// Determines if the mask of bits intersects with the given flag.
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="Mask">A mask of bits</param>
        /// <returns>True if the mask of bits intersects; otherwise false.</returns>
        [CLSCompliant(false)]
        public static bool IsBitOn(long Flag, ulong Mask) {
            if (((ulong)Flag & Mask) == Mask) {
                return true;
            }
            else {
                return false;
            }
        }
        #endregion
        #region Flag Operations (int)
        /// <summary>
        /// Sets the given masked bits to 'on'.
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="Mask">A mask of bits.</param>
        public static void SetOn(ref int Flag, int Mask) {
            Flag |= Mask;
        }
        /// <summary>
        /// Sets the given masked bits to 'off'.
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="Mask">A mask of bits.</param>
        public static void SetOff(ref int Flag, int Mask) {
            Flag &= ~Mask;
        }
        /// <summary>
        /// Toggles the given masked bits in Flags.
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="Mask">A mask of bits.</param>
        public static void Toggle(ref int Flag, int Mask) {
            Flag ^= Mask;
        }
        /// <summary>
        /// Sets non intersecting bits to 'off'.
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="Mask">A mask of bits.</param>
        public static void GetIntersects(ref  int Flag, int Mask) {
            Flag &= Mask;
        }
        /// <summary>
        /// Sets intersecting bits to 'off'.
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="Mask">A mask of bits.</param>
        public static void GetDifference(ref int Flag, int Mask) {
            Flag |= Mask;
            Flag ^= Mask;
        }
        /// <summary>
        /// Determines if the given masked bits are "on".
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="Mask">A mask of bits.</param>
        /// <returns>True if the given masked bits are "on"; otherwise false.</returns>
        public static bool IsIn(int Flag, int Mask) {
            return (Flag | Mask) == Flag;
        }
        /// <summary>
        /// Determines if any of the given masked bits are "on".
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="Mask">A mask of bits.</param>
        /// <returns>True if any of the given masked bits are "on"; otherwise false.</returns>
        public static bool AnyIsIn(int Flag, int Mask) {
            if ((Flag &= Mask) == 0) { return false; }
            else { return true; }
        }
        /// <summary>
        /// Determines which bits are "on".
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <returns>An array of values; one for each bit that is "on".</returns>
        public static int[] GetFlags(int Flag) {
            if (Flag <= 0) {
                return new int[] { 0 };
            }
            int[] cFlag = new int[0];
            while (true) {
                int c = (int)Math.Floor((double)(Flag + 1) / 2);
                int x = 1;
                while (true) {
                    if (c > x) {
                        x = x * 2;
                    }
                    else {
                        PArray.IncrementArray(ref cFlag);
                        cFlag[cFlag.GetUpperBound(0)] = x;
                        Flag -= x;
                        break;
                    }
                }
                if (Flag == 0) {
                    break;
                }
            }
            return cFlag;
        }
        /// <summary>
        /// Determines if the <seealso cref="Perseus.BitWise.BitMask"/> intersects with the given flag.
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="BitLength">The bit length</param>
        /// <param name="BitMask">A <seealso cref="Perseus.BitWise.BitMask"/> enumeration representing a bit mask.</param>
        /// <returns>True if the mask of bits intersects; otherwise false.</returns>
        public static bool IsBitOn(int Flag, BitLength BitLength, BitMask BitMask) {
            if (BitMask == BitMask.LowOrderBit) {
                return IsBitOn(Flag, 1);
            }

            uint Mask = 0;
            //uint Flag = (uint) Value;
            switch (BitLength) {
                case BitLength.Bit4:
                    Mask = (uint)HIGH_ORDER_BIT_4;
                    break;
                case BitLength.Bit8:
                    Mask = (uint)HIGH_ORDER_BIT_8;
                    break;
                case BitLength.Bit16:
                    Mask = (uint)HIGH_ORDER_BIT_16;
                    break;
                case BitLength.Bit32:
                case BitLength.Bit64:
                    Mask = (uint)HIGH_ORDER_BIT_32;
                    break;
                default:
                    throw new Exception("Illegal enum value: " + BitLength.ToString());
            }

            if (BitMask == BitMask.HighOrderBit) {
                return IsBitOn(Flag, Mask);
            }
            else if (BitMask == BitMask.HighAndLowOrderBit) {
                ++Mask;
                return IsBitOn(Flag, Mask);
            }
            else if (BitMask == BitMask.Empty) {
                ++Mask;
                return !IsBitOn(Flag, Mask);
            }
            else {
                throw new Exception("Illegal enum value: " + BitMask.ToString());
            }
        }
        /// <summary>
        /// Determines if the <seealso cref="Perseus.BitWise.BitMask"/> intersects with the given flag.
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="BitMask">A <seealso cref="Perseus.BitWise.BitMask"/> enumeration representing a bit mask.</param>
        /// <returns>True if the mask of bits intersects; otherwise false.</returns>
        public static bool IsBitOn(int Flag, BitMask BitMask) {
            if (BitMask == BitMask.LowOrderBit) {
                return IsBitOn(Flag, 1);
            }

            uint Mask = (uint)HIGH_ORDER_BIT_32;

            if (BitMask == BitMask.HighOrderBit) {
                return IsBitOn(Flag, Mask);
            }
            else if (BitMask == BitMask.HighAndLowOrderBit) {
                ++Mask;
                return IsBitOn(Flag, Mask);
            }
            else if (BitMask == BitMask.Empty) {
                ++Mask;
                return !IsBitOn(Flag, Mask);
            }
            else {
                throw new Exception("Illegal enum value: " + BitMask.ToString());
            }
        }
        /// <summary>
        /// Determines if the mask of bits intersects with the given flag.
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="Mask">A mask of bits</param>
        /// <returns>True if the mask of bits intersects; otherwise false.</returns>
        [CLSCompliant(false)]
        public static bool IsBitOn(int Flag, uint Mask) {
            if ((Flag & Mask) == Mask) {
                return true;
            }
            else {
                return false;
            }
        }
        #endregion
        #region Flag Operations (short)
        /// <summary>
        /// Sets the given masked bits to 'on'.
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="Mask">A mask of bits.</param>
        public static void SetOn(ref short Flag, short Mask) {
            Flag |= Mask;
        }
        /// <summary>
        /// Sets the given masked bits to 'off'.
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="Mask">A mask of bits.</param>
        public static void SetOff(ref short Flag, short Mask) {
            Flag &= (short)(~(int)Mask);
        }
        /// <summary>
        /// Toggles the given masked bits in Flags.
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="Mask">A mask of bits.</param>
        public static void Toggle(ref short Flag, short Mask) {
            Flag ^= Mask;
        }
        /// <summary>
        /// Sets non intersecting bits to 'off'.
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="Mask">A mask of bits.</param>
        public static void GetIntersects(ref  short Flag, short Mask) {
            Flag &= Mask;
        }
        /// <summary>
        /// Sets intersecting bits to 'off'.
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="Mask">A mask of bits.</param>
        public static void GetDifference(ref short Flag, short Mask) {
            Flag |= Mask;
            Flag ^= Mask;
        }
        /// <summary>
        /// Determines if the given masked bits are "on".
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="Mask">A mask of bits.</param>
        /// <returns>True if the given masked bits are "on"; otherwise false.</returns>
        public static bool IsIn(short Flag, short Mask) {
            return (Flag | Mask) == Flag;
        }
        /// <summary>
        /// Determines if any of the given masked bits are "on".
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="Mask">A mask of bits.</param>
        /// <returns>True if any of the given masked bits are "on"; otherwise false.</returns>
        public static bool AnyIsIn(short Flag, short Mask) {
            if ((Flag &= Mask) == 0) { return false; }
            else { return true; }
        }
        /// <summary>
        /// Determines which bits are "on".
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <returns>An array of values; one for each bit that is "on".</returns>
        public static short[] GetFlags(short Flag) {
            if (Flag <= 0) {
                return new short[] { 0 };
            }
            short[] cFlag = new short[0];
            while (true) {
                short c = (short)Math.Floor((double)(Flag + 1) / 2);
                short x = 1;
                while (true) {
                    if (c > x) {
                        x = (short)((int)x * 2);
                    }
                    else {
                        PArray.IncrementArray(ref cFlag);
                        cFlag[cFlag.GetUpperBound(0)] = x;
                        Flag -= x;
                        break;
                    }
                }
                if (Flag == 0) {
                    break;
                }
            }
            return cFlag;
        }
        /// <summary>
        /// Determines if the <seealso cref="Perseus.BitWise.BitMask"/> intersects with the given flag.
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="BitLength">The bit length</param>
        /// <param name="BitMask">A <seealso cref="Perseus.BitWise.BitMask"/> enumeration representing a bit mask.</param>
        /// <returns>True if the mask of bits intersects; otherwise false.</returns>
        public static bool IsBitOn(short Flag, BitLength BitLength, BitMask BitMask) {
            if (BitMask == BitMask.LowOrderBit) {
                return IsBitOn(Flag, 1);
            }

            ushort Mask = 0;
            //uint Flag = (uint) Value;
            switch (BitLength) {
                case BitLength.Bit4:
                    Mask = (ushort)HIGH_ORDER_BIT_4;
                    break;
                case BitLength.Bit8:
                    Mask = (ushort)HIGH_ORDER_BIT_8;
                    break;
                case BitLength.Bit16:
                case BitLength.Bit32:
                case BitLength.Bit64:
                    Mask = (ushort)HIGH_ORDER_BIT_16;
                    break;
                default:
                    throw new Exception("Illegal enum value: " + BitLength.ToString());
            }

            if (BitMask == BitMask.HighOrderBit) {
                return IsBitOn(Flag, Mask);
            }
            else if (BitMask == BitMask.HighAndLowOrderBit) {
                ++Mask;
                return IsBitOn(Flag, Mask);
            }
            else if (BitMask == BitMask.Empty) {
                ++Mask;
                return !IsBitOn(Flag, Mask);
            }
            else {
                throw new Exception("Illegal enum value: " + BitMask.ToString());
            }
        }
        /// <summary>
        /// Determines if the <seealso cref="Perseus.BitWise.BitMask"/> intersects with the given flag.
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="BitMask">A <seealso cref="Perseus.BitWise.BitMask"/> enumeration representing a bit mask.</param>
        /// <returns>True if the mask of bits intersects; otherwise false.</returns>
        public static bool IsBitOn(short Flag, BitMask BitMask) {
            if (BitMask == BitMask.LowOrderBit) {
                return IsBitOn(Flag, 1);
            }

            ushort Mask = (ushort)HIGH_ORDER_BIT_16;

            if (BitMask == BitMask.HighOrderBit) {
                return IsBitOn(Flag, Mask);
            }
            else if (BitMask == BitMask.HighAndLowOrderBit) {
                ++Mask;
                return IsBitOn(Flag, Mask);
            }
            else if (BitMask == BitMask.Empty) {
                ++Mask;
                return !IsBitOn(Flag, Mask);
            }
            else {
                throw new Exception("Illegal enum value: " + BitMask.ToString());
            }
        }
        /// <summary>
        /// Determines if the mask of bits intersects with the given flag.
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="Mask">A mask of bits</param>
        /// <returns>True if the mask of bits intersects; otherwise false.</returns>
        [CLSCompliant(false)]
        public static bool IsBitOn(short Flag, ushort Mask) {
            if ((Flag & Mask) == Mask) {
                return true;
            }
            else {
                return false;
            }
        }
        #endregion
        #region Flag Operations (ulong)
        /// <summary>
        /// Sets the given masked bits to 'on'.
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="Mask">A mask of bits.</param>
        [CLSCompliant(false)]
        public static void SetOn(ref ulong Flag, ulong Mask) {
            Flag |= Mask;
        }
        /// <summary>
        /// Sets the given masked bits to 'off'.
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="Mask">A mask of bits.</param>
        [CLSCompliant(false)]
        public static void SetOff(ref ulong Flag, ulong Mask) {
            Flag &= ~Mask;
        }
        /// <summary>
        /// Toggles the given masked bits in Flags.
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="Mask">A mask of bits.</param>
        [CLSCompliant(false)]
        public static void Toggle(ref ulong Flag, ulong Mask) {
            Flag ^= Mask;
        }
        /// <summary>
        /// Sets non intersecting bits to 'off'.
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="Mask">A mask of bits.</param>
        [CLSCompliant(false)]
        public static void GetIntersects(ref  ulong Flag, ulong Mask) {
            Flag &= Mask;
        }
        /// <summary>
        /// Sets intersecting bits to 'off'.
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="Mask">A mask of bits.</param>
        [CLSCompliant(false)]
        public static void GetDifference(ref ulong Flag, ulong Mask) {
            Flag |= Mask;
            Flag ^= Mask;
        }
        /// <summary>
        /// Determines if the given masked bits are "on".
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="Mask">A mask of bits.</param>
        /// <returns>True if the given masked bits are "on"; otherwise false.</returns>
        [CLSCompliant(false)]
        public static bool IsIn(ulong Flag, ulong Mask) {
            return (Flag | Mask) == Flag;
        }
        /// <summary>
        /// Determines if any of the given masked bits are "on".
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="Mask">A mask of bits.</param>
        /// <returns>True if any of the given masked bits are "on"; otherwise false.</returns>
        [CLSCompliant(false)]
        public static bool AnyIsIn(ulong Flag, ulong Mask) {
            if ((Flag &= Mask) == 0) { return false; }
            else { return true; }
        }
        /// <summary>
        /// Determines which bits are "on".
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <returns>An array of values; one for each bit that is "on".</returns>
        [CLSCompliant(false)]
        public static ulong[] GetFlags(ulong Flag) {
            if (Flag <= 0) {
                return new ulong[] { 0 };
            }
            ulong[] cFlag = new ulong[0];
            while (true) {
                ulong c = (ulong)Math.Floor((double)(Flag + 1) / 2);
                ulong x = 1;
                while (true) {
                    if (c > x) {
                        x = x * 2;
                    }
                    else {
                        PArray.IncrementArray(ref cFlag);
                        cFlag[cFlag.GetUpperBound(0)] = x;
                        Flag -= x;
                        break;
                    }
                }
                if (Flag == 0) {
                    break;
                }
            }
            return cFlag;
        }
        /// <summary>
        /// Determines if the <seealso cref="Perseus.BitWise.BitMask"/> intersects with the given flag.
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="BitLength">The bit length</param>
        /// <param name="BitMask">A <seealso cref="Perseus.BitWise.BitMask"/> enumeration representing a bit mask.</param>
        /// <returns>True if the mask of bits intersects; otherwise false.</returns>
        [CLSCompliant(false)]
        public static bool IsBitOn(ulong Flag, BitLength BitLength, BitMask BitMask) {
            if (BitMask == BitMask.LowOrderBit) {
                return IsBitOn(Flag, 1);
            }

            ulong Mask = 0;
            //uint Flag = (uint) Value;
            switch (BitLength) {
                case BitLength.Bit4:
                    Mask = HIGH_ORDER_BIT_4;
                    break;
                case BitLength.Bit8:
                    Mask = HIGH_ORDER_BIT_8;
                    break;
                case BitLength.Bit16:
                    Mask = HIGH_ORDER_BIT_16;
                    break;
                case BitLength.Bit32:
                    Mask = HIGH_ORDER_BIT_32;
                    break;
                case BitLength.Bit64:
                    Mask = HIGH_ORDER_BIT_64;
                    break;
                default:
                    throw new Exception("Illegal enum value: " + BitLength.ToString());
            }

            if (BitMask == BitMask.HighOrderBit) {
                return IsBitOn(Flag, Mask);
            }
            else if (BitMask == BitMask.HighAndLowOrderBit) {
                ++Mask;
                return IsBitOn(Flag, Mask);
            }
            else if (BitMask == BitMask.Empty) {
                ++Mask;
                return !IsBitOn(Flag, Mask);
            }
            else {
                throw new Exception("Illegal enum value: " + BitMask.ToString());
            }
        }
        /// <summary>
        /// Determines if the <seealso cref="Perseus.BitWise.BitMask"/> intersects with the given flag.
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="BitMask">A <seealso cref="Perseus.BitWise.BitMask"/> enumeration representing a bit mask.</param>
        /// <returns>True if the mask of bits intersects; otherwise false.</returns>
        [CLSCompliant(false)]
        public static bool IsBitOn(ulong Flag, BitMask BitMask) {
            if (BitMask == BitMask.LowOrderBit) {
                return IsBitOn(Flag, 1);
            }

            ulong Mask = HIGH_ORDER_BIT_64;

            if (BitMask == BitMask.HighOrderBit) {
                return IsBitOn(Flag, Mask);
            }
            else if (BitMask == BitMask.HighAndLowOrderBit) {
                ++Mask;
                return IsBitOn(Flag, Mask);
            }
            else if (BitMask == BitMask.Empty) {
                ++Mask;
                return !IsBitOn(Flag, Mask);
            }
            else {
                throw new Exception("Illegal enum value: " + BitMask.ToString());
            }
        }
        /// <summary>
        /// Determines if the mask of bits intersects with the given flag.
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="Mask">A mask of bits</param>
        /// <returns>True if the mask of bits intersects; otherwise false.</returns>
        [CLSCompliant(false)]
        public static bool IsBitOn(ulong Flag, ulong Mask) {
            if ((Flag & Mask) == Mask) {
                return true;
            }
            else {
                return false;
            }
        }
        #endregion
        #region Flag Operations (uint)
        /// <summary>
        /// Sets the given masked bits to 'on'.
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="Mask">A mask of bits.</param>
        [CLSCompliant(false)]
        public static void SetOn(ref uint Flag, uint Mask) {
            Flag |= Mask;
        }
        /// <summary>
        /// Sets the given masked bits to 'off'.
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="Mask">A mask of bits.</param>
        [CLSCompliant(false)]
        public static void SetOff(ref uint Flag, uint Mask) {
            Flag &= ~Mask;
        }
        /// <summary>
        /// Toggles the given masked bits in Flags.
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="Mask">A mask of bits.</param>
        [CLSCompliant(false)]
        public static void Toggle(ref uint Flag, uint Mask) {
            Flag ^= Mask;
        }
        /// <summary>
        /// Sets non intersecting bits to 'off'.
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="Mask">A mask of bits.</param>
        [CLSCompliant(false)]
        public static void GetIntersects(ref  uint Flag, uint Mask) {
            Flag &= Mask;
        }
        /// <summary>
        /// Sets intersecting bits to 'off'.
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="Mask">A mask of bits.</param>
        [CLSCompliant(false)]
        public static void GetDifference(ref uint Flag, uint Mask) {
            Flag |= Mask;
            Flag ^= Mask;
        }
        /// <summary>
        /// Determines if the given masked bits are "on".
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="Mask">A mask of bits.</param>
        /// <returns>True if the given masked bits are "on"; otherwise false.</returns>
        [CLSCompliant(false)]
        public static bool IsIn(uint Flag, uint Mask) {
            return (Flag | Mask) == Flag;
        }
        /// <summary>
        /// Determines if any of the given masked bits are "on".
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="Mask">A mask of bits.</param>
        /// <returns>True if any of the given masked bits are "on"; otherwise false.</returns>
        [CLSCompliant(false)]
        public static bool AnyIsIn(uint Flag, uint Mask) {
            if ((Flag &= Mask) == 0) { return false; }
            else { return true; }
        }
        /// <summary>
        /// Determines which bits are "on".
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <returns>An array of values; one for each bit that is "on".</returns>
        [CLSCompliant(false)]
        public static uint[] GetFlags(uint Flag) {
            if (Flag <= 0) {
                return new uint[] { 0 };
            }
            uint[] cFlag = new uint[0];
            while (true) {
                uint c = (uint)Math.Floor((double)(Flag + 1) / 2);
                uint x = 1;
                while (true) {
                    if (c > x) {
                        x = x * 2;
                    }
                    else {
                        PArray.IncrementArray(ref cFlag);
                        cFlag[cFlag.GetUpperBound(0)] = x;
                        Flag -= x;
                        break;
                    }
                }
                if (Flag == 0) {
                    break;
                }
            }
            return cFlag;
        }
        /// <summary>
        /// Determines if the <seealso cref="Perseus.BitWise.BitMask"/> intersects with the given flag.
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="BitLength">The bit length</param>
        /// <param name="BitMask">A <seealso cref="Perseus.BitWise.BitMask"/> enumeration representing a bit mask.</param>
        /// <returns>True if the mask of bits intersects; otherwise false.</returns>
        [CLSCompliant(false)]
        public static bool IsBitOn(uint Flag, BitLength BitLength, BitMask BitMask) {
            if (BitMask == BitMask.LowOrderBit) {
                return IsBitOn(Flag, 1);
            }

            uint Mask = 0;
            //uint Flag = (uint) Value;
            switch (BitLength) {
                case BitLength.Bit4:
                    Mask = (uint)HIGH_ORDER_BIT_4;
                    break;
                case BitLength.Bit8:
                    Mask = (uint)HIGH_ORDER_BIT_8;
                    break;
                case BitLength.Bit16:
                    Mask = (uint)HIGH_ORDER_BIT_16;
                    break;
                case BitLength.Bit32:
                case BitLength.Bit64:
                    Mask = (uint)HIGH_ORDER_BIT_32;
                    break;
                default:
                    throw new Exception("Illegal enum value: " + BitLength.ToString());
            }

            if (BitMask == BitMask.HighOrderBit) {
                return IsBitOn(Flag, Mask);
            }
            else if (BitMask == BitMask.HighAndLowOrderBit) {
                ++Mask;
                return IsBitOn(Flag, Mask);
            }
            else if (BitMask == BitMask.Empty) {
                ++Mask;
                return !IsBitOn(Flag, Mask);
            }
            else {
                throw new Exception("Illegal enum value: " + BitMask.ToString());
            }
        }
        /// <summary>
        /// Determines if the <seealso cref="Perseus.BitWise.BitMask"/> intersects with the given flag.
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="BitMask">A <seealso cref="Perseus.BitWise.BitMask"/> enumeration representing a bit mask.</param>
        /// <returns>True if the mask of bits intersects; otherwise false.</returns>
        [CLSCompliant(false)]
        public static bool IsBitOn(uint Flag, BitMask BitMask) {
            if (BitMask == BitMask.LowOrderBit) {
                return IsBitOn(Flag, 1);
            }

            uint Mask = (uint)HIGH_ORDER_BIT_32;

            if (BitMask == BitMask.HighOrderBit) {
                return IsBitOn(Flag, Mask);
            }
            else if (BitMask == BitMask.HighAndLowOrderBit) {
                ++Mask;
                return IsBitOn(Flag, Mask);
            }
            else if (BitMask == BitMask.Empty) {
                ++Mask;
                return !IsBitOn(Flag, Mask);
            }
            else {
                throw new Exception("Illegal enum value: " + BitMask.ToString());
            }
        }
        /// <summary>
        /// Determines if the mask of bits intersects with the given flag.
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="Mask">A mask of bits</param>
        /// <returns>True if the mask of bits intersects; otherwise false.</returns>
        [CLSCompliant(false)]
        public static bool IsBitOn(uint Flag, uint Mask) {
            if ((Flag & Mask) == Mask) {
                return true;
            }
            else {
                return false;
            }
        }
        #endregion
        #region Flag Operations (ushort)
        /// <summary>
        /// Sets the given masked bits to 'on'.
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="Mask">A mask of bits.</param>
        [CLSCompliant(false)]
        public static void SetOn(ref ushort Flag, ushort Mask) {
            Flag |= Mask;
        }
        /// <summary>
        /// Sets the given masked bits to 'off'.
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="Mask">A mask of bits.</param>
        [CLSCompliant(false)]
        public static void SetOff(ref ushort Flag, ushort Mask) {
            Flag &= (ushort)(~(int)Mask);
        }
        /// <summary>
        /// Toggles the given masked bits in Flags.
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="Mask">A mask of bits.</param>
        [CLSCompliant(false)]
        public static void Toggle(ref ushort Flag, ushort Mask) {
            Flag ^= Mask;
        }
        /// <summary>
        /// Sets non intersecting bits to 'off'.
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="Mask">A mask of bits.</param>
        [CLSCompliant(false)]
        public static void GetIntersects(ref  ushort Flag, ushort Mask) {
            Flag &= Mask;
        }
        /// <summary>
        /// Sets intersecting bits to 'off'.
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="Mask">A mask of bits.</param>
        [CLSCompliant(false)]
        public static void GetDifference(ref ushort Flag, ushort Mask) {
            Flag |= Mask;
            Flag ^= Mask;
        }
        /// <summary>
        /// Determines if the given masked bits are "on".
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="Mask">A mask of bits.</param>
        /// <returns>True if the given masked bits are "on"; otherwise false.</returns>
        [CLSCompliant(false)]
        public static bool IsIn(ushort Flag, ushort Mask) {
            return (Flag | Mask) == Flag;
        }
        /// <summary>
        /// Determines if any of the given masked bits are "on".
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="Mask">A mask of bits.</param>
        /// <returns>True if any of the given masked bits are "on"; otherwise false.</returns>
        [CLSCompliant(false)]
        public static bool AnyIsIn(ushort Flag, ushort Mask) {
            if ((Flag &= Mask) == 0) { return false; }
            else { return true; }
        }
        /// <summary>
        /// Determines which bits are "on".
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <returns>An array of values; one for each bit that is "on".</returns>
        [CLSCompliant(false)]
        public static ushort[] GetFlags(ushort Flag) {
            if (Flag <= 0) {
                return new ushort[] { 0 };
            }
            ushort[] cFlag = new ushort[0];
            while (true) {
                ushort c = (ushort)Math.Floor((double)(Flag + 1) / 2);
                ushort x = 1;
                while (true) {
                    if (c > x) {
                        x = (ushort)((int)x * 2);
                    }
                    else {
                        PArray.IncrementArray(ref cFlag);
                        cFlag[cFlag.GetUpperBound(0)] = x;
                        Flag -= x;
                        break;
                    }
                }
                if (Flag == 0) {
                    break;
                }
            }
            return cFlag;
        }
        /// <summary>
        /// Determines if the <seealso cref="Perseus.BitWise.BitMask"/> intersects with the given flag.
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="BitLength">The bit length</param>
        /// <param name="BitMask">A <seealso cref="Perseus.BitWise.BitMask"/> enumeration representing a bit mask.</param>
        /// <returns>True if the mask of bits intersects; otherwise false.</returns>
        [CLSCompliant(false)]
        public static bool IsBitOn(ushort Flag, BitLength BitLength, BitMask BitMask) {
            if (BitMask == BitMask.LowOrderBit) {
                return IsBitOn(Flag, 1);
            }

            ushort Mask = 0;
            //uint Flag = (uint) Value;
            switch (BitLength) {
                case BitLength.Bit4:
                    Mask = (ushort)HIGH_ORDER_BIT_4;
                    break;
                case BitLength.Bit8:
                    Mask = (ushort)HIGH_ORDER_BIT_8;
                    break;
                case BitLength.Bit16:
                case BitLength.Bit32:
                case BitLength.Bit64:
                    Mask = (ushort)HIGH_ORDER_BIT_16;
                    break;
                default:
                    throw new Exception("Illegal enum value: " + BitLength.ToString());
            }

            if (BitMask == BitMask.HighOrderBit) {
                return IsBitOn(Flag, Mask);
            }
            else if (BitMask == BitMask.HighAndLowOrderBit) {
                ++Mask;
                return IsBitOn(Flag, Mask);
            }
            else if (BitMask == BitMask.Empty) {
                ++Mask;
                return !IsBitOn(Flag, Mask);
            }
            else {
                throw new Exception("Illegal enum value: " + BitMask.ToString());
            }
        }
        /// <summary>
        /// Determines if the <seealso cref="Perseus.BitWise.BitMask"/> intersects with the given flag.
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="BitMask">A <seealso cref="Perseus.BitWise.BitMask"/> enumeration representing a bit mask.</param>
        /// <returns>True if the mask of bits intersects; otherwise false.</returns>
        [CLSCompliant(false)]
        public static bool IsBitOn(ushort Flag, BitMask BitMask) {
            if (BitMask == BitMask.LowOrderBit) {
                return IsBitOn(Flag, 1);
            }

            ushort Mask = (ushort)HIGH_ORDER_BIT_16;

            if (BitMask == BitMask.HighOrderBit) {
                return IsBitOn(Flag, Mask);
            }
            else if (BitMask == BitMask.HighAndLowOrderBit) {
                ++Mask;
                return IsBitOn(Flag, Mask);
            }
            else if (BitMask == BitMask.Empty) {
                ++Mask;
                return !IsBitOn(Flag, Mask);
            }
            else {
                throw new Exception("Illegal enum value: " + BitMask.ToString());
            }
        }
        /// <summary>
        /// Determines if the mask of bits intersects with the given flag.
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="Mask">A mask of bits</param>
        /// <returns>True if the mask of bits intersects; otherwise false.</returns>
        [CLSCompliant(false)]
        public static bool IsBitOn(ushort Flag, ushort Mask) {
            if ((Flag & Mask) == Mask) {
                return true;
            }
            else {
                return false;
            }
        }
        #endregion
        #region Flag Operations (byte)
        /// <summary>
        /// Sets the given masked bits to 'on'.
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="Mask">A mask of bits.</param>
        public static void SetOn(ref byte Flag, byte Mask) {
            Flag |= Mask;
        }
        /// <summary>
        /// Sets the given masked bits to 'off'.
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="Mask">A mask of bits.</param>
        public static void SetOff(ref byte Flag, byte Mask) {
            Flag &= (byte)(~(int)Mask);
        }
        /// <summary>
        /// Toggles the given masked bits in Flags.
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="Mask">A mask of bits.</param>
        public static void Toggle(ref byte Flag, byte Mask) {
            Flag ^= Mask;
        }
        /// <summary>
        /// Sets non intersecting bits to 'off'.
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="Mask">A mask of bits.</param>
        public static void GetIntersects(ref  byte Flag, byte Mask) {
            Flag &= Mask;
        }
        /// <summary>
        /// Sets intersecting bits to 'off'.
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="Mask">A mask of bits.</param>
        public static void GetDifference(ref byte Flag, byte Mask) {
            Flag |= Mask;
            Flag ^= Mask;
        }
        /// <summary>
        /// Determines if the given masked bits are "on".
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="Mask">A mask of bits.</param>
        /// <returns>True if the given masked bits are "on"; otherwise false.</returns>
        public static bool IsIn(byte Flag, byte Mask) {
            return (Flag | Mask) == Flag;
        }
        /// <summary>
        /// Determines if any of the given masked bits are "on".
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="Mask">A mask of bits.</param>
        /// <returns>True if any of the given masked bits are "on"; otherwise false.</returns>
        public static bool AnyIsIn(byte Flag, byte Mask) {
            if ((Flag &= Mask) == 0) { return false; }
            else { return true; }
        }
        /// <summary>
        /// Determines which bits are "on".
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <returns>An array of values; one for each bit that is "on".</returns>
        public static byte[] GetFlags(byte Flag) {
            if (Flag <= 0) {
                return new byte[] { 0 };
            }
            byte[] cFlag = new byte[0];
            while (true) {
                byte c = (byte)Math.Floor((double)(Flag + 1) / 2);
                byte x = 1;
                while (true) {
                    if (c > x) {
                        x = (byte)((int)x * 2);
                    }
                    else {
                        PArray.IncrementArray(ref cFlag);
                        cFlag[cFlag.GetUpperBound(0)] = x;
                        Flag -= x;
                        break;
                    }
                }
                if (Flag == 0) {
                    break;
                }
            }
            return cFlag;
        }
        /// <summary>
        /// Determines if the <seealso cref="Perseus.BitWise.BitMask"/> intersects with the given flag.
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="BitLength">The bit length</param>
        /// <param name="BitMask">A <seealso cref="Perseus.BitWise.BitMask"/> enumeration representing a bit mask.</param>
        /// <returns>True if the mask of bits intersects; otherwise false.</returns>
        public static bool IsBitOn(byte Flag, BitLength BitLength, BitMask BitMask) {
            if (BitMask == BitMask.LowOrderBit) {
                return IsBitOn(Flag, 1);
            }

            byte Mask = 0;
            //uint Flag = (uint) Value;
            switch (BitLength) {
                case BitLength.Bit4:
                    Mask = (byte)HIGH_ORDER_BIT_4;
                    break;
                case BitLength.Bit8:
                case BitLength.Bit16:
                case BitLength.Bit32:
                case BitLength.Bit64:
                    Mask = (byte)HIGH_ORDER_BIT_8;
                    break;
                default:
                    throw new Exception("Illegal enum value: " + BitLength.ToString());
            }

            if (BitMask == BitMask.HighOrderBit) {
                return IsBitOn(Flag, Mask);
            }
            else if (BitMask == BitMask.HighAndLowOrderBit) {
                ++Mask;
                return IsBitOn(Flag, Mask);
            }
            else if (BitMask == BitMask.Empty) {
                ++Mask;
                return !IsBitOn(Flag, Mask);
            }
            else {
                throw new Exception("Illegal enum value: " + BitMask.ToString());
            }
        }
        /// <summary>
        /// Determines if the <seealso cref="Perseus.BitWise.BitMask"/> intersects with the given flag.
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="BitMask">A <seealso cref="Perseus.BitWise.BitMask"/> enumeration representing a bit mask.</param>
        /// <returns>True if the mask of bits intersects; otherwise false.</returns>
        public static bool IsBitOn(byte Flag, BitMask BitMask) {
            if (BitMask == BitMask.LowOrderBit) {
                return IsBitOn(Flag, 1);
            }

            byte Mask = (byte)HIGH_ORDER_BIT_8;

            if (BitMask == BitMask.HighOrderBit) {
                return IsBitOn(Flag, Mask);
            }
            else if (BitMask == BitMask.HighAndLowOrderBit) {
                ++Mask;
                return IsBitOn(Flag, Mask);
            }
            else if (BitMask == BitMask.Empty) {
                ++Mask;
                return !IsBitOn(Flag, Mask);
            }
            else {
                throw new Exception("Illegal enum value: " + BitMask.ToString());
            }
        }
        /// <summary>
        /// Determines if the mask of bits intersects with the given flag.
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="Mask">A mask of bits</param>
        /// <returns>True if the mask of bits intersects; otherwise false.</returns>
        public static bool IsBitOn(byte Flag, byte Mask) {
            if ((Flag & Mask) == Mask) {
                return true;
            }
            else {
                return false;
            }
        }
        #endregion
        #region Flag Operations (sbyte)
        /// <summary>
        /// Sets the given masked bits to 'on'.
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="Mask">A mask of bits.</param>
        [CLSCompliant(false)]
        public static void SetOn(ref sbyte Flag, sbyte Mask) {
            Flag |= Mask;
        }
        /// <summary>
        /// Sets the given masked bits to 'off'.
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="Mask">A mask of bits.</param>
        [CLSCompliant(false)]
        public static void SetOff(ref sbyte Flag, sbyte Mask) {
            Flag &= (sbyte)(~(int)Mask);
        }
        /// <summary>
        /// Toggles the given masked bits in Flags.
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="Mask">A mask of bits.</param>
        [CLSCompliant(false)]
        public static void Toggle(ref sbyte Flag, sbyte Mask) {
            Flag ^= Mask;
        }
        /// <summary>
        /// Sets non intersecting bits to 'off'.
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="Mask">A mask of bits.</param>
        [CLSCompliant(false)]
        public static void GetIntersects(ref  sbyte Flag, sbyte Mask) {
            Flag &= Mask;
        }
        /// <summary>
        /// Sets intersecting bits to 'off'.
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="Mask">A mask of bits.</param>
        [CLSCompliant(false)]
        public static void GetDifference(ref sbyte Flag, sbyte Mask) {
            Flag |= Mask;
            Flag ^= Mask;
        }
        /// <summary>
        /// Determines if the given masked bits are "on".
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="Mask">A mask of bits.</param>
        /// <returns>True if the given masked bits are "on"; otherwise false.</returns>
        [CLSCompliant(false)]
        public static bool IsIn(sbyte Flag, sbyte Mask) {
            return (Flag | Mask) == Flag;
        }
        /// <summary>
        /// Determines if any of the given masked bits are "on".
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="Mask">A mask of bits.</param>
        /// <returns>True if any of the given masked bits are "on"; otherwise false.</returns>
        [CLSCompliant(false)]
        public static bool AnyIsIn(sbyte Flag, sbyte Mask) {
            if ((Flag &= Mask) == 0) { return false; }
            else { return true; }
        }
        /// <summary>
        /// Determines which bits are "on".
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <returns>An array of values; one for each bit that is "on".</returns>
        [CLSCompliant(false)]
        public static sbyte[] GetFlags(sbyte Flag) {
            if (Flag <= 0) {
                return new sbyte[] { 0 };
            }
            sbyte[] cFlag = new sbyte[0];
            while (true) {
                sbyte c = (sbyte)Math.Floor((double)(Flag + 1) / 2);
                sbyte x = 1;
                while (true) {
                    if (c > x) {
                        x = (sbyte)((int)x * 2);
                    }
                    else {
                        PArray.IncrementArray(ref cFlag);
                        cFlag[cFlag.GetUpperBound(0)] = x;
                        Flag -= x;
                        break;
                    }
                }
                if (Flag == 0) {
                    break;
                }
            }
            return cFlag;
        }
        /// <summary>
        /// Determines if the <seealso cref="Perseus.BitWise.BitMask"/> intersects with the given flag.
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="BitLength">The bit length</param>
        /// <param name="BitMask">A <seealso cref="Perseus.BitWise.BitMask"/> enumeration representing a bit mask.</param>
        /// <returns>True if the mask of bits intersects; otherwise false.</returns>
        [CLSCompliant(false)]
        public static bool IsBitOn(sbyte Flag, BitLength BitLength, BitMask BitMask) {
            if (BitMask == BitMask.LowOrderBit) {
                return IsBitOn(Flag, 1);
            }

            byte Mask = 0;
            //uint Flag = (uint) Value;
            switch (BitLength) {
                case BitLength.Bit4:
                    Mask = (byte)HIGH_ORDER_BIT_4;
                    break;
                case BitLength.Bit8:
                case BitLength.Bit16:
                case BitLength.Bit32:
                case BitLength.Bit64:
                    Mask = (byte)HIGH_ORDER_BIT_8;
                    break;
                default:
                    throw new Exception("Illegal enum value: " + BitLength.ToString());
            }

            if (BitMask == BitMask.HighOrderBit) {
                return IsBitOn(Flag, Mask);
            }
            else if (BitMask == BitMask.HighAndLowOrderBit) {
                ++Mask;
                return IsBitOn(Flag, Mask);
            }
            else if (BitMask == BitMask.Empty) {
                ++Mask;
                return !IsBitOn(Flag, Mask);
            }
            else {
                throw new Exception("Illegal enum value: " + BitMask.ToString());
            }
        }
        /// <summary>
        /// Determines if the <seealso cref="Perseus.BitWise.BitMask"/> intersects with the given flag.
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="BitMask">A <seealso cref="Perseus.BitWise.BitMask"/> enumeration representing a bit mask.</param>
        /// <returns>True if the mask of bits intersects; otherwise false.</returns>
        [CLSCompliant(false)]
        public static bool IsBitOn(sbyte Flag, BitMask BitMask) {
            if (BitMask == BitMask.LowOrderBit) {
                return IsBitOn(Flag, 1);
            }

            byte Mask = (byte)HIGH_ORDER_BIT_8;

            if (BitMask == BitMask.HighOrderBit) {
                return IsBitOn(Flag, Mask);
            }
            else if (BitMask == BitMask.HighAndLowOrderBit) {
                ++Mask;
                return IsBitOn(Flag, Mask);
            }
            else if (BitMask == BitMask.Empty) {
                ++Mask;
                return !IsBitOn(Flag, Mask);
            }
            else {
                throw new Exception("Illegal enum value: " + BitMask.ToString());
            }
        }
        /// <summary>
        /// Determines if the mask of bits intersects with the given flag.
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="Mask">A mask of bits</param>
        /// <returns>True if the mask of bits intersects; otherwise false.</returns>
        [CLSCompliant(false)]
        public static bool IsBitOn(sbyte Flag, byte Mask) {
            if ((Flag & Mask) == Mask) {
                return true;
            }
            else {
                return false;
            }
        }
        #endregion

        #region Flag Operations (Perseus Internal)
        /// <summary>
        /// Determines if the given masked bits are "on".
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="Mask">A mask of bits.</param>
        /// <returns>True if the given masked bits are "on"; otherwise false.</returns>
        internal static bool IsIn(SplitOptions Flag, SplitOptions Mask) {
            return (Flag | Mask) == Flag;
        }
        /// <summary>
        /// Determines if any of the given masked bits are "on".
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="Mask">A mask of bits.</param>
        /// <returns>True if any of the given masked bits are "on"; otherwise false.</returns>
        internal static bool AnyIsIn(SplitOptions Flag, SplitOptions Mask) {
            if ((Flag &= Mask) == SplitOptions.None) { return false; }
            else { return true; }
        }
        /// <summary>
        /// Sets the given masked bits to 'off'.
        /// </summary>
        /// <param name="Flag">The flag to operate on.</param>
        /// <param name="Mask">A mask of bits.</param>
        internal static void SetOff(ref SplitOptions Flag, SplitOptions Mask) {
            Flag &= ~Mask;
        }
        #endregion
    }
}
