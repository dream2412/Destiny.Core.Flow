﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Destiny.Core.Flow.Mapping
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AutoMapFromAttribute : AutoMappingAttribute
    {
        public override AutoMapDirection Direciton
        {
            get { return AutoMapDirection.From; }
        }
        public AutoMapFromAttribute(params Type[] targetTypes)
            : base(targetTypes)
        {

        }
    }
}
