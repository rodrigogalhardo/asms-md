using MRGSP.ASMS.Core.Model;
using Omu.ValueInjecter;
using MRGSP.ASMS.Infra.Dto;

namespace MRGSP.ASMS.Infra
{
    public class UserBuilder<TInput> : BuilderBase<User, TInput> where TInput : new()
    {
        protected override User MakeEntity(User entity, TInput input)
        {
            entity.InjectFrom<LookupToRoles>(input);
            return entity;
        }

        protected override TInput MakeInput(TInput input, User entity)
        {
            input.InjectFrom<RolesToLookup>(entity);
            return input;
        }
    }

    public class CaseBuilder : BuilderBase<Case, CaseCreateInput>
    {
        protected override CaseCreateInput MakeInput(CaseCreateInput input, Case entity)
        {
            input.InjectFrom<IdToLookup<Area>>(entity)
                 .InjectFrom<IdToLookup<District>>(entity)
                 .InjectFrom<IdToLookup<Consultant>>(entity)
                 .InjectFrom<IdToDisplay<Farmer>>(entity)
                 .InjectFrom<IdToDisplay<Bank>>(entity);
            return input;
        }
        protected override Case MakeEntity(Case entity, CaseCreateInput input)
        {
            entity.InjectFrom<LookupToLong>(input);
            return entity;
        }
    }

    public class FarmerCreateBuilder : BuilderBase<Farmer, FarmerCreateInput>
    {
        protected override FarmerCreateInput MakeInput(FarmerCreateInput input, Farmer entity)
        {
            input.InjectFrom<IdToLookup<CompanyType>>(entity);
            return input;
        }

        protected override Farmer MakeEntity(Farmer entity, FarmerCreateInput input)
        {
            entity.InjectFrom<LookupToLong>(input);
            return entity;
        }
    }
}