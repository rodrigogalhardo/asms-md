using System;

namespace MRGSP.ASMS.Core
{
    [Serializable]
    public class AsmsEx : Exception
    {
        public AsmsEx(string message)
            : base(message)
        {
            
        }
    }
}