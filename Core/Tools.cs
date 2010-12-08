using MRGSP.ASMS.Core.Model;

namespace MRGSP.ASMS.Core
{
    public static class Tools
    {
        public static int GetPageCount(int pageSize, int count)
        {
            var pages = count / pageSize;
            if (count % pageSize > 0) pages++;
            return pages;
        }

        public static bool IsEqual(this FieldsetStates oo, int o)
        {
            return o == (int)oo;
        } 
        
        public static bool IsEqual(this States oo, int o)
        {
            return o == (int)oo;
        }

        public static object Value(this string o)
        {
            if (o == null) return null;
            return o.Trim().Length != 0 ? o.Trim() : null;
        }
    }

    public static class Extensions
    {
        public static string RemovePrefix(this string o, string prefix)
        {
            if (prefix == null) return o;
            return !o.StartsWith(prefix) ? o : o.Remove(0, prefix.Length);
        }

        public static string RemoveSuffix(this string o, string suffix)
        {
            if (suffix == null) return o;
            return !o.EndsWith(suffix) ? o : o.Remove(o.Length - suffix.Length, suffix.Length);
        }

        public static bool IsNull(this object o)
        {
            return o == null;
        }
        
        public static bool IsNotNull(this object o)
        {
            return o != null;
        }
    }
}