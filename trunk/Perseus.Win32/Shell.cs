using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using Perseus.IO;

namespace Perseus.Win32 {
    public static class Shell {
        #region PInvoke
        internal const uint SHGFI_ICON = 0x100; // Get icon
        internal const uint SHGFI_LARGEICON = 0x0; // Large icon
        internal const uint SHGFI_SMALLICON = 0x1; // Small icon
        internal const uint SHGFI_OPENICON = 0x2; // Open folder icon
        internal const uint SHGFI_SHELLICONSIZE = 0x4; // Icon size used in shell
        internal const uint SHGFI_USEFILEATTRIBUTES = 0x10;
        internal const uint SHGFI_ADDOVERLAYS = 0x20; // Add overlays
        internal const uint SHGFI_TYPENAME = 0x400; // Type name
        internal const uint SHGFI_SYSICONINDEX = 0x4000; // Index in image list

        internal const uint FILE_ATTRIBUTE_NORMAL = 0x80;
        internal const uint FILE_ATTRIBUTE_DIRECTORY = 0x10;

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        internal struct SHFILEINFO {
            public IntPtr hIcon;
            public IntPtr iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        };

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DestroyIcon(IntPtr hIcon);

        [DllImport("Shlwapi.dll", CharSet = CharSet.Auto)]
        internal static extern long StrFormatByteSize(long fileSize, [MarshalAs(UnmanagedType.LPTStr)] StringBuilder buffer, int bufferSize);

        [DllImport("shell32.dll", EntryPoint = "#727", PreserveSig = false)]
        internal static extern void SHGetImageList(int iImageList, ref Guid riid, out IntPtr ppv);

        [DllImport("comctl32.dll", SetLastError = true)]
        internal static extern IntPtr ImageList_GetIcon(IntPtr himl, int i, int flags);
        #endregion

        public static ImageSource FileIcon(string fileName) {
            return Shell.FileIcon(fileName, ShellIconSize.Small);
        }
        public static ImageSource FileIcon(string fileName, ShellIconSize size) {
            return Shell.ShellIcon(fileName, size, 0, Shell.FILE_ATTRIBUTE_NORMAL);                       
        }
        public static ImageSource FileIcon(this FileInfo fileInfo) {
            return Shell.FileIcon(fileInfo.FullName);
        }
        
        public static string FileTypeName(string fileName) {
            Shell.SHFILEINFO shinfo = new Shell.SHFILEINFO();
            Shell.SHGetFileInfo(fileName, 0, ref shinfo, (uint)Marshal.SizeOf(shinfo), Shell.SHGFI_TYPENAME);
            return shinfo.szTypeName;
        }
        public static string FileTypeName(this FileInfo fileInfo) {
            return Shell.FileTypeName(fileInfo.FullName);
        }

        public static string FormatFileSize(long length) {
            StringBuilder buffer = new StringBuilder();
            Shell.StrFormatByteSize(length, buffer, 100);
            return buffer.ToString();
        }
        public static string FormatFileSize(this FileInfo fileInfo) {
            return Shell.FormatFileSize(fileInfo.Length);
        }

        public static ImageSource DirectoryIcon(string path) {
            return Shell.DirectoryIcon(path, ShellIconSize.Small, false);
        }
        public static ImageSource DirectoryIcon(string path, ShellIconSize size) {
            return Shell.DirectoryIcon(path, size, false);
        }
        public static ImageSource DirectoryIcon(string path, ShellIconSize size, bool open) {
            uint flags = 0;
            if (open) {
                flags |= Shell.SHGFI_OPENICON;
            }
            return Shell.ShellIcon(path, size, flags, Shell.FILE_ATTRIBUTE_DIRECTORY);
        }
        public static ImageSource DirectoryIcon(this DirectoryInfo directoryInfo) {
            return Shell.DirectoryIcon(directoryInfo.FullName);
        }
        
        private static ImageSource ShellIcon(string path, ShellIconSize size, uint flags, uint fileAttribute) {
            bool imageList = false;

            flags |= Shell.SHGFI_ICON | Shell.SHGFI_ADDOVERLAYS | Shell.SHGFI_USEFILEATTRIBUTES;
            switch (size) {
                case ShellIconSize.Small:
                    flags |= Shell.SHGFI_SMALLICON;
                    break;
                case ShellIconSize.Large:
                    flags |= Shell.SHGFI_LARGEICON;
                    break;
                case ShellIconSize.System:
                    flags |= Shell.SHGFI_SHELLICONSIZE;
                    break;
                default:
                    flags |= Shell.SHGFI_SYSICONINDEX;
                    imageList = true;
                    break;
            }

            Shell.SHFILEINFO shinfo = new Shell.SHFILEINFO();
            Shell.SHGetFileInfo(path, fileAttribute, ref shinfo, (uint)Marshal.SizeOf(shinfo), flags);

            ImageSource source;
            IntPtr hIcon;
            if (!imageList) {
                hIcon = shinfo.hIcon;
            }
            else {
                Guid IID_IImageList = new Guid("46EB5926-582E-4017-9FDF-E8998DAA0950");            
                IntPtr himl;
                int imageSize;
                if (size == ShellIconSize.Jumbo) {
                    imageSize = 4;
                }
                else {
                    imageSize = 2;
                }
                SHGetImageList(imageSize, ref IID_IImageList, out himl);

                hIcon = Shell.ImageList_GetIcon(himl, shinfo.iIcon.ToInt32(), 0);
            }

            using (Icon i = Icon.FromHandle(hIcon)) {
                source = Imaging.CreateBitmapSourceFromHIcon(
                    i.Handle,
                    new Int32Rect(0, 0, i.Width, i.Height),
                    BitmapSizeOptions.FromEmptyOptions()
                );
            }
            Shell.DestroyIcon(hIcon);
            return source;
        }
    }
}
