using System;
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

        public CapoController(IUberRepo ur)
        {
            this.ur = ur;
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
            return View("index", new CapoViewModel { List = list, SearchForm = input }) ;
        }

        public ActionResult SearchForm(CapoSearchInput input)
        {
            return View(input);
        }
    }

    public class CurrentCultureDateTimeBinder : IModelBinder
    {
        private const string PARSE_ERROR = "\"{0}\" is not a valid date";
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            ValueProviderResult valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (valueProviderResult == null) return null;

            var date = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).AttemptedValue;

            if (String.IsNullOrEmpty(date))
                return null;

            bindingContext.ModelState.SetModelValue(bindingContext.ModelName, bindingContext.ValueProvider.GetValue(bindingContext.ModelName));

            try
            {
                return DateTime.Parse(date);
            }
            catch (Exception)
            {
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, String.Format(PARSE_ERROR, bindingContext.ModelName));
                return null;
            }
        }
    }
}