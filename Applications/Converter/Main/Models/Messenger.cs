﻿/* ------------------------------------------------------------------------- */
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
using Cube.Forms;

namespace Cube.Pdf.App.Converter
{
    /* --------------------------------------------------------------------- */
    ///
    /// Messenger
    ///
    /// <summary>
    /// ViewModel から View を操作するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class Messenger : IAggregator
    {
        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Close
        ///
        /// <summary>
        /// 画面を閉じるイベントを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public RelayEvent Close { get; } = new RelayEvent();

        /* ----------------------------------------------------------------- */
        ///
        /// MessageBox
        ///
        /// <summary>
        /// MessageBox を表示するイベントを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public RelayEvent<MessageEventArgs> MessageBox { get; } =
            new RelayEvent<MessageEventArgs>();

        /* ----------------------------------------------------------------- */
        ///
        /// OpenFileDialog
        ///
        /// <summary>
        /// OpenFileDialog を表示するイベントを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public RelayEvent<OpenFileEventArgs> OpenFileDialog { get; } =
            new RelayEvent<OpenFileEventArgs>();

        /* ----------------------------------------------------------------- */
        ///
        /// SaveFileDialog
        ///
        /// <summary>
        /// SaveFileDialog を表示するイベントを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public RelayEvent<SaveFileEventArgs> SaveFileDialog { get; } =
            new RelayEvent<SaveFileEventArgs>();

        #endregion
    }
}
