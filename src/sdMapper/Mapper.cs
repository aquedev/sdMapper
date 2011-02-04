using System;

namespace sdMapper
{
    public class Mapper
    {
        public Mapper()
        {

        }

        public ISitecoreSession CreateSession()
        {
            return new SitecoreSession();
        }
    }
}
