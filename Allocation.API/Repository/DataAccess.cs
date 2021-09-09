using System;
using System.Data;
using System.Threading.Tasks;
using System.Xml;
using Allocation.API.Repository.Interfaces;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace Allocation_API.Repository
{
    public class DataAccess : IDisposable, IDataAccess
    {
        private readonly IConfiguration _config;

        public DataAccess(IConfiguration config)
        {
            _config = config;

            if (_config.GetValue<bool>("USE_LOCAL_DB"))
            {
                m_conn = new SqlConnection(string.Format(
                @"Server=DESKTOP-2OT7B3T;
                  Database=HumanResources;
                  Trusted_Connection=True;"));
            }
            else
            {
                m_conn = new SqlConnection(string.Format(
                @"Server=" + _config.GetValue<string>("SERVER") + 
                ";Database=" + _config.GetValue<string>("DB") + 
                ";User Id=" + _config.GetValue<string>("USERNAME") + 
                ";Password=" + _config.GetValue<string>("PASSWORD") + ";"));
            }
        }

        SqlConnection m_conn;
        SqlCommand m_cmd;
        SqlDataReader m_reader;
        XmlReader xml_reader;
        int? m_timeout;

        public void ResetParams()
        {
            if (m_cmd != null && m_cmd.Parameters.Count > 0)
            {
                m_cmd.Parameters.Clear();
            }
        }


        public void Execute(string command)
        {
            Execute(command, null);
        }
        public async Task ExecuteAsync(string command)
        {
            await ExecuteAsync(command, null);
        }


        public void Execute(string command, params SqlParameter[] param)
        {
            executeCommand(command, true, CommandType.Text, null, param);
        }
        public async Task ExecuteAsync(string command, params SqlParameter[] param)
        {
            await executeCommandAsync(command, true, CommandType.Text, null, param);
        }


        public void ExecuteNonQuery(string command)
        {
            ExecuteNonQuery(command, null);
        }
        public async Task ExecuteNonQueryAsync(string command)
        {
            await ExecuteNonQueryAsync(command, null);
        }


        public void ExecuteNonQuery(string command, params SqlParameter[] param)
        {
            executeCommand(command, false, CommandType.Text, null, param);
        }
        public async Task ExecuteNonQueryAsync(string command, params SqlParameter[] param)
        {
            await executeCommandAsync(command, false, CommandType.Text, null, param);
        }


        public void ExecuteStoredProcedure(string command)
        {
            ExecuteStoredProcedure(command, null);
        }
        public async Task ExecuteStoredProcedureAsync(string command)
        {
            await ExecuteStoredProcedureAsync(command, null);
        }


        public void ExecuteStoredProcedure(string command, params SqlParameter[] param)
        {
            executeCommand(command, true, CommandType.StoredProcedure, null, param);
        }
        public async Task ExecuteStoredProcedureAsync(string command, params SqlParameter[] param)
        {
            await executeCommandAsync(command, true, CommandType.StoredProcedure, null, param);
        }

        public void Load(string command, CommandType commandType, DataTable table, params SqlParameter[] param)
        {
            executeCommand(command, true, commandType, table, param);
        }
        public async Task LoadAsync(string command, CommandType commandType, DataTable table, params SqlParameter[] param)
        {
            await executeCommandAsync(command, true, commandType, table, param);
        }


        void executeCommand(string command, bool query, CommandType type, DataTable table, params SqlParameter[] param)
        {
            //Create new command if one doesn't already exist
            if (m_cmd == null)
            {
                m_cmd = new SqlCommand(command, m_conn);
            }

            //Reset command text
            else m_cmd.CommandText = command;

            //Set command type
            m_cmd.CommandType = type;

            //If timeout limit has been set, set
            if (m_timeout.HasValue)
            {
                m_cmd.CommandTimeout = m_timeout.Value;
            }

            //Add any parameters
            if (param != null && param.Length > 0)
            {
                foreach (SqlParameter p in param)
                {
                    //If parameter with the same name has already been added just set the value
                    if (m_cmd.Parameters.Contains(p.ParameterName))
                    {
                        m_cmd.Parameters[p.ParameterName].Value = p.Value;
                    }

                    //Add the parameter
                    else m_cmd.Parameters.Add(p);
                }
            }

            //Open connection if not already open
            if (m_conn.State == ConnectionState.Closed) m_conn.Open();

            //If reader already exists and is open dispose of it
            if (m_reader != null)
            {
                m_reader.Close();
                m_reader.Dispose();
            }

            //Create reader if a query
            if (query)
            {
                m_reader = m_cmd.ExecuteReader();

                //Load datatable if present
                if (table != null)
                {
                    table.Load(m_reader);
                }
            }

            else
            {
                m_cmd.ExecuteNonQuery();
            }
        }
        async Task executeCommandAsync(string command, bool query, CommandType type, DataTable table, params SqlParameter[] param)
        {
            //Create new command if one doesn't already exist
            if (m_cmd == null)
            {
                m_cmd = new SqlCommand(command, m_conn);
            }

            //Reset command text
            else m_cmd.CommandText = command;

            //Set command type
            m_cmd.CommandType = type;

            //If timeout limit has been set, set
            if (m_timeout.HasValue)
            {
                m_cmd.CommandTimeout = m_timeout.Value;
            }

            //Add any parameters
            if (param != null && param.Length > 0)
            {
                foreach (SqlParameter p in param)
                {
                    //If parameter with the same name has already been added just set the value
                    if (m_cmd.Parameters.Contains(p.ParameterName))
                    {
                        m_cmd.Parameters[p.ParameterName].Value = p.Value;
                    }

                    //Add the parameter
                    else m_cmd.Parameters.Add(p);
                }
            }

            //Open connection if not already open
            if (m_conn.State == ConnectionState.Closed) await m_conn.OpenAsync();

            //If reader already exists and is open dispose of it
            if (m_reader != null)
            {
                await m_reader.CloseAsync();
                await m_reader.DisposeAsync();
            }

            //Create reader if a query
            if (query)
            {
                m_reader = (SqlDataReader)await m_cmd.ExecuteReaderAsync();

                //Load datatable if present
                if (table != null)
                {
                    table.Load(m_reader);
                }
            }

            else
            {
                await m_cmd.ExecuteNonQueryAsync();
            }
        }

        
        //private XmlReader executeXmlReader(string command, bool query, CommandType type, DataTable table, params SqlParameter[] param)
        //{
        //    //Create new command if one doesn't already exist
        //    if (m_cmd == null)
        //    {
        //        m_cmd = new SqlCommand(command, m_conn);
        //    }

        //    //Reset command text
        //    else m_cmd.CommandText = command;

        //    //Set command type
        //    m_cmd.CommandType = type;

        //    //If timeout limit has been set, set
        //    if (m_timeout.HasValue)
        //    {
        //        m_cmd.CommandTimeout = m_timeout.Value;
        //    }

        //    //Add any parameters
        //    if (param != null && param.Length > 0)
        //    {
        //        foreach (SqlParameter p in param)
        //        {
        //            //If parameter with the same name has already been added just set the value
        //            if (m_cmd.Parameters.Contains(p.ParameterName))
        //            {
        //                m_cmd.Parameters[p.ParameterName].Value = p.Value;
        //            }

        //            //Add the parameter
        //            else m_cmd.Parameters.Add(p);
        //        }
        //    }

        //    //Open connection if not already open
        //    if (m_conn.State == ConnectionState.Closed) m_conn.Open();

        //    //If reader already exists and is open dispose of it
        //    if (xml_reader != null)
        //    {
        //        xml_reader.Close();
        //        xml_reader.Dispose();
        //    }

        //    //Create reader if a query
        //    if (query)
        //    {
        //        xml_reader =  m_cmd.ExecuteXmlReader();
        //    }

        //    return xml_reader;
        //}


        public void SetTimeout(int timeout)
        {
            m_timeout = timeout;
        }


        public bool Read()
        {
            if (m_reader == null)
            {
                throw new ApplicationException("Can't read data as no command has been executed");
            }

            return m_reader.Read();
        }
        public async Task<bool> ReadAsync()
        {
            if (m_reader == null)
            {
                throw new ApplicationException("Can't read data as no command has been executed");
            }

            return await m_reader.ReadAsync();
        }


        public bool IsNull(int index)
        {
            return m_reader.IsDBNull(index);
        }
        public async Task<bool> IsNullAsync(int index)
        {
            return await m_reader.IsDBNullAsync(index);
        }


        public bool IsNull(string name)
        {
            if (m_reader[name] == DBNull.Value)
            {
                return true;
            }

            return false;
        }

        public T GetValue<T>(int index)
        {
            return (T)m_reader.GetValue(index);
        }

        public T GetValue<T>(string name)
        {
            return (T)m_reader[name];
        }

        public void Dispose()
        {
            //Dispose of reader
            if (m_reader != null)
            {
                //Close reader if not already
                if (!m_reader.IsClosed) m_reader.Close();

                //Dispose of reader
                m_reader.Dispose();
                m_reader = null;
            }

            //Dispose of command
            if (m_cmd != null)
            {
                m_cmd.Dispose();
                m_cmd = null;
            }

            if (m_conn != null && m_conn.State != ConnectionState.Closed)
            {
                //Close connection if not already done so
                if (m_conn.State != ConnectionState.Closed) m_conn.Close();

                m_conn.Dispose();
                m_conn = null;
            }
        }
        public async Task DisposeAsync()
        {
            //Dispose of reader
            if (m_reader != null)
            {
                //Close reader if not already
                if (!m_reader.IsClosed) m_reader.Close();

                //Dispose of reader
                await m_reader.DisposeAsync();
                m_reader = null;
            }

            //Dispose of command
            if (m_cmd != null)
            {
                await m_cmd.DisposeAsync();
                m_cmd = null;
            }

            if (m_conn != null && m_conn.State != ConnectionState.Closed)
            {
                //Close connection if not already done so
                if (m_conn.State != ConnectionState.Closed) m_conn.Close();

                await m_conn.DisposeAsync();
                m_conn = null;
            }
        }
    }
}
