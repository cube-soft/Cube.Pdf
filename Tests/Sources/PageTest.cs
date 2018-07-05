﻿/* ------------------------------------------------------------------------- */
//
// Copyright (c) 2010 CubeSoft, Inc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//  http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
/* ------------------------------------------------------------------------- */
using Cube.Pdf.Mixin;
using NUnit.Framework;
using System.Collections.Generic;
using System.Drawing;

namespace Cube.Pdf.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// PageTest
    ///
    /// <summary>
    /// Page のテスト用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class PageTest : DocumentReaderFixture
    {
        #region Tests

        /* ----------------------------------------------------------------- */
        ///
        /// Get
        ///
        /// <summary>
        /// 各ページの情報を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCaseSource(nameof(TestCases))]
        public void Get(string klass, string filename, int n, float w, float h, int degree)
        {
            var src = GetExamplesWith(filename);

            using (var reader = Create(klass, src, ""))
            {
                var dest = reader.GetPage(n);

                Assert.That(dest.Resolution.X,    Is.EqualTo(72.0f));
                Assert.That(dest.Resolution.Y,    Is.EqualTo(72.0f));
                Assert.That(dest.Size.Width,      Is.EqualTo(w));
                Assert.That(dest.Size.Height,     Is.EqualTo(h));
                Assert.That(dest.Rotation.Degree, Is.EqualTo(degree));
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetViewSize
        ///
        /// <summary>
        /// ViewSize の計算結果を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(  0,  595.0f,  842.0f)]
        [TestCase( 45, 1016.1f, 1016.1f)]
        [TestCase( 90,  842.0f,  595.0f)]
        [TestCase(135, 1016.1f, 1016.1f)]
        [TestCase(180,  595.0f,  842.0f)]
        [TestCase(225, 1016.1f, 1016.1f)]
        [TestCase(270,  842.0f,  595.0f)]
        [TestCase(315, 1016.1f, 1016.1f)]
        public void GetViewSize(int degree, float w, float h)
        {
            var dest = new Page
            {
                Rotation   = new Angle(),
                Resolution = new PointF(72, 72),
                Size       = new SizeF(595.0f, 842.0f),
            }.GetViewSize(new Angle(degree));

            Assert.That(dest.Width,  Is.EqualTo(w).Within(1.0));
            Assert.That(dest.Height, Is.EqualTo(h).Within(1.0));
        }

        #endregion

        #region TestCases

        /* ----------------------------------------------------------------- */
        ///
        /// TestCases
        ///
        /// <summary>
        /// テストケース一覧を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static IEnumerable<TestCaseData> TestCases
        {
            get
            {
                foreach (var klass in GetClassIds())
                {
                    yield return new TestCaseData(klass, "SampleRotation.pdf", 1, 595.0f, 842.0f,   0);
                    yield return new TestCaseData(klass, "SampleRotation.pdf", 2, 595.0f, 842.0f,  90);
                    yield return new TestCaseData(klass, "SampleRotation.pdf", 3, 595.0f, 842.0f, 180);
                    yield return new TestCaseData(klass, "SampleRotation.pdf", 4, 595.0f, 842.0f, 270);
                    yield return new TestCaseData(klass, "SampleRotation.pdf", 5, 595.0f, 842.0f,   0);
                }
            }
        }

        #endregion
    }
}
