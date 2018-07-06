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
using Cube.FileSystem;
using Cube.Pdf.Mixin;
using Cube.Pdf.Mupdf.MupdfApi;
using System;

namespace Cube.Pdf.Mupdf
{
    /* --------------------------------------------------------------------- */
    ///
    /// MupdfReader
    ///
    /// <summary>
    /// PDFium の API をラップした Reader クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    internal class MupdfReader : MupdfLibrary
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// MupdfReader
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /// <param name="src">入力ファイルのパス</param>
        /// <param name="io">I/O オブジェクト</param>
        ///
        /* ----------------------------------------------------------------- */
        public MupdfReader(string src, IO io)
        {
            Source = src;
            IO     = io;

            _stream   = Facade.FZ_OpenFile(Context, src);
            _document = Facade.PDF_OpenDocumentWithStream(Context, _stream);

            if (_document == IntPtr.Zero) throw new ArgumentException();
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Source
        ///
        /// <summary>
        /// 対象となる PDF ファイルのパスを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Source { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// IO
        ///
        /// <summary>
        /// I/O オブジェクトを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public IO IO { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// File
        ///
        /// <summary>
        /// ファイル情報を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public PdfFile File { get; private set; }

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Create
        ///
        /// <summary>
        /// MupdfReader オブジェクトを生成します。
        /// </summary>
        ///
        /// <param name="src">PDF ファイルのパス</param>
        /// <param name="query">パスワード用オブジェクト</param>
        /// <param name="io">I/O オブジェクト</param>
        ///
        /// <returns>PdfReader</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static MupdfReader Create(string src, IQuery<string> query, IO io)
        {
            var dest = new MupdfReader(src, io);
            var password = string.Empty;

            while (true)
            {
                try
                {
                    dest.Load(password);
                    return dest;
                }
                catch (ArgumentException)
                {
                    var e = query.RequestPassword(src);
                    if (!e.Cancel) password = e.Result;
                    else throw new OperationCanceledException();
                }
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Load
        ///
        /// <summary>
        /// PDF を読み込みます。
        /// </summary>
        ///
        /// <param name="password">パスワード</param>
        ///
        /* ----------------------------------------------------------------- */
        private void Load(string password)
        {
            var status = Facade.PDF_AuthenticatePassword(Context, _document, password);
            if (status == 0) throw new ArgumentException();

            var n = Facade.PDF_CountPages(Context, _document);

            File = CreateFile(password, n, status);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Dispose
        ///
        /// <summary>
        /// リソースを開放します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void Dispose(bool disposing)
        {
            try
            {
                Facade.PDF_DropDocument(Context, _document);
                Facade.FZ_DropStream(Context, _stream);
            }
            finally { base.Dispose(disposing); }
        }

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// CreateFile
        ///
        /// <summary>
        /// File オブジェクトを生成します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private PdfFile CreateFile(string password, int n, int status) =>
            new PdfFile(Source, password, IO.GetRefreshable())
            {
                FullAccess = (status & 0x02) != 0,
                Count      = n,
            };

        #endregion

        #region Fields
        private readonly IntPtr _stream;
        private readonly IntPtr _document;
        #endregion
    }
}
