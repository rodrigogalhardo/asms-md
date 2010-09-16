using System.Collections.Generic;

namespace MRGSP.ASMS.Core
{
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
}
