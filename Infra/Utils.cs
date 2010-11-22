using System;

namespace MRGSP.ASMS.Infra
{
    public static class Utils
    {
        public static int ReadInt32(object o)
        {
            return Convert.ToInt32((((string[])o)[0]));
        }
    }
}