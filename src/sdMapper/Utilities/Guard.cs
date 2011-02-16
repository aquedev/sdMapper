using System;

namespace sdMapper.Utilities
{
    public static class Guard
    {
        public static void NotNull(object arg, string name)
        {
            if (arg == null)
                throw new ArgumentNullException(name);
        }
    }
}
