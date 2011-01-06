using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;
using MRGSP.ASMS.Core.Service;
using Omu.ValueInjecter;

namespace MRGSP.ASMS.Infra
{
   public class LookupToInt : LoopValueInjection<object, int>
    {
        protected override int SetValue(object sourcePropertyValue)
        {
            return Utils.ReadInt32(sourcePropertyValue);
        }
    }

    public class IdToLookupMeasure : ExactValueInjection
    {
        public override string SourceName()
        {
            return "MeasureId";
        }

        protected override bool TypesMatch(Type sourceType, Type targetType)
        {
            return sourceType == typeof(int) && targetType == typeof(object);
        }

        protected override object SetValue(object sourcePropertyValue)
        {
            var value = (int)sourcePropertyValue;
            return IoC.Resolve<IMeasureRepo>().GetActives()
                .Select(o => new SelectListItem
                {
                    Value = o.Id.ToString(),
                    Text = o.Name + " " + o.Description,
                    Selected = o.Id == value
                }).ToList();
        }
    }


    public class IdToLookup<T> : ExactValueInjection where T : EntityWithName, new()
    {
        public override string SourceName()
        {
            return typeof(T).Name + "Id";
        }

        public override string TargetName()
        {
            return typeof(T).Name + "Id";
        }

        protected override bool TypesMatch(Type sourceType, Type targetType)
        {
            return sourceType == typeof(int) && targetType == typeof(object);
        }

        protected override object SetValue(object sourcePropertyValue)
        {
            var value = (int)sourcePropertyValue;
            return IoC.Resolve<IRepo<T>>().GetAll()
                .Select(o => new SelectListItem
                                 {
                                     Value = o.Id.ToString(),
                                     Text = o.Name,
                                     Selected = o.Id == value
                                 }).ToList();
        }
    }

    public class RolesToInts : LoopValueInjection<IEnumerable<Role>, IEnumerable<int>>
    {
        protected override IEnumerable<int> SetValue(IEnumerable<Role> v)
        {
            return v.Select(o => o.Id);
        }
    }

    public class IntsToRoles : LoopValueInjection<IEnumerable<int>, IEnumerable<Role>>
    {
        protected override IEnumerable<Role> SetValue(IEnumerable<int> v)
        {
            if (v == null) return Enumerable.Empty<Role>();

            return IoC.Resolve<IUserService>()
                .GetRoles()
                .Where(o => v.Contains(o.Id));
        }
    }

}