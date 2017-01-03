using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mysoft.Core;
using System.Data.Common;
using System.Data;
using System.Linq.Expressions;
using Autofac;

namespace Mysoft.Test
{

    public class Test2 : FastVerifyEntity<string>
    {
        [NotNull("A")]
        public string A { get; set; }

        [NotNull("A")]
        public string B { get; set; }

        [NotNull("A")]
        public string C { get; set; }
    }
    class Program
    {



        static void Main(string[] args)
        {
            //  ContainerBuilder _builder = new ContainerBuilder();
            //  _builder.RegisterType<U>().As<IU>();
            //IContainer _container = _builder.Build();

            //IU iu = _container.Resolve<IU>();

            Test2 tes = new Test2();
            tes.A = "123";
            var res = tes.Verify();



            var reuslt = typeof(string).IsValueType();
            Console.WriteLine(reuslt);
             Console.Read();
        }

        public interface IU {
            void G();
        }
        public class U : IU {
            public void G() { }
        }

        


        public class User
        {
            public string Id { get; set; }
            public string Name { get; set; }

            public List<User2> Users { get; set; }
        }
        public class User2
        {
            public string Id { get; set; }

            public string UserId { get; set; }
            public string Name { get; set; }

        }
    }
}
