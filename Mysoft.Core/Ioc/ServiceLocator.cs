using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mysoft.Core
{
    public class ServiceLocator : IServiceProvider
    {
        private ServiceLocator() { }
        private static ServiceLocator _instance = new ServiceLocator();
        public static ServiceLocator Instance { get { return _instance; } } 

        public TService GetService<TService>()
        {
            return IocContainer.Instance.GetService<TService>();
        }

        public object GetService(Type serviceType)
        {
            return IocContainer.Instance.GetService(serviceType);
        }
    }
}
