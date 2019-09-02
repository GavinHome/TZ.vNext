//-----------------------------------------------------------------------------------
// <copyright file="CommonExtensions.cs" company="TZ.vNext">
//     Copyright  TZ.vNext. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/11/6 8:13:46</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;

namespace TZ.vNext.ViewModel.Extensions
{
    public static class CommonExtensions
    {
        /// <summary>
        /// whether an entityset is null or empty
        /// </summary>
        /// <param name="obj">obj</param>
        /// <returns>bool</returns>
        public static bool IsNull(this IViewModel obj)
        {
            if (obj == null)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// whether an entityset is not null
        /// </summary>
        /// <param name="obj">obj</param>
        /// <returns>bool</returns>
        public static bool IsNotNull(this IViewModel obj)
        {
            return !obj.IsNull();
        }
    }
}
