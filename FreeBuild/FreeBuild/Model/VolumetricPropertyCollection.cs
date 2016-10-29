﻿using FreeBuild.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeBuild.Model
{
    /// <summary>
    /// A collection of VolumetricProperty objects
    /// </summary>
    public class VolumetricPropertyCollection : ModelObjectCollection<VolumetricProperty>
    {
        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public VolumetricPropertyCollection() : base() { }

        /// <summary>
        /// Owner constructor
        /// </summary>
        /// <param name="model"></param>
        protected VolumetricPropertyCollection(Model model) : base(model) { }

        #endregion
    }
}