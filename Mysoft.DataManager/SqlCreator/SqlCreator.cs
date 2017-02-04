using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mysoft.DataManager
{
    public class SqlCreator
    {
        public SqlCreator(string tableName, IEnumerable<string> cols)
        {
            TableName = tableName;
            Cols = cols;
        }
        public string TableName { get; set; }

        public IEnumerable<string> Cols { get; set; }

        public string CreateSql_Insert()
        {
            const string sqlformat = "insert into {0}({1})values(@{2})";
            return string.Format(sqlformat, TableName, string.Join(",", Cols), string.Join(",@", Cols));
        }

        public string CreateSql_SelectAll()
        {
            const string sqlformat = "select {0} from {1}";
            return string.Format(sqlformat, string.Join(",", Cols), TableName);
        }

        public string CreateSql_SelectById()
        {
            return CreateSql_SelectAll() + "where Id=@Id";
        }

        public string CreateSql_SelectBySelf(string selfFilter)
        {
            return CreateSql_SelectAll() + "where " + selfFilter;
        }
        public string CreateSql_Update()
        {
            const string sqlformat = "update {0} set {1} where Id=@Id";
            return string.Format(sqlformat, TableName, string.Join(",", Cols.Select(c=>c+"=@"+c)));
        }

    }
}
