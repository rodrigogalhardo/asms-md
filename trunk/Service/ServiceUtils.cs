using MRGSP.ASMS.Core;
using MRGSP.ASMS.Core.Repository;
using Omu.Awesome.Core;

namespace MRGSP.ASMS.Service
{
    public class ServiceUtils
    {
        public static int GetPageCount(int pageSize, int count)
        {
            var pages = count / pageSize;
            if (count % pageSize > 0) pages++;
            return pages;
        }

        public static IPageable<T> GetPage<T>(int page, int pageSize, IPagedRepo<T> repo)
        {
            return new Pageable<T>
            {
                Page = repo.GetPage(page, pageSize),
                PageCount = GetPageCount(pageSize, repo.Count()),
                PageIndex = page,
            };
        }

    }
}