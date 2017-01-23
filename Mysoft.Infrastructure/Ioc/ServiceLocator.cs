using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mysoft.Infrastructure
{
    /// <summary>
    /// 服务接口定位器
    /// </summary>
    public class ServiceLocator:IServiceProvider
    {
        public IocContainer IocContainer { get; }
        public ServiceLocator(IocContainer iocContainer) {
            if (iocContainer == null)
                throw new ArgumentNullException(nameof(iocContainer));
            IocContainer = iocContainer;
        }
        public static ServiceLocator Instance { get; } = new ServiceLocator(new IocContainer());

        public object GetService(Type serviceType)
        {
            var s = IocContainer.GetServiceType(serviceType);
            return Activator.CreateInstance(s.CurrentType);
        }

        public object GetSingle(Type serviceType)
        {
            var s = IocContainer.GetServiceType(serviceType);
            return s.Singleton ?? (s.Singleton = Activator.CreateInstance(s.CurrentType));
        }

        public TService GetService<TService>()
        {
            var s = IocContainer.GetServiceType(typeof(TService));
            return (TService)Activator.CreateInstance(s.CurrentType);
        }

        public TService GetSingle<TService>()
        {
            return (TService)GetSingle(typeof(TService));
        }
    }
}
