using System;
using System.ComponentModel;
using Omu.ValueInjecter;

namespace MRGSP.ASMS.WebUI.Controllers
{
    public class FillObject : NoSourceValueInjection
    {
        protected override void Inject(object target, PropertyDescriptorCollection targetProps)
        {
            for (var i = 0; i < targetProps.Count; i++)
            {
                var prop = targetProps[i];
                object val = null;
                if (prop.PropertyType == typeof(string)) val = "a";
                if (prop.PropertyType == typeof(DateTime)) val = DateTime.Now;
                if (val != null)
                    prop.SetValue(target, val);
            }
        }
    }
}