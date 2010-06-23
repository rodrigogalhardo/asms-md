using System;
using System.Reflection;
using Castle.MicroKernel.Registration;
using MvcContrib.Castle;

namespace MRGSP.ASMS.Infra
{
    public class WindsorRegistrar
    {

        private static WindsorRegistrar instance = new WindsorRegistrar();
        private static readonly object LockObj = new object();

        private WindsorRegistrar()
        {
        }

        public static WindsorRegistrar Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (LockObj)
                    {
                        if (instance == null)
                        {
                            instance = new WindsorRegistrar();
                        }
                    }
                }
                return instance;
            }

        }

        public static void Register(string key, Type interfaceType, Type implementationType)
        {
            IoC.Container.AddComponent(key, interfaceType, implementationType);
        }

        public static void RegisterControllers(params Assembly[] assemblies)
        {
            IoC.Container.RegisterControllers(assemblies);
        }

        public static void RegisterAllFromAssemblies(string baseAssembly, string relatedAssembly)
        {
            IoC.Container.Register(AllTypes.Pick().FromAssemblyNamed(baseAssembly).WithService.FirstNonGenericCoreInterface(relatedAssembly));
        }
    }
}