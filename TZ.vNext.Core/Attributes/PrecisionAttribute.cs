//-----------------------------------------------------------------------
// <copyright file="PrecisionAttribute.cs" company="TZ.vNext">
//     Copyright  TZ.vNext. All rights reserved.
// </copyright>
// <author>??</author>
// <date>2017/3/22 12:52:35</date>
//-----------------------------------------------------------------------
using System;

namespace TZ.vNext.Core.Attributes
{
    /// <summary>
    /// customization attribute to set precimal
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class PrecisionAttribute : Attribute
    {
        private readonly int Max = 28;

        /// <summary>
        /// Initializes a new instance of the PrecisionAttribute class.
        /// </summary>
        /// <param name="precision">precision</param>
        /// <param name="scale">scale</param>
        public PrecisionAttribute(byte precision = 18, byte scale = 2)
        {
            Precision = precision;
            Scale = scale;

            if (Precision < 1 || Precision > Max)
            {
                throw new ArgumentOutOfRangeException(nameof(precision), precision, "精度必须在1和28之间.");
            }

            if (Scale < 1 || Scale > Max)
            {
                throw new ArgumentOutOfRangeException(nameof(scale), scale, "刻度必须在1和28之间.");
            }
        }

        /// <summary>
        /// precision
        /// </summary>
        public byte Precision { get; set; }

        /// <summary>
        /// scale
        /// </summary>
        public byte Scale { get; set; }
    }
}
