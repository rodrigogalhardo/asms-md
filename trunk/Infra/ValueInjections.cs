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
    //public class NormalToNullables : LoopValueInjection
    //{
    //    protected override bool TypesMatch(Type sourceType, Type targetType)
    //    {
    //        var type = Nullable.GetUnderlyingType(targetType);
    //        if (type == null) return false;
    //        return sourceType == type;
    //    }
    //}

    ////e.g. from int? to int, bool? to bool, DateTime? to DateTime
    //public class NullablesToNormal : LoopValueInjection
    //{
    //    protected override bool TypesMatch(Type sourceType, Type targetType)
    //    {
    //        var type = Nullable.GetUnderlyingType(sourceType);
    //        if (type == null) return false;
    //        return targetType == type;
    //    }
    //}   

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

    public class RolesToLookup : LoopValueInjection<IEnumerable<Role>, object>
    {
        protected override object SetValue(IEnumerable<Role> sourcePropertyValue)
        {
            var roles = IoC.Resolve<IUserService>().GetRoles();

            return roles.Select(o => new SelectListItem
                                         {
                                             Text = o.Name,
                                             Value = o.Id.ToString(),
                                             Selected = sourcePropertyValue.Select(v => v.Id).Contains(o.Id)
                                         });
        }
    }

    public class LookupToRoles : LoopValueInjection<object, IEnumerable<Role>>
    {
        protected override IEnumerable<Role> SetValue(object sourcePropertyValue)
        {
            if (sourcePropertyValue == null) return Enumerable.Empty<Role>();

            var keys = (string[])sourcePropertyValue;

            return IoC.Resolve<IUserService>()
                .GetRoles()
                .Where(o => keys.Contains(o.Id.ToString()));
        }
    }

}