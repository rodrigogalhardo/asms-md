using System;
using MRGSP.ASMS.Core;

namespace MRGSP.ASMS.Data
{
    public static class TableConvention
    {
        public static string Resolve(Type t)
        {
            var name = t.Name;
            if (name.EndsWith("s")) return t.Name + "es";
            if (name.EndsWith("y")) return t.Name.RemoveSuffix("y") + "ies";

            return t.Name + "s";
        }

        public static string Resolve(object o)
        {
            return Resolve(o.GetType());
        }
    }
}