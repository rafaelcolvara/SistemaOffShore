using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;



namespace SistemaOffShore.Class

{
    public class cProcessamento
    {
        #region PROPERTIES
        public string arq_sac { get; set; }
        public string arq_di8 { get; set; }
        public string Text { get; private set; }
        public object sender { get; private set; }

        #endregion

        cConfiguracao conf = new cConfiguracao();
        cSwap swap = new cSwap();
        cDAO dao = new cDAO();



        public void preenche_listview(ref ListView lv)
        {
            try
            {
                cGlobal.query = "SELECT * FROM ARQUIVO_FISICO";
                using (DataTable dt = dao.retorna_datatable(cGlobal.query, "cnn_proc"))
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        ListViewItem lvi = new ListViewItem(item["ID"].ToString());
                        lvi.SubItems.Add(item["SAC"].ToString());
                        lvi.SubItems.Add(item["DI8"].ToString());
                        lvi.SubItems.Add(item["DESCRICAO"].ToString());
                        lvi.SubItems.Add("");
                        lvi.SubItems.Add("");
                        lv.Items.Add(lvi);
                    }
                }
            }
            catch (Exception ex)
            {
                #region LOG ERRO
                cLog lg = new cLog();
                lg.log = ex.Message.Replace("'", "");
                lg.form = this.Text;
                lg.metodo = sender.ToString();
                lg.dt = DateTime.Now;
                lg.usersistema = cGlobal.userlogado;
                lg.userRede = Environment.UserName;
                lg.terminal = Environment.MachineName;
                lg.tp_flag = false;
                lg.grava_log(lg);
                #endregion
            }
        }

        public bool importacao(int id, DateTime dta)
        {
            bool retorno = false;
            string tp_arq;

            try
            {
                cGlobal.query = "SELECT * FROM ARQUIVO_FISICO WHERE ID = " + id + " ";
                using (DataTable dt = dao.retorna_datatable(cGlobal.query, "cnn_proc"))
                {
                    arq_sac = dt.Rows[0].ItemArray[1].ToString();
                    arq_di8 = dt.Rows[0].ItemArray[2].ToString();
                }

                //localiza arquivos no diretorio
                DataTable ret = conf.arquivos("SAC, DI8");

                for (int i = 0; i < ret.Columns.Count; i++)
                {
                    if (i == 0)
                    { tp_arq = arq_sac; }
                    else
                    { tp_arq = string.Concat(arq_di8, "_DI8_", dta.Year, dta.Month, dta.Day); }


                    if (!conf.localiza_arquivo_diretorio(ret.Rows[0].ItemArray[i].ToString(), tp_arq, ".txt"))
                    {
                        retorno = false;
                    }
                    else
                    {
                        retorno = true;
                        cGlobal.caminho_arq.Add(string.Concat(ret.Rows[0].ItemArray[i].ToString(), @"\", tp_arq, ".txt"));
                    }
                }
                return retorno;
            }
            catch (Exception ex)
            {
                #region LOG ERRO
                cLog lg = new cLog();
                lg.log = ex.Message.Replace("'", "");
                lg.form = this.Text;
                lg.metodo = sender.ToString();
                lg.dt = DateTime.Now;
                lg.usersistema = cGlobal.userlogado;
                lg.userRede = Environment.UserName;
                lg.terminal = Environment.MachineName;
                lg.tp_flag = false;
                lg.grava_log(lg);
                #endregion
                throw ex;
            }
        }

        public List<string> identifica_arquivo(string arquivo, string caminho_completo, string DML)
        {
            int[] ids;
            string[] types;
            string[] cols;
            string[] keys;
            string[] divs;
            int idarq;
            string region = "";
            string tipo = "";

            try
            {
                switch (arquivo)
                {
                    //----------------- TRATAMENTO DE SWAP ---------------------------
                    #region POSICAO SWAP

                    #region SAC
                    case "VCRAPSWP":
                            region = "POS_SWAP";
                            tipo = "SAC";
                            idarq = arquivo_insert(arquivo, tipo);
                            ids = new int[7] { 2, 3, 6, 7, 10, 11, 14 };
                            types = new string[7] { "texto", "texto", "texto", "numero", "numero", "numero", "numero" };
                            cols = new string[7] { "CLCLI_CD", "DT", "SWCAD_CD", "QT", "VL_PASSIVO", "VL_ATIVO", "VL_APROP_LIQ" };
                            keys = new string[3] { "CLCLI_CD", "DT", "SWCAD_CD" };
                            divs = new string[4] { "QT", "VL_PASSIVO", "VL_ATIVO", "VL_APROP_LIQ" };
                            if (DML == "insert")
                            {
                                cGlobal.ar_query = query_insert(caminho_completo,
                                ids,
                                types,
                                cols,
                                ";",
                                region,
                                idarq,
                                true,
                                tipo);
                            }
                            else
                            {
                               cGlobal.ar_query = query_select(cols,
                               keys,
                               divs,
                               region);
                            }
                            break;
                        #endregion

                        #region DI8
                        case "pos_SW":
                            region = "POS_SWAP";
                            tipo = "DI8";
                            idarq = arquivo_insert(arquivo, tipo);
                            ids = new int[7] { 4, 3, 2, 6, 8, 11, 21 };
                            types = new string[7] { "texto", "texto", "texto", "numero", "numero", "numero", "numero" };
                            cols = new string[7] { "CLCLI_CD", "DT", "SWCAD_CD", "QT", "VL_PASSIVO", "VL_ATIVO", "VL_APROP_LIQ" };
                            keys = new string[3] { "CLCLI_CD", "DT", "SWCAD_CD" };
                            divs = new string[4] { "QT", "VL_PASSIVO", "VL_ATIVO", "VL_APROP_LIQ" };
                            if (DML == "insert")
                            {
                                cGlobal.ar_query = query_insert(caminho_completo,
                                ids,
                                types,
                                cols,
                                "	",
                                region,
                                idarq,
                                false,
                                tipo);
                            }
                            else
                            {
                               cGlobal.ar_query = query_select(cols,
                               keys,
                               divs,
                               region);
                            }
                            break;
                    #endregion
                    #endregion
                    
                    #region MOVIMENTO SWAP
                        #region SAC
                        case "VCRAOSWP":
                            region = "MOV_SWAP";
                            tipo = "SAC";
                            idarq = arquivo_insert(arquivo, tipo);
                            ids = new int[4] { 2, 3, 5, 7 };
                            types = new string[4] { "texto", "texto", "texto", "numero" };
                            cols = new string[4] { "CLCLI_CD", "DT", "SWCAD_CD", "QT" };
                            keys = new string[3] { "CLCLI_CD", "DT", "SWCAD_CD" };
                            divs = new string[1] { "QT" };
                            if (DML == "insert")
                            {
                                cGlobal.ar_query = query_insert(caminho_completo,
                                ids,
                                types,
                                cols,
                                ";",
                                region,
                                idarq,
                                true,
                                tipo);
                            }
                            else
                            {
                               cGlobal.ar_query = query_select(cols,
                               keys,
                               divs,
                               region);
                            }
                            break;
                        #endregion

                        #region DI8
                        case "mov_SW":
                            region = "MOV_SWAP";
                            tipo = "DI8";
                            idarq = arquivo_insert(arquivo, tipo);
                            ids = new int[4] { 4, 3, 2, 13 };
                            types = new string[4] { "texto", "texto", "texto", "numero" };
                            cols = new string[4] { "CLCLI_CD", "DT", "SWCAD_CD", "QT" };
                            keys = new string[3] { "CLCLI_CD", "DT", "SWCAD_CD" };
                            divs = new string[1] { "QT" };
                            if (DML == "insert")
                            {
                                cGlobal.ar_query = query_insert(caminho_completo,
                                ids,
                                types,
                                cols,
                                "	",
                                region,
                                idarq,
                                false,
                                tipo);
                            }
                            else
                            {
                                cGlobal.ar_query = query_select(cols,
                                keys,
                                divs,
                                region);
                            }
                            break;
                    #endregion
                    #endregion

                    //----------------- TRATAMENTO DE RENDA VARIÁVEL -----------------
                    #region MOVIMENTO RENDA VARIÁVEL
                    #region SAC
                    case "VCRAOPRV":
                        region = "MOV_RENDAVARIAVEL";
                        tipo = "SAC";
                        idarq = arquivo_insert(arquivo, tipo);
                        ids = new int[10] { 2, 3, 5, 31, 32, 33, 34, 35, 39, 41 };
                        types = new string[10] { "texto", "texto", "texto", "texto", "numero", "numero", "numero", "texto", "numero", "numero" };
                        cols = new string[10] { "CODCLI", "DATAMOV", "CODPAP", "CODCOR", "QUANTIDADE", "PRECO", "VALOR", "COMPRAVENDA", "VL_EMOLUMENTO", "CORCLI" };
                        keys = new string[4] { "CODCLI", "DATAMOV", "CODPAP", "CODCOR" };
                        divs = new string[6] { "QUANTIDADE", "PRECO", "VALOR", "COMPRAVENDA", "VL_EMOLUMENTO", "CORCLI" };
                        if (DML == "insert")
                        {
                            cGlobal.ar_query = query_insert(caminho_completo,
                            ids,
                            types,
                            cols,
                            ";",
                            region,
                            idarq,
                            true,
                            tipo);
                        }
                        else
                        {
                            cGlobal.ar_query = query_select(cols,
                            keys,
                            divs,
                            region);
                        }
                        break;
                    #endregion

                    #region DI8
                    case "mov_RV":
                        region = "MOV_RENDAVARIAVEL";
                        tipo = "DI8";
                        idarq = arquivo_insert(arquivo, tipo);
                        ids = new int[10] { 3, 2, 6, 4, 24, 23, 26, 08, 27, 09 };
                        types = new string[10] { "texto", "texto", "texto", "texto", "numero", "numero", "numero", "texto", "numero", "numero" };
                        cols = new string[10] { "CODCLI", "DATAMOV", "CODPAP", "CODCOR", "QUANTIDADE", "PRECO", "VALOR", "COMPRAVENDA", "VL_EMOLUMENTO", "CORCLI" };
                        keys = new string[4] { "CODCLI", "DATAMOV", "CODPAP", "CODCOR" };
                        divs = new string[6] { "QUANTIDADE", "PRECO", "VALOR", "COMPRAVENDA", "VL_EMOLUMENTO", "CORCLI" };
                        if (DML == "insert")
                        {
                            cGlobal.ar_query = query_insert(caminho_completo,
                            ids,
                            types,
                            cols,
                            "	",
                            region,
                            idarq,
                            false,
                            tipo);
                        }
                        else
                        {
                            cGlobal.ar_query = query_select(cols,
                            keys,
                            divs,
                            region);
                        }
                        break;
                    #endregion
                    #endregion

                    #region POSICAO RENDA VARIÁVEL
                    #region SAC
                    case "VCRAPRV":
                            region = "POS_RENDAVARIAVEL";
                            tipo = "SAC";
                            idarq = arquivo_insert(arquivo, tipo);
                            ids = new int[7] { 2, 3, 4, 21, 22, 23, 24 };
                            types = new string[7] { "texto", "texto", "texto", "numero", "numero", "numero", "numero" };
                            cols = new string[7] { "CODCLI", "DATAPOS", "CODPAP", "QTDDISP", "QT_TOTAL", "VL_PRECO_HIST", "VL_PRECO_MERCMEDIO" };
                            keys = new string[3] { "CODCLI", "DATAPOS", "CODPAP" };
                            divs = new string[4] { "QTDDISP", "QT_TOTAL", "VL_PRECO_HIST", "VL_PRECO_MERCMEDIO" };
                            if (DML == "insert")
                            {
                                cGlobal.ar_query = query_insert(caminho_completo,
                                ids,
                                types,
                                cols,
                                ";",
                                region,
                                idarq,
                                true,
                                tipo);
                            }
                            else
                            {
                                cGlobal.ar_query = query_select(cols,
                                keys,
                                divs,
                                region);
                            }
                            break;
                        #endregion

                        #region DI8
                        case "pos_RV":
                            region = "POS_RENDAVARIAVEL";
                            tipo = "DI8";
                            idarq = arquivo_insert(arquivo, tipo);
                            ids = new int[7] { 3, 2, 4, 8, 18, 21, 31 };
                            types = new string[7] { "texto", "texto", "texto", "numero", "numero", "numero", "numero" };
                            cols = new string[7] { "CODCLI", "DATAPOS", "CODPAP", "QTDDISP", "QT_TOTAL", "VL_PRECO_HIST", "VL_PRECO_MERCMEDIO" };
                            keys = new string[3] { "CODCLI", "DATAPOS", "CODPAP" };
                            divs = new string[4] { "QTDDISP", "QT_TOTAL", "VL_PRECO_HIST", "VL_PRECO_MERCMEDIO" };
                            if (DML == "insert")
                            {
                                cGlobal.ar_query = query_insert(caminho_completo,
                                ids,
                                types,
                                cols,
                                "	",
                                region,
                                idarq,
                                false,
                                tipo);
                            }
                            else
                            {
                                cGlobal.ar_query = query_select(cols,
                                keys,
                                divs,
                                region);
                            }
                            break;
                    #endregion
                    #endregion
                    
                    //----------------- TRATAMENTO DE RENDA FIXA -----------------
                    #region MOVIMENTO RENDA FIXA
                    #region SAC
                    case "VCRAOPRF":
                        region = "MOV_RENDAFIXA";
                        tipo = "SAC";
                        idarq = arquivo_insert(arquivo, tipo);
                        ids = new int[9] { 2, 3, 9, 5, 32, 33, 34, 36, 39 };
                        types = new string[9] { "texto", "texto", "texto", "texto", "numero", "numero", "texto", "numero", "numero" };
                        cols = new string[9] { "CLCLI_CD", "DT", "CD_LASTRO", "CD", "QT", "VL_PU_OPERACAO", "SG_OPERACAO", "VL_BRUTO", "VL_LIQ" };
                        keys = new string[4] { "CLCLI_CD", "DT", "CD_LASTRO", "CD" };
                        divs = new string[5] { "QT", "VL_PU_OPERACAO", "SG_OPERACAO", "VL_BRUTO", "VL_LIQ" };
                        if (DML == "insert")
                        {
                            cGlobal.ar_query = query_insert(caminho_completo,
                            ids,
                            types,
                            cols,
                            ";",
                            region,
                            idarq,
                            true,
                            tipo);
                        }
                        else
                        {
                            cGlobal.ar_query = query_select(cols,
                            keys,
                            divs,
                            region);
                        }
                        break;
                    #endregion

                    #region DI8
                    case "mov_RF":
                        region = "MOV_RENDAFIXA";
                        tipo = "DI8";
                        idarq = arquivo_insert(arquivo, tipo);
                        ids = new int[9] { 2, 40, 3, 21, 44, 45, 43, 46, 49 };
                        types = new string[9] { "texto", "texto", "texto", "texto", "numero", "numero", "texto", "numero", "numero" };
                        cols = new string[9] { "CLCLI_CD", "DT", "CD_LASTRO", "CD", "QT", "VL_PU_OPERACAO", "SG_OPERACAO", "VL_BRUTO", "VL_LIQ" };
                        keys = new string[4] { "CLCLI_CD", "DT", "CD_LASTRO", "CD" };
                        divs = new string[5] { "QT", "VL_PU_OPERACAO", "SG_OPERACAO", "VL_BRUTO", "VL_LIQ" };
                        if (DML == "insert")
                        {
                            cGlobal.ar_query = query_insert(caminho_completo,
                            ids,
                            types,
                            cols,
                            "	",
                            region,
                            idarq,
                            false,
                            tipo);
                        }
                        else
                        {
                            cGlobal.ar_query = query_select(cols,
                            keys,
                            divs,
                            region);
                        }
                        break;
                    #endregion
                    #endregion
                    
                    #region POSICAO RENDA FIXA
                    #region SAC
                    case "VCRAPRF":
                        region = "POS_RENDAFIXA";
                        tipo = "SAC";
                        idarq = arquivo_insert(arquivo, tipo);
                        ids = new int[7] { 2, 3, 4, 23, 26, 32, 36 };
                        types = new string[7] { "texto", "texto", "texto", "numero", "numero", "numero", "numero" };
                        cols = new string[7] { "CLCLI_CD", "DT", "CD", "VL_PU", "QT_TOTAL", "VL_BRUTOPOS", "VL_LIQ_POS" };
                        keys = new string[3] { "CLCLI_CD", "DT", "CD" };
                        divs = new string[4] { "VL_PU", "QT_TOTAL", "VL_BRUTOPOS", "VL_LIQ_POS" };
                        if (DML == "insert")
                        {
                            cGlobal.ar_query = query_insert(caminho_completo,
                            ids,
                            types,
                            cols,
                            ";",
                            region,
                            idarq,
                            true,
                            tipo);
                        }
                        else
                        {
                            cGlobal.ar_query = query_select(cols,
                            keys,
                            divs,
                            region);
                        }
                        break;
                    #endregion

                    #region DI8
                    case "pos_RF":
                        region = "POS_RENDAFIXA";
                        tipo = "DI8";
                        idarq = arquivo_insert(arquivo, tipo);
                        ids = new int[7] { 4, 2, 3, 6, 7, 15, 18 };
                        types = new string[7] { "texto", "texto", "texto", "numero", "numero", "numero", "numero" };
                        cols = new string[7] { "CLCLI_CD", "DT", "CD", "VL_PU", "QT_TOTAL", "VL_BRUTOPOS", "VL_LIQ_POS" };
                        keys = new string[3] { "CLCLI_CD", "DT", "CD" };
                        divs = new string[4] { "VL_PU", "QT_TOTAL", "VL_BRUTOPOS", "VL_LIQ_POS" };
                        if (DML == "insert")
                        {
                            cGlobal.ar_query = query_insert(caminho_completo,
                            ids,
                            types,
                            cols,
                            "	",
                            region,
                            idarq,
                            false,
                            tipo);
                        }
                        else
                        {
                            cGlobal.ar_query = query_select(cols,
                            keys,
                            divs,
                            region);
                        }
                        break;
                    #endregion
                    #endregion
                    
                    //----------------- TRATAMENTO DE FUTUROS ------------------------
                    #region MOVIMENTO FUTUROS
                    #region SAC
                    case "VCRAOFUT":
                        region = "MOV_FUTUROS";
                        tipo = "SAC";
                        idarq = arquivo_insert(arquivo, tipo);
                        ids = new int[8] { 2, 3, 9, 14, 18, 19, 20, 21 };
                        types = new string[8] { "texto", "texto", "texto", "texto", "texto", "numero", "numero", "texto" };
                        cols = new string[8] { "CLCLI_CD", "DT", "CD_BMF", "FUVCT_CD", "RVCOR_CD", "QT", "VL_PRECO", "IC_CV" };
                        keys = new string[3] { "CLCLI_CD", "DT", "CD_BMF" };
                        divs = new string[5] { "FUVCT_CD", "RVCOR_CD", "QT", "VL_PRECO", "IC_CV" };
                        if (DML == "insert")
                        {
                            cGlobal.ar_query = query_insert(caminho_completo,
                            ids,
                            types,
                            cols,
                            ";",
                            region,
                            idarq,
                            true,
                            tipo);
                        }
                        else
                        {
                            cGlobal.ar_query = query_select(cols,
                            keys,
                            divs,
                            region);
                        }
                        break;
                    #endregion

                    #region DI8
                    case "mov_FU":
                        region = "MOV_FUTUROS";
                        tipo = "DI8";
                        idarq = arquivo_insert(arquivo, tipo);
                        ids = new int[8] { 3, 2, 5, 7, 28, 9, 10, 8 };
                        types = new string[8] { "texto", "texto", "texto", "texto", "texto", "numero", "numero", "texto" };
                        cols = new string[8] { "CLCLI_CD", "DT", "CD_BMF", "FUVCT_CD", "RVCOR_CD", "QT", "VL_PRECO", "IC_CV" };
                        keys = new string[3] { "CLCLI_CD", "DT", "CD_BMF" };
                        divs = new string[5] { "FUVCT_CD", "RVCOR_CD", "QT", "VL_PRECO", "IC_CV" };
                        if (DML == "insert")
                        {
                            cGlobal.ar_query = query_insert(caminho_completo,
                            ids,
                            types,
                            cols,
                            "	",
                            region,
                            idarq,
                            false,
                            tipo);
                        }
                        else
                        {
                            cGlobal.ar_query = query_select(cols,
                            keys,
                            divs,
                            region);
                        }
                        break;
                    #endregion
                    #endregion
                    
                    #region POSICAO FUTUROS
                    #region SAC
                    case "VCRAPFUT":
                        region = "POS_FUTUROS";
                        tipo = "SAC";
                        idarq = arquivo_insert(arquivo, tipo);
                        ids = new int[9] { 2, 3, 8, 9, 13, 14, 15, 16, 19 };
                        types = new string[9] { "texto", "texto", "texto", "texto", "texto", "numero", "numero", "numero", "numero" };
                        cols = new string[9] { "CLCLI_CD", "DT", "CD_BMF", "FUVCT_CD", "RVCOR_CD", "QT", "VL_PRECO", "VL_PRECO_CORTG", "VL_PRECOMERC" };
                        keys = new string[3] { "CLCLI_CD", "DT", "CD_BMF" };
                        divs = new string[6] { "FUVCT_CD", "RVCOR_CD", "QT", "VL_PRECO", "VL_PRECO_CORTG", "VL_PRECOMERC" };
                        if (DML == "insert")
                        {
                            cGlobal.ar_query = query_insert(caminho_completo,
                            ids,
                            types,
                            cols,
                            ";",
                            region,
                            idarq,
                            true,
                            tipo);
                        }
                        else
                        {
                            cGlobal.ar_query = query_select(cols,
                            keys,
                            divs,
                            region);
                        }
                        break;
                    #endregion

                    #region DI8
                    case "pos_FU":
                        region = "POS_FUTUROS";
                        tipo = "DI8";
                        idarq = arquivo_insert(arquivo, tipo);
                        ids = new int[9] { 3, 2, 5, 6, 7, 8, 9, 10, 15 }; //ajustar
                        types = new string[9] { "texto", "texto", "texto", "texto", "texto", "numero", "numero", "numero", "numero" };
                        cols = new string[9] { "CLCLI_CD", "DT", "CD_BMF", "FUVCT_CD", "RVCOR_CD", "QT", "VL_PRECO", "VL_PRECO_CORTG", "VL_PRECOMERC" };
                        keys = new string[3] { "CLCLI_CD", "DT", "CD_BMF" };
                        divs = new string[6] { "FUVCT_CD", "RVCOR_CD", "QT", "VL_PRECO", "VL_PRECO_CORTG", "VL_PRECOMERC" };
                        if (DML == "insert")
                        {
                            cGlobal.ar_query = query_insert(caminho_completo,
                            ids,
                            types,
                            cols,
                            "	",
                            region,
                            idarq,
                            false,
                            tipo);
                        }
                        else
                        {
                            cGlobal.ar_query = query_select(cols,
                            keys,
                            divs,
                            region);
                        }
                        break;
                    #endregion
                    #endregion
                       
                    //----------------- TRATAMENTO DE CAMBIO -------------------------
                    #region MOVIMENTO CAMBIO
                    #region SAC
                    case "MOVIMENTO_CAMBIO":
                        region = "MOV_CAMBIO";
                        tipo = "SAC";
                        idarq = arquivo_insert(arquivo, tipo);
                        ids = new int[16] { 1, 3, 4, 6, 7, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19 };
                        types = new string[16] { "texto", "texto", "texto", "texto", "texto", "texto", "numero", "numero", "texto", "numero", "texto", "texto", "texto", "texto", "texto", "texto" };
                        cols = new string[16] { "CLCLI_CD", "IC_FORWARD", "CD_ATIVO", "DT_MOVIMENTO", "DT_LIQUIDACAO", "CD_MOEDA_COMPRA", "VL_MOEDA_COMPRA", "VL_PARIDADE", "CD_MOEDA_VENDA", "VL_MOEDA_VENDA", "CD_PRACA_OPERACAO", "CD_PRACA_LIQUIDACAO", "CD_CONTRAPARTE", "CD_LOCAL_CUSTODIA", "DT_VENCIMENTO", "DT_LIQUIDACAO_VENDA" };
                        keys = new string[3] { "CLCLI_CD", "CD_ATIVO", "DT_MOVIMENTO" };
                        divs = new string[13] { "IC_FORWARD", "DT_LIQUIDACAO", "CD_MOEDA_COMPRA", "VL_MOEDA_COMPRA", "VL_PARIDADE", "CD_MOEDA_VENDA", "VL_MOEDA_VENDA", "CD_PRACA_OPERACAO", "CD_PRACA_LIQUIDACAO", "CD_CONTRAPARTE", "CD_LOCAL_CUSTODIA", "DT_VENCIMENTO", "DT_LIQUIDACAO_VENDA" };
                        if (DML == "insert")
                        {
                            cGlobal.ar_query = query_insert(caminho_completo,
                            ids,
                            types,
                            cols,
                            "	",
                            region,
                            idarq,
                            true,
                            tipo);
                        }
                        else
                        {
                            cGlobal.ar_query = query_select(cols,
                            keys,
                            divs,
                            region);
                        }
                        break;
                    #endregion

                    #region DI8
                    case "mov_CB":
                        region = "MOV_CAMBIO";
                        tipo = "DI8";
                        idarq = arquivo_insert(arquivo, tipo);
                        ids = new int[16] { 5, 4, 3, 2, 23, 7, 8, 9, 10, 11, 20, 21, 18, 19, 22, 24 };
                        types = new string[16] { "texto", "texto", "texto", "texto", "texto", "texto", "numero", "numero", "texto", "numero", "texto", "texto", "texto", "texto", "texto", "texto" };
                        cols = new string[16] { "CLCLI_CD", "IC_FORWARD", "CD_ATIVO", "DT_MOVIMENTO", "DT_LIQUIDACAO", "CD_MOEDA_COMPRA", "VL_MOEDA_COMPRA", "VL_PARIDADE", "CD_MOEDA_VENDA", "VL_MOEDA_VENDA", "CD_PRACA_OPERACAO", "CD_PRACA_LIQUIDACAO", "CD_CONTRAPARTE", "CD_LOCAL_CUSTODIA", "DT_VENCIMENTO", "DT_LIQUIDACAO_VENDA" };
                        keys = new string[3] { "CLCLI_CD", "CD_ATIVO", "DT_MOVIMENTO" };
                        divs = new string[13] { "IC_FORWARD", "DT_LIQUIDACAO", "CD_MOEDA_COMPRA", "VL_MOEDA_COMPRA", "VL_PARIDADE", "CD_MOEDA_VENDA", "VL_MOEDA_VENDA", "CD_PRACA_OPERACAO", "CD_PRACA_LIQUIDACAO", "CD_CONTRAPARTE", "CD_LOCAL_CUSTODIA", "DT_VENCIMENTO", "DT_LIQUIDACAO_VENDA" };
                        if (DML == "insert")
                        {
                            cGlobal.ar_query = query_insert(caminho_completo,
                            ids,
                            types,
                            cols,
                            "	",
                            region,
                            idarq,
                            false,
                            tipo);
                        }
                        else
                        {
                            cGlobal.ar_query = query_select(cols,
                            keys,
                            divs,
                            region);
                        }
                        break;
                    #endregion
                    #endregion
                    
                    #region POSICAO CAMBIO
                    #region SAC
                    case "POSICAO_CAMBIO":
                        region = "POS_CAMBIO";
                        tipo = "SAC";
                        idarq = arquivo_insert(arquivo, tipo);
                        ids = new int[7] { 2, 1, 4, 11, 12, 13, 14 };
                        types = new string[7] { "texto", "texto", "texto", "numero", "numero", "numero", "numero" };
                        cols = new string[7] { "CLCLI_CD", "DT", "CD", "VL_MOEDA_VENDA", "VL_MOEDA_COMPRA", "VL_PARIDADE", "VL_RENDIMENTO" };
                        keys = new string[3] { "CLCLI_CD", "DT", "CD" };
                        divs = new string[4] { "VL_MOEDA_VENDA", "VL_MOEDA_COMPRA", "VL_PARIDADE", "VL_RENDIMENTO" };
                        if (DML == "insert")
                        {
                            cGlobal.ar_query = query_insert(caminho_completo,
                            ids,
                            types,
                            cols,
                            "	",
                            region,
                            idarq,
                            true,
                            tipo);
                        }
                        else
                        {
                            cGlobal.ar_query = query_select(cols,
                            keys,
                            divs,
                            region);
                        }
                        break;
                    #endregion

                    #region DI8
                    case "pos_CB":
                        region = "POS_CAMBIO";
                        tipo = "DI8";
                        idarq = arquivo_insert(arquivo, tipo);
                        ids = new int[7] { 3, 2, 4, 6, 5, 10, 7 };
                        types = new string[7] { "texto", "texto", "texto", "numero", "numero", "numero", "numero" };
                        cols = new string[7] { "CLCLI_CD", "DT", "CD", "VL_MOEDA_VENDA", "VL_MOEDA_COMPRA", "VL_PARIDADE", "VL_RENDIMENTO" };
                        keys = new string[3] { "CLCLI_CD", "DT", "CD" };
                        divs = new string[4] { "VL_MOEDA_VENDA", "VL_MOEDA_COMPRA", "VL_PARIDADE", "VL_RENDIMENTO" };
                        if (DML == "insert")
                        {
                            cGlobal.ar_query = query_insert(caminho_completo,
                            ids,
                            types,
                            cols,
                            "	",
                            region,
                            idarq,
                            false,
                            tipo);
                        }
                        else
                        {
                            cGlobal.ar_query = query_select(cols,
                            keys,
                            divs,
                            region);
                        }
                        break;
                    #endregion
                    #endregion
                    
                    //----------------- TRATAMENTO DE EMPRESTIMO ---------------------
                    #region MOVIMENTO EMPRESTIMO
                    #region SAC
                    case "MOVIMENTO_BTC":
                        region = "MOV_EMPRESTIMO";
                        tipo = "SAC";
                        idarq = arquivo_insert(arquivo, tipo);
                        ids = new int[13] { 1, 2, 3, 4, 5, 6, 7, 8, 14, 15, 16, 20, 43 };
                        types = new string[] {  }; //ajustar
                        cols = new string[13] { "ID_MOVIMENTO", "DT_MOVIMENTO", "ID_OPERACAO_EMPR_ACOES", "SG_OPERACAO", "IC_TIPO_MOVIMENTO", "CLCLI_CD", "RVPAP_CD", "QT_MOVIMENTADA", "DT_DEVOLUCAO_ANTECIPADA", "RVCOR_CD_AGENTE", "RVCOR_CD", "DT_VENCIMENTO", "RVCOR_CD_CUST_FISICA" };
                        keys = new string[3] { "ID_MOVIMENTO", "DT_MOVIMENTO", "ID_OPERACAO_EMPR_ACOES" };
                        divs = new string[10] { "SG_OPERACAO", "IC_TIPO_MOVIMENTO", "CLCLI_CD", "RVPAP_CD", "QT_MOVIMENTADA", "DT_DEVOLUCAO_ANTECIPADA", "RVCOR_CD_AGENTE", "RVCOR_CD", "DT_VENCIMENTO", "RVCOR_CD_CUST_FISICA" };
                        if (DML == "insert")
                        {
                            cGlobal.ar_query = query_insert(caminho_completo,
                            ids,
                            types,
                            cols,
                            ";",
                            region,
                            idarq,
                            true,
                            tipo);
                        }
                        else
                        {
                            cGlobal.ar_query = query_select(cols,
                            keys,
                            divs,
                            region);
                        }
                        break;
                    #endregion

                    #region DI8
                    case "mov_EA":
                        region = "MOV_EMPRESTIMO";
                        tipo = "DI8";
                        idarq = arquivo_insert(arquivo, tipo);
                        ids = new int[13] { 3, 2, 4, 5, 6, 7, 8, 9, 15, 16, 17, 21, 44 };
                        types = new string[] { }; //ajustar
                        cols = new string[13] { "ID_MOVIMENTO", "DT_MOVIMENTO", "ID_OPERACAO_EMPR_ACOES", "SG_OPERACAO", "IC_TIPO_MOVIMENTO", "CLCLI_CD", "RVPAP_CD", "QT_MOVIMENTADA", "DT_DEVOLUCAO_ANTECIPADA", "RVCOR_CD_AGENTE", "RVCOR_CD", "DT_VENCIMENTO", "RVCOR_CD_CUST_FISICA" };
                        keys = new string[3] { "ID_MOVIMENTO", "DT_MOVIMENTO", "ID_OPERACAO_EMPR_ACOES" };
                        divs = new string[10] { "SG_OPERACAO", "IC_TIPO_MOVIMENTO", "CLCLI_CD", "RVPAP_CD", "QT_MOVIMENTADA", "DT_DEVOLUCAO_ANTECIPADA", "RVCOR_CD_AGENTE", "RVCOR_CD", "DT_VENCIMENTO", "RVCOR_CD_CUST_FISICA" };
                        if (DML == "insert")
                        {
                            cGlobal.ar_query = query_insert(caminho_completo,
                            ids,
                            types,
                            cols,
                            "	",
                            region,
                            idarq,
                            false,
                            tipo);
                        }
                        else
                        {
                            cGlobal.ar_query = query_select(cols,
                            keys,
                            divs,
                            region);
                        }
                        break;
                    #endregion
                    #endregion

                    #region POSICAO EMPRESTIMO
                    #region SAC
                    case "POSICAO_BTC":
                        region = "POS_EMPRESTIMO";
                        tipo = "SAC";
                        idarq = arquivo_insert(arquivo, tipo);
                        ids = new int[4] { 1, 9, 11, 12 };
                        types = new string[] { };  //ajustar
                        cols = new string[4] { "CLCLI_CD", "DT_MOVIMENTO", "ID_MOVIMENTO", "QT_EMPRESTIMO" };
                        keys = new string[3] { "CLCLI_CD", "DT_MOVIMENTO", "ID_MOVIMENTO" };
                        divs = new string[1] { "QT_EMPRESTIMO" };
                        if (DML == "insert")
                        {
                            cGlobal.ar_query = query_insert(caminho_completo,
                            ids,
                            types,
                            cols,
                            ";",
                            region,
                            idarq,
                            true,
                            tipo);
                        }
                        else
                        {
                            cGlobal.ar_query = query_select(cols,
                            keys,
                            divs,
                            region);
                        }
                        break;
                    #endregion

                    #region DI8
                    case "pos_EA":
                        region = "POS_EMPRESTIMO";
                        tipo = "DI8";
                        idarq = arquivo_insert(arquivo, tipo);
                        ids = new int[4] { 2, 3, 4, 5 };
                        types = new string[] { };  //ajustar
                        cols = new string[4] { "CLCLI_CD", "DT_MOVIMENTO", "ID_MOVIMENTO", "QT_EMPRESTIMO" };
                        keys = new string[3] { "CLCLI_CD", "DT_MOVIMENTO", "ID_MOVIMENTO" };
                        divs = new string[1] { "QT_EMPRESTIMO" };
                        if (DML == "insert")
                        {
                            cGlobal.ar_query = query_insert(caminho_completo,
                            ids,
                            types,
                            cols,
                            "	",
                            region,
                            idarq,
                            false,
                            tipo);
                        }
                        else
                        {
                            cGlobal.ar_query = query_select(cols,
                            keys,
                            divs,
                            region);
                        }
                        break;
                    #endregion
                    #endregion
                    
                    //----------------- TRATAMENTO DE CPR ----------------------------
                    #region CPR
                    #region SAC
                    case "VCRAPRDP":
                        region = "CPR";
                        tipo = "SAC";
                        idarq = arquivo_insert(arquivo, tipo);
                        ids = new int[4] { 3, 2, 5, 6 };
                        types = new string[4] { "texto", "texto", "texto", "numero" };
                        cols = new string[4] { "CLCLI_CD", "DT", "DS", "VL" };
                        keys = new string[2] { "CLCLI_CD", "DT" };
                        divs = new string[2] { "DS", "VL" };
                        if (DML == "insert")
                        {
                            cGlobal.ar_query = query_insert(caminho_completo,
                            ids,
                            types,
                            cols,
                            ";",
                            region,
                            idarq,
                            true,
                            tipo);
                        }
                        else
                        {
                            cGlobal.ar_query = query_select(cols,
                            keys,
                            divs,
                            region);
                        }
                        break;
                    #endregion

                    #region DI8
                    case "pos_CPR":
                        region = "CPR";
                        tipo = "DI8";
                        idarq = arquivo_insert(arquivo, tipo);
                        ids = new int[4] { 3, 2, 5, 4 };
                        types = new string[4] { "texto", "texto", "texto", "numero" };
                        cols = new string[4] { "CLCLI_CD", "DT", "DS", "VL" };
                        keys = new string[2] { "CLCLI_CD", "DT" };
                        divs = new string[2] { "DS", "VL" };
                        if (DML == "insert")
                        {
                            cGlobal.ar_query = query_insert(caminho_completo,
                            ids,
                            types,
                            cols,
                            "	",
                            region,
                            idarq,
                            false,
                            tipo);
                        }
                        else
                        {
                            cGlobal.ar_query = query_select(cols,
                            keys,
                            divs,
                            region);
                        }
                        break;
                    #endregion
                    #endregion
                    
                    //----------------- TRATAMENTO DE TESOURARIA ---------------------
                    #region TESOURARIA
                    // ANALISAR O TRABALHO QUE SERÁ REALIZADO NO CAMPO 10 DO DI8 LEVANDO EM CONTA A SIGLA "R" E "D" (RECEITA E DESPESA)
                    // ESTE VALOR ENVOLVE O SINAL, PODENDO SER NEGATIVO OU POSITIVO
                    #region SAC
                    case "VCRADREC":
                        region = "TESOURARIA";
                        tipo = "SAC";
                        idarq = arquivo_insert(arquivo, tipo);
                        ids = new int[5] { 2, 3, 5, 8, 12 };
                        types = new string[5] { "texto", "texto", "numero", "texto", "texto" };
                        cols = new string[5] { "CLCLI_CD", "DT", "VL", "DS", "DATALANC" };
                        keys = new string[3] { "CLCLI_CD", "DT", "VL" };
                        divs = new string[2] { "DS", "DATALANC" };
                        if (DML == "insert")
                        {
                            cGlobal.ar_query = query_insert(caminho_completo,
                            ids,
                            types,
                            cols,
                            ";",
                            region,
                            idarq,
                            true,
                            tipo);
                        }
                        else
                        {
                            cGlobal.ar_query = query_select(cols,
                            keys,
                            divs,
                            region);
                        }
                        break;
                    #endregion

                    #region DI8
                    case "rel_DC":
                        region = "TESOURARIA";
                        tipo = "DI8";
                        idarq = arquivo_insert(arquivo, tipo);
                        ids = new int[] { 5, 3, 8, 9, 4 }; //ajustar
                        types = new string[5] { "texto", "texto", "numero", "texto", "texto" };
                        cols = new string[5] { "CLCLI_CD", "DT", "VL", "DS", "DATALANC" };
                        keys = new string[3] { "CLCLI_CD", "DT", "VL" };
                        divs = new string[2] { "DS", "DATALANC" };
                        if (DML == "insert")
                        {
                            cGlobal.ar_query = query_insert(caminho_completo,
                            ids,
                            types,
                            cols,
                            "	",
                            region,
                            idarq,
                            false,
                            tipo);
                        }
                        else
                        {
                            cGlobal.ar_query = query_select(cols,
                            keys,
                            divs,
                            region);
                        }
                        break;
                    #endregion
                    #endregion
                    
                    //----------------- TRATAMENTO DE PATRIMONIO ---------------------
                    #region PATRIMONIO
                    // ANALISAR A FORMA DE IMPORTAÇÃO POIS TERÁ 2 DELIMITADORES
                    #region SAC
                    case "VCRAPLSI":
                        region = "PATRIMONIO";
                        tipo = "SAC";
                        idarq = arquivo_insert(arquivo, tipo);
                        ids = new int[3] { 2, 3, 8 };
                        types = new string[3] { "texto", "texto", "numero" };
                        cols = new string[3] { "CLCLI_CD", "DT", "VL_PATRLIQTOT1" };
                        keys = new string[2] { "CLCLI_CD", "DT" };
                        divs = new string[1] { "VL_PATRLIQTOT1" };
                        if (DML == "insert")
                        {
                            cGlobal.ar_query = query_insert(caminho_completo,
                            ids,
                            types,
                            cols,
                            ";",
                            region,
                            idarq,
                            true,
                            tipo);
                        }
                        else
                        {
                            cGlobal.ar_query = query_select(cols,
                            keys,
                            divs,
                            region);
                        }
                        break;
                    #endregion

                    #region DI8
                    case "Filtro PL":
                        region = "PATRIMONIO";
                        tipo = "DI8";
                        idarq = arquivo_insert(arquivo, tipo);
                        ids = new int[3] { 1, 3, 2 };
                        types = new string[3] { "texto", "texto", "numero" };
                        cols = new string[3] { "CLCLI_CD", "DT", "VL_PATRLIQTOT1" };
                        keys = new string[2] { "CLCLI_CD", "DT" };
                        divs = new string[1] { "VL_PATRLIQTOT1" };
                        if (DML == "insert")
                        {
                            cGlobal.ar_query = query_insert(caminho_completo,
                            ids,
                            types,
                            cols,
                            "	",
                            region,
                            idarq,
                            true,
                            tipo);
                        }
                        else
                        {
                            cGlobal.ar_query = query_select(cols,
                            keys,
                            divs,
                            region);
                        }
                        break;
                    #endregion
                    #endregion

                    //----------------- TRATAMENTO DE FUNDO --------------------------
                    #region MOVIMENTO FUNDO
                    // O SST não gera este arquivo ainda. Aguardando definição que será repassada pelo Roberto								
                    #endregion

                    #region POSICAO FUNDO
                    #region SAC
                    case "VCRAPFDO":
                        region = "POS_FUNDO";
                        tipo = "SAC";
                        idarq = arquivo_insert(arquivo, tipo);
                        ids = new int[6] { 2, 3, 4, 10, 12, 13 };
                        types = new string[6] { "texto", "texto", "texto", "texto", "numero", "numero" };
                        cols = new string[6] { "CLCLI_CD", "DT", "FICAD_CD", "DT_APLICACAO", "VL_COTA", "VL_TOTAL" };
                        keys = new string[3] { "CLCLI_CD", "DT", "FICAD_CD" };
                        divs = new string[3] { "DT_APLICACAO", "VL_COTA", "VL_TOTAL" };
                        if (DML == "insert")
                        {
                            cGlobal.ar_query = query_insert(caminho_completo,
                            ids,
                            types,
                            cols,
                            ";",
                            region,
                            idarq,
                            true,
                            tipo);
                        }
                        else
                        {
                            cGlobal.ar_query = query_select(cols,
                            keys,
                            divs,
                            region);
                        }
                        break;
                    #endregion

                    #region DI8
                    case "pos_FI":
                        region = "POS_FUNDO";
                        tipo = "DI8";
                        idarq = arquivo_insert(arquivo, tipo);
                        ids = new int[6] { 3, 2, 4, 5, 8, 9 }; //ajustar
                        types = new string[6] { "texto", "texto", "texto", "texto", "numero", "numero" };
                        cols = new string[6] { "CLCLI_CD", "DT", "FICAD_CD", "DT_APLICACAO", "VL_COTA", "VL_TOTAL" };
                        keys = new string[3] { "CLCLI_CD", "DT", "FICAD_CD" };
                        divs = new string[3] { "DT_APLICACAO", "VL_COTA", "VL_TOTAL" };
                        if (DML == "insert")
                        {
                            cGlobal.ar_query = query_insert(caminho_completo,
                            ids,
                            types,
                            cols,
                            "	",
                            region,
                            idarq,
                            false,
                            tipo);
                        }
                        else
                        {
                            cGlobal.ar_query = query_select(cols,
                            keys,
                            divs,
                            region);
                        }
                        break;
                    #endregion
                    #endregion

                    default:
                    break;
                }

                return cGlobal.ar_query;
            }
            catch (Exception ex)
            {
                #region LOG ERRO
                cLog lg = new cLog();
                lg.log = ex.Message.Replace("'", "");
                lg.form = this.Text;
                lg.metodo = sender.ToString();
                lg.dt = DateTime.Now;
                lg.usersistema = cGlobal.userlogado;
                lg.userRede = Environment.UserName;
                lg.terminal = Environment.MachineName;
                lg.tp_flag = false;
                lg.grava_log(lg);
                #endregion
                throw ex;
            }
        }

        private List<string> query_insert(string caminho, int[] ids, string[] types, string[] cols, string delimitador, string tabela, int idarq, bool cabecalho, string tipo)
        {
            List<string> al = new List<string>();
            string valores = "";
            string colunas = "";
            try
            {
                String[] LineTxt = System.IO.File.ReadAllLines(caminho);
                for (int i = (cabecalho ? 1 : 0); i < LineTxt.Count(); i++)
                {
                    valores = "";
                    colunas = "";
                    String[] Line = LineTxt[i].Split(new String[] { delimitador }, StringSplitOptions.None);
                    string sep = "";
                    for (int j = 0; j < ids.Count(); j++)
                    {
                        if (types[j] == "texto")
                        {
                            valores = valores + sep + "'" + Line[ids[j]-1].Replace("'","''") + "'";
                        }
                        else
                        {
                            if (String.IsNullOrEmpty(Line[ids[j]-1])) { 
                                valores = valores + sep + "null";
                            } else {
                                //Verifico se no campo 10 (Line[9]) do arquivo DI8 de TESOURARIA possui valor de Receita ou Despesa
                                //Somente se for Despesa, considera o valor como sendo negativo
                                //j = 7 significa que a alteração será feita somente na posição 8 do arquivo que é o Valor do Lançamento 
                                if (tabela == "TESOURARIA" && Line[9].Trim() == "D" && j == 7 && tipo == "DI8")
                                {
                                    valores = valores + sep + string.Concat("-",Line[ids[j] - 1].Replace(",", "."));
                                }
                                //Para o PATRIMONIO o campo 2 possui mais um delimitador "|" e temos que trabalhar apenas com a posição 3
                                else if (tabela == "PATRIMONIO" && j == 2 && tipo == "DI8")
                                {
                                    valores = valores + sep + Line[ids[j] - 1].Split('|')[2].Replace(",", ".").Trim();
                                }
                                //Importação normal dos demais fundos e campos
                                else
                                { 
                                    valores = valores + sep + Line[ids[j]-1].Replace(",", ".");
                                }
                            }
                        }
                        colunas = colunas + sep + cols[j];
                        sep = ",";
                    }
                    al.Add(string.Format("INSERT INTO {0} ({1},ID_ARQ) VALUES ({2},{3})", tabela, colunas, valores, idarq));
                }

                return al;
            }
            catch (Exception ex)
            {
                #region LOG ERRO
                cLog lg = new cLog();
                lg.log = ex.Message.Replace("'", "");
                lg.form = this.Text;
                lg.metodo = sender.ToString();
                lg.dt = DateTime.Now;
                lg.usersistema = cGlobal.userlogado;
                lg.userRede = Environment.UserName;
                lg.terminal = Environment.MachineName;
                lg.tp_flag = false;
                lg.grava_log(lg);
                #endregion
                throw ex;
            }
        }

        private List<string> query_select(string[] cols, string[] keys, string[] divs, string tabela)
        {
            List<string> al = new List<string>();
            DataTable dt = new DataTable();

            //Select para identificação dos registros com valores iguais/corretos
            //------------------------------------------------------------------------------------------------------
            string colunasC = "";
            string sepC = "";
            for (int j = 0; j < cols.Count(); j++)
            {
                colunasC = string.Concat(colunasC,sepC," SAC.",cols[j].ToString()," ="," DI8.", cols[j].ToString());
                sepC = " AND ";
            }
                        
            string selectDiv0 = string.Format("SELECT DISTINCT 'UPDATE {0} SET FLAG_DIV = 0 WHERE ID = ' & SAC.ID AS UPDATESAC, " +
                                              "       'UPDATE {0} SET FLAG_DIV = 0 WHERE ID = ' & DI8.ID AS UPDATEDI8 " +
                                              "FROM " +
                                              "      (SELECT * FROM {0} M INNER JOIN ARQUIVO_IMP A ON M.ID_ARQ = A.ID_ARQ " +
                                              "       WHERE A.TP_ARQ = 'SAC') SAC " +
                                              "INNER JOIN " +
                                              "      (SELECT * FROM {0} M INNER JOIN ARQUIVO_IMP A ON M.ID_ARQ = A.ID_ARQ " +
                                              "       WHERE A.TP_ARQ = 'DI8') DI8 " +
                                              "ON {1} ",tabela,colunasC);

            dt = dao.retorna_datatable(selectDiv0, "cnn_proc");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                al.Add(dt.Rows[i].ItemArray[0].ToString());
                al.Add(dt.Rows[i].ItemArray[1].ToString());
            }
            dt.Clear();
            
            //------------------------------------------------------------------------------------------------------

            //Select para identificação dos registros com valores divergentes/incorretos
            //------------------------------------------------------------------------------------------------------
            string colunasIKeys = "";
            string colunasIDivs = "";
            string sepIKeys = "";
            string sepIDivs = "";

            for (int j = 0; j < keys.Count(); j++)
            {
                colunasIKeys = string.Concat(colunasIKeys, sepIKeys, " SAC.", keys[j].ToString(), " =", " DI8.", keys[j].ToString());
                sepIKeys = " AND ";
            }

            for (int j = 0; j < divs.Count(); j++)
            {
                colunasIDivs = string.Concat(colunasIDivs, sepIDivs, " SAC.", divs[j].ToString(), " <>", " DI8.", divs[j].ToString());
                sepIDivs = " OR ";
            }

            string selectDiv1 = string.Format("SELECT DISTINCT 'UPDATE {0} SET FLAG_DIV = 1 WHERE ID = ' & SAC.ID AS UPDATESAC, " +
                                              "       'UPDATE {0} SET FLAG_DIV = 1 WHERE ID = ' & DI8.ID AS UPDATEDI8 " +
                                              "FROM " +
                                              "      (SELECT * FROM {0} M INNER JOIN ARQUIVO_IMP A ON M.ID_ARQ = A.ID_ARQ " +
                                              "       WHERE A.TP_ARQ = 'SAC') SAC " +
                                              "INNER JOIN " +
                                              "      (SELECT * FROM {0} M INNER JOIN ARQUIVO_IMP A ON M.ID_ARQ = A.ID_ARQ " +
                                              "       WHERE A.TP_ARQ = 'DI8') DI8 " +
                                              "ON {1} AND ({2}) ", tabela, colunasIKeys, colunasIDivs);

            dt = dao.retorna_datatable(selectDiv1, "cnn_proc");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                al.Add(dt.Rows[i].ItemArray[0].ToString());
                al.Add(dt.Rows[i].ItemArray[1].ToString());
            }
            dt.Clear();
            //------------------------------------------------------------------------------------------------------

            //Select para identificação dos registros com valores somente de SAC, não sendo possível encontrar no DI8
            //------------------------------------------------------------------------------------------------------
            string colunasSKeys = "";
            string sepS = "";
            for (int j = 0; j < keys.Count(); j++)
            {
                colunasSKeys = string.Concat(colunasSKeys, sepS, " SAC.", keys[j].ToString(), " =", " DI8.", keys[j].ToString());
                sepS = " AND ";
            }

            string selectDiv2 = string.Format("SELECT DISTINCT 'UPDATE {0} SET FLAG_DIV = 2 WHERE ID = ' & SAC.ID AS UPDATESAC, " +
                                              "       'UPDATE {0} SET FLAG_DIV = 2 WHERE ID = ' & DI8.ID AS UPDATEDI8 " +
                                              "FROM " +
                                              "      (SELECT * FROM {0} M INNER JOIN ARQUIVO_IMP A ON M.ID_ARQ = A.ID_ARQ " +
                                              "       WHERE A.TP_ARQ = 'SAC') SAC " +
                                              "LEFT JOIN " +
                                              "      (SELECT * FROM {0} M INNER JOIN ARQUIVO_IMP A ON M.ID_ARQ = A.ID_ARQ " +
                                              "       WHERE A.TP_ARQ = 'DI8') DI8 " +
                                              "ON {1} " +
                                              "WHERE ISNULL(DI8.ID)", tabela, colunasIKeys);

            dt = dao.retorna_datatable(selectDiv2, "cnn_proc");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                al.Add(dt.Rows[i].ItemArray[0].ToString());
            }
            dt.Clear();
            //------------------------------------------------------------------------------------------------------

            //Select para identificação dos registros com valores somente de DI8, não sendo possível encontrar no SAC
            //------------------------------------------------------------------------------------------------------
            string colunasDKeys = "";
            string sepD = "";
            for (int j = 0; j < keys.Count(); j++)
            {
                colunasDKeys = string.Concat(colunasDKeys, sepD, " SAC.", keys[j].ToString(), " =", " DI8.", keys[j].ToString());
                sepD = " AND ";
            }

            string selectDiv3 = string.Format("SELECT DISTINCT 'UPDATE {0} SET FLAG_DIV = 3 WHERE ID = ' & SAC.ID AS UPDATESAC, " +
                                              "       'UPDATE {0} SET FLAG_DIV = 3 WHERE ID = ' & DI8.ID AS UPDATEDI8 " +
                                              "FROM " +
                                              "      (SELECT * FROM {0} M INNER JOIN ARQUIVO_IMP A ON M.ID_ARQ = A.ID_ARQ " +
                                              "       WHERE A.TP_ARQ = 'SAC') SAC " +
                                              "RIGHT JOIN " +
                                              "      (SELECT * FROM {0} M INNER JOIN ARQUIVO_IMP A ON M.ID_ARQ = A.ID_ARQ " +
                                              "       WHERE A.TP_ARQ = 'DI8') DI8 " +
                                              "ON {1} " +
                                              "WHERE ISNULL(SAC.ID)", tabela, colunasDKeys);

            dt = dao.retorna_datatable(selectDiv3, "cnn_proc");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                al.Add(dt.Rows[i].ItemArray[1].ToString());
            }
            dt.Clear();
            //------------------------------------------------------------------------------------------------------


            return al.Distinct().ToList();
        }

        private int arquivo_insert(string nome_arq, string tipo_arq)
        {
            DataTable dt = new DataTable();
            int id;

            string insArq = "INSERT INTO ARQUIVO_IMP (NM_ARQ,TP_ARQ,DT_IMP,FLAG_IMP,DT_PROC,FLAG_PROC) " +
                                             "VALUES ('" + nome_arq + "','" +
                                                      tipo_arq + "','" +
                                                      DateTime.Now.ToString() + "'," +
                                                      "1," +
                                                      "null," +
                                                      "0" +
                                            ")";
            dao.executa_query(insArq, "cnn_proc");

            dt = dao.retorna_datatable("SELECT TOP 1 ID_ARQ FROM [ARQUIVO_IMP] ORDER BY 1 DESC", "cnn_proc");
            id = Convert.ToInt32(dt.Rows[0].ItemArray[0]);
            return id;
        }

    }
}

