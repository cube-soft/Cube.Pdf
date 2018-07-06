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
using Cube.Pdf.Mupdf.MupdfApi;
using System;

namespace Cube.Pdf.Mupdf
{
    /* --------------------------------------------------------------------- */
    ///
    /// MupdfLibrary
    ///
    /// <summary>
    /// MuPDF API を利用するための基底クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    internal abstract class MupdfLibrary : IDisposable
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// MupdfLibrary
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected MupdfLibrary()
        {
            _dispose = new OnceAction<bool>(Dispose);
            Context = Facade.FZ_NewContext();
            if (Context == IntPtr.Zero) throw new ArgumentException();
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Context
        ///
        /// <summary>
        /// MuPDF API で利用されるオブジェクトを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected IntPtr Context { get; }

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// ~MupdfLibrary
        ///
        /// <summary>
        /// オブジェクトを破棄します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        ~MupdfLibrary() { _dispose.Invoke(false); }

        /* ----------------------------------------------------------------- */
        ///
        /// Dispose
        ///
        /// <summary>
        /// リソースを開放します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Dispose()
        {
            _dispose.Invoke(true);
            GC.SuppressFinalize(this);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Dispose
        ///
        /// <summary>
        /// リソースを開放します。
        /// </summary>
        ///
        /// <param name="disposing">
        /// マネージオブジェクトを開放するかどうか
        /// </param>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void Dispose(bool disposing) => Facade.FZ_DropContext(Context);

        #endregion

        #region Fields
        private readonly OnceAction<bool> _dispose;
        #endregion
    }
}
