//-----------------------------------------------------------------------
// <copyright file="GuardUtils.cs" company="TZEPM">
//     Copyright  TZEPM. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2019/01/15 12:52:35</date>
//-----------------------------------------------------------------------

using System;
using TZ.vNext.Core.Attributes;

namespace TZ.vNext.Core.Utility
{
    public static class GuardUtils
    {
        public static void NotNull<T>([ValidatedNotNull] this T value, string name) where T : class
        {
            if (value == null)
            {
                throw new ArgumentNullException(name);
            }
        }
    }
}
