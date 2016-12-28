using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Mysoft.Core
{
    public partial class DbQuery : IDisposable
    {
        private DbConnection conn = null;
        private DbTransaction trans = null;
       
        public DbConnection Connection { get { return conn; } }
        public bool IsTrans { get { return trans != null; } }
        public bool IsStill { get; private set; }
        private DbQuery()
        {
            conn = DbProvider.NewConnection();
        }

        public static DbQuery New(bool isStill=false)
        {
            return new DbQuery() { IsStill = isStill };
        }
        public static DbQuery NewTrans()
        {
            var query = new DbQuery();
            query.BeginTrans();
            return query;
        }

        public void Commit()
        {
            if (trans != null)
                trans.Commit();
        }
        public void RollBack()
        {
            if (trans != null)
                trans.Rollback();
        }
        public DbTransaction BeginTrans()
        {
            if(conn.State != ConnectionState.Open)
                conn.Open();
            trans = conn.BeginTransaction();
            return trans;
        }

        public void Dispose()
        {
            if (conn!=null && conn.State != ConnectionState.Closed)
                conn.Close();
            conn.Dispose();
        }


        /// <summary>
        ///  执行SQL，返回影响行数
        /// </summary>
        /// <returns></returns>
        public int ExecuteNoQuery(string sql,object param = null)
        {
            using (var context = new SqlExecuteContext(conn, trans,IsStill))
            {
                var comm =  CreateDbCommand(sql, param);
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                return  comm.ExecuteNonQuery(); 
            } 
        }

        /// <summary>
        /// 执行SQL，返回影响行数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public int ExecuteNoQuery(string sql, IEnumerable<object> param)
        {
            using (var context = new SqlExecuteContext(conn, trans,IsStill))
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                int result = 0;
                foreach (var p in param) {
                    var comm =  CreateDbCommand(sql, p);
                    result += comm.ExecuteNonQuery();
                }
                return result;
            }
        }

        /// <summary>
        /// 返回第一行第一列的值
        /// </summary> 
        /// <returns></returns>
        public T ExecuteScalar<T>(string sql, object param = null)
        {
            using (var context = new SqlExecuteContext(conn, trans, IsStill))
            {
               var comm =   CreateDbCommand(sql, param);
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                return comm.ExecuteScalar().To<T>();
            }

        }

        /// <summary>
        /// 执行SQL，返回datatable
        /// </summary>
        /// <returns></returns>
        public DataTable ExecuteDataTable(string sql, object param = null)
        {
            using (var context = new SqlExecuteContext(conn, trans,IsStill))
            {
                var comm =  CreateDbCommand(sql, param);
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                DbDataAdapter dbDataAdapter = DbProvider.NewAdapter();
                dbDataAdapter.SelectCommand = comm;
                DataTable dataTable = new DataTable();
                dbDataAdapter.Fill(dataTable);
                return dataTable;
            } 
        }


        /// <summary>
        /// 执行SQL，返回dataSet
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public DataSet ExecuteDataSet(string sql, object param = null)
        {
            using (var context = new SqlExecuteContext(conn, trans,IsStill))
            {
                var comm =  CreateDbCommand(sql, param);
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                DbDataAdapter dbDataAdapter = DbProvider.NewAdapter();
                dbDataAdapter.SelectCommand = comm;
                DataSet dataset = new DataSet();
                dbDataAdapter.Fill(dataset);
                return dataset;
            } 
        }


        /// <summary>
        /// 返回第一行，并转化成实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public T ExecuteSingle<T>(string sql, object param = null,Action<T, DbReader> anfterAction=null) where T : class, new()
        {
            using (var context = new SqlExecuteContext(conn, trans,IsStill))
            {
                var cmd = CreateDbCommand(sql, param);
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    reader.Read();
                    var t =(T)GetEntity(reader, typeof(T));
                    anfterAction?.Invoke(t, new DbReader(reader));
                    return t;   
                }
            }
           
        }

        public DynamicEntity ExecuteSingle(string sql, object param = null) {
            using (var context = new SqlExecuteContext(conn, trans,IsStill))
            {
                var cmd = CreateDbCommand(sql, param);
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    reader.Read();
                    var item = new DynamicEntity();
                    for (var i = 0; i < reader.FieldCount; i++)
                    {
                        item[reader.GetName(i)]= reader[i];
                    }
                    return item;
                }
            }
        }
        public List<DynamicEntity> ExecuteList(string sql, object param=null)
        {
            using (var context = new SqlExecuteContext(conn, trans,IsStill))
            {
                var cmd = CreateDbCommand(sql, param);
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    List<DynamicEntity> list = new List<DynamicEntity>();
                    while (reader.Read())
                    {
                        var item = new DynamicEntity();
                        for (var i = 0; i < reader.FieldCount; i++)
                        {
                            item[reader.GetName(i)] = reader[i];
                        }
                        list.Add(item);                        
                    }
                    return list;
                }
            }

        }
        public List<T> ExecuteList<T>(string sql, object param = null, Action<T, DbReader> anfterAction = null) where T : class, new()
        {
            using (var context = new SqlExecuteContext(conn, trans,IsStill))
            {
                var list = new List<T>();
                var type = typeof(T);
                var properties = type.GetProperties(System.Reflection.BindingFlags.SetProperty);
                var cmd = CreateDbCommand(sql, param);
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                    {
                        var t = (T)GetEntity(reader, type, properties);
                        anfterAction?.Invoke(t, new DbReader(reader));
                        list.Add(t);
                    }
                    return list;
                }
            } 

        }

        //public List<T1> ExecuteList<T1, T2>(string sql, object param = null,Func<List<T1>, List<T2>, List<T1>> func=null) {
        //    using (var context = new SqlExecuteContext(conn, trans,IsStill))
        //    {
        //        var list = new List<T1>();
        //        var type = typeof(T1);
        //        List<T2> list2 = new List<T2>();
        //        var type2 = typeof(T2);
        //        var properties = type.GetProperties( );
        //        var properties2 = type2.GetProperties( );
        //        var cmd = CreateDbCommand(sql, param);
        //        if (conn.State != ConnectionState.Open)
        //            conn.Open();
        //        if (func == null)
        //        {
        //            func = (x, y)
        //                =>
        //            {
        //                //1.1找到T1中 与T2类型一致,表示T1：T2 是 N：1
        //                var t = properties.FirstOrDefault(p => p.PropertyType == type2);
        //                if (t != null) {
        //                    x.ForEach(x1 =>
        //                    {
        //                        //1.2 找到 T1的外键
        //                        var fkey = properties.First(p => p.Name.IsSame(t.Name + "Id"));
        //                        var fValue = fkey.FastGetValue(x1);
        //                        //1.3. 找到 T2的主键
        //                        var t2Key = properties2.First(p => p.Name.IsSame("Id"));
        //                        //1.4.遍历T2集合，找到与T1外键匹配的项
        //                        t.FastSetValue(x1, y.FirstOrDefault(y1 => t2Key.FastGetValue(y1).Equals(fValue)));
        //                    });
        //                }
        //                //2.1 找到T1中 与T2类型一致,表示T1：T2 是 1：N
                       
        //               else if ((t =properties.FirstOrDefault(p => p.PropertyType.IsGenericType && p.PropertyType == typeof(List<T2>)))!= null) {
        //                    x.ForEach(x1 =>
        //                    {
        //                        //1.2 找到 T2的外键
        //                        var fkey = properties2.First(p => p.Name.IsSame(type.Name + "Id"));
        //                        //1.3 找到 T1的主键
        //                        var t1key = properties.First(p => p.Name.IsSame("Id"));
        //                        var t1value = t1key.FastGetValue(x1);
        //                        //1.4.遍历T2集合，找到与T1主键键匹配的项
        //                        t.FastSetValue(x1, y.Where(y1 => fkey.FastGetValue(y1)==(t1value)).ToList());
        //                    });
        //                }
        //                return x;
        //            };

        //        }

        //        using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
        //        {   
        //            while (reader.Read())
        //            {
        //                var t = (T1)GetEntity(reader, type, properties);
        //                list.Add(t);
        //            }
                    
        //            if (reader.NextResult()) { 
        //                while (reader.Read())
        //                {
        //                    var t2 = (T2)GetEntity(reader, typeof(T2));
        //                    list2.Add(t2);
        //                }
        //            }
        //            return func(list,list2);
        //        }
        //    } 
        //}

        public DbDataReader ExecuteDataReader(string sql, object param = null)
        {
            var cmd = CreateDbCommand(sql, param);
            if (conn.State != ConnectionState.Open)
                conn.Open();
            DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return reader;
        }

        public DbCommand CreateDbCommand(string sql, object param)
        {
            var comm = Connection.CreateCommand();
            comm.CommandText = sql;
            comm.Transaction = trans;
            if (param != null)
            {
                foreach (var p in param.GetType().GetProperties())
                {
                    if (!p.PropertyType.IsSimpleType())
                        continue;
                    var pa = comm.CreateParameter();
                    pa.ParameterName = "@" + p.Name;
                    pa.Value = p.FastGetValue(param);
                    comm.Parameters.Add(pa);
                }
            }
            return comm;
        }

        private object GetEntity(DbDataReader dr,  Type type, PropertyInfo[] properties = null, int startNumber = 0) 
        {
            object obj = Activator.CreateInstance(type);
            properties = properties ?? type.GetProperties( );
            for (var i = startNumber; i < dr.FieldCount; i++) {
                var name = dr.GetName(i);
                var p = properties.FirstOrDefault(x => x.Name.IsSame(name));
                if (p != null) {
                    p.FastSetValue(obj, dr[i].To(p.PropertyType));
                }
            }
            return obj;
        }
    }
    public class SqlExecuteContext : IDisposable
    {
        public DbConnection Connection { get; set; }
        public DbTransaction Trans { get; set; }

        public bool IsTrans { get; set; }
        public DbQuery DbQuery { get; private set; }

        public bool IsStill { get; private set; }
        public SqlExecuteContext(DbConnection conn,DbTransaction trans)
        {
            this.Connection = conn;
            this.Trans = trans;
        }
        public SqlExecuteContext(DbConnection conn, DbTransaction trans,bool isStill)
        {
            this.Connection = conn;
            this.Trans = trans;
            IsTrans = trans != null;
            IsStill = IsTrans || isStill;
        }

        public SqlExecuteContext(DbQuery dbquery)
        {
            
            IsTrans = dbquery.IsTrans;
            IsStill = IsTrans || dbquery.IsStill;
        }
 
        public void Dispose()
        {

            if (!IsTrans)
            {
                if (Connection.State != ConnectionState.Closed)
                    Connection.Close();
                if (!IsStill)
                {
                    Connection.Dispose();
                }
            } 
            
        }
    }

    public partial class DbQuery
    {
        private static Dictionary<Type, string> insertSqlDic = new Dictionary<Type, string>();

        private static Dictionary<Type, string> updateSqlDic = new Dictionary<Type, string>();
        public static int Insert<T>(T entity) where T : class, new()
        {
            var type = typeof(T);
            var ps = type.GetProperties().Where(x => x.CanRead && x.CanWrite).ToList();
            string sql = null;
            if (insertSqlDic.ContainsKey(type))
            {
                sql = insertSqlDic[type];
            }
            else {
                StringBuilder sb1 = new StringBuilder();
                StringBuilder sb2 = new StringBuilder();
                sb1.Append("insert into ");
                sb1.Append(type.Name + "s(");

                for (int i = 0;i<ps.Count;i++) {
                    if (!ps[i].PropertyType.IsValueType && ps[i].PropertyType != typeof(string))
                        continue;
                    if (i > 0) {
                        sb1.Append(",");
                        sb2.Append(",");
                    }
                       
                    sb1.Append(ps[i].Name);
                    sb2.Append("@"+ ps[i].Name);
                }
                sb1.Append(")values(");
                sb1.Append(sb2);
                sb1.Append(")");
                sql = sb1.ToString();
                if (!insertSqlDic.ContainsKey(type))
                    insertSqlDic[type] = sql;
            }
            return DbQuery.New().ExecuteNoQuery(sql, entity);
        }

        public static int Update<T>(T entity) where T : class, new()
        {
            var type = typeof(T);
            var ps = type.GetProperties().Where(x => x.CanRead && x.CanWrite && !x.Name.IsSame("Id")).ToList();
            string sql = updateSqlDic.GetOrAdd(type, () =>
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("update ");
                sb.Append(type.Name + "s ");
                for (int i = 0; i < ps.Count; i++)
                {
                    if (!ps[i].PropertyType.IsSimpleType())
                        continue;
                    if (i > 0)
                    {
                        sb.Append(",");
                    }
                    sb.Append("set ");
                    sb.Append(ps[i].Name);
                    sb.Append("=");
                    sb.Append("@" + ps[i].Name);
                }
                sb.Append(" where Id=@Id");
                return sb.ToString();  
            });
            return DbQuery.New().ExecuteNoQuery(sql, entity);
        }

        public static int Delete<T, TKey>(TKey key) where T : class, new()
        {
            var sql = string.Format("delete from {0}s where Id =@Id" ,typeof(T).Name);
            return DbQuery.New().ExecuteNoQuery(sql, new { Id = key });
        }

        public static T Get<T, TKey>(TKey key) where T : class, new()
        {
            var sql = string.Format("select * from {0}s where Id=@Id", typeof(T).Name);
            return New().ExecuteSingle<T>(sql, new { Id = key });
        }


        public static List<T> GetList<T>() where T : class, new()
        {
            var sql = string.Format("select * from {0}s ", typeof(T).Name);
            return New().ExecuteList<T>(sql);
        }
    }
}
