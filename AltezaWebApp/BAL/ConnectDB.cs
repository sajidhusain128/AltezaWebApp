using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace AltezaWebApp.BAL
{
    public class ConnectDB
    {
        SqlConnection scon = null;
        private readonly IConfiguration _configuration;

        public ConnectDB(IConfiguration configuration)
        {
            
            _configuration = configuration;
            string connectionString = _configuration.GetConnectionString("ConnString");
            scon = new SqlConnection(connectionString);
        }

        public SqlConnection OpenConn()
        {
            scon.Open();
            return scon;
        }
        public void CloseConn()
        {
            scon.Close();
        }

        public DataTable GetDataBySP(string spName, SqlParameter[] SQLParam)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand(spName, OpenConn());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(SQLParam);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                return dt;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                CloseConn();
            }
        }

        public DataSet GetMultipleDataBySP(string spName, SqlParameter[] SQLParam)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand(spName, OpenConn());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(SQLParam);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds);
                return ds;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                CloseConn();
            }
        }

        public int ExecuteSP(string spName, SqlParameter[] SQLParam)
        {
            int i = 0;
            try
            {
                SqlCommand cmd = new SqlCommand(spName, OpenConn())
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddRange(SQLParam);
                i = cmd.ExecuteNonQuery();
                return i;
            }
            catch (Exception Ex)
            {
                return i;
            }
            finally
            {
                CloseConn();
            }
        }
        public object ExecuteSP_ReturnVal(string spName, SqlParameter[] SQLParam)
        {
            try
            {
                object i = new object();
                SqlCommand cmd = new SqlCommand(spName, OpenConn())
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddRange(SQLParam);
                cmd.ExecuteNonQuery();
                if (cmd.Parameters["@ReturnVal"].Direction == ParameterDirection.Output)
                {
                    i = (object)cmd.Parameters["@ReturnVal"].Value;
                }
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConn();
            }
        }
        public DataTable GetDataByQuery(string Query)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand(Query, OpenConn());
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                dt = new DataTable();
                dt = ds.Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConn();
            }
        }
        public int ExecuteQuery(string strQuery)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(strQuery, OpenConn())
                {
                    CommandType = CommandType.Text
                };
                int i = cmd.ExecuteNonQuery();
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConn();
            }
        }
    }
}
