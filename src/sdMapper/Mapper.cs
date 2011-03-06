using System;
using sdMapper.Utilities;

namespace sdMapper
{
    public class Mapper
    {
        public static IServiceResolver Resolver;

        public static Mapper Initialise(IServiceResolver resolver)
        {
            Guard.NotNull(resolver, "resolver");

            Resolver = resolver;
            return new Mapper();
        }

        private Mapper()
        {

        }

        public ISitecoreSession CreateSession()
        {
            return Resolver.Resolve<ISitecoreSession>();
        }
    }
}
