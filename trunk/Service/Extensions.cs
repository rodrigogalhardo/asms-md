using MRGSP.ASMS.Core;
using MRGSP.ASMS.Core.Model;

namespace MRGSP.ASMS.Service
{
    public static class Extensions
    {
        public static string Display(this AddressInfo o)
        {
            var dist = o.District.Contains("Chi") ? "" : "r.";
            var r = string.Format("{2} {0} loc. {1}", o.District, o.Locality, dist);
            if (!string.IsNullOrWhiteSpace(o.Street)) r += " str. " + o.Street;
            if (!string.IsNullOrWhiteSpace(o.House)) r += " bl. " + o.House;
            if (!string.IsNullOrWhiteSpace(o.Apartment)) r += " ap. " + o.Apartment;
            return r;
        }

        public static void B(this bool o, string m)
        {
            if(o) throw new AsmsEx(m);
        }
    }
}