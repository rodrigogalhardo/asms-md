using System.Linq;
using System.Web.Mvc;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;
using Omu.Awesome.Mvc;

namespace MRGSP.ASMS.WebUI.Controllers
{
    public class LocalityAutocompleteController : Controller
    {
        private readonly IRepo<Locality> repo;

        public LocalityAutocompleteController(IRepo<Locality> repo)
        {
            this.repo = repo;
        }

        public JsonResult Search(string searchText, int maxResults, int? parent)
        {
            var source = repo.GetWhereStartsWith("Name", searchText, maxResults);
            return Json(source.Select(p => new IdTextItem { Text = p.Name, Id = p.Id }));
        }
    }
}