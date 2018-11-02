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
using System.Runtime.Serialization;

namespace Cube.Pdf.App.Pinstaller
{
    /* --------------------------------------------------------------------- */
    ///
    /// PrinterDriverConfig
    ///
    /// <summary>
    /// Represents the configuration to install a printer driver.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [DataContract]
    public class PrinterDriverConfig : ObservableProperty
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// PrinterDriverConfig
        ///
        /// <summary>
        /// Initializes a new instance of the PrinterDriverConfig class.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public PrinterDriverConfig() { Reset(); }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Name
        ///
        /// <summary>
        /// Gets or sets the name of the printer drvier.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// FileName
        ///
        /// <summary>
        /// Gets or sets the name of the drvier file.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string FileName
        {
            get => _fileName;
            set => SetProperty(ref _fileName, value);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// MonitorName
        ///
        /// <summary>
        /// Gets or set the name of the port monitor.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string MonitorName
        {
            get => _monitorName;
            set => SetProperty(ref _monitorName, value);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Config
        ///
        /// <summary>
        /// Gets the name of config file.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Config
        {
            get => _config;
            set => SetProperty(ref _config, value);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Data
        ///
        /// <summary>
        /// Gets the name of data file.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Data
        {
            get => _data;
            set => SetProperty(ref _data, value);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Help
        ///
        /// <summary>
        /// Gets the name of help file.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Help
        {
            get => _help;
            set => SetProperty(ref _help, value);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Dependencies
        ///
        /// <summary>
        /// Gets the name of dependency files.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Dependencies
        {
            get => _dependencies;
            set => SetProperty(ref _dependencies, value);
        }

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// OnDeserializing
        ///
        /// <summary>
        /// Occurs bofore deserializing.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [OnDeserializing]
        private void OnDeserializing(StreamingContext context) => Reset();

        /* ----------------------------------------------------------------- */
        ///
        /// Reset
        ///
        /// <summary>
        /// Resets values of fields.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void Reset()
        {
            _name         = string.Empty;
            _fileName     = string.Empty;
            _monitorName  = string.Empty;
            _config       = string.Empty;
            _data         = string.Empty;
            _help         = string.Empty;
            _dependencies = string.Empty;
        }

        #endregion

        #region Fields
        private string _name;
        private string _fileName;
        private string _monitorName;
        private string _config;
        private string _data;
        private string _help;
        private string _dependencies;
        #endregion
    }
}
