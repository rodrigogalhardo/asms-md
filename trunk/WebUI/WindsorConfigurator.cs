using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Security;
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
            WindsorRegistrar.Register("q1", typeof(IBuilder<User, UserCreateInput>), typeof(UserBuilder<UserCreateInput>));
            WindsorRegistrar.Register("q2", typeof(IBuilder<User, UserEditInput>), typeof(UserBuilder<UserEditInput>));

            

            //WindsorRegistrar.Register("cs", typeof(IConnectionFactory), typeof(ConnectionFactory));
            //WindsorRegistrar.Register("smtpsn", typeof(ISmtpServerConfig), typeof(SmtpServerConfig));
            //WindsorRegistrar.Register("forms", typeof(IFormsAuthentication), typeof(FormAuths));
        }
    }
}