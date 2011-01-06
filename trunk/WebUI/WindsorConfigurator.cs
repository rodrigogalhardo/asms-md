using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Security;
using MRGSP.ASMS.Infra;
using MRGSP.ASMS.Infra.Dto;

namespace MRGSP.ASMS.WebUI
{
    public class WindsorConfigurator
    {
        public static void Configure()
        {
            WindsorRegistrar.Register(typeof(IFormsAuthentication), typeof(FormAuthService));
            WindsorRegistrar.Register(typeof(IBuilder<Address, AddressInput>), typeof(AddressBuilder));
            WindsorRegistrar.Register(typeof(IBuilder<User, UserCreateInput>), typeof(UserBuilder<UserCreateInput>));
            WindsorRegistrar.Register(typeof(IBuilder<User, UserEditInput>), typeof(UserBuilder<UserEditInput>));
            WindsorRegistrar.Register(typeof(IBuilder<Dossier, DossierCreateInput>), typeof(DossierBuilder));

            WindsorRegistrar.RegisterAllFromAssemblies("MRGSP.ASMS.Data");
            WindsorRegistrar.RegisterAllFromAssemblies("MRGSP.ASMS.Service");
            WindsorRegistrar.RegisterAllFromAssemblies("MRGSP.ASMS.Infra");
        }

    }
}