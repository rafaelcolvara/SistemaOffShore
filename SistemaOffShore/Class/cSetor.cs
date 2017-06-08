using System;
using System.Data;
using System.Data.OleDb;

namespace SistemaOffShore.Class
{
    public class cSetor
    {
        cDAO dao = new cDAO();

        #region PROPERTIES
        public int id_setor { get; set; }
        public string setor { get; set; }
        #endregion

        public DataTable preenche_lista_setor()
        {
            try
            {
                #region QUERY
                cGlobal.query = "SELECT * FROM SETOR_TB WHERE ID_SETOR <> 1 ORDER BY ID_SETOR DESC";
                using (DataTable dt = dao.retorna_datatable(cGlobal.query))
                {
                    return dt;
                }
                #endregion
            }
            catch (OleDbException ex)
            {
                throw ex;
            }
        }

        public DataSet retorna_setor()
        {
            try
            {
                #region QUERY
                cGlobal.query = "SELECT * FROM SETOR_TB WHERE ID_SETOR <> 1 ORDER BY ID_SETOR DESC";
                using (DataSet ds = dao.retorna_dataset(cGlobal.query))
                {
                    return ds;
                }
                #endregion
            }
            catch (OleDbException ex)
            {
                throw ex;
            }
        }

        public void grava_setor(cSetor cst)
        {
            try
            {
                #region QUERY
                cGlobal.query = "INSERT INTO SETOR_TB (SETOR, USERCAD, DTCAD) " +
                                "VALUES('" + cst.setor + "',  '" + cGlobal.userlogado + "', '" + DateTime.Now + "') ";
                dao.executa_query(cGlobal.query);
                #endregion
            }
            catch (OleDbException ex)
            {
                throw ex;
            }
        }
        public void atualiza_cadastro_setor(cSetor cst)
        {
            try
            {
                #region QUERY
                cGlobal.query = "UPDATE SETOR_TB SET SETOR = '" + cst.setor + "' " +
                                "WHERE ID_SETOR = " + cst.id_setor + " ";
                dao.executa_query(cGlobal.query);
                #endregion
            }
            catch (OleDbException ex)
            {
                throw ex;
            }
        }
        public bool exclui_setor(cSetor cst)
        {
            try
            {
                #region QUERY
                cGlobal.query = "DELETE FROM SETOR_TB WHERE ID_SETOR = " + cst.id_setor + " ";
                dao.executa_query(cGlobal.query);
                return false;
                #endregion
            }
            catch (OleDbException ex)
            {
                throw ex;
            }
        }

    }

}
