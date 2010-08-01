using System;
using System.IO;

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
    }
}
