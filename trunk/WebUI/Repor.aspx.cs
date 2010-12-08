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

            switch (name)
            {
                case "operInfo":
                    {
                        var id = Convert.ToInt32(Request["MeasuresetId"]);
                        var data = rds.GetOperInfoReport(id);
                        report.Load(@"c:\operInfo.mrt");
                        report.RegData("o", data);
                    }
                    break;
                case "agreement":
                    {
                        var id = Convert.ToInt32(Request["id"]);
                        var data = rds.Agreement(id);
                        report.Load(@"c:\agreement.mrt");
                        report.RegData("o", data);
                    }
                    break;
                case "dossiersByDistrict":
                    {
                        var year = Convert.ToInt32(Request["year"]);
                        var district = Convert.ToInt32(Request["district"]);
                        var data = rds.DossiersByDistrictReport(year, district);
                        report.Load(@"c:\DossiersByDistrict.mrt");
                        report.RegData("o", data);
                        report.RegBusinessObject("v", new { Name = rds.GetDistrictName(district) });
                    }
                    break;
                case "crossDistrictMeasure":
                    {
                        var measuresetId = Convert.ToInt32(Request["measuresetId"]);
                        var date = Convert.ToDateTime(Request["date"]);
                        var data = rds.CrossDistrictMeasure(date, measuresetId);
                        report.Load(@"c:\crossDistrictMeasure.mrt");
                        report.RegData("o", data);
                        report.RegBusinessObject("opt", new { Data = date });
                    }
                    break;
                case "crossDistrictMeasureAmountPayed":
                    {
                        var measuresetId = Convert.ToInt32(Request["measuresetId"]);
                        var date = Convert.ToDateTime(Request["date"]);
                        var data = rds.CrossDistrictMeasureAmountPayed(date, measuresetId);
                        report.Load(@"c:\crossDistrictMeasureAmountPayed.mrt");
                        report.RegData("o", data);
                        report.RegBusinessObject("opt", new { Data = date });
                    }
                    break;
                case "contract":
                    {
                        var id = Convert.ToInt32(Request["id"]);
                        var data = rds.Contract(id);
                        report.Load(@"c:\contract.mrt");
                        report.RegData("contract", data);
                    }
                    break;
                case "auth":
                    {
                        var fpiId = Convert.ToInt32(Request["fpiId"]);
                        report.Load(@"c:\auth.mrt");
                        var data = competitorRepo.GetWhere(new { fpiId, StateId = DossierStates.Authorized, Disqualified = false }).ToList();
                        report.RegData("farmers", data);
                    }
                    break;
                case "losers":
                    {
                        var fpiId = Convert.ToInt32(Request["fpiId"]);
                        report.Load(@"c:\losers.mrt");
                        var data = competitorRepo.Losers(fpiId).OrderByDescending(o => o.Value);
                        report.RegData("farmers", data);
                    }
                    break;
            }

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