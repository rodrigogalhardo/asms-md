using System;
using System.Collections.Generic;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;
using Omu.Awesome.Core;

namespace MRGSP.ASMS.Core.Service
{
    public interface IMeasuresetService : ICrudService<Measureset>
    {
        IEnumerable<Measure> GetAssignedMeasures(int measuresetId);
        IEnumerable<Measure> GetUnassignedMeasures(int measuresetId);
        void Assign(int measureId, int measuresetId);
        void Unassign(int measureId, int measuresetId);
        IPageable<MeasuresetDisplay> GetDisplayPageable(int page, int pageSize);
        void Activate(int id);
        void Deactivate(int id);
        Measureset GetActive();
        MeasuresetDisplay GetDisplay(int id);
    }

    public interface IFieldsetService : ICrudService<Fieldset>
    {
        IEnumerable<Field> GetAssignedFields(int fieldsetId);
        IEnumerable<Field> GetUnassignedFields(int fieldsetId);
        void Assign(int fieldId, int fieldsetId);
        void Unassign(int fieldId, int fieldsetId);
        void CreateIndicator(Indicator o);
        void DeleteIndicator(int id);
        void CreateCoefficient(Coefficient o);
        void DeleteCoefficient(int id);
        void HasIndicators(int id);
        void HasCoefficients(int id);
        void HasFields(int id);
        void Activate(int id);
        void Deactivate(int id);
        Fieldset GetActive();
        IEnumerable<Field> GetFieldsByDossier(int dossierId);
        IPageable<FieldsetDisplay> GetDisplayPageable(int page, int pageSize);
        FieldsetDisplay GetDisplay(int id);
    }

    public interface IEcoCalc
    {
        IEnumerable<CoefficientValue> CalculateCoefficientValues(IEnumerable<IndicatorValue> indicatorValues, IEnumerable<Dossier> dossiers, IEnumerable<Coefficient> coefficients);
        IEnumerable<IndicatorValue> CalculateIndicatorValues(Dossier dossier, IEnumerable<FieldValue> fieldValues, IEnumerable<Indicator> indicators);
    }
    public interface IFpiService : IRepo<Fpi>
    {
        void ChangeAmount(int id, decimal amount, decimal amountm);
        void Create(Fpi o);
        void GoAgreement(int id);
        void Seal(int id);
        void PreviousGoAgreement(int id);
        /// <summary>
        /// closes previous fpis and moves their eligible (state = has_coeffiecients) dossiers to this one
        /// ranks the dossiers according to their value and changes the state to the winners
        /// </summary>
        /// <param name="fpiId">the fpi</param>
        void Rank(int fpiId);
        void Rerank(int fpiId);
        void Recalculate(int fpiId);
    }

    public interface IDossierService
    {
        int Create(Dossier o);
        Dossier Get(int id);
        IPageable<Dossier> GetPageable(int page, int pageSize);
        bool IsNoContest(int id);
        IEnumerable<Dossier> GetForTop(int measuresetId, int measureId, int month);
        void Disqualify(int id, string reason);
        void ChangeFieldValues(IEnumerable<FieldValue> fieldValues, int dossierId);

        void Init(IEnumerable<FieldValue> fieldValues, int dossierId);
        


        void Authorize(int dossierId);
        void ChangeAmountPayed(int id, decimal amountPayed);

    }

    public interface IFormulaValidationService
    {
        bool IsIndicatorFormulaValidForFieldset(int fieldsetId, string formula);
        bool IsCoefficientFormulaValidForFieldset(int fieldsetId, string formula);
    }
    public interface ISystemStateServcie
    {
        void AssureAbilityToCreateDossier();
    }

    public interface IUserService : ICrudService<User>
    {
        long Insert(User user);
        IPageable<User> GetPage(int page, int pageSize);
        IEnumerable<string> GetRoles(int id);
        bool Exists(string name);
        IEnumerable<Role> GetRoles();
        bool ChangePassword(int id, string password);
        User GetFull(int id);
        User Get(string name);
        bool Validate(string name, string password);
    }
    public interface ILocalityService : IRepo<Locality>
    {
        int? Resolve(int? id, string name);
    }

    public interface IBankService
    {
        long Create(Bank o);
        IPageable<Bank> GetPage(int page, int pageSize = 10, string name = null, string code = null);
        bool Exists(string code);
        string Delete(int id);
        Bank Get(long id);
    }

    public interface IAgreementService : ICrudService<Agreement>
    {
        IEnumerable<Agreement> GetByContractId(int contractId);
    }

    public interface IFarmerService
    {
        FarmerInfo GetInfo(int id);
        IPageable<FarmerInfo> GetPageableInfo(int page, int pageSize);
        IEnumerable<LandOwnerInfo> GetLandOwners(int farmerId);
        IEnumerable<OrganizationInfo> GetOrganizations(int farmerId);
    }

    public interface IContractService : ICrudService<Contract>
    {
        bool Exists(int dossierid);
        Contract GetByDossier(int dossierId);
    }

    public interface IPaymentOrderService : ICrudService<PaymentOrder>
    {
        void CreateForContract(PaymentOrder o, int id);
        void CreateForAgreement(PaymentOrder o, int id);
    }
    
    public interface ICrudService<T> where T : Entity, new()
    {
        int Create(T e);
        void Save(T e);
        IPageable<T> GetPageable(int page, int pageSize);
        void Delete(int id);
        T Get(int id);
        int Count();
    }
  

    public interface IReportDataService
    {
        object Contract(int id);
        IEnumerable<CrossDistrictMeasure> CrossDistrictMeasure(DateTime date, int measuresetId);
        IEnumerable<DossiersByDistrictReport> DossiersByDistrictReport(int year, int districtId);
        string GetDistrictName(int districtId);
        IEnumerable<CrossDistrictMeasureAmountPayed> CrossDistrictMeasureAmountPayed(DateTime date, int measuresetId);
        object Agreement(int id);
        IEnumerable<OperInfoReport> GetOperInfoReport(int measuresetId);
    }

    public interface IFarmersEntityService<T> where T : FarmersEntity, new()
    {
        IEnumerable<T> GetByFarmerId(int farmerId);
        int Create(T o);
        void Deactivate(int o);
    }

}