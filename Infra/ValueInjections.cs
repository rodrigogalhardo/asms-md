using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Service;
using Omu.ValueInjecter;

namespace MRGSP.ASMS.Infra
{
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