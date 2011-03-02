using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace sdMapper
{
    public interface IServiceResolver
    {
        T Resolve<T>();
        T TryResolve<T>();
        T Resolve<T>(string serviceName);
        
        object Resolve(Type type);
        object Resolve(Type type, string serviceName);

    }
}
