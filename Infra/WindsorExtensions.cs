using System;
using System.Linq;
using Castle.MicroKernel.Registration;

namespace MRGSP.ASMS.Infra
{
    public static class WindsorExtensions
    {
        public static BasedOnDescriptor FirstNonGenericCoreInterface(this ServiceDescriptor descriptor,
                                                                     string interfaceNamespace)
        {
            return descriptor
                .Select(delegate(Type type, Type baseType)
                            {
                                var source =
                                    type.GetInterfaces().Where(
                                        (t =>
                                         (!t.IsGenericType &&
                                          t.Namespace.StartsWith(interfaceNamespace))));
                                return source.Count() > 0 ? new[] { source.ElementAt(0) } : null;
                            });
        }
    }
}