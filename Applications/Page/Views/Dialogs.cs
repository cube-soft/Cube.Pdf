﻿/* ------------------------------------------------------------------------- */
///
/// Dialogs.cs
///
/// Copyright (c) 2010 CubeSoft, Inc.
///
/// This program is free software: you can redistribute it and/or modify
/// it under the terms of the GNU Affero General Public License as published
/// by the Free Software Foundation, either version 3 of the License, or
/// (at your option) any later version.
///
/// This program is distributed in the hope that it will be useful,
/// but WITHOUT ANY WARRANTY; without even the implied warranty of
/// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
/// GNU Affero General Public License for more details.
///
/// You should have received a copy of the GNU Affero General Public License
/// along with this program.  If not, see <http://www.gnu.org/licenses/>.
///
/* ------------------------------------------------------------------------- */
using System;
using System.Reflection;
using System.Windows.Forms;

namespace Cube.Pdf.App.Page
{
    /* --------------------------------------------------------------------- */
    ///
    /// Dialogs
    ///
    /// <summary>
    /// 各種ダイアログの生成を行うためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class Dialogs
    {
        /* --------------------------------------------------------------------- */
        ///
        /// Add
        /// 
        /// <summary>
        /// 追加するファイルを選択するためのダイアログを生成します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public static OpenFileDialog Add()
            => new OpenFileDialog()
        {
            CheckFileExists = true,
            Multiselect = true,
            Title = Properties.Resources.OpenFileTitle,
            Filter = Properties.Resources.OpenFileFilter,
        };

        /* --------------------------------------------------------------------- */
        ///
        /// Merge
        /// 
        /// <summary>
        /// 結合したファイルの保存先を選択するためのダイアログを生成します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public static SaveFileDialog Merge()
            => new SaveFileDialog
        {
            OverwritePrompt = true,
            Title = Properties.Resources.MergeTitle,
            Filter = Properties.Resources.SaveFileFilter,
        };

        /* --------------------------------------------------------------------- */
        ///
        /// Split
        /// 
        /// <summary>
        /// 分割したファイルの保存先を選択するためのダイアログを生成します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public static FolderBrowserDialog Split()
            => new FolderBrowserDialog
        {
            Description = Properties.Resources.SplitDescription,
            ShowNewFolderButton = true,
        };

        #region MessageBox

        /* --------------------------------------------------------------------- */
        ///
        /// Version
        /// 
        /// <summary>
        /// バージョン情報を表示します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public static DialogResult Version()
        {
            var dest = new Cube.Forms.VersionForm
            {
                Assembly = Assembly.GetExecutingAssembly(),
                Logo = Properties.Resources.Logo,
                Description = string.Empty,
                Height = 280,
                ShowInTaskbar = false,
                StartPosition = FormStartPosition.CenterParent,
            };
            dest.Version.Digit = 3;

            return dest.ShowDialog();
        }

        /* --------------------------------------------------------------------- */
        ///
        /// Confirm
        /// 
        /// <summary>
        /// 確認メッセージを表示します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public static DialogResult Confirm(string message)
            => MessageBox.Show(
            message,
            Properties.Resources.MessageTitle,
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Information
        );

        /* --------------------------------------------------------------------- */
        ///
        /// Error
        /// 
        /// <summary>
        /// エラーメッセージを表示します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public static DialogResult Error(Exception err)
            => MessageBox.Show(
            err.Message,
            Properties.Resources.ErrorTitle,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error
        );

        #endregion
    }
}
