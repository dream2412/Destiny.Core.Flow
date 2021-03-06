﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Destiny.Core.Flow.Mapping
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AutoMapToAttribute : AutoMappingAttribute
    {
        public override AutoMapDirection Direciton
        {
            get { return AutoMapDirection.To; }
        }
        public AutoMapToAttribute(params Type[] targetTypes)
            : base(targetTypes)
        {

        }
    }
}
