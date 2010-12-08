using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;
using MRGSP.ASMS.Core.Security;
using MRGSP.ASMS.Core.Service;
using MRGSP.ASMS.Data;
using MRGSP.ASMS.Infra;
using MRGSP.ASMS.Infra.Dto;
using MRGSP.ASMS.Service;
using MRGSP.ASMS.WebUI.Controllers;

namespace MRGSP.ASMS.WebUI
{
    public class WindsorConfigurator
    {
        public static void Configure()
        {
            WindsorRegistrar.RegisterAllFromAssemblies("MRGSP.ASMS.Data", "MRGSP.ASMS.Core");
            WindsorRegistrar.RegisterAllFromAssemblies("MRGSP.ASMS.Service", "MRGSP.ASMS.Core");

            WindsorRegistrar.RegisterControllers(typeof(HomeController).Assembly);
            WindsorRegistrar.Register("forms", typeof(IFormsAuthentication), typeof(FormAuthService));
            WindsorRegistrar.Register("s1", typeof(IFarmersEntityService<>), typeof(FarmersEntityService<>));
            WindsorRegistrar.Register("q1", typeof(IBuilder<User, UserCreateInput>), typeof(UserCreateBuilder<UserCreateInput>));
            WindsorRegistrar.Register("q2", typeof(IBuilder<User, UserEditInput>), typeof(UserCreateBuilder<UserEditInput>));
            WindsorRegistrar.Register("q3", typeof(IBuilder<Dossier, DossierCreateInput>), typeof(DossierBuilder));
            WindsorRegistrar.Register("q4", typeof(IBuilder<Organization, OrganizationInput>), typeof(OrganizationBuilder));
            WindsorRegistrar.Register("q5", typeof(IBuilder<Address, AddressInput>), typeof(AddressBuilder));
            WindsorRegistrar.Register("qg", typeof(IBuilder<,>), typeof(Builder<,>));
            WindsorRegistrar.Register("ur", typeof(IRepo<>), typeof(Repo<>));
            WindsorRegistrar.Register("oO", typeof(ICrudService<>), typeof(CrudService<>));

            //WindsorRegistrar.Register("cns", typeof(ICrudService<Contract>), typeof(ContractService));
        }
    }
}