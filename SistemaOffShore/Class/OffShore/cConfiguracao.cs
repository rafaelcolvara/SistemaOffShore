using System;
using System.Data;
using System.IO;

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
        public string arquivo { get; set; }
        #endregion

        #region CONEXOES
        public DataTable conexoes()
        {
            try
            {
                #region QUERY
                cDAO dao = new cDAO();
                cGlobal.query = "SELECT * FROM CONEXAO_TB ";
                using (DataTable dt = dao.retorna_datatable(cGlobal.query, "cnn"))
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
                dao.executa_query(cGlobal.query, "cnn");
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
                if (cf.id_setor.Equals(2))
                {
                    cGlobal.query = "SELECT SAC,DI8 FROM DIRETORIO_TB " +
                                    "INNER JOIN SETOR_TB ON SETOR_TB.ID_SETOR = DIRETORIO_TB.ID_SETOR " +
                                    "WHERE SETOR_TB.ID_SETOR = 2 ";
                }

                using (DataTable dt = dao.retorna_datatable(cGlobal.query, "cnn"))
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

                if (cf.id_setor.Equals(2))
                {
                    cGlobal.query = "UPDATE DIRETORIO_TB SET " +
                                    "SAC = '" + cf.sac + "', " +
                                    "DI8 = '" + cf.di8 + "' " +
                                    "WHERE ID_SETOR = (SELECT ID_SETOR FROM SETOR_TB WHERE ID_SETOR = " + cf.id_setor + ")";
                }
                dao.executa_query(cGlobal.query, "cnn");
                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable arquivos(string campos)
        {
            try
            {
                #region QUERY
                cDAO dao = new cDAO();
                cGlobal.query = string.Format("SELECT {0} FROM DIRETORIO_TB WHERE ID_SETOR = {1}", campos, cGlobal.userSetor);
                using (DataTable dt = dao.retorna_datatable(cGlobal.query, "cnn"))
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

        public bool localiza_arquivo_diretorio(string pasta, string arq, string extensao)
        {
            try
            {
                if (File.Exists(string.Concat(pasta, @"\", arq, extensao)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}
