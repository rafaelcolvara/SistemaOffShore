using System;
using System.Data;

namespace SistemaOffShore.Class
{
    public class cFundo
    {
        cDAO dao = new cDAO();

        public string codfundo { get; set; }
        public string fundo { get; set; }

        public DataTable preenche_lista_fundo()
        {
            try
            {
                #region QUERY
                cGlobal.query = "SELECT * FROM FUNDO_TB ";
                using (DataTable dt = dao.retorna_datatable(cGlobal.query))
                {
                    return dt;
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool exclui_fundo(cFundo cfun)
        {
            try
            {
                #region QUERY
                cGlobal.query = "DELETE FROM FUNDO_TB WHERE ID_SETOR = " + cfun.codfundo + " ";
                dao.executa_query(cGlobal.query);
                return false;
                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void grava_fundo(cFundo cfun)
        {
            try
            {
                #region QUERY
                cGlobal.query = "INSERT INTO FUNDO_TB (CODFUNDO, NMFUNDO) " +
                                "VALUES('" + cfun.codfundo + "',  '" + cfun.fundo + "') ";
                dao.executa_query(cGlobal.query);
                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void atualiza_fundo(cFundo cfun)
        {
            try
            {
                #region QUERY
                cGlobal.query = "UPDATE FUNDO_TB SET FUNDO = '" + cfun.fundo + "' " +
                                "WHERE CODFUNDO = " + cfun.codfundo + " ";
                dao.executa_query(cGlobal.query);
                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
