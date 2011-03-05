using System;
using sdMapper.Utilities;

namespace sdMapper
{
    public class Mapper
    {
        public static IServiceResolver Resolver;

        public static void Initialise(IServiceResolver resolver)
        {
            Guard.NotNull(resolver, "resolver");

            Resolver = resolver;
        }

        public Mapper()
        {

        }

        public ISitecoreSession CreateSession()
        {
            return Resolver.Resolve<ISitecoreSession>();
        }
    }
}
