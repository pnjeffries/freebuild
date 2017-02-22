﻿// Copyright (c) 2016 Paul Jeffries
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Schema;
using FreeBuild.Extensions;

namespace FreeBuild.Base
{
    /// <summary>
    /// A structure that represents a file path.
    /// Wraps a string path variable to provide additional
    /// file handling utility methods.  Essentially provides
    /// much of the same functionality as the System.IO.Path
    /// and File classes but in a non-static way that is 
    /// quicker to use and easier to bind to.
    /// Can be used interchangably with file paths stored
    /// as strings.
    /// </summary>
    [Serializable]
    public struct FilePath : IXmlSerializable
    {
        #region Properties

        /// <summary>
        /// Private backing field for Path property
        /// </summary>
        private string _Path;

        /// <summary>
        /// The full string value of the filepath
        /// </summary>
        public string Path
        {
            get { return _Path; }
        }

        /// <summary>
        /// Is the path a valid file location?
        /// </summary>
        public bool IsValid
        {
            get { return !string.IsNullOrWhiteSpace(_Path); } //TODO: Other checks?
        }

        /// <summary>
        /// Is the path set?  (i.e. is it non-null?)
        /// </summary>
        public bool IsSet
        {
            get { return _Path != null; }
        }

        /// <summary>
        /// Does the file that this path points to exist?
        /// </summary>
        public bool Exists { get { return File.Exists(_Path); } }

        /// <summary>
        /// Gets the name of the file at the end of the path,
        /// including its extension but not the preceding
        /// directory structure.
        /// </summary>
        public string FileName { get { return System.IO.Path.GetFileName(_Path); } }

        /// <summary>
        /// Gets the extension of this filepath.
        /// Includes the preceding '.'.
        /// </summary>
        public string Extension { get { return System.IO.Path.GetExtension(_Path); } }

        /// <summary>
        /// Gets the directory of the filepath
        /// </summary>
        public string Directory { get { return System.IO.Path.GetDirectoryName(_Path); } }

        /// <summary>
        /// Get this filepath shortened to 50 characters or less
        /// </summary>
        public string Shortened { get { return Directory.TruncateMiddle(80 - FileName.Length) + "\\" + FileName; } }

        #endregion

        #region Constructors

        /// <summary>
        /// Path Constructor
        /// </summary>
        /// <param name="path"></param>
        public FilePath(string path)
        {
            _Path = path ?? "";
        }

        #endregion

        #region Methods

        /// <summary>
        /// ToString override
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Path;
        }

        /// <summary>
        /// GetHashCode override
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Path.GetHashCode();
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Return the path of the directory within which the specified loaded assembly exists
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static FilePath DirectoryOf(Assembly assembly)
        {
            return new FilePath(assembly.Location).Directory;
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            _Path = reader.ReadString();
            reader.ReadEndElement();
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteString(_Path);
        }

        #endregion

        #region Operators

        /// <summary>
        /// Implicit to string conversion operator
        /// </summary>
        /// <param name="path"></param>
        public static implicit operator string(FilePath path)
        {
            return path.Path;
        }

        /// <summary>
        /// Implicit from string conversion operator
        /// </summary>
        /// <param name="path"></param>
        public static implicit operator FilePath(string path)
        {
            return new FilePath(path);
        }

        #endregion
    }
}
