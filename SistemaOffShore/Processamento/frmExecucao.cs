using SistemaOffShore.Class;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SistemaOffShore.Processamento
{
    public partial class frmExecucao : Form
    {
        cDAO dao = new cDAO();
        cProcessamento cproc = new cProcessamento();




        public frmExecucao()
        {
            InitializeComponent();
        }

        private void frmExecucao_Load(object sender, EventArgs e)
        {
            try
            {
                inicio();
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

        private void inicio()
        {
            try
            {
                #region FORMATACAO DA LISTVIEW
                lvArquivos.Clear();
                lvArquivos.View = View.Details;
                lvArquivos.LabelEdit = true;
                lvArquivos.AllowColumnReorder = true;
                lvArquivos.CheckBoxes = true;
                lvArquivos.FullRowSelect = true;
                lvArquivos.GridLines = true;
                lvArquivos.Columns.Add("", 50, HorizontalAlignment.Center);
                lvArquivos.Columns.Add("SAC", 160, HorizontalAlignment.Center);
                lvArquivos.Columns.Add("DI8", 160, HorizontalAlignment.Center);
                lvArquivos.Columns.Add("Descrição", 250, HorizontalAlignment.Center);
                lvArquivos.Columns.Add("Status Importação", 180, HorizontalAlignment.Center);
                lvArquivos.Columns.Add("Status Batimento", 180, HorizontalAlignment.Center);
                #endregion
                cproc.preenche_listview(ref lvArquivos);
            }
            catch (Exception ex)
            {
                #region LOG ERRO
                cLog lg = new cLog();
                lg.log = ex.Message.Replace("'", "");
                lg.form = Text;
                lg.metodo = "inicio";
                lg.dt = DateTime.Now;
                lg.usersistema = cGlobal.userlogado;
                lg.userRede = Environment.UserName;
                lg.terminal = Environment.MachineName;
                lg.tp_flag = false;
                lg.grava_log(lg);
                #endregion
            }
        }

        #region METODOS
        #endregion

        private void tsbtnIniciar_Click(object sender, EventArgs e)
        {
            try
            {
                #region VALIDAÇÃO
                int ponteiro = 0;
                for (int i = 0; i < lvArquivos.Items.Count; i++)
                {
                    if (lvArquivos.Items[i].Checked == true)
                    {
                        ponteiro += 1;
                    }
                }
                #endregion

                if (ponteiro == 0)
                {
                    if (lvArquivos.SelectedItems.Count == 0)
                    {
                        MessageBox.Show("Nenhum registro foi selecionado.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else
                {
                    for (int i = 0; i < lvArquivos.Items.Count; i++)
                    {
                        lvArquivos.Items[i].SubItems[4].Text = "";

                        if (lvArquivos.Items[i].Checked == true)
                        {

                            //VERIFICA SE OS ARQUIVOS ESTÃO NO DIRETÓRIO INFORMADO
                            if (!cproc.importacao(int.Parse(lvArquivos.Items[i].Text), dtpData.Value))
                            {
                                lvArquivos.Items[i].SubItems[4].Text = "Arquivo não encontrado.";
                                lvArquivos.Update();
                            }
                            else
                            { 

                            //INICIA A IMPORTACAO
                            for (int k = 0; k < cGlobal.caminho_arq.Count; k++)
                            {
                                string nm_arq = lvArquivos.Items[i].SubItems[k + 1].Text;
                                tslblMsg.Text = "Importando arquivo " + nm_arq;
                                statusStrip1.Update();

                                inicia_importacao(cGlobal.caminho_arq[k].ToString(), nm_arq);
                            }
                            lvArquivos.Items[i].SubItems[4].Text = "Concluído";
                            lvArquivos.Update();


                            //INICIA O PROCESSAMENTO
                            for (int k = 0; k < cGlobal.caminho_arq.Count; k++)
                            {
                                string nm_arq = lvArquivos.Items[i].SubItems[k + 1].Text;
                                tslblMsg.Text = "Processando arquivo " + nm_arq;
                                statusStrip1.Update();

                                inicia_processamento(cGlobal.caminho_arq[k].ToString(), nm_arq);
                            }
                            lvArquivos.Items[i].SubItems[5].Text = "Concluído";
                            lvArquivos.Update();

                            cGlobal.caminho_arq.Clear();
                            }
                        }
                    }
                    MessageBox.Show("Processo concluído.","",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    tslblMsg.Text = "";
                    statusStrip1.Update();
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

        private void inicia_importacao(string caminho_arquivo, string nm_arq)
        {
            try
            {
                //Importação dos registros 
                //-----------------------------------------------------------------------------------------------
                List<string> querysImp = new List<string>();

                querysImp = cproc.identifica_arquivo(nm_arq, caminho_arquivo,"insert");

                tsProgresso.Visible = true;
                tsProgresso.Value = 0;
                tsProgresso.Maximum = querysImp.Count;

                for (int i = 0; i < querysImp.Count; i++)
                {
                    dao.executa_query(querysImp[i].ToString(), "cnn_proc");
                    tsProgresso.Value++;
                }
                //-----------------------------------------------------------------------------------------------
                tsProgresso.Value = 0;
            }
            catch (Exception ex)
            {
                #region LOG ERRO
                cLog lg = new cLog();
                lg.log = ex.Message.Replace("'", "");
                lg.form = this.Text;
                lg.metodo = "inicia_importacao";
                lg.dt = DateTime.Now;
                lg.usersistema = cGlobal.userlogado;
                lg.userRede = Environment.UserName;
                lg.terminal = Environment.MachineName;
                lg.tp_flag = false;
                lg.grava_log(lg);
                #endregion
            }
        }

        private void inicia_processamento(string caminho_arquivo, string nm_arq)
        {
            try
            {
                //Processamento da Colagem
                //-----------------------------------------------------------------------------------------------
                var querysProc = cproc.identifica_arquivo(nm_arq, caminho_arquivo, "update");

                tsProgresso.Visible = true;
                tsProgresso.Value = 0;
                tsProgresso.Maximum = querysProc.Count;

                for (int i = 0; i < querysProc.Count; i++)
                {
                    dao.executa_query(querysProc[i].ToString(), "cnn_proc");
                    tsProgresso.Value++;
                }
                //-----------------------------------------------------------------------------------------------
                tsProgresso.Value = 0;
            }
            catch (Exception ex)
            {
                #region LOG ERRO
                cLog lg = new cLog();
                lg.log = ex.Message.Replace("'", "");
                lg.form = this.Text;
                lg.metodo = "inicia_processamento";
                lg.dt = DateTime.Now;
                lg.usersistema = cGlobal.userlogado;
                lg.userRede = Environment.UserName;
                lg.terminal = Environment.MachineName;
                lg.tp_flag = false;
                lg.grava_log(lg);
                #endregion
            }
        }

        private void chkTodos_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkTodos.Checked)
                {
                    for (int i = 0; i < lvArquivos.Items.Count; i++)
                    {
                        lvArquivos.Items[i].Checked = true;
                    }
                }
                else
                {
                    for (int i = 0; i < lvArquivos.Items.Count; i++)
                    {
                        lvArquivos.Items[i].Checked = false;
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

    }
}
