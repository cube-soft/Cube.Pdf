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
using Cube.Pdf.Pdfium;
using System.Threading;

namespace Cube.Pdf.App.Editor
{
    /* --------------------------------------------------------------------- */
    ///
    /// MainFacade
    ///
    /// <summary>
    /// MainViewModel と各種 Model の窓口となるクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class MainFacade
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// MainFacade
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /// <param name="settings">設定情報</param>
        /// <param name="context">同期用コンテキスト</param>
        ///
        /* ----------------------------------------------------------------- */
        public MainFacade(SettingsFolder settings, SynchronizationContext context)
        {
            Settings = settings;

            Images = new ImageList(context);
            Images.Preferences.BaseSize = settings.Value.ViewSize;
            Images.Preferences.Margin = 3;
            Images.Preferences.TextHeight = 25;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Settings
        ///
        /// <summary>
        /// 設定情報を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public SettingsFolder Settings { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// Images
        ///
        /// <summary>
        /// PDF のサムネイル一覧を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public ImageList Images { get; }

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Open
        ///
        /// <summary>
        /// PDF ファイルを開きます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Open(string src)
        {
            _core = new DocumentReader(src);
            Images.Renderer = _core;
            foreach (var page in _core.Pages) Images.Add(page);
        }

        #endregion

        #region Fields
        private DocumentReader _core;
        #endregion
    }
}