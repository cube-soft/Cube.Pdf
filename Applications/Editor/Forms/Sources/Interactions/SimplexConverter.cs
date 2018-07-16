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
using Cube.Generics;
using Cube.Xui.Converters;
using System.Windows.Input;

namespace Cube.Pdf.App.Editor
{
    #region BooleanToCursor

    /* --------------------------------------------------------------------- */
    ///
    /// BooleanToCursor
    ///
    /// <summary>
    /// Provides functionality to convert a boolean to the Cursor.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class BooleanToCursor : BooleanToValue<Cursor>
    {
        /* ----------------------------------------------------------------- */
        ///
        /// BooleanToCursor
        ///
        /// <summary>
        /// Initializes a new instance of the BooleanToCursor class.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public BooleanToCursor() : base(Cursors.Wait, Cursors.Arrow) { }
    }

    #endregion

    #region IndexToText

    /* --------------------------------------------------------------------- */
    ///
    /// IndexToText
    ///
    /// <summary>
    /// Provides functionality to convert an index to the display text.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class IndexToText : SimplexConverter
    {
        /* ----------------------------------------------------------------- */
        ///
        /// IndexToText
        ///
        /// <summary>
        /// Initializes a new instance of the IndexToText class.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public IndexToText() : base(e => (e.TryCast<int>() + 1).ToString()) { }
    }

    #endregion
}