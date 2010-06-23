using MRGSP.ASMS.Core.Model;
using Omu.ValueInjecter;

namespace MRGSP.ASMS.Infra
{
    public class UserBuilder<TInput> : BuilderBase<User, TInput> where TInput : new()
    {
        protected override User MakeEntity(User entity, TInput input)
        {
            return (User) base.MakeEntity(entity, input)
                .InjectFrom<LookupToRoles>(input);
        }

        protected override TInput MakeInput(TInput input, User entity)
        {
            return (TInput)base.MakeInput(input, entity)
                                         .InjectFrom<RolesToLookup>(entity);
        }
    }
}