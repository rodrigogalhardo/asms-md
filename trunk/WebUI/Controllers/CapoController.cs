using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;
using MRGSP.ASMS.Infra.Dto;

namespace MRGSP.ASMS.WebUI.Controllers
{
    public class CapoController : BaseController
    {
        private readonly IUberRepo ur;
        private IUniRepo u;

        public CapoController(IUberRepo ur, IUniRepo u)
        {
            this.ur = ur;
            this.u = u;
        }

        public ActionResult Index()
        {
            return View(new CapoViewModel
                         {
                             SearchForm =
                                 new CapoSearchInput
                                     {
                                         StartDate = new DateTime(DateTime.Now.Year, 1, 1), 
                                         EndDate = DateTime.Now
                                     },
                                     List = Enumerable.Empty<Capo>()
                         });
        }

        public ActionResult Search(CapoSearchInput input)
        {
            var list = ur.GetCapo(input.MeasureId, input.StartDate, input.EndDate, input.PoState);
            return View(list) ;
        }

        public ActionResult Item(Capo o)
        {
            return View(o);
        }

        public ActionResult ItemByContract(int id)
        {
            return View("item", u.GetWhere<Capo>(new {ContractNr = id}).SingleOrDefault());
        } 
        
        public ActionResult ItemByAgreement(int id)
        {
            return View("item", u.GetWhere<Capo>(new {AgreementId = id}).SingleOrDefault());
        }

        public ActionResult ItemByPaymentOrder(int id)
        {
            return View("item",u.GetWhere<Capo>(new {PoId = id}).SingleOrDefault());
        }

        public ActionResult SearchForm(CapoSearchInput input)
        {
            return View(input);
        }
    }

    public static class ExtensionMethods
    {
        public static string RenderViewToString<T>(this ControllerBase controller,
                                string viewName, T model)
        {
            using (var writer = new StringWriter())
            {
                ViewEngineResult result = ViewEngines
                          .Engines
                          .FindView(controller.ControllerContext,
                                    viewName, null);

                var viewPath = ((WebFormView)result.View).ViewPath;
                var view = new WebFormView(viewPath);
                var vdd = new ViewDataDictionary<T>(model);
                var viewCxt = new ViewContext(
                                    controller.ControllerContext,
                                    view,
                                    vdd,
                                    new TempDataDictionary(), writer);
                viewCxt.View.Render(viewCxt, writer);
                return writer.ToString();
            }
        }
    }

}