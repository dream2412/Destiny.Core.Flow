﻿using Destiny.Core.Flow.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Destiny.Core.Flow.Audit
{
    public class GetChangeTracker: IGetChangeTracker
    {
        private readonly IPrincipal _principal;

        public GetChangeTracker(IPrincipal principal)
        {
            _principal = principal;
        }

        public async Task<List<AuditEntryInputDto>> GetChangeTrackerList(IEnumerable<EntityEntry> Entries)
        {
            await Task.CompletedTask;
            var list = new List<AuditEntryInputDto>();
            foreach (var entityEntry in Entries)
            {
                var auditentry = new AuditEntryInputDto();
                auditentry.EntityAllName = entityEntry.Metadata.Name;
                auditentry.EntityDisplayName = entityEntry.Entity.GetType().ToDescription();
                //auditentry.TableName=
                switch (entityEntry.State)
                {
                    case EntityState.Detached:
                        auditentry.OperationType = DataOperationType.None;
                        break;
                    case EntityState.Unchanged:
                        auditentry.OperationType = DataOperationType.None;
                        break;
                    case EntityState.Deleted:
                        auditentry.OperationType = DataOperationType.Delete;
                        break;
                    case EntityState.Modified:
                        auditentry.OperationType = DataOperationType.Update;
                        break;
                    case EntityState.Added:
                        auditentry.OperationType = DataOperationType.Add;
                        break;
                }
                var properties = entityEntry.Metadata.GetProperties();
                foreach (var propertie in properties)
                {
                    var AuditPropertys = new AuditPropertyEntryInputDto();
                    var propertyEntry = entityEntry.Property(propertie.Name);//获取字段名
                    if (propertyEntry.Metadata.IsPrimaryKey())
                    {
                        auditentry.KeyValues.Add(propertie.Name, propertyEntry.CurrentValue?.ToString());
                    }
                    else
                    {
                        AuditPropertys.Properties = propertie.Name;
                        AuditPropertys.NewValues = propertyEntry.CurrentValue?.ToString();
                        AuditPropertys.OriginalValues = propertyEntry.OriginalValue?.ToString();
                        AuditPropertys.PropertiesType = propertie.ClrType.FullName ;
                        AuditPropertys.PropertieDisplayName = propertyEntry.Metadata.PropertyInfo.ToDescription();
                        auditentry.AuditPropertys.Add(AuditPropertys);
                    }
                    
                }
                list.Add(auditentry);
            }
            return list;
        }
    }
}
