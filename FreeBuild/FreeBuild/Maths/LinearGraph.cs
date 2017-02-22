﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeBuild.Maths
{
    /// <summary>
    /// An interpolatable data set of values along a single axis.
    /// </summary>
    public abstract class LinearGraph<TValue> : SortedList<double, TValue>
    {
        #region Properties

        /// <summary>
        /// Get the range of key values currently stored in this data set
        /// </summary>
        public Interval KeyRange
        {
            get
            {
                Interval result = Interval.Unset;
                foreach (double key in Keys)
                {
                    if (!result.IsValid) result = new Interval(key);
                    else result = result.Include(key);
                }
                return result;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initialise a new empty LinearGraph
        /// </summary>
        public LinearGraph() : base() { }

        /// <summary>
        /// Initialise a new LinearGraph from a list of values.
        /// The values will be plotted against their indices.
        /// </summary>
        /// <param name="values"></param>
        public LinearGraph(IList<TValue> values)
        {
            for (int i = 0; i < values.Count; i++)
            {
                Add(i, values[i]);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get (or interpolate) the value at the specified parameter
        /// </summary>
        /// <param name="t">The parameter at which to retrieve or derive a value</param>
        /// <returns></returns>
        public TValue ValueAt(double t)
        {
            if (ContainsKey(t)) return this[t]; // SHortcut: exact datapoint
            if (Count == 1) return Values[0]; // Shortcut: single value
            else if (Count == 2) return Interpolate(0, 1, t); // Shortcut: only two values

            // Find the appropriate two datapoints to interpolate between:
            int i0 = 0;
            int i1 = 1;
            for (int i = 0; i < Count; i++)
            {
                double key = Keys[i];
                if (key > t)
                {
                    if (i == 0)
                    {
                        i0 = 0;
                        i1 = 1;
                    }
                    else
                    {
                        i0 = i - 1;
                        i1 = i;
                    }
                    break;
                }
                else if (i == Count - 1)
                {
                    // Have got to last entry without exceeding t
                    i0 = i - 1;
                    i1 = i;
                }
            }

            return Interpolate(i0, i1, t);
        }

        /// <summary>
        /// Use straight-line interpolation to derive a value at the parameter t
        /// based on the entries in this list at indices i0 and i1.
        /// </summary>
        /// <param name="i0"></param>
        /// <param name="i1"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        protected abstract TValue Interpolate(int i0, int i1, double t);

        #endregion
    }
}
