﻿using Destiny.Core.Flow.Caching;
using Destiny.Core.Flow.Events;
using Destiny.Core.Flow.Services.Users.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Destiny.Core.Flow.Services.Users.EventHandlers
{
    public class UserRoleCacheDeleteHandler : CacheHandlerBase<UserRoleCacheDeleteEvent>
    {
      

        public UserRoleCacheDeleteHandler(ICache cache):base(cache)
        {
         
        }

        public override async Task Handle(UserRoleCacheDeleteEvent eventData, CancellationToken cancellationToken)
        {
            var key = $"{UserCacheKeys.userRoleKeyPrefix}{eventData.UserId}";
            await _cache.RemoveAsync(key);
        }
    }
}
