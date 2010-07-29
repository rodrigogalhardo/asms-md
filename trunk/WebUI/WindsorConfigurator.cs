using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;
using MRGSP.ASMS.Core.Security;
using MRGSP.ASMS.Data;
using MRGSP.ASMS.Infra;
using MRGSP.ASMS.Infra.Dto;
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
            WindsorRegistrar.Register("q1", typeof(IBuilder<User, UserCreateInput>), typeof(UserBaseBuilder<UserCreateInput>));
            WindsorRegistrar.Register("q2", typeof(IBuilder<User, UserEditInput>), typeof(UserBaseBuilder<UserEditInput>));
            WindsorRegistrar.Register("q3", typeof(IBuilder<Dossier, DossierCreateInput>), typeof(DossierBuilder));
            WindsorRegistrar.Register("q4", typeof(IBuilder<Farmer, FarmerCreateInput>), typeof(FarmerCreateBaseBuilder));
            WindsorRegistrar.Register("q5", typeof(IBuilder<, >), typeof(BaseBuilder<,>));
            WindsorRegistrar.Register("ur", typeof(IRepo<>), typeof(Repo<>));

            

            //WindsorRegistrar.Register("cs", typeof(IConnectionFactory), typeof(ConnectionFactory));
            //WindsorRegistrar.Register("smtpsn", typeof(ISmtpServerConfig), typeof(SmtpServerConfig));
            //WindsorRegistrar.Register("forms", typeof(IFormsAuthentication), typeof(FormAuths));
        }
    }
}