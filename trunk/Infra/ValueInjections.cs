using System;
using System.Collections.Generic;
using System.ComponentModel;
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

    public class IdToDisplay<T> : ValueInjection where T : EntityWithName, new()
    {
        protected override void Inject(object source, object target, PropertyDescriptorCollection sourceProps, PropertyDescriptorCollection targetProps)
        {
            var sp = sourceProps.GetByNameType<int>(typeof (T).Name + "Id");
            var tp = targetProps.GetByNameType<string>("Display" + typeof(T).Name);
            
            var t = IoC.Resolve<IUberRepo<T>>().Get((int) sp.GetValue(source));
            if(t != null)
            tp.SetValue(target,t.Name);
        }
    }

    public class IdToLookupMeasure: ValueInjection
    {
        protected override void Inject(object source, object target, PropertyDescriptorCollection sourceProps, PropertyDescriptorCollection targetProps)
        {
            var s = sourceProps.GetByNameType<int>("MeasureId");
            var t = targetProps.GetByNameType<object>("MeasureId");
            var value = (int)s.GetValue(source);
            var sv = IoC.Resolve<IMeasureRepo>().GetActives()
                .Select(o => new SelectListItem
                {
                    Value = o.Id.ToString(),
                    Text = o.Name,
                    Selected = o.Id == value
                });
            t.SetValue(target, sv);
        }
    }

    public class IdToLookup<T> : ValueInjection where T : EntityWithName, new()
    {
        protected override void Inject(object source, object target, PropertyDescriptorCollection sourceProps, PropertyDescriptorCollection targetProps)
        {
            var s = sourceProps.GetByNameType<int>(typeof(T).Name + "Id");
            var t = targetProps.GetByNameType<object>(typeof(T).Name + "Id");
            var value = (int)s.GetValue(source);
            var sv = IoC.Resolve<IUberRepo<T>>().GetAll()
                .Select(o => new SelectListItem
                                 {
                                     Value = o.Id.ToString(),
                                     Text = o.Name,
                                     Selected = o.Id == value
                                 });
            t.SetValue(target, sv);
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