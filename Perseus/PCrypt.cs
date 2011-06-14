using System;
using System.Security.Cryptography;

namespace Perseus {
    public static class PCrypt {
        public static string MD5(string s) {
            byte[] inputBytes = System.Text.Encoding.UTF8.GetBytes(s);
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

            byte[] hash = md5.ComputeHash(inputBytes);
            string hashString = string.Empty;

            for (int i = 0; i < hash.Length; i++) {
                hashString += Convert.ToString(hash[i], 16).PadLeft(2, '0');
            }

            return hashString.PadLeft(32, '0');            
        }
    }
}
