using System;
using MRGSP.ASMS.Core.Model;

namespace MRGSP.ASMS.WebUI.Helpers
{
    public static class Extensions
    {
        public static string Display(this DateTime? d)
        {
            return !d.HasValue ? string.Empty : d.Value.ToShortDateString();
        }

        public static string Display(this DateTime d)
        {
            return d.ToShortDateString();
        }

        public static string Display(this Decimal d)
        {
            return d.ToString("N2");
        }

        public static string Display(this PoState? o)
        {
            return !o.HasValue
                       ? ""
                       : o.Value == PoState.Registered
                             ? "Înregistrat"
                             : o.Value == PoState.Waiting ? "În așteptare" : "Plătit";
        }
    }
}