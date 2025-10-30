using Microsoft.Extensions.Configuration;
using StoreCoreApi.DAL.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreCoreApi.DAL.Repository
{
    public class DbServices: IDbServices
    {
        IConfiguration _configuration;

        string connection = string.Empty;
        public DbServices(IConfiguration configuration)
        {
            _configuration = configuration;
            connection = _configuration.GetConnectionString("DefaultConnection");
        }

        //string connection = _configuration.GetConnectionString("DefaultConnection");
        public async Task<T> DBase<T>(string SP, Hashtable ht, string ClientName = null)
        {
            T obj = default(T);
            if (typeof(T).Name == "DataTable")
            {
                DataTable dt = await this.GetDataTable(SP, ht, ClientName);
                obj = (T)Convert.ChangeType(dt, typeof(T));
            }
            if (typeof(T).Name == "DataSet")
            {
                DataSet dt = await this.GetDataSet(SP, ht, ClientName);
                obj = (T)Convert.ChangeType(dt, typeof(T));
            }

            return (T)obj;
        }
        public async Task<DataTable> GetDataTable(string sp, Hashtable ht, string ClientName = null)
        {
            DataTable dt = new DataTable();
            SqlConnection conState = new SqlConnection();
            try
            {
                //string connection = _configuration.GetConnectionString("DefaultConnection");

                if (conState.State == ConnectionState.Closed)
                {
                    conState.ConnectionString = connection;
                    await conState.OpenAsync();
                }
                SqlCommand cmd = new SqlCommand(sp, conState);
                cmd.CommandType = CommandType.StoredProcedure;
                if (ht != null)
                {
                    foreach (DictionaryEntry d in ht)
                        cmd.Parameters.AddWithValue((string)d.Key, d.Value);
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                await Task.Run(() => da.Fill(dt));
                conState.Close();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                //dt.Columns.Add("Message", typeof(string));
                //DataRow row = dt.NewRow();
                //row["Message"] = ex.Message;
                //dt.Rows.Add(row);
                //_ilogger.LogWrite("Proc_" + sp, ExceptionToString(ex), ClientName, "GetDataTable", "DataBase", "DB", "12345");
            }
            finally
            {
                conState.Close();
                conState.Dispose();
            }
            return dt;
        }
        public async Task<DataSet> GetDataSet(string sp, Hashtable ht, string ClientName = null)
        {
            DataSet ds = new DataSet();
            SqlConnection conState = new SqlConnection();
            try
            {
                if (conState.State == ConnectionState.Closed)
                {
                    conState.ConnectionString = connection;
                    await conState.OpenAsync();
                }
                SqlCommand cmd = new SqlCommand(sp, conState);
                cmd.CommandType = CommandType.StoredProcedure;
                if (ht != null)
                {
                    foreach (DictionaryEntry d in ht)
                        cmd.Parameters.AddWithValue((string)d.Key, d.Value);
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                await Task.Run(() => da.Fill(ds));
                conState.Close();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                //_ilogger.LogWrite("Proc_" + sp, ExceptionToString(ex), ClientName, "GetDataSet", "DataBase", "DB", "12345");
            }
            finally
            {
                conState.Close();
                conState.Dispose();

            }
            return ds;

        }

        // Execute a stored procedure
        public async Task<DataTable> ExecuteStoredProcedureAsync(string procedureName, params SqlParameter[] parameters)
        {
            SqlConnection conState = new SqlConnection();

            //using (var connection = GetConnection())
            using (var command = new SqlCommand(procedureName, conState))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddRange(parameters);
                var dataTable = new DataTable();
                var adapter = new SqlDataAdapter(command);
                await Task.Run(() => adapter.Fill(dataTable));
                return dataTable;
            }
        }

        // Execute a direct SQL query
        public async Task<DataTable> ExecuteQueryAsync(string query, params SqlParameter[] parameters)
        {
            SqlConnection conState = new SqlConnection();
            if (conState.State == ConnectionState.Closed)
            {
                conState.ConnectionString = connection;
                await conState.OpenAsync();
            }
            using (var command = new SqlCommand(query, conState))
            {
                command.Parameters.AddRange(parameters);
                var dataTable = new DataTable();
                var adapter = new SqlDataAdapter(command);
                await Task.Run(() => adapter.Fill(dataTable));
                return dataTable;
            }
        }

        // Execute a command that does not return a result set (e.g., INSERT, UPDATE, DELETE)
        public async Task<int> ExecuteNonQueryAsync(string query, params SqlParameter[] parameters)
        {
            SqlConnection conState = new SqlConnection();
            //
            //using (var connection = GetConnection())
            using (var command = new SqlCommand(query, conState))
            {
                command.Parameters.AddRange(parameters);
                conState.Open();
                return await command.ExecuteNonQueryAsync();
            }
        }

        public async Task<DataTable> ExecuteQueryAsync2(string query, SqlParameter[] parameters = null)
        {
            DataTable dt = new DataTable(); 
            using (SqlConnection conState = new SqlConnection())
            {
                if (conState.State == ConnectionState.Closed)
                {
                    conState.ConnectionString = connection;
                    await conState.OpenAsync();
                }

                using (SqlCommand command = new SqlCommand(query, conState))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        dt.Load(reader);
                    }
                }
            }

            return dt;
        }


        public async Task<string> Insert<T>(string query, Hashtable ht, string ClientName = null)
        {
            return await this.InsertUpdateDeleteData(query, ht, ClientName);
        }
        private async Task<string> InsertUpdateDeleteData(string sp, Hashtable ht, string ClientName = null)
        {
            string s = "";

            SqlConnection conState = new SqlConnection();
            try
            {
                if (conState.State == ConnectionState.Closed)
                {
                    conState.ConnectionString = connection;
                    await conState.OpenAsync();
                }
                SqlCommand cmd = new SqlCommand(sp, conState);
                cmd.CommandType = CommandType.StoredProcedure;
                if (ht != null)
                {
                    foreach (DictionaryEntry d in ht)
                        cmd.Parameters.AddWithValue((string)d.Key, d.Value);
                }
                if (sp == "SPInsertIntoProposalDetail" || sp == "SPInsertIntoProposalDetail_ForIC" || sp == "SpInsertIntoTTravelProposalDetails" || sp == "SPInsertIntoPrsnlProposalDetail")
                {
                    SqlParameter outPutParameter = new SqlParameter();
                    if (outPutParameter != null)
                    {
                        outPutParameter.ParameterName = "@TransactionId";
                        outPutParameter.SqlDbType = System.Data.SqlDbType.NVarChar;
                        outPutParameter.Size = 50;
                        outPutParameter.Direction = System.Data.ParameterDirection.Output;
                        cmd.Parameters.Add(outPutParameter);
                    }
                    await cmd.ExecuteNonQueryAsync();
                    s = outPutParameter.Value.ToString();
                }
                else if (sp == "SPCarInsertProposalDetailForAllOTP_MAKEPAYMENT_SAVEPERPOSAL" || sp == "SPCommercialInsertProposalDetailForAllOTP_MAKEPAYMENT_SAVEPERPOSAL")
                {
                    SqlParameter outPutParameter = new SqlParameter();
                    if (outPutParameter != null)
                    {
                        outPutParameter.ParameterName = "@TransId";
                        outPutParameter.SqlDbType = System.Data.SqlDbType.Int;
                        outPutParameter.Direction = System.Data.ParameterDirection.Output;
                        cmd.Parameters.Add(outPutParameter);
                    }
                    await cmd.ExecuteNonQueryAsync();
                    s = outPutParameter.Value.ToString();
                }
                else if (sp == "SPInsertUpdateBikeProposalDetails")
                {
                    SqlParameter outPutParameter = new SqlParameter();
                    if (outPutParameter != null)
                    {
                        outPutParameter.ParameterName = "@Id";
                        outPutParameter.SqlDbType = System.Data.SqlDbType.NVarChar;
                        outPutParameter.Size = 100;
                        outPutParameter.Direction = System.Data.ParameterDirection.Output;
                        cmd.Parameters.Add(outPutParameter);
                    }
                    await cmd.ExecuteNonQueryAsync();
                    s = outPutParameter.Value.ToString();
                }
                else if (sp == "LifeCheckNegativePinCodes")
                {
                    SqlParameter outPutParameter = new SqlParameter();
                    if (outPutParameter != null)
                    {
                        outPutParameter.ParameterName = "@Exist";
                        outPutParameter.SqlDbType = System.Data.SqlDbType.BigInt;
                        outPutParameter.Size = 100;
                        outPutParameter.Direction = System.Data.ParameterDirection.Output;
                        cmd.Parameters.Add(outPutParameter);
                    }
                    await cmd.ExecuteNonQueryAsync();
                    s = outPutParameter.Value.ToString();
                }

                else if (sp == "sp_PrivateCarMainVehicleMaster")
                {
                    SqlParameter outPutParameter = new SqlParameter();
                    if (outPutParameter != null)
                    {
                        outPutParameter.ParameterName = "@Exists";
                        outPutParameter.SqlDbType = System.Data.SqlDbType.NVarChar;
                        outPutParameter.Size = 4000;
                        outPutParameter.Direction = System.Data.ParameterDirection.Output;
                        cmd.Parameters.Add(outPutParameter);
                    }
                    await cmd.ExecuteNonQueryAsync();
                    s = outPutParameter.Value.ToString();
                }

                else
                {
                    await cmd.ExecuteNonQueryAsync();
                }


                conState.Close();
                cmd.Dispose();
            }
            catch (Exception ex)
            {

                //await BaseDOAIssueQuery(sp, "DB Error Occured", ex.ToString(), "InsertUpdateDeleteData", "1234", ClientName);
            }
            finally
            {
                conState.Close();

            }
            return s;

        }
    }
}
