﻿using System.Collections.Generic;
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

    public interface IDossierService
    {
        int Create(Dossier o);
        Dossier Get(int id);
        void Go(IEnumerable<FieldValue> fieldValues);
        IPageable<Dossier> GetPageable(int page, int pageSize);
        bool IsNoContest(int id);
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
        IEnumerable<string> GetRoles(long id);
        bool Exists(string name);
        IEnumerable<Role> GetRoles();
        bool ChangePassword(long id, string password);
        void Save(User user);
        User GetFull(int id);
        bool Exists(string name, string password);
        User Get(string name, string password);
        User Get(string name);
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
        long Create(Farmer o);
        IPageable<Farmer> GetPage(int page, int pageSize, string name = null, string code = null);
        bool Exists(string code);
        Farmer Get(long id);
    }

}