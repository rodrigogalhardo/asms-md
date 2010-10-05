using Omu.ValueInjecter;

namespace MRGSP.ASMS.WebUI.Helpers
{
    public class ToHtml : KnownTargetValueInjection<string>
    {
        protected override void Inject(object source, ref string target)
        {
            if (source == null) return;
            var props = source.GetProps();
            for (var i = 0; i < props.Count; i++)
            {
                target += " " + props[i].Name + "= \"" + props[i].GetValue(source) + "\"";
            }
        }
    }
}