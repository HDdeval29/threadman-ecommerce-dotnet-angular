using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreCoreApi.DAL.Interface
{
    public interface IDbServices
    {
        Task<T> DBase<T>(string SP, Hashtable ht, string ClientName = null);
        Task<DataTable> ExecuteQueryAsync(string query, params SqlParameter[] parameters);
        Task<DataTable> ExecuteQueryAsync2(string query, SqlParameter[] parameters = null);
        Task<string> Insert<T>(string query, Hashtable ht, string ClientName = null);
    }
}
