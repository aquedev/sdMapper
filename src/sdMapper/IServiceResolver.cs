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
        T Resolve<T>(string serviceName);
        T TryResolve<T>();
        T TryResolve<T>(string serviceName);
        
        object Resolve(Type type);
        object Resolve(Type type, string serviceName);

        object TryResolve(Type type);
        object TryResolve(Type type, string serviceName);

        IList<T> ResolveAll<T>();
        IList<object> ResolveAll(Type type);

        void Register(Type type, string serviceName);
        void RegisterAll<T>(Assembly assembly, Func<T, string> getServiceName);
        void RegisterAll<T>(string @namespace, Func<T, string> getServiceName);
        void RegisterAllGeneric<TOpenGeneric>(Assembly assembly, Func<object, string> getServiceName);
        void RegisterAllGeneric<TOpenGeneric>(string @namespace, Func<object, string> getServiceName);
    }
}
