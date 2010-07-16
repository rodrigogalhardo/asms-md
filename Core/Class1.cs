using System;
using System.Collections.Generic;
using MRGSP.ASMS.Core.Model;

namespace MRGSP.ASMS.Core
{
    public class Class1
    {
    }

    public interface IPageableInfo
    {
        int PageCount { get; set; }
        int PageIndex { get; set; }
    }

    public interface IPageable<T> : IPageableInfo
    {
        IEnumerable<T> Page { get; set; }
    }

    public class Pageable<T> : IPageable<T>
    {
        public int PageCount { get; set; }

        public IEnumerable<T> Page { get; set; }

        public int PageIndex { get; set; }
    }

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

    }
}
