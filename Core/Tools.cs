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

    }
}