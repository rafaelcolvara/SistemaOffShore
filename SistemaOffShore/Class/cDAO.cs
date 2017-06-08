using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;

namespace SistemaOffShore.Class
{
    public class cDAO
    {
        #region CONEXAO
        private static OleDbConnection con = null;

        //Conexão
        public static OleDbConnection abre_conexao()
        {
            //con = new OleDbConnection(string.Concat(ConfigurationManager.ConnectionStrings["cnn"].ConnectionString, ";Jet OLEDB:Database Password=gr@w*16"));
            con = new OleDbConnection(string.Concat(ConfigurationManager.ConnectionStrings["cnn"].ConnectionString));
            try
            {
                con.Open();
            }
            catch (OleDbException ex)
            {
                con = null;
            }
            return con;
        }

        //fecha conexão
        public static void fecha_conexao()
        {
            if (con != null)
            {
                con.Dispose();
                con.Close();
            }
        }
        #endregion

        #region DATASET
        public DataSet retorna_dataset(string query)
        {
            DataSet ds = new DataSet();
            try
            {
                using (OleDbCommand cmd = new OleDbCommand(cmdText: query, connection: abre_conexao()))
                {
                    cmd.CommandTimeout = 120;
                    cmd.CommandType = CommandType.Text;
                    using (OleDbDataAdapter da = new OleDbDataAdapter(cmd))
                    {
                        ds.Clear();
                        da.Fill(ds, "TABLE");
                        ds.Dispose();
                    }
                }
                return ds;
            }
            catch (OleDbException ex)
            {
                throw ex;
            }
            finally
            {
                fecha_conexao();
            }
        }
        #endregion

        #region DATATABLE
        public DataTable retorna_datatable(string query)
        {
            DataTable dt = new DataTable();
            try
            {
                using (OleDbCommand cmd = new OleDbCommand(cmdText: query, connection: abre_conexao()))
                {
                    cmd.CommandTimeout = 120;
                    cmd.CommandType = CommandType.Text;
                    using (OleDbDataAdapter da = new OleDbDataAdapter(cmd))
                    {
                        dt.Clear();
                        da.Fill(dt);
                        dt.Dispose();
                    }
                }
                return dt;
            }
            catch (OleDbException ex)
            {
                throw ex;
            }
            finally
            {
                fecha_conexao();
            }
        }
        #endregion

        #region EXECUTE
        public void executa_query(string query)
        {
            try
            {
                using (OleDbCommand cmd = new OleDbCommand(cmdText: query, connection: abre_conexao()))
                {
                    cmd.CommandTimeout = 120;
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (OleDbException ex)
            {
                throw ex;
            }
            finally
            {
                fecha_conexao();
            }
        }

        public bool executa_query_scalar(string query)
        {
            try
            {
                using (OleDbCommand cmd = new OleDbCommand(cmdText: query, connection: abre_conexao()))
                {
                    cmd.CommandTimeout = 120;
                    cmd.CommandType = CommandType.Text;
                    cGlobal.existline = (Int32)cmd.ExecuteScalar();
                }

                if (cGlobal.existline == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (OleDbException ex)
            {
                throw ex;
            }
            finally
            {
                fecha_conexao();
            }
        }
        #endregion
    }

}
