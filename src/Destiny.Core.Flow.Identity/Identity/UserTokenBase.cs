﻿
using Destiny.Core.Flow.Entity;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Destiny.Core.Flow.Identity
{
   
    public abstract class UserTokenBase<TUserKey> : EntityBase<Guid>
        where TUserKey : IEquatable<TUserKey>
    {
     
        [DisplayName("用户编号")]
        public TUserKey UserId { get; set; }

      
        [DisplayName("登录提供者")]
        public string LoginProvider { get; set; }

    
        [DisplayName("令牌名称")]
        public string Name { get; set; }

      
        [DisplayName("令牌值")]
        public string Value { get; set; }
    }
}
