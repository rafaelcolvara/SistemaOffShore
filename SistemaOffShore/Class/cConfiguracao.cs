using System;
using System.Data;

namespace SistemaOffShore.Class
{
    public class cConfiguracao : cSetor
    {

        #region PROPERTIES
        public string banco { get; set; }
        public string diretorio { get; set; }
        public string tamanho { get; set; }
        public string sac { get; set; }
        public string di8 { get; set; }
        public string bloomberg { get; set; }
        public string po1 { get; set; }
        public string mta { get; set; }
        public string sst { get; set; }
        #endregion

        #region CONEXOES
        public DataTable conexoes()
        {
            try
            {
                #region QUERY
                cDAO dao = new cDAO();
                cGlobal.query = "SELECT * FROM CONEXAO_TB ";
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

        public void update_conexao(cConfiguracao cf)
        {
            try
            {
                #region QUERY
                cDAO dao = new cDAO();
                cGlobal.query = "UPDATE CONEXAO_TB SET DIRETORIO = '" + cf.diretorio + "', TAMANHO = '" + cf.tamanho + "' WHERE BANCO = '" + cf.banco + "' ";
                dao.executa_query(cGlobal.query);
                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region CONFIGURAÇÃO DE DIRETORIOS

        public DataTable diretorios(cConfiguracao cf)
        {
            try
            {
                #region QUERY
                cDAO dao = new cDAO();
                if (cf.setor.Equals("PROCESSAMENTO"))
                {
                    cGlobal.query = "SELECT SAC,DI8 FROM DIRETORIO_TB " +
                                    "INNER JOIN SETOR_TB ON SETOR_TB.ID_SETOR = DIRETORIO_TB.ID_SETOR " +
                                    "WHERE SETOR_TB.SETOR = '" + cf.setor + "' ";
                }

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

        public void update_diretorios(cConfiguracao cf)
        {
            try
            {
                #region QUERY
                cDAO dao = new cDAO();

                if (cf.setor.Equals("PROCESSAMENTO"))
                {
                    cGlobal.query = "UPDATE DIRETORIO_TB SET " +
                                    "SAC = '" + cf.sac + "', " +
                                    "DI8 = '" + cf.di8 + "' " +
                                    "WHERE ID_SETOR = (SELECT ID_SETOR FROM SETOR_TB WHERE SETOR = '" + cf.setor + "')";
                }
                dao.executa_query(cGlobal.query);
                #endregion

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}
