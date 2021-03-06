﻿using Nucleus.Extensions;
using Nucleus.Geometry;
using Nucleus.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nucleus.Maths
{
    /// <summary>
    /// An enumerated value which describes different algorithms to be used for
    /// tweening interpolation.
    /// </summary>
    public enum Interpolation
    {
        /// <summary>
        /// Linear interpolation: t/d
        /// </summary>
        Linear,

        /// <summary>
        /// Quadratic interpolation: (t/d)^2
        /// </summary>
        Quadratic,

        /// <summary>
        /// Cubic interpolation: (t/d)^3
        /// </summary>
        Cubic,

        /// <summary>
        /// Square root interpolation: (t/d)^0.5
        /// </summary>
        SquareRoot,

        /// <summary>
        /// Cube root interpolation: (t/d)^1/3
        /// </summary>
        CubeRoot,


        /// <summary>
        /// Exponential interpolation: (exp(a*(t/d))-1)*1/(exp(a)-1)
        /// </summary>
        Exponential,

        /// <summary>
        /// Pseudo-elastic interpolation: 1/(exp(a)-1)*cos(b*(t/d)*2*PI)*exp(a*(t/d))-1/(exp(a)-1)
        /// </summary>
        Elastic,

        /// <summary>
        /// Sine interpolation: sin(a*(t/d)*2*PI)
        /// </summary>
        Sin,

        /// <summary>
        /// Cosine interpolation: cos(a*(t/d)*2*PI)
        /// </summary>
        Cos,

        /// <summary>
        /// Step interpolation: jumps from one value to another at the mid-point
        /// </summary>
        Step
    }

    /// <summary>
    /// Extension methods for the Interpolation enum
    /// </summary>
    public static class InterpolationExtensions
    {
        public const double DefaultAlpha = 10;
        public const double DefaultBeta = 3;

        /// <summary>
        /// Interpolate between two values using the algorithm represented by this enumerated value
        /// </summary>
        /// <param name="i"></param>
        /// <param name="v0">The first value to interpolate from</param>
        /// <param name="v1">The second value to interpolate towards</param>
        /// <param name="t">The interpolation parameter.  Typically will be between 0-1,
        /// where 0 is v0 and 1 is v1</param>
        /// <param name="alpha">The optional Alpha parameter used in some tweening methods</param>
        /// <param name="beta">The optional Beta parameter used in some tweening methods</param>
        /// <returns>The interpolated value</returns>
        public static double Interpolate(this Interpolation i, double v0, double v1, double t, double alpha = DefaultAlpha, double beta = DefaultBeta)
        {
            t = i.Tween(t, alpha, beta);
            return v0 + (v1 - v0) * t;
        }

        /// <summary>
        /// Interpolate between two angles using the algorithm represented by this enumerated value.
        /// The direction of rotation will be automatically determined as whichever is the smallest
        /// change in angle.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="v0">The first value to interpolate from</param>
        /// <param name="v1">The second value to interpolate towards</param>
        /// <param name="t">The interpolation parameter.  Typically will be between 0-1,
        /// where 0 is v0 and 1 is v1</param>
        /// <param name="alpha">The optional Alpha parameter used in some tweening methods</param>
        /// <param name="beta">The optional Beta parameter used in some tweening methods</param>
        /// <returns>The interpolated value</returns>
        public static Angle Interpolate(this Interpolation i, Angle v0, Angle v1, double t, double alpha = DefaultAlpha, double beta = DefaultBeta)
        {
            t = i.Tween(t, alpha, beta);
            Angle rotation = (v1 - v0).Normalize();
            return v0 + rotation * t;
        }

        /// <summary>
        /// Interpolate between two values using the algorithm represented by this enumerated value
        /// </summary>
        /// <param name="i"></param>
        /// <param name="v0">The first value to interpolate from</param>
        /// <param name="v1">The second value to interpolate towards</param>
        /// <param name="t">The interpolation parameter.  Typically will be between 0-1,
        /// where 0 is v0 and 1 is v1</param>
        /// <param name="alpha">The optional Alpha parameter used in some tweening methods</param>
        /// <param name="beta">The optional Beta parameter used in some tweening methods</param>
        /// <returns>The interpolated value</returns>
        public static Vector Interpolate(this Interpolation i, Vector v0, Vector v1, double t, double alpha = DefaultAlpha, double beta = DefaultBeta)
        {
            t = i.Tween(t, alpha, beta);
            return v0 + (v1 - v0) * t;
        }

        /// <summary>
        /// Interpolate between two values using the algorithm represented by this enumerated value
        /// </summary>
        /// <param name="i"></param>
        /// <param name="c0">The first value to interpolate from</param>
        /// <param name="c1">The second value to interpolate towards</param>
        /// <param name="t">The interpolation parameter.  Typically will be between 0-1,
        /// where 0 is v0 and 1 is v1</param>
        /// <param name="alpha">The optional Alpha parameter used in some tweening methods</param>
        /// <param name="beta">The optional Beta parameter used in some tweening methods</param>
        /// <returns>The interpolated value</returns>
        public static Colour Interpolate(this Interpolation i, Colour c0, Colour c1, double t, double alpha = DefaultAlpha, double beta = DefaultBeta)
        {
            t = i.Tween(t, alpha, beta);
            return c0.Interpolate(c1, t);
        }

        /// <summary>
        /// Interpolate between two values using the algorithm represented by this enumerated value.
        /// This will only successfully work for datatypes which implement the +, - and * operators
        /// and are unbounded.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="v0">The first value to interpolate from</param>
        /// <param name="v1">The second value to interpolate towards</param>
        /// <param name="t">The interpolation parameter.  Typically will be between 0-1,
        /// where 0 is v0 and 1 is v1</param>
        /// <param name="alpha">The optional Alpha parameter used in some tweening methods</param>
        /// <param name="beta">The optional Beta parameter used in some tweening methods</param>
        /// <returns>The interpolated value</returns>
        public static TValue Interpolate<TValue>(this Interpolation i, TValue v0, TValue v1, double t, double alpha = DefaultAlpha, double beta = DefaultBeta)
        {
            //if (typeof(TValue).IsAssignableFrom(typeof(Colour)))
            //{
            //    return (TValue)(object)i.Interpolate<Colour>((Colour)(object)v0, (Colour)(object)v1, t, alpha, beta); //Fuck me this is awkward...
            //}
            t = i.Tween(t, alpha, beta);
            dynamic v0d = v0;
            dynamic v1d = v1;
            return v0d + (v1d - v0d) * t;
        }


        /// <summary>
        /// Tween a value using the algorithm represented by this enumerated value
        /// </summary>
        /// <param name="i"></param>
        /// <param name="t">The interpolation parameter to be adjusted by this interpolation algorithm.
        /// Typically should be between 0-1</param>
        /// <param name="alpha">The optional Alpha parameter used in some tweening methods</param>
        /// <param name="beta">The optional Beta parameter used in some tweening methods</param>
        /// <returns></returns>
        public static double Tween(this Interpolation i, double t, double alpha = DefaultAlpha, double beta = DefaultBeta)
        {
            switch (i)
            {
                case Interpolation.Linear:
                    return t;
                case Interpolation.Quadratic:
                    return t.Power(2);
                case Interpolation.Cubic:
                    return t.Power(3);
                case Interpolation.SquareRoot:
                    return t.Root();
                case Interpolation.CubeRoot:
                    return t.Power(1.0 / 3);
                case Interpolation.Exponential: // Exponential interpolation: (e(a * (t / d)) - 1) * 1 / (e(a) - 1)
                    return (Math.Exp(-alpha * t) - 1) * 1 / (Math.Exp(alpha) - 1.0);
                case Interpolation.Elastic: //Pseudo-elastic interpolation: 1/(exp(a)-1)*cos(b*(t/d)*2*PI)*exp(a*(t/d))-1/(exp(a)-1)
                    return 1 / (Math.Exp(-alpha) - 1) * Math.Cos(beta * t * 2 * Math.PI) * Math.Exp(-alpha * t) - 1 / (Math.Exp(-alpha) - 1);
                case Interpolation.Sin:
                    return Math.Sin(t * 0.5 * Math.PI);
                case Interpolation.Cos:
                    return 1 - (Math.Cos(t * 1 * Math.PI) + 1) / 2;
                case Interpolation.Step:
                    return (t >= 0.5 ? 1 : 0);
            }
            return t;
        }
    }
}
