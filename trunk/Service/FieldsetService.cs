using System;
using System.Collections.Generic;
using System.Linq;
using MRGSP.ASMS.Core;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;
using MRGSP.ASMS.Core.Service;
using Omu.ValueInjecter;

namespace MRGSP.ASMS.Service
{
    public class FieldsetService : IFieldsetService
    {
        private readonly IFieldsetRepo fieldsetRepo;
        private readonly IUberRepo<FieldsetState> stateRepo;
        private readonly IUberRepo<Indicator> indicatorRepo;
        private readonly IUberRepo<Coefficient> cRepo;
        private readonly IFieldRepo fieldRepo;

        public FieldsetService(IFieldsetRepo fieldsetRepo,
            IUberRepo<FieldsetState> stateRepo,
            IUberRepo<Indicator> indicatorRepo,
            IFieldRepo fieldRepo,
            IUberRepo<Coefficient> cRepo)
        {
            this.fieldsetRepo = fieldsetRepo;
            this.cRepo = cRepo;
            this.fieldRepo = fieldRepo;
            this.indicatorRepo = indicatorRepo;
            this.stateRepo = stateRepo;
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
            Do(() => fieldsetRepo.ChangeState(id, (int) FieldsetStates.HasFields), FieldsetStates.Registered, id);
        }

        public void Activate(int id)
        {
            Do(() => fieldsetRepo.ChangeState(id, (int)FieldsetStates.Active), FieldsetStates.HasCoefficients, id);
        }

        public void Deactivate(int id)
        {
            Do(() => fieldsetRepo.ChangeState(id, (int)FieldsetStates.Inactive), FieldsetStates.Active, id);
        }

        public void HasIndicators(int id)
        {
            Do(() => fieldsetRepo.ChangeState(id, (int) FieldsetStates.HasIndicators), FieldsetStates.HasFields, id);
        }

        public void HasCoefficients(int id)
        {
            Do(() => fieldsetRepo.ChangeState(id, (int) FieldsetStates.HasCoefficients), FieldsetStates.HasIndicators, id);
        }

        public Fieldset Get(int id)
        {
            return fieldsetRepo.Get(id);
        }

        public int Create(Fieldset o)
        {
            o.StateId = 1;
            return fieldsetRepo.Insert(o);
        }

        public void CreateIndicator(Indicator o)
        {
            Do(() => indicatorRepo.Insert(o), FieldsetStates.HasFields, o.FieldsetId);
        }

        public void DeleteIndicator(int id)
        {
            var o = indicatorRepo.Get(id);
            Do(() => indicatorRepo.Delete(id), FieldsetStates.HasFields, o.FieldsetId);
        }

        public void CreateCoefficient(Coefficient o)
        {
            Do(() => cRepo.Insert(o), FieldsetStates.HasIndicators, o.FieldsetId);
        }

        public void DeleteCoefficient(int id)
        {
            var o = cRepo.Get(id);
            Do(() => cRepo.Delete(id), FieldsetStates.HasIndicators, o.FieldsetId);
        }

        public void Do(Action a, FieldsetStates state, int fieldsetId)
        {
            if (Check(fieldsetId, state))
                a();
            else
                Invalid();
        }

        public IPageable<FieldsetDisplay> GetPageable(int page, int pageSize)
        {
            var pageable = fieldsetRepo.GetPageable(page, pageSize);
            var states = stateRepo.GetAll();
            var displays = new Pageable<FieldsetDisplay>();
            displays.InjectFrom(pageable);
            displays.Page = pageable.Page.Join(states, fs => fs.StateId, s => s.Id, (fs, s) =>
                                                                               new FieldsetDisplay
                                                                                   {
                                                                                       Id = fs.Id,
                                                                                       EndDate = fs.EndDate,
                                                                                       Name = fs.Name,
                                                                                       State = s.Name
                                                                                   }).ToArray();

            return displays;
        }
    }
}