/* ------------------------------------------------------------------------- */
//
// Copyright (c) 2010 CubeSoft, Inc.
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as published
// by the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.
//
// You should have received a copy of the GNU Affero General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
//
/* ------------------------------------------------------------------------- */
using System;
using System.Runtime.InteropServices;

namespace Cube.Pdf.Mupdf.MupdfApi
{
    /* --------------------------------------------------------------------- */
    ///
    /// MupdfApi.NativeMethods
    ///
    /// <summary>
    /// MuPDF の API を定義したクラスです。
    /// </summary>
    ///
    /// <remarks>
    /// このクラスのメソッドを直接実行しないで下さい。また、新しいメソッドを
    /// 定義した場合、同名のメソッドを PdfiumApi.Facade にも定義し、
    /// Facade 経由で実行するようにして下さい。
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    internal class NativeMethods
    {
        #region Methods

        #region Common

        /* ----------------------------------------------------------------- */
        ///
        /// FZ_NewContextImp
        ///
        /// <summary>
        /// Allocate context containing global state.
        /// </summary>
        ///
        /// <see href="https://github.com/cube-soft/mupdf/blob/master/include/mupdf/fitz/context.h" />
        ///
        /* ----------------------------------------------------------------- */
        [DllImport(LibName, EntryPoint = "fz_new_context_imp")]
        public static extern IntPtr FZ_NewContextImp(IntPtr alloc, IntPtr locks, uint max_store,
            [MarshalAs(UnmanagedType.LPStr)] string fz_version);

        /* ----------------------------------------------------------------- */
        ///
        /// FZ_DropContext
        ///
        /// <summary>
        /// Free a context and its global state.
        /// </summary>
        ///
        /// <see href="https://github.com/cube-soft/mupdf/blob/master/include/mupdf/fitz/context.h" />
        ///
        /* ----------------------------------------------------------------- */
        [DllImport(LibName, EntryPoint = "fz_drop_context")]
        public static extern void FZ_DropContext(IntPtr ctx);

        #endregion

        #region Document

        /* ----------------------------------------------------------------- */
        ///
        /// FZ_OpenFile
        ///
        /// <summary>
        /// Open the named file and wrap it in a stream.
        /// </summary>
        ///
        /// <remarks>
        /// filename is Wide character path to the file as it would be
        /// given to _wfopen().
        /// </remarks>
        ///
        /// <see href="https://github.com/cube-soft/mupdf/blob/master/include/mupdf/fitz/stream.h" />
        ///
        /* ----------------------------------------------------------------- */
        [DllImport(LibName, EntryPoint = "fz_open_file_w", CharSet = CharSet.Unicode)]
        public static extern IntPtr FZ_OpenFile(IntPtr ctx, string filename);

        /* ----------------------------------------------------------------- */
        ///
        /// FZ_DropStream
        ///
        /// <summary>
        /// Close an open stream.
        /// </summary>
        ///
        /// <see href="https://github.com/cube-soft/mupdf/blob/master/include/mupdf/fitz/stream.h" />
        ///
        /* ----------------------------------------------------------------- */
        [DllImport(LibName, EntryPoint = "fz_drop_stream")]
        public static extern void FZ_DropStream(IntPtr ctx, IntPtr stm);

        /* ----------------------------------------------------------------- */
        ///
        /// PDF_OpenDocumentWithStream
        ///
        /// <summary>
        /// Opens a PDF document.
        /// </summary>
        ///
        /// <see href="https://github.com/cube-soft/mupdf/blob/master/include/mupdf/pdf/document.h" />
        ///
        /* ----------------------------------------------------------------- */
        [DllImport(LibName, EntryPoint = "pdf_open_document_with_stream")]
        public static extern IntPtr PDF_OpenDocumentWithStream(IntPtr ctx, IntPtr stm);

        /* ----------------------------------------------------------------- */
        ///
        /// PDF_AuthenticatePassword
        ///
        /// <summary>
        /// Attempt to authenticate a password.
        /// </summary>
        ///
        /// <returns>
        /// 0 for failure, non-zero for success.
        ///
        /// In the non-zero case:
        /// - bit 0 set => no password required
        /// - bit 1 set => user password authenticated
        /// - bit 2 set => owner password authenticated
        /// </returns>
        ///
        /// <see href="https://github.com/cube-soft/mupdf/blob/master/include/mupdf/pdf/document.h" />
        ///
        /* ----------------------------------------------------------------- */
        [DllImport(LibName, EntryPoint = "pdf_authenticate_password")]
        public static extern int PDF_AuthenticatePassword(IntPtr ctx, IntPtr doc, string pw);

        /* ----------------------------------------------------------------- */
        ///
        /// PDF_DropDocument
        ///
        /// <summary>
        /// Closes and frees an opened PDF document.
        /// </summary>
        ///
        /// <see href="https://github.com/cube-soft/mupdf/blob/master/include/mupdf/pdf/document.h" />
        ///
        /* ----------------------------------------------------------------- */
        [DllImport(LibName, EntryPoint = "pdf_drop_document")]
        public static extern void PDF_DropDocument(IntPtr ctx, IntPtr doc);

        #endregion

        #region Page

        /* ----------------------------------------------------------------- */
        ///
        /// PDF_CountPages
        ///
        /// <summary>
        /// Get total number of pages in the document.
        /// </summary>
        ///
        /// <see href="https://github.com/cube-soft/mupdf/blob/master/include/mupdf/pdf/page.h" />
        ///
        /* ----------------------------------------------------------------- */
        [DllImport(LibName, EntryPoint = "pdf_drop_document")]
        public static extern int PDF_CountPages(IntPtr ctx, IntPtr doc);

        #endregion

        #endregion

        #region Fields
        private const string LibName = "mupdf.dll";
        #endregion
    }
}
