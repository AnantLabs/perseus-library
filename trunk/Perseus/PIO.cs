using System;
using System.IO;
using Microsoft.Win32;

namespace Perseus {
    public static class PIO {
        public static string RemoveInvalidFileNameChars(string fileName) {
            char[] invalidFileChars = Path.GetInvalidFileNameChars();

            foreach (char c in invalidFileChars) {
                fileName = fileName.Replace(c.ToString(), string.Empty);
            }

            return fileName;
        }
        public static void CopyStream(this Stream input, Stream output) {
            byte[] buffer = new byte[32768];
            int read;


            input.Position = 0;


            while ((read = input.Read(buffer, 0, buffer.Length)) > 0) {
                output.Write(buffer, 0, read);
            }
        }
        public static string GetMimeType(string fileName) {
            string extension = Path.GetExtension(fileName).ToLower();

            RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(extension);
            if (regKey != null && regKey.GetValue("Content Type") != null) {
                return regKey.GetValue("Content Type").ToString();
            }

            return "application/octetstream";
        }
    }
}
