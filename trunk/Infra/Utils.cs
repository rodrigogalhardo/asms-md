using System;

namespace MRGSP.ASMS.Infra
{
    public static class Utils
    {
        public static long ReadInt64(object o)
        {
            return Convert.ToInt64((((string[])o)[0]));
        }
        
        public static int ReadInt32(object o)
        {
            return Convert.ToInt32((((string[])o)[0]));
        }
    }
}