using System;
using System.Collections.Generic;
using System.Linq;
using MRGSP.ASMS.Core;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;
using MRGSP.ASMS.Core.Service;
using Omu.Awesome.Core;
using Omu.ValueInjecter;

namespace MRGSP.ASMS.Service
{
    public class FieldsetService : CrudService<Fieldset>, IFieldsetService
    {
        private readonly IFieldsetRepo fieldsetRepo;
        private readonly IUniRepo u;
        private readonly IFieldRepo fieldRepo;
        private readonly IDossierRepo dRepo;


        public FieldsetService(IRepo<Fieldset> repo, IFieldsetRepo fieldsetRepo, IUniRepo u, IFieldRepo fieldRepo, IDossierRepo dRepo) : base(repo)
        {
            this.fieldsetRepo = fieldsetRepo;
            this.u = u;
            this.fieldRepo = fieldRepo;
            this.dRepo = dRepo;
        }

        public IEnumerable<Field> GetAssignedFields(int fieldsetId)
        {
            return fieldRepo.GetAssigned(fieldsetId);
        }

        public IEnumerable<Field> GetUnassignedFields(int fieldsetId)
        {
            return fieldRepo.GetUnassigned(fieldsetId);
        }

        private bool Check(int fieldsetId, FieldsetStates fieldsetState)
        {
            var fs = fieldsetRepo.Get(fieldsetId);
            return fieldsetState.IsEqual(fs.StateId);
        }

        public void Assign(int fieldId, int fieldsetId)
        {
            Do(() => fieldRepo.AssignField(fieldId, fieldsetId), FieldsetStates.Registered, fieldsetId);
        }

        private static void Invalid()
        {
            throw new Exception("wtf");
        }

        public void Unassign(int fieldId, int fieldsetId)
        {
            Do(() => fieldRepo.UnassignField(fieldId, fieldsetId), FieldsetStates.Registered, fieldsetId);
        }

        public void HasFields(int id)
        {
            Do(() => fieldsetRepo.ChangeState(id, (int)FieldsetStates.HasFields), FieldsetStates.Registered, id);
        }

        public void Activate(int id)
        {
            Do(() => fieldsetRepo.Activate(id), FieldsetStates.HasCoefficients, id);
        }

        public void Deactivate(int id)
        {
            Do(() => fieldsetRepo.ChangeState(id, (int)FieldsetStates.Inactive), FieldsetStates.Active, id);
        }

        public void HasIndicators(int id)
        {
            Do(() => fieldsetRepo.ChangeState(id, (int)FieldsetStates.HasIndicators), FieldsetStates.HasFields, id);
        }

        public void HasCoefficients(int id)
        {
            Do(() => fieldsetRepo.ChangeState(id, (int)FieldsetStates.HasCoefficients), FieldsetStates.HasIndicators, id);
        }

        public override void Create(Fieldset o)
        {
            o.StateId = 1;
            fieldsetRepo.Insert(o);
        }

        public void CreateIndicator(Indicator o)
        {
            Do(() => u.Insert(o), FieldsetStates.HasFields, o.FieldsetId);
        }

        public void DeleteIndicator(int id)
        {
            var o = u.Get<Indicator>(id);
            Do(() => u.Delete<Indicator>(id), FieldsetStates.HasFields, o.FieldsetId);
        }

        public void CreateCoefficient(Coefficient o)
        {
            o.Formula = o.Formula.Replace(" ", "");
            Do(() => u.Insert(o), FieldsetStates.HasIndicators, o.FieldsetId);
        }

        public void DeleteCoefficient(int id)
        {
            var o = u.Get<Coefficient>(id);
            Do(() => u.Delete<Coefficient>(id), FieldsetStates.HasIndicators, o.FieldsetId);
        }

        public void Do(Action a, FieldsetStates state, int fieldsetId)
        {
            if (Check(fieldsetId, state))
                a();
            else
                Invalid();
        }

        public IPageable<FieldsetDisplay> GetDisplayPageable(int page, int pageSize)
        {
            var pageable = u.GetPageable<Fieldset>(page, pageSize);
            var states = u.GetAll<FieldsetState>();
            var displays = new Pageable<FieldsetDisplay>();
            displays.InjectFrom(pageable);
            displays.Page = pageable.Page.Join(states, fs => fs.StateId, s => s.Id, (fs, s) =>
                                                                               new FieldsetDisplay
                                                                                   {
                                                                                       Id = fs.Id,
                                                                                       Year = fs.Year,
                                                                                       Name = fs.Name,
                                                                                       State = s.Name
                                                                                   }).ToArray();

            return displays;
        }

        public Fieldset GetActive()
        {
            return u.GetWhere<Fieldset>(new { stateId = (int)FieldsetStates.Active }).FirstOrDefault();
        }

        public IEnumerable<Field> GetFieldsByDossier(int dossierId)
        {
            var d = dRepo.Get(dossierId);
            d.IsNull().B("acest dosar nu exista");
            var fs = u.Get<Fieldset>(d.FieldsetId);
            return fieldRepo.GetAssigned(fs.Id);
        }
    }
}