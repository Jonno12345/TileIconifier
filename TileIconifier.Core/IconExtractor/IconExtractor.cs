﻿/*
 *  IconExtractor/IconUtil for .NET
 *  Copyright (C) 2014 Tsuda Kageyu. All rights reserved.
 *
 *  Redistribution and use in source and binary forms, with or without
 *  modification, are permitted provided that the following conditions
 *  are met:
 *
 *   1. Redistributions of source code must retain the above copyright
 *      notice, this list of conditions and the following disclaimer.
 *   2. Redistributions in binary form must reproduce the above copyright
 *      notice, this list of conditions and the following disclaimer in the
 *      documentation and/or other materials provided with the distribution.
 *
 *  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
 *  "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED
 *  TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A
 *  PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER
 *  OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL,
 *  EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO,
 *  PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR
 *  PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
 *  LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
 *  NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
 *  SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace TileIconifier.Core.IconExtractor
{
    public class IconExtractor
    {
        ////////////////////////////////////////////////////////////////////////
        // Constants

        // Flags for LoadLibraryEx().

        private const uint LOAD_LIBRARY_AS_DATAFILE = 0x00000002;

        private const int MAX_PATH = 260;

        // Resource types for EnumResourceNames().

        private static readonly IntPtr RtIcon = (IntPtr) 3;
        private static readonly IntPtr RtGroupIcon = (IntPtr) 14;

        ////////////////////////////////////////////////////////////////////////
        // Fields

        private byte[][] _iconData; // Binary data of each icon.

        /// <summary>
        ///     Initializes a new instance of the IconExtractor class from the specified file name.
        /// </summary>
        /// <param name="fileName">The file to extract icons from.</param>
        public IconExtractor(string fileName)
        {
            Initialize(fileName);
        }

        ////////////////////////////////////////////////////////////////////////
        // Public properties

        /// <summary>
        ///     Gets the full path of the associated file.
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        ///     Gets the count of the icons in the associated file.
        /// </summary>
        public int Count => _iconData.Length;

        /// <summary>
        ///     Extracts an icon from the file.
        /// </summary>
        /// <param name="index">Zero based index of the icon to be extracted.</param>
        /// <returns>A System.Drawing.Icon object.</returns>
        /// <remarks>Always returns new copy of the Icon. It should be disposed by the user.</remarks>
        public Icon GetIcon(int index)
        {
            if (index < 0 || Count <= index)
                throw new ArgumentOutOfRangeException(nameof(index));

            // Create an Icon based on a .ico file in memory.

            using (var ms = new MemoryStream(_iconData[index]))
            {
                return new Icon(ms);
            }
        }

        /// <summary>
        ///     Extracts all the icons from the file.
        /// </summary>
        /// <returns>An array of System.Drawing.Icon objects.</returns>
        /// <remarks>Always returns new copies of the Icons. They should be disposed by the user.</remarks>
        public Icon[] GetAllIcons()
        {
            var icons = new List<Icon>();
            for (var i = 0; i < Count; ++i)
                icons.Add(GetIcon(i));

            return icons.ToArray();
        }

        private void Initialize(string fileName)
        {
            if (fileName == null)
                throw new ArgumentNullException(nameof(fileName));

            var hModule = IntPtr.Zero;
            try
            {
                hModule = NativeMethods.LoadLibraryEx(fileName, IntPtr.Zero, LOAD_LIBRARY_AS_DATAFILE);
                if (hModule == IntPtr.Zero)
                    throw new Win32Exception();

                FileName = GetFileName(hModule);

                // Enumerate the icon resource and build .ico files in memory.

                var tmpData = new List<byte[]>();

                Enumresnameproc callback = (h, t, name, l) =>
                {
                    // Refer the following URL for the data structures used here:
                    // http://msdn.microsoft.com/en-us/library/ms997538.aspx

                    // RT_GROUP_ICON resource consists of a GRPICONDIR and GRPICONDIRENTRY's.

                    var dir = GetDataFromResource(hModule, RtGroupIcon, name);

                    // Calculate the size of an entire .icon file.

                    int count = BitConverter.ToUInt16(dir, 4); // GRPICONDIR.idCount
                    var len = 6 + 16*count; // sizeof(ICONDIR) + sizeof(ICONDIRENTRY) * count
                    for (var i = 0; i < count; ++i)
                        len += BitConverter.ToInt32(dir, 6 + 14*i + 8); // GRPICONDIRENTRY.dwBytesInRes

                    using (var dst = new BinaryWriter(new MemoryStream(len)))
                    {
                        // Copy GRPICONDIR to ICONDIR.

                        dst.Write(dir, 0, 6);

                        var picOffset = 6 + 16*count; // sizeof(ICONDIR) + sizeof(ICONDIRENTRY) * count

                        for (var i = 0; i < count; ++i)
                        {
                            // Load the picture.

                            var id = BitConverter.ToUInt16(dir, 6 + 14*i + 12); // GRPICONDIRENTRY.nID
                            var pic = GetDataFromResource(hModule, RtIcon, (IntPtr) id);

                            // Copy GRPICONDIRENTRY to ICONDIRENTRY.

                            dst.Seek(6 + 16*i, SeekOrigin.Begin);

                            dst.Write(dir, 6 + 14*i, 8); // First 8bytes are identical.
                            dst.Write(pic.Length); // ICONDIRENTRY.dwBytesInRes
                            dst.Write(picOffset); // ICONDIRENTRY.dwImageOffset

                            // Copy a picture.

                            dst.Seek(picOffset, SeekOrigin.Begin);
                            dst.Write(pic, 0, pic.Length);

                            picOffset += pic.Length;
                        }

                        tmpData.Add(((MemoryStream) dst.BaseStream).ToArray());
                    }

                    return true;
                };
                NativeMethods.EnumResourceNames(hModule, RtGroupIcon, callback, IntPtr.Zero);

                _iconData = tmpData.ToArray();
            }
            finally
            {
                if (hModule != IntPtr.Zero)
                    NativeMethods.FreeLibrary(hModule);
            }
        }

        private byte[] GetDataFromResource(IntPtr hModule, IntPtr type, IntPtr name)
        {
            // Load the binary data from the specified resource.

            var hResInfo = NativeMethods.FindResource(hModule, name, type);
            if (hResInfo == IntPtr.Zero)
                throw new Win32Exception();

            var hResData = NativeMethods.LoadResource(hModule, hResInfo);
            if (hResData == IntPtr.Zero)
                throw new Win32Exception();

            var pResData = NativeMethods.LockResource(hResData);
            if (pResData == IntPtr.Zero)
                throw new Win32Exception();

            var size = NativeMethods.SizeofResource(hModule, hResInfo);
            if (size == 0)
                throw new Win32Exception();

            var buf = new byte[size];
            Marshal.Copy(pResData, buf, 0, buf.Length);

            return buf;
        }

        private string GetFileName(IntPtr hModule)
        {
            // Alternative to GetModuleFileName() for the module loaded with
            // LOAD_LIBRARY_AS_DATAFILE option.

            // Get the file name in the format like:
            // "\\Device\\HarddiskVolume2\\Windows\\System32\\shell32.dll"

            string fileName;
            {
                var buf = new StringBuilder(MAX_PATH);
                var len = NativeMethods.GetMappedFileName(
                    NativeMethods.GetCurrentProcess(), hModule, buf, buf.Capacity);
                if (len == 0)
                    throw new Win32Exception();

                fileName = buf.ToString();
            }

            // Convert the device name to drive name like:
            // "C:\\Windows\\System32\\shell32.dll"

            for (var c = 'A'; c <= 'Z'; ++c)
            {
                var drive = c + ":";
                var buf = new StringBuilder(MAX_PATH);
                var len = NativeMethods.QueryDosDevice(drive, buf, buf.Capacity);
                if (len == 0)
                    continue;

                var devPath = buf.ToString();
                if (fileName.StartsWith(devPath))
                    return drive + fileName.Substring(devPath.Length);
            }

            return fileName;
        }
    }
}