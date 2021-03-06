﻿using Destiny.Core.Flow.Entity;
using Destiny.Core.Flow.Mapping;
using Destiny.Core.Flow.Model.Entities.Function;
using System;
using System.Collections.Generic;
using System.Text;

namespace Destiny.Core.Flow.Dtos.Functions
{
    [AutoMapping(typeof(Function))]
    public   class FunctionOutputPageList : FunctionDtoBase<Guid>, IOutputDto<Guid>
    {



        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }



        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? LastModifierTime { get; set; }

    }
}
