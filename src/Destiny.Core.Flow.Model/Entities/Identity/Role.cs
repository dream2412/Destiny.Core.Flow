﻿using Destiny.Core.Flow.Entity;
using Destiny.Core.Flow.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Destiny.Core.Flow.Model.Entities.Identity
{
    [DisplayName("角色信息")]
    public  class Role:RoleBase<Guid>, IFullAuditedEntity<Guid>
    {

        /// <summary>
        ///  获取或设置 最后修改用户
        /// </summary>

        [DisplayName("最后修改用户")]
        public virtual Guid? LastModifierUserId { get; set; }

        /// <summary>
        /// 获取或设置 最后修改时间
        /// </summary>
        [DisplayName("最后修改时间")]
        public virtual DateTime? LastModifierTime { get; set; }

        /// <summary>
        ///获取或设置 是否删除
        /// </summary>
        [DisplayName("是否删除")]
        public virtual bool IsDeleted { get; set; }

        /// <summary>
        ///获取或设置 创建用户ID
        /// </summary>
        [DisplayName("创建用户ID")]
        public virtual Guid? CreatorUserId { get; set; }

        /// <summary>
        ///获取或设置 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public virtual DateTime CreatedTime { get; set; }


        /// <summary>
        ///获取或设置 描述
        /// </summary>
        [DisplayName("描述")]
        public virtual string Description { get; set; }



        /// <summary>
        ///获取或设置 编码
        /// </summary>
        [DisplayName("编码")]
        public virtual string Code { get; set; }
    }
}
