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

using FreeBuild.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeBuild.Model
{
    /// <summary>
    /// Base class for objects which describe the orientation
    /// of the local coordinate system of an element,
    /// which in turn may influence how the volumetric property
    /// is applied to the set-out geometry to create the full
    /// solid representation of the object
    /// </summary>
    [Serializable]
    public abstract class ElementOrientation
    {

        #region Methods

        /// <summary>
        /// Get this orientation expressed as an orientation angle
        /// </summary>
        /// <returns></returns>
        public virtual Angle Angle()
        {
            return new Angle();
        }

        #endregion

        #region Operator

        /// <summary>
        /// Implicit conversion from a double to an ElementOrientationAngle
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator ElementOrientation(double value)
        {
            return new ElementOrientationAngle(value);
        }

        /// <summary>
        /// Implicit conversion from an ElementOrientation to an angle
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Angle(ElementOrientation value)
        {
            if (value == null) return 0;
            else return value.Angle();
        }

        #endregion

    }
}
