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
        private readonly IUberRepo uberRepo;
        private readonly IUniRepo u;

        public ReportDataService(IUberRepo uberRepo, IUniRepo u)
        {
            this.uberRepo = uberRepo;
            this.u = u;
        }

        public string GetDistrictName(int districtId)
        {
            return u.Get<District>(districtId).Name;
        }

        public IEnumerable<OperInfoReport> GetOperInfoReport(int measuresetId)
        {
            return uberRepo.GetOperInfoReport(measuresetId);
        }

        public IEnumerable<DossiersByDistrictReport> DossiersByDistrictReport(int year, int districtId)
        {
            return uberRepo.DossiersByDistrictReport(year, districtId);
        }

        public IEnumerable<CrossDistrictMeasure> CrossDistrictMeasure(DateTime date, int measuresetId)
        {
            return uberRepo.GetCrossDistrictMeasure(date, measuresetId);
        }

        public IEnumerable<CrossDistrictMeasureAmountPayed> CrossDistrictMeasureAmountPayed(DateTime date, int measuresetId)
        {
            return uberRepo.GetCrossDistrictMeasureAmountPayed(date, measuresetId);
        }

        public object Contract(int id)
        {
            var c = u.Get<Contract>(id);
            var dossier = u.Get<Dossier>(c.DossierId);
            var fvi = u.Get<FarmerVersionInfo>(dossier.FarmerVersionId.Value);
            var measure = u.Get<Measure>(dossier.MeasureId);
            var a = u.GetWhere<AddressInfo>(new { fvi.FarmerId, EndDate = DBNull.Value }).FirstOrDefault();

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
                           Sprijinw = NumberToWords.Do(dossier.AmountPayed),
                           Invest = dossier.InvestmentValue.ToString("0.00"),
                           Investw = NumberToWords.Do(dossier.InvestmentValue),
                           Functia = !string.IsNullOrWhiteSpace(dossier.AdminFirstName) ? "Director" : "Reprezentant legal",
                           SprijinNr = c.SupportNr,
                           Filiala = c.BankName,
                       };
        } 
        
        public object Agreement(int id)
        {
            var a = u.Get<Agreement>(id);
            var c = u.Get<Contract>(a.ContractId);
            var dossier = u.Get<Dossier>(c.DossierId);
            var amount = u.GetWhere<Agreement>(new {a.ContractId}).Where(f => f.Nr <= a.Nr).Sum(j => j.Amount) +
                         dossier.AmountPayed; 


            var fvi = u.Get<FarmerVersionInfo>(dossier.FarmerVersionId.Value);
            var ai = u.GetWhere<AddressInfo>(new { fvi.FarmerId, EndDate = DBNull.Value }).FirstOrDefault();

            return new
                       {
                           a.Nr,
                           Data = a.Date.Value.ToShortDateString(),
                           Adresa = ai.Display(),
                           Beneficiar = fvi.Name,
                           Reprezentant =
                               !string.IsNullOrWhiteSpace(dossier.AdminFirstName)
                                   ? dossier.AdminFirstName + " " + dossier.AdminLastName
                                   : dossier.RepresentativeFirstName + " " + dossier.RepresentativeLastName,
                           FunctiaDe = !string.IsNullOrWhiteSpace(dossier.AdminFirstName) ? "în funcţia de Director" : " în calitate de reprezentant legal în baza procurii ",
                           Cd = c.Account,
                           CodBancar = c.BankCode,
                           Cf = fvi.FiscalCode,
                           Sprijin = amount.ToString("0.00"),
                           Sprijinw = NumberToWords.Do(amount),
                           Functia = !string.IsNullOrWhiteSpace(dossier.AdminFirstName) ? "Director" : "Reprezentant legal",
                           SprijinNr = c.SupportNr + " din " + c.Date.Value.ToShortDateString(),
                           Filiala = c.BankName,
                       };
        }
    }
}