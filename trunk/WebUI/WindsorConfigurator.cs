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
            WindsorRegistrar.Register("q1", typeof(ICreateBuilder<User, UserCreateInput>), typeof(UserCreateBuilder<UserCreateInput>));
            WindsorRegistrar.Register("q2", typeof(ICreateBuilder<User, UserEditInput>), typeof(UserCreateBuilder<UserEditInput>));
            WindsorRegistrar.Register("q3", typeof(ICreateBuilder<Dossier, DossierCreateInput>), typeof(DossierBuilder));
            WindsorRegistrar.Register("q4", typeof(ICreateBuilder<Organization, OrganizationInput>), typeof(OrganizationBuilder));
            WindsorRegistrar.Register("q5", typeof(ICreateBuilder<Address, AddressInput>), typeof(AddressBuilder));
            WindsorRegistrar.Register("qg", typeof(ICreateBuilder<,>), typeof(CreateBuilder<,>));
            WindsorRegistrar.Register("ur", typeof(IRepo<>), typeof(Repo<>));
            WindsorRegistrar.Register("cns", typeof(ICrudService<Contract>), typeof(ContractService));
            WindsorRegistrar.Register("edb", typeof(IEditBuilder<,>), typeof(EditBuilder<,>));




            //WindsorRegistrar.Register("cs", typeof(IConnectionFactory), typeof(ConnectionFactory));
            //WindsorRegistrar.Register("smtpsn", typeof(ISmtpServerConfig), typeof(SmtpServerConfig));
            //WindsorRegistrar.Register("forms", typeof(IFormsAuthentication), typeof(FormAuths));
        }
    }
}