using System;
using System.Collections.Generic;
using MRGSP.ASMS.Core.Model;

namespace MRGSP.ASMS.Core.Service
{
    public interface IMeasuresetService
    {
        IEnumerable<Measure> GetAssignedMeasures(int measuresetId);
        IEnumerable<Measure> GetUnassignedMeasures(int measuresetId);
        void Assign(int measureId, int measuresetId);
        void Unassign(int measureId, int measuresetId);
        IPageable<MeasuresetDisplay> GetPageable(int page, int pageSize);
        void Activate(int id);
        void Deactivate(int id);
        Measureset GetActive();
        Measureset Get(int id);
    }

    public interface IFieldsetService
    {
        IPageable<FieldsetDisplay> GetPageable(int page, int pageSize);
        int Create(Fieldset o);
        Fieldset Get(int id);
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
    }

    public interface IEcoCalc
    {
        IEnumerable<CoefficientValue> CalculateCoefficientValues(IEnumerable<IndicatorValue> indicatorValues, IEnumerable<Dossier> dossiers, IEnumerable<Coefficient> coefficients);
        IEnumerable<IndicatorValue> CalculateIndicatorValues(Dossier dossier, IEnumerable<FieldValue> fieldValues, IEnumerable<Indicator> indicators);
    }
    public interface IFpiService
    {
        void ChangeAmount(int id, decimal amount);
        Fpi Get(int id);
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
        void Recalculate(int fpiId);
        void Init(IEnumerable<FieldValue> fieldValues, int dossierId);
        void Rank(int fpiId);
        void Authorize(int dossierId);
        void ChangeAmountPayed(int id, decimal amountPayed);
        void Rerank(int fpiId);
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

    public interface IUserService
    {
        long Insert(User user);
        User Get(int id);
        IPageable<User> GetPage(int page, int pageSize);
        long Create(User user);
        IEnumerable<string> GetRoles(int id);
        bool Exists(string name);
        IEnumerable<Role> GetRoles();
        bool ChangePassword(int id, string password);
        void Save(User user);
        User GetFull(int id);
        User Get(string name);
        bool Validate(string name, string password);
    }

    public interface IBankService
    {
        long Create(Bank o);
        IPageable<Bank> GetPage(int page, int pageSize = 10, string name = null, string code = null);
        bool Exists(string code);
        string Delete(int id);
        Bank Get(long id);
    }

    public interface IFarmerService
    {
        FarmerInfo GetInfo(int id);
        IPageable<FarmerInfo> GetPageableInfo(int page, int pageSize);
        IEnumerable<LandOwnerInfo> GetLandOwners(int farmerId);
        IEnumerable<OrganizationInfo> GetOrganizations(int farmerId);
    }

    public interface IFarmersEntityService<T> where T : FarmersEntity, new()
    {
        IEnumerable<T> GetByFarmerId(int farmerId);
        int Create(T o);
        void Deactivate(int o);
    }

}