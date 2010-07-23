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