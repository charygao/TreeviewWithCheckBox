using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows;
using System.Windows.Media.Imaging;

namespace WPFStudy
{
    public class ExtractIcon
    {
        [StructLayout(LayoutKind.Sequential)]
        private struct SHFILEINFO
        {
            public SHFILEINFO(bool b)
            {
                hIcon = IntPtr.Zero; iIcon = 0; dwAttributes = 0; szDisplayName = ""; szTypeName = "";
            }
            public IntPtr hIcon;
            public int iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        };

        private enum SHGFI
        {
            SHGFI_ICON = 0x000000100,     // get icon
            SHGFI_DISPLAYNAME = 0x000000200,     // get display name
            SHGFI_TYPENAME = 0x000000400,     // get type name
            SHGFI_ATTRIBUTES = 0x000000800,     // get attributes
            SHGFI_ICONLOCATION = 0x000001000,     // get icon location
            SHGFI_EXETYPE = 0x000002000,     // return exe type
            SHGFI_SYSICONINDEX = 0x000004000,     // get system icon index
            SHGFI_LINKOVERLAY = 0x000008000,     // put a link overlay on icon
            SHGFI_SELECTED = 0x000010000,     // show icon in selected state
            SHGFI_ATTR_SPECIFIED = 0x000020000,     // get only specified attributes
            SHGFI_LARGEICON = 0x000000000,     // get large icon
            SHGFI_SMALLICON = 0x000000001,     // get small icon
            SHGFI_OPENICON = 0x000000002,     // get open icon
            SHGFI_SHELLICONSIZE = 0x000000004,     // get shell size icon
            SHGFI_PIDL = 0x000000008,     // pszPath is a pidl
            SHGFI_USEFILEATTRIBUTES = 0x000000010     // use passed dwFileAttribute
        }

        [DllImport("Shell32.dll")]
        private static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, out SHFILEINFO psfi, uint cbfileInfo, SHGFI uFlags);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Boolean DestroyIcon(IntPtr hIcon);

        public static System.Windows.Media.ImageSource GetIcon(string strPath, bool selected, bool small)
        {
            SHFILEINFO info = new SHFILEINFO(true);
            int cbFileInfo = Marshal.SizeOf(info);
            SHGFI flags = SHGFI.SHGFI_ICON;
            if (selected)
                flags |= SHGFI.SHGFI_OPENICON;
            if (small)
                flags |= SHGFI.SHGFI_SMALLICON;

            SHGetFileInfo(strPath, 256, out info, (uint)cbFileInfo, flags);
            if (info.hIcon != IntPtr.Zero)
            {
                Icon icon = Icon.FromHandle(info.hIcon);
                if (icon != null)
                {
                    System.Windows.Media.ImageSource img = System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(icon.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                    DestroyIcon(info.hIcon);
                    return img;
                }
            }
            return null;
        }
    }

}
