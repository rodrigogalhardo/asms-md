using System;

namespace MRGSP.ASMS.Infra
{
    public static class Utils
    {
        public static long ReadInt64(object o)
        {
            return Convert.ToInt64((((string[])o)[0]));
        }
    }
}