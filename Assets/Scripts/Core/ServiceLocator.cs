using System;
using System.Collections.Generic;

namespace Game.Core
{
    public static class ServiceLocator
    {
        private static readonly Dictionary<Type, object> services = new Dictionary<Type, object>();

        public static void Register<T>(T service)
        {
            if (services.ContainsKey(typeof(T)))
            {
                services[typeof(T)] = service;
            }
            else
            {
                services.Add(typeof(T), service);
            }
        }

        public static T Get<T>()
        {
            if (services.TryGetValue(typeof(T), out object service))
            {
                return (T)service;
            }
            throw new Exception($"Service of type {typeof(T)} is not registered in the ServiceLocator.");
        }

        public static void Clear()
        {
            services.Clear();
        }
    }
}
