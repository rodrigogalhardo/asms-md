using System;
using System.Linq;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;
using MRGSP.ASMS.Core.Service;
using MRGSP.ASMS.Infra;
using Stimulsoft.Report;

namespace MRGSP.ASMS.WebUI
{
    public partial class Repor : System.Web.UI.Page
    {
        private readonly ICompetitorRepo competitorRepo;
        private readonly IReportDataService rds;

        public Repor()
        {
            competitorRepo = IoC.Resolve<ICompetitorRepo>();
            rds = IoC.Resolve<IReportDataService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            var name = Request["report"];
            if (string.IsNullOrWhiteSpace(name)) return;
            var report = new StiReport();
            report.Load(Server.MapPath("reports") +"\\"+ name + ".mrt");
            var data = new object();
            switch (name)
            {
                case "operInfo":
                    {
                        var id = Convert.ToInt32(Request["MeasuresetId"]);
                        data = rds.GetOperInfoReport(id);
                    }
                    break;
                case "agreement":
                    {
                        var id = Convert.ToInt32(Request["id"]);
                        data = rds.Agreement(id);
                    }
                    break;
                case "dossiersByDistrict":
                    {
                        var year = Convert.ToInt32(Request["year"]);
                        var district = Convert.ToInt32(Request["district"]);
                        data = rds.DossiersByDistrictReport(year, district);
                        report.RegBusinessObject("v", new { Name = rds.GetDistrictName(district) });
                    }
                    break;
                case "crossDistrictMeasure":
                    {
                        var measuresetId = Convert.ToInt32(Request["measuresetId"]);
                        var date = Convert.ToDateTime(Request["date"]);
                        data = rds.CrossDistrictMeasure(date, measuresetId);
                        report.RegBusinessObject("opt", new { Data = date });
                    }
                    break;
                case "crossDistrictMeasureAmountPayed":
                    {
                        var measuresetId = Convert.ToInt32(Request["measuresetId"]);
                        var date = Convert.ToDateTime(Request["date"]);
                        data = rds.CrossDistrictMeasureAmountPayed(date, measuresetId);
                        report.RegBusinessObject("opt", new { Data = date });
                    }
                    break;
                case "contract":
                    {
                        var id = Convert.ToInt32(Request["id"]);
                        data = rds.Contract(id);
                    }
                    break;
                case "auth":
                    {
                        var fpiId = Convert.ToInt32(Request["fpiId"]);
                        data = competitorRepo.GetWhere(new { fpiId, StateId = DossierStates.Authorized, Disqualified = false }).ToList();
                    }
                    break;
                case "losers":
                    {
                        var fpiId = Convert.ToInt32(Request["fpiId"]);
                        data = competitorRepo.Losers(fpiId).OrderByDescending(o => o.Value);
                    }
                    break;
            }
            report.RegData("o", data);
            StiWebViewerFx1.Report = report;
        }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            
            StiWebViewerFx1.ShowExportToMetafile = false;
            StiWebViewerFx1.ShowExportToPcx = false;

            StiWebViewerFx1.ShowExportToSvg = false;

        }
    }
}