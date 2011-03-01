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

        public static void NotEmptyGuid(Guid arg, string name)
        {
            if (Guid.Empty == arg)
                throw new ArgumentException("Empty guid not allowed", name);
        }
    }
}
