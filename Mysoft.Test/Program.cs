using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mysoft.Core;
using System.Data.Common;
using System.Data;
using System.Linq.Expressions;

namespace Mysoft.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //List<User> user = new List<User>() { new User { Id = "1",Name ="2"} };

            //List<int> user2 = new List<int>() { 1,2,3 };
            //var ss = user as IEnumerable;
            //foreach (var zz in ss) {
            //    Console.WriteLine(zz);
            //}
            //DbQuery query;
            //try
            //{
            //    using ( query = DbQuery.NewTrans())
            //    {
            //        var objs = new object[] {
            //    new { Id = Guid.NewGuid(), Name = "1111" },
            //    new { Id = Guid.NewGuid(), Name = "2232" },
            //    new { Id = Guid.NewGuid(), Name = "3333" },
            //    new { Id = Guid.NewGuid(), Name = "4444" },

            //};
            //        query.ExecuteNoQuery("insert into users values(@Id,@Name)", objs[0]);
            //        query.ExecuteNoQuery("insert into users values(@Id,@Name)", objs[1]);
            //        query.Commit();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.Write(ex.Message);
            //}

            // DbDataReader reader = DbQuery.New().ExecuteDataReader("select * from users a inner join users b on a.id = b.id");
            //reader.Read();
           // var list = DbQuery.New().ExecuteList<User, User2>("select * from Users;select * from Users");
           var list2=  DbQuery.New().ExecuteList("Select * FROM Users");
            DbQuery.Insert(new User() { Id = Guid.NewGuid().ToString(), Name = "aaaaaaaaaa" });

            Console.Read();
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
