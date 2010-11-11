using System;
using System.Collections.Generic;
using System.Linq;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;
using MRGSP.ASMS.Core.Service;

namespace MRGSP.ASMS.Service
{
    public class ReportDataService : IReportDataService
    {
        private readonly IDossierRepo dossierRepo;
        private readonly IRepo<FarmerVersionInfo> fviRepo;
        private readonly IRepo<AddressInfo> aiRepo;
        private readonly IRepo<Measure> measureRepo;
        private readonly IRepo<Contract> contractRepo;
        private readonly IUberRepo uberRepo;
        public ReportDataService(
            IDossierRepo dossierRepo, 
            IRepo<FarmerVersionInfo> fviRepo, 
            IRepo<AddressInfo> aiRepo, 
            IRepo<Measure> measureRepo, 
            IRepo<Contract> contractRepo, IUberRepo uberRepo)
        {
            this.dossierRepo = dossierRepo;
            this.uberRepo = uberRepo;
            this.fviRepo = fviRepo;
            this.aiRepo = aiRepo;
            this.measureRepo = measureRepo;
            this.contractRepo = contractRepo;
        }

        public IEnumerable<CrossDistrictMeasure> CrossDistrictMeasure(DateTime date, int measuresetId)
        {
            return uberRepo.GetCrossDistrictMeasure(date, measuresetId);
        }

        public object Contract(int id)
        {

            var c = contractRepo.Get(id);
            var dossier = dossierRepo.Get(c.DossierId);
            var fvi = fviRepo.Get(dossier.FarmerVersionId);
            var measure = measureRepo.Get(dossier.Id);
            var a = aiRepo.GetWhere(new { fvi.FarmerId, EndDate = DBNull.Value }).FirstOrDefault();

            return new
                       {
                           Nr = c.Id,
                           Data = c.Date.Value.ToShortDateString(),
                           Masura = measure.Name,
                           Titlu = measure.Description,
                           Adresa = a.Display(),
                           Beneficiar = fvi.Name,
                           Reprezentant =
                               !string.IsNullOrWhiteSpace(dossier.AdminFirstName)
                                   ? dossier.AdminFirstName + " " + dossier.AdminLastName
                                   : dossier.RepresentativeFirstName + " " + dossier.RepresentativeLastName,
                           FunctiaDe = !string.IsNullOrWhiteSpace(dossier.AdminFirstName) ? "în funcţia de Director" : " în calitate de reprezentant legal în baza procurii ",
                           Cd = c.Account,
                           CodBancar = c.BankCode,
                           Cf = fvi.FiscalCode,
                           Sprijin = dossier.AmountPayed.ToString("0.00"),
                           Invest = dossier.InvestmentValue.ToString("0.00"),
                           Functia = !string.IsNullOrWhiteSpace(dossier.AdminFirstName) ? "Director" : "Reprezentant legal",
                           SprijinNr = c.SupportNr,
                           Filiala = c.BankName,
                       };
        }
    }
}