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
using Cube.Log;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Cube.Pdf.Mupdf.MupdfApi
{
    /* --------------------------------------------------------------------- */
    ///
    /// MupdfApi.Facade
    ///
    /// <summary>
    /// MuPDF API のラッパクラスです。
    /// </summary>
    ///
    /// <remarks>
    /// MuPDF がスレッドセーフではないため、lock オブジェクトを利用して
    /// 実際の API を実行します。
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    internal static class Facade
    {
        #region Methods

        #region Common

        /* ----------------------------------------------------------------- */
        ///
        /// FZ_NewContext
        ///
        /// <summary>
        /// Allocate context containing global state.
        /// </summary>
        ///
        /// <see href="https://github.com/cube-soft/mupdf/blob/master/include/mupdf/fitz/context.h" />
        ///
        /* ----------------------------------------------------------------- */
        public static IntPtr FZ_NewContext() =>
            Invoke(() => NativeMethods.FZ_NewContextImp(IntPtr.Zero, IntPtr.Zero, 256 << 20, "1.13"));

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
        public static void FZ_DropContext(IntPtr ctx) =>
            Invoke(() => NativeMethods.FZ_DropContext(ctx));

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
        public static IntPtr FZ_OpenFile(IntPtr ctx, string filename) =>
            Invoke(() => FZ_OpenFile(ctx, filename));

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
        public static void FZ_DropStream(IntPtr ctx, IntPtr stm) =>
            Invoke(() => NativeMethods.FZ_DropStream(ctx, stm));

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
        public static IntPtr PDF_OpenDocumentWithStream(IntPtr ctx, IntPtr stm) =>
            Invoke(() => PDF_OpenDocumentWithStream(ctx, stm));

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
        public static int PDF_AuthenticatePassword(IntPtr ctx, IntPtr doc, string pw) =>
            Invoke(() => PDF_AuthenticatePassword(ctx, doc, pw));

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
        public static void PDF_DropDocument(IntPtr ctx, IntPtr doc) =>
            Invoke(() => NativeMethods.PDF_DropDocument(ctx, doc));

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
        public static int PDF_CountPages(IntPtr ctx, IntPtr doc) =>
            Invoke(() => NativeMethods.PDF_CountPages(ctx, doc));

        #endregion

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// Invoke
        ///
        /// <summary>
        /// Action オブジェクトを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static void Invoke(Action action, [CallerMemberName] string name = null) =>
            Invoke(action, 1, name);

        /* ----------------------------------------------------------------- */
        ///
        /// Invoke
        ///
        /// <summary>
        /// Action オブジェクトを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static void Invoke(Action action, int retry, [CallerMemberName] string name = null)
        {
            for (var i = 0; i < retry; ++i)
            {
                try { lock (_lock) action(); }
                catch (Exception err) { LogWait(err, name, i, retry); }
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Invoke
        ///
        /// <summary>
        /// Func(T) オブジェクトを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static T Invoke<T>(Func<T> func, [CallerMemberName] string name = null) =>
            Invoke(func, 1, name);

        /* ----------------------------------------------------------------- */
        ///
        /// Invoke
        ///
        /// <summary>
        /// Func(T) オブジェクトを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static T Invoke<T>(Func<T> func, int retry, [CallerMemberName] string name = null)
        {
            for (var i = 0; i < retry; ++i)
            {
                try { lock (_lock) return func(); }
                catch (Exception err) { LogWait(err, name, i, retry); }
            }
            return default(T);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// LogWait
        ///
        /// <summary>
        /// エラー内容をログに記録し、一定時間スリープします。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static void LogWait(Exception err, string name, int i, int n)
        {
            Logger.Warn(typeof(Facade), $"{name} error ({i + 1}/{n})");
            Logger.Warn(typeof(Facade), err.ToString());
            if (i + 1 < n) Task.Delay(50).Wait();
        }

        #endregion

        #region Fields
        private static readonly string _lock = string.Intern("D1243CD3-B323-48AF-BEF4-0CF5EC9DF0C3");
        #endregion
    }
}
