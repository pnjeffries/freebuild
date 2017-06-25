﻿using Nucleus.Geometry;
using Nucleus.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nucleus.Model
{
    /// <summary>
    /// Abstract base class for structural and physical load types that act in a particular
    /// direction
    /// </summary>
    /// <typeparam name="TAppliedTo"></typeparam>
    public abstract class ForceLoad<TAppliedTo> : Load<TAppliedTo>
        where TAppliedTo : ModelObjectSetBase, new()
    {
        #region Properties

        /// <summary>
        /// Private backing field for Direction property
        /// </summary>
        private Direction _Direction = Direction.Z;

        /// <summary>
        /// The axis (of the coordinate system specified by the Axes property)
        /// along which or about which this force is applied
        /// </summary>
        [AutoUIComboBox(Order=450)]
        public Direction Direction
        {
            get { return _Direction; }
            set { ChangeProperty(ref _Direction, value, "Direction"); }
        }

        /// <summary>
        /// Private backing field for Axes property
        /// </summary>
        private CoordinateSystemReference _Axes = CoordinateSystemReference.Global;

        /// <summary>
        /// The reference system used to resolve the application of the load
        /// in 3D space.  The direction property determines which axis of this
        /// system is used.
        /// </summary>
        [AutoUIComboBox(Order=475)]
        public CoordinateSystemReference Axes
        {
            get { return _Axes; }
            set { ChangeProperty(ref _Axes, value, "Axes"); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Set the force exerted by this load by specifying a direction vector and
        /// a value.  The direction and axis system will be derived from this information.
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="value"></param>
        public void SetForce(Vector direction, double value)
        {
            direction = direction.Unitize();
            if (direction.IsXOnly())
            {
                Direction = Direction.X;
                Value = value / direction.X;
                Axes = CoordinateSystemReference.Global;
            }
            else if (direction.IsYOnly())
            {
                Direction = Direction.Y;
                Value = value / direction.Y;
                Axes = CoordinateSystemReference.Global;
            }
            else if (direction.IsZOnly())
            {
                Direction = Direction.Z;
                Value = value / direction.Z;
                Axes = CoordinateSystemReference.Global;
            }
            else
            {
                //Need to generate axes:
                Axes = new UserCoordinateSystemReference("[Locally Defined]", new CartesianCoordinateSystem(new Vector(), direction));
                Direction = Direction.Z;
                Value = value;
            }
        }

        #endregion
    }
}
