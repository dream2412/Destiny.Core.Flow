﻿
using Destiny.Core.Flow.Identity;
using Destiny.Core.Flow.Model.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Destiny.Core.Flow.Model.Security
{
    public class RoleStore : RoleStoreBase<Role, Guid, RoleClaim>
    {

        public RoleStore(IEFCoreRepository<Role, Guid> roleRepository, IEFCoreRepository<RoleClaim, Guid> roleClaimRepository)
            : base(roleRepository, roleClaimRepository)
        { }
    }
}
