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
using System;
using System.Text;

namespace Cube.Pdf.Ghostscript
{
    /* --------------------------------------------------------------------- */
    ///
    /// Argument
    ///
    /// <summary>
    /// Interpreter に指定可能な引数を表すクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class Argument
    {
        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Key
        ///
        /// <summary>
        /// キーを取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Key { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Value
        ///
        /// <summary>
        /// 値を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Value { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// KeyPrefix
        ///
        /// <summary>
        /// キーの接頭辞を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public char KeyPrefix { get; set; } = 'd';

        /* ----------------------------------------------------------------- */
        ///
        /// ValuePrefix
        ///
        /// <summary>
        /// 値の接頭辞を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public char ValuePrefix { get; set; } = '/';

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// ToString
        ///
        /// <summary>
        /// 引数を表す文字列を取得します。
        /// </summary>
        ///
        /// <returns>文字列</returns>
        ///
        /* ----------------------------------------------------------------- */
        public override string ToString()
        {
            if (string.IsNullOrEmpty(Key)) throw new ArgumentException(nameof(Key));

            var sb = new StringBuilder();
            sb.Append($"-{KeyPrefix}{Key}");
            if (!string.IsNullOrEmpty(Value)) sb.Append($"={ValuePrefix}{Value}");

            return base.ToString();
        }

        #endregion
    }
}
