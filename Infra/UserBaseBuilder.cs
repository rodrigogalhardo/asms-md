using MRGSP.ASMS.Core.Model;
using Omu.ValueInjecter;
using MRGSP.ASMS.Infra.Dto;

namespace MRGSP.ASMS.Infra
{
    public class UserBaseBuilder<TInput> : BaseBuilder<User, TInput> where TInput : new()
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

    public class DossierBuilder : BaseBuilder<Dossier, DossierCreateInput>
    {
        protected override DossierCreateInput MakeInput(DossierCreateInput input, Dossier entity)
        {
            input.InjectFrom<IdToLookup<Area>>(entity)
                .InjectFrom<IdToLookup<District>>(entity)
                .InjectFrom<IdToLookup<Consultant>>(entity);
            return input;
        }
        protected override Dossier MakeEntity(Dossier entity, DossierCreateInput input)
        {
            entity.InjectFrom<LookupToInt>(input);
            return entity;
        }
    }

    public class FarmerCreateBaseBuilder : BaseBuilder<Farmer, FarmerCreateInput>
    {
        protected override FarmerCreateInput MakeInput(FarmerCreateInput input, Farmer entity)
        {
            input.InjectFrom<IdToLookup<CompanyType>>(entity);
            return input;
        }

        protected override Farmer MakeEntity(Farmer entity, FarmerCreateInput input)
        {
            entity.InjectFrom<LookupToInt>(input);
            return entity;
        }
    }
}