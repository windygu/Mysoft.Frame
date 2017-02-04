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
using Mysoft.DataManager;

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
            
            var list = MetaBusiness.GetAllClass();
            var obj = CommonDataObj.NewObj(list[0].Id);
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
