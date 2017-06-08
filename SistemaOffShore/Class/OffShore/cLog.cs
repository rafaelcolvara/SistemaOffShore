using System;
using System.Data;
using System.Data.OleDb;
namespace SistemaOffShore.Class
{
    public class cLog
    {
        cDAO dao = new cDAO();

        #region PROPERTIES
        public string log { get; set; }
        public string form { get; set; }
        public string metodo { get; set; }
        public DateTime dt { get; set; }
        public string usersistema { get; set; }
        public string userRede { get; set; }
        public string terminal { get; set; }
        public bool tp_flag { get; set; }
        #endregion


        public void grava_log(cLog log)
        {
            try
            {
                #region QUERY
                cGlobal.query = "INSERT INTO LOG_TB(LOG,FORM,METODO,DT,USER_SISTEMA,USER_REDE,TERMINAL,FLAG) VALUES('" + log.log + "','" + log.form + "', '" + log.metodo + "', '" + log.dt + "','" + log.usersistema + "','" + log.userRede + "','" + log.terminal + "', " + log.tp_flag + ")";
                dao.executa_query(cGlobal.query, "cnn");
                #endregion
            }
            catch (OleDbException ex)
            {
                throw ex;
            }
        }

        public DataSet retorna_log(bool flag)
        {
            try
            {
                #region QUERY
                if (flag)
                {
                    cGlobal.query = "SELECT * FROM LOG_TB WHERE FLAG = TRUE ORDER BY ID_LOG ASC";
                }
                else
                {
                    cGlobal.query = "SELECT * FROM LOG_TB WHERE FLAG = FALSE ORDER BY ID_LOG ASC";
                }
                using (DataSet dst = dao.retorna_dataset(cGlobal.query, "cnn"))
                {
                    return dst;
                }
                #endregion
            }
            catch (OleDbException ex)
            {
                throw ex;
            }
        }
    }

}
