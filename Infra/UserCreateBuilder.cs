using MRGSP.ASMS.Core.Model;
using Omu.ValueInjecter;

namespace MRGSP.ASMS.Infra
{
    public class UserCreateBuilder<TInput> : CreateBuilder<User, TInput> where TInput : new()
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
}