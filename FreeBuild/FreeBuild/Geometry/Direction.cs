﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nucleus.Geometry
{
    /// <summary>
    /// Six-axis dimensional direction, consisting of the standard three
    /// translational dimensions and rotations about those axes.
    /// Used to determine the application of directional loads.
    /// </summary>
    public enum Direction
    {
        X = 1,
        Y = 2,
        Z = 4,
        XX = 8,
        YY = 16,
        ZZ = 32
    }

    /// <summary>
    /// Extension methods for the Direction enum
    /// </summary>
    public static class DirectionExtensions
    {
        /// <summary>
        /// Does this value represent a translation along an axis?
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsTranslation(this Direction value)
        {
            return (value <= Direction.Z);
        }

        /// <summary>
        /// Does this value represent a rotation about an axis?
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsRotation(this Direction value)
        {
            return (value >= Direction.XX);
        }
    }
}