using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mysoft.Core
{
    public class IocContainer
    {
        private ContainerBuilder _builder = new ContainerBuilder();
        private IContainer _container;

        public  static IocContainer Instance { get; private set; } = new IocContainer();
        private IocContainer() { }

        internal ContainerBuilder ContainerBuilder { get { return _builder; } }

        public void RegisterOver()
        {
            _container = _builder.Build();
        }
        public TService GetService<TService>()
        {
            return _container.Resolve<TService>();
        }
        public object GetService(Type type)
        {
            return _container.Resolve(type);
        }

        public void RegisterType<TFrom, TTo>() where TTo : TFrom
        {
            _builder.RegisterType<TTo>().As<TFrom>();
        }
        public void RegisterType(Type from, Type to)
        {
            _builder.RegisterType(from).As(to);
        }
    }
}
