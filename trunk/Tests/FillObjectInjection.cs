using Omu.ValueInjecter;

namespace MRGSP.ASMS.Tests
{
    public class FillObjectInjection : NoSourceValueInjection
    {
        protected override void Inject(object target)
        {
            var targetProps = target.GetProps();
            for (int i = 0; i < targetProps.Count; i++)
            {
                var prop = targetProps[i];
                object val = null;
                if (prop.PropertyType == typeof(string)) val = "a";
                if(val != null)
                    prop.SetValue(target, val);
            }
        }
    }
}