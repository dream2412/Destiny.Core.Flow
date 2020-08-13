﻿using Destiny.Core.Flow.Events;
using Destiny.Core.Flow.Events.EventBus;
using Destiny.Core.Flow.Services.RoleServices.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Destiny.Core.Flow.Caching;
using Microsoft.EntityFrameworkCore.Internal;
using Destiny.Core.Flow.Model.Entities.Rolemenu;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Destiny.Core.Flow.Services.RoleServices.EventHandlers
{

    public class RoleMenuCacheAddOrUpdateHandler : CacheHandlerBase<RoleMenuCacheAddOrUpdateEvent>
    {


        private readonly IServiceProvider _serviceProvider = null;

        public RoleMenuCacheAddOrUpdateHandler(IServiceProvider serviceProvider, ICache cache) :base(cache)
        {
            _serviceProvider = serviceProvider;

          

        }
        public override async Task Handle(RoleMenuCacheAddOrUpdateEvent notification, CancellationToken cancellationToken)
        {
   
            var key = notification.GetCacheKey();
            RoleMenuItem roleMenuItem = new RoleMenuItem();

            //var ss = _roleMenuRepository.Entities.Where(x => x.RoleId == notification.RoleId).ToListAsync();
            roleMenuItem.RoleId = notification.RoleId;
            roleMenuItem.MenuIds = notification.MenuIds;

            if (notification.EventState==EventState.Add)
            {
                await _cache.SetAsync(key, roleMenuItem);
            }
            else {

                await _cache.RemoveAsync(key);
                await _cache.SetAsync(key, roleMenuItem);
            }

        }

        internal class RoleMenuItem
        {
            public Guid RoleId { get; set; }



            public IEnumerable<Guid> MenuIds { get; set; }
        }
    }



 
}
