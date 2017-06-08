using System;
using System.Data;
using System.Data.OleDb;

namespace SistemaOffShore.Class
{
    public class cUsuario
    {
        cDAO dao = new cDAO();

        #region PROPERTIES
        public int id_usuario { get; set; }
        public int id_setor { get; set; }
        public string usuario { get; set; }
        public string email { get; set; }
        public string login { get; set; }
        public string senha { get; set; }
        public bool reset_pwd { get; set; }
        public DateTime dtcad { get; set; }
        public bool adm { get; set; }
        public bool exec { get; set; }
        public bool rpt { get; set; }
        public bool ativo { get; set; }
        #endregion

        public bool valida_login(cUsuario user)
        {
            try
            {
                #region QUERY
                cGlobal.query = "SELECT TB1.LOGIN, TB2.ID_SETOR FROM USUARIO_TB TB1, SETOR_TB TB2 " +
                                "WHERE TB2.ID_SETOR = TB1.ID_SETOR " +
                                "AND TB1.LOGIN = '" + user.usuario + "' " +
                                "AND TB1.PWD = '" + user.senha + "' ";
                using (DataTable dt = dao.retorna_datatable(cGlobal.query, "cnn"))
                {
                    if (dt.Rows.Count == 0)
                    {
                        return false;
                    }
                    else
                    {
                        cGlobal.userlogado = (dt.Rows[0]).ItemArray[0].ToString();
                        cGlobal.userSetor = int.Parse((dt.Rows[0]).ItemArray[1].ToString());
                        return true;
                    }
                }
                #endregion
            }
            catch (OleDbException ex)
            {
                throw ex;
            }
        }
        public bool verifica_reset_senha(cUsuario user)
        {
            #region RETORNA LINHA AFETADA
            cGlobal.query = "SELECT * FROM USUARIO_TB WHERE LOGIN = '" + user.usuario + "' AND PWD = '" + user.senha + "' AND RESET_PWD = -1 ";
            using (DataTable dt = dao.retorna_datatable(cGlobal.query, "cnn"))
            {
                if (dt.Rows.Count == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            #endregion
        }
        public void grava_acesso(cUsuario user)
        {
            try
            {
                #region QUERY
                cGlobal.query = "INSERT INTO USUARIO_TB (USUARIO," +
                                                        "EMAIL," +
                                                        "LOGIN," +
                                                        "PWD," +
                                                        "DTCAD," +
                                                        "RESET_PWD," +
                                                        "ADM," +
                                                        "ATIVO," +
                                                        "ID_SETOR)" +
                                "VALUES('" + user.usuario + "', " +
                                       "'" + user.email + "', " +
                                       "'" + user.login + "', " +
                                       "'" + user.senha + "', " +
                                       "'" + user.dtcad + "'," +
                                       "0," +
                                       "0," +
                                       "0," +
                                       "1)";
                dao.executa_query(cGlobal.query, "cnn");
                #endregion
            }
            catch (OleDbException ex)
            {
                throw ex;
            }
        }
        public int retorna_id_usuario(cUsuario user)
        {
            try
            {
                #region RETORNO
                int id = 0;

                cGlobal.query = "SELECT ID_USUARIO FROM USUARIO_TB WHERE LOGIN = '" + user.usuario + "' AND PWD = '" + user.senha + "' ";
                using (DataTable dt = dao.retorna_datatable(cGlobal.query, "cnn"))
                {
                    return id = int.Parse((dt.Rows[0]).ItemArray[0].ToString());
                }
                #endregion
            }
            catch (OleDbException ex)
            {
                throw ex;
            }
        }
        public void grava_alteracao_senha(cUsuario user)
        {
            try
            {
                #region QUERY
                cGlobal.query = "UPDATE USUARIO_TB SET PWD = '" + user.senha + "'," +
                                                      "RESET_PWD = 0 " +
                                "WHERE ID_USUARIO = " + user.id_usuario + " ";
                dao.executa_query(cGlobal.query, "cnn");
                #endregion
            }
            catch (OleDbException ex)
            {
                throw ex;
            }
        }
        public bool verifica_usuario_existe(cUsuario user)
        {
            try
            {
                #region QUERY
                cGlobal.query = "SELECT LOGIN FROM USUARIO_TB WHERE LOGIN = '" + user.login + "' ";
                using (DataTable dt = dao.retorna_datatable(cGlobal.query, "cnn"))
                {
                    if (dt.Rows.Count == 0)
                    {
                        return false;
                    }
                    else
                    {
                        cGlobal.userlogado = (dt.Rows[0]).ItemArray[0].ToString();
                        return true;
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable preenche_lista_usuario()
        {
            try
            {
                #region QUERY
                cGlobal.query = "SELECT TB1.* FROM USUARIO_TB TB1, SETOR_TB TB2 " +
                                "WHERE TB2.ID_SETOR = TB1.ID_SETOR " +
                                "AND   TB1.USUARIO <> 'ADMINISTRADOR' " +
                                "ORDER BY TB1.ID_USUARIO DESC";
                using (DataTable dt = dao.retorna_datatable(cGlobal.query, "cnn"))
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
        public void atualiza_cadastro_usuario(cUsuario user, bool altsenha)
        {
            try
            {
                #region QUERY
                if (!altsenha)
                {
                    cGlobal.query = "UPDATE USUARIO_TB SET USUARIO = '" + user.usuario + "'," +
                                                     "EMAIL = '" + user.email + "'," +
                                                     "LOGIN = '" + user.login + "'," +
                                                     "RESET_PWD = " + user.reset_pwd + "," +
                                                     "ADM = " + user.adm + "," +
                                                     "ATIVO = " + user.ativo + "," +
                                                     "ID_SETOR = " + user.id_setor + "," +
                                                     "COLAGEM = " + user.exec + "," +
                                                     "RPT = " + user.rpt + " " +
                               "WHERE ID_USUARIO = " + user.id_usuario + " ";
                }
                else
                {
                    cGlobal.query = "UPDATE USUARIO_TB SET USUARIO = '" + user.usuario + "'," +
                                                     "EMAIL = '" + user.email + "'," +
                                                     "LOGIN = '" + user.login + "'," +
                                                     "PWD = '" + user.senha + "'," +
                                                     "RESET_PWD = " + user.reset_pwd + "," +
                                                     "RPT = " + user.rpt + "," +
                                                     "ATIVO = " + user.ativo + "," +
                                                     "ID_SETOR = " + user.id_setor + "," +
                                                     "ADM = " + user.adm + "," +
                                                     "COLAGEM = " + user.exec + " " +
                               "WHERE ID_USUARIO = " + user.id_usuario + " ";
                }
                dao.executa_query(cGlobal.query, "cnn");
                #endregion
            }
            catch (OleDbException ex)
            {
                throw ex;
            }
        }
        public void exclui_usuario(cUsuario user)
        {
            try
            {
                #region QUERY
                cGlobal.query = "DELETE FROM USUARIO_TB WHERE ID_USUARIO = " + user.id_usuario + " ";
                dao.executa_query(cGlobal.query, "cnn");
                #endregion
            }
            catch (OleDbException ex)
            {
                throw ex;
            }
        }
        //public List<cPermissao> retorna_permissoes(cUsuario user)
        //{
        //    List<cPermissao> per = new List<cPermissao>();
        //    per.Clear();
        //    try
        //    {
        //        cGlobal.query = "SELECT * FROM USUARIO_TB WHERE LOGIN = '" + user.login + "' ";
        //        using (OleDbCommand cmd = new OleDbCommand(cmdText: cGlobal.query, connection: cDAO.abre_conexao()))
        //        {
        //            cmd.CommandTimeout = 120;
        //            cmd.CommandType = CommandType.Text;
        //            using (OleDbDataReader dr = cmd.ExecuteReader())
        //            {
        //                while (dr.Read())
        //                {
        //                    per.Add(new cPermissao(Convert.ToBoolean(dr["CAD_EVENTO"].ToString()),
        //                                           Convert.ToBoolean(dr["APR_EVENTO"].ToString()),
        //                                           Convert.ToBoolean(dr["CAD_CLIENTE"].ToString()),
        //                                           Convert.ToBoolean(dr["CRONOGRAMA"].ToString()),
        //                                           Convert.ToBoolean(dr["PRODUTO"].ToString()),
        //                                           Convert.ToBoolean(dr["SUPORTE"].ToString())
        //                        ));
        //                }
        //            }
        //            cDAO.fecha_conexao();
        //        }
        //        return per.ToList();
        //    }
        //    catch (OleDbException ex)
        //    {
        //        throw ex;
        //    }
        //}
        public bool verifica_login_ativo(cUsuario user)
        {
            #region QUERY
            cGlobal.query = "SELECT * FROM USUARIO_TB WHERE LOGIN = '" + user.usuario + "' AND PWD = '" + user.senha + "' AND ATIVO = 0 ";
            using (DataTable dt = dao.retorna_datatable(cGlobal.query, "cnn"))
            {
                if (dt.Rows.Count == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            #endregion
        }
        public DataTable retorna_area_usuario(int id_user, int area)
        {
            try
            {
                #region QUERY
                cGlobal.query = "SELECT TB1.ID_USUARIO, TB1.ID_SETOR, TB2.SETOR FROM USUARIO_TB TB1, SETOR_TB TB2 WHERE TB2.ID_SETOR = TB1.ID_SETOR AND  TB1.ID_USUARIO = " + id_user + " AND TB1.ID_SETOR = " + area + " ";
                using (DataTable dt = dao.retorna_datatable(cGlobal.query, "cnn"))
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
    }

    public class cPermissao
    {
        public bool per_cad_evento { get; set; }
        public bool per_aprova_evento { get; set; }
        public bool per_cad_cliente { get; set; }
        public bool per_cronograma { get; set; }
        public bool per_produto { get; set; }
        public bool per_suporte { get; set; }

        public cPermissao(bool _per_cad_evento,
                          bool _per_aprova_evento,
                          bool _per_cad_cliente,
                          bool _per_cronograma,
                          bool _per_produto,
                          bool _per_suporte
                         )
        {
            per_cad_evento = _per_cad_evento;
            per_aprova_evento = _per_aprova_evento;
            per_cad_cliente = _per_cad_cliente;
            per_cronograma = _per_cronograma;
            per_produto = _per_produto;
            per_suporte = _per_suporte;
        }

        public cPermissao() { }
    }
}
