using SistemaOffShore.Class;
using System;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SistemaOffShore
{
    public partial class frmConfiguracao : Form
    {
        public frmConfiguracao()
        {
            InitializeComponent();
        }

        #region EVENTOS
        private void frmConfiguracao_Load(object sender, EventArgs e)
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
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Configuration conf = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
            cConfiguracao cf = new cConfiguracao();

            try
            {

                MessageBox.Show("Deseja realmente salvar as alterações?", "Mensagem", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                DialogResult dr = new DialogResult();
                if (dr == DialogResult.Yes)
                {
                    #region PERCORRE DATAGRIDVIEW
                    foreach (DataGridViewRow linha in dgv.Rows)
                    {
                        foreach (DataGridViewCell col in linha.Cells)
                        {
                            if (linha.Cells[0].Value.Equals("OffShoreDB"))
                            {
                                conf.ConnectionStrings.ConnectionStrings["cnn"].ConnectionString = string.Format("{0}{1}{2}", "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=", linha.Cells[1].Value.ToString(), "OffShoreDB.mdb;");
                                FileInfo fi = new FileInfo(string.Format("{0}{1}", linha.Cells[1].Value.ToString(), "OffShoreDB.mdb"));
                                cf.banco = "OffShoreDB";
                                cf.diretorio = linha.Cells[1].Value.ToString();
                                cf.tamanho = cGlobal.TamanhoAmigavel(fi.Length);
                                cf.update_conexao(cf);
                            }
                            else if (linha.Cells[0].Value.Equals("ProcessamentoDB"))
                            {
                                conf.ConnectionStrings.ConnectionStrings["cnn_proc"].ConnectionString = string.Format("{0}{1}{2}", "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=", linha.Cells[1].Value.ToString(), "ProcessamentoDB.mdb;");
                                FileInfo fi = new FileInfo(string.Format("{0}{1}", linha.Cells[1].Value.ToString(), "ProcessamentoDB.mdb"));
                                cf.banco = "ProcessamentoDB";
                                cf.diretorio = linha.Cells[1].Value.ToString();
                                cf.tamanho = cGlobal.TamanhoAmigavel(fi.Length);
                                cf.update_conexao(cf);
                            }
                            else if (linha.Cells[0].Value.Equals("PrecificacaoDB"))
                            {
                                conf.ConnectionStrings.ConnectionStrings["cnn_prec"].ConnectionString = string.Format("{0}{1}{2}", "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=", linha.Cells[1].Value.ToString(), "PrecificacaoDB.mdb;");
                                FileInfo fi = new FileInfo(string.Format("{0}{1}", linha.Cells[1].Value.ToString(), "ProcessamentoDB.mdb"));
                                cf.banco = "PrecificacaoDB";
                                cf.diretorio = linha.Cells[1].Value.ToString();
                                cf.tamanho = cGlobal.TamanhoAmigavel(fi.Length);
                                cf.update_conexao(cf);
                            }
                            else if (linha.Cells[0].Value.Equals("DespesasImpostosDB"))
                            {
                                conf.ConnectionStrings.ConnectionStrings["cnn_desp"].ConnectionString = string.Format("{0}{1}{2}", "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=", linha.Cells[1].Value.ToString(), "DespesasImpostosDB.mdb;");
                                FileInfo fi = new FileInfo(string.Format("{0}{1}", linha.Cells[1].Value.ToString(), "DespesasImpostosDB.mdb"));
                                cf.banco = "DespesasImpostosDB";
                                cf.diretorio = linha.Cells[1].Value.ToString();
                                cf.tamanho = cGlobal.TamanhoAmigavel(fi.Length);
                                cf.update_conexao(cf);
                            }
                            else if (linha.Cells[0].Value.Equals("CorporateActionsDB"))
                            {
                                conf.ConnectionStrings.ConnectionStrings["cnn_corp"].ConnectionString = string.Format("{0}{1}{2}", "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=", linha.Cells[1].Value.ToString(), "CorporateActionsDB.mdb;");
                                FileInfo fi = new FileInfo(string.Format("{0}{1}", linha.Cells[1].Value.ToString(), "CorporateActionsDB.mdb"));
                                cf.banco = "CorporateActionsDB";
                                cf.diretorio = linha.Cells[1].Value.ToString();
                                cf.tamanho = cGlobal.TamanhoAmigavel(fi.Length);
                                cf.update_conexao(cf);
                            }
                        }
                    }
                    #endregion
                    conf.Save();

                    MessageBox.Show("Configuração alterada com sucesso.\r\nÉ necessário fechar e iniciar novamente o sistema,\r\npara que a alteração tenha efeito.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    #region LOG
                    cLog lg = new cLog();
                    lg.log = string.Concat("Configuração de Conexão", Environment.NewLine);
                    lg.form = this.Text;
                    lg.metodo = sender.ToString();
                    lg.dt = DateTime.Now;
                    lg.usersistema = cGlobal.userlogado;
                    lg.userRede = Environment.UserName;
                    lg.terminal = Environment.MachineName;
                    lg.tp_flag = true;
                    lg.grava_log(lg);
                    #endregion

                    Dispose();
                    Close();
                }
                else
                {
                    inicio();
                    return;
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
        private void btnFechar_Click(object sender, EventArgs e)
        {
            try
            {
                Dispose();
                Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void dgvConexao_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewImageColumn && e.RowIndex >= 0)
            {
                #region VIEW
                if (e.ColumnIndex == 3)
                {
                    FolderBrowserDialog fbd = new FolderBrowserDialog();
                    if (DialogResult.OK == fbd.ShowDialog())
                    {
                        dgv.Rows[e.RowIndex].Cells[1].Value = fbd.SelectedPath;
                    }
                }
                #endregion
            }
        }
        #endregion

        #region METODO
        private void inicio()
        {
            cConfiguracao config = new cConfiguracao();
            var retorno = config.conexoes();
            #region CAMPOS
            DataGridViewTextBoxColumn campo0 = new DataGridViewTextBoxColumn();
            campo0.Name = "BANCO";
            campo0.HeaderText = "Banco";
            campo0.DataPropertyName = "BANCO";
            campo0.ToolTipText = "";
            campo0.Width = 120;

            DataGridViewTextBoxColumn campo1 = new DataGridViewTextBoxColumn();
            campo1.Name = "DIRETORIO";
            campo1.HeaderText = "Diretório";
            campo1.DataPropertyName = "DIRETORIO";
            campo1.ToolTipText = "";
            campo1.Width = 450;

            DataGridViewTextBoxColumn campo2 = new DataGridViewTextBoxColumn();
            campo2.Name = "TAMANHO";
            campo2.HeaderText = "Tamanho";
            campo2.DataPropertyName = "TAMANHO";
            campo2.ToolTipText = "";
            campo2.Width = 100;

            DataGridViewImageColumn campo3 = new DataGridViewImageColumn();
            campo3.ValuesAreIcons = false;
            Image img1 = Image.FromFile(string.Concat(Application.StartupPath, @"\folder.ico"));
            campo3.Image = img1;
            campo3.HeaderText = "";
            campo3.Name = "View";
            campo3.ToolTipText = "configurar diretório";
            campo3.Width = 30;

            dgv.Columns.Clear();
            dgv.AutoGenerateColumns = false;
            dgv.Columns.AddRange(new DataGridViewColumn[] { campo0, campo1, campo2, campo3 });

            dgv.DataSource = retorno;

            if (dgv.Rows[0].Cells[1].Value.ToString() == "" &&
                dgv.Rows[1].Cells[1].Value.ToString() == "" &&
                dgv.Rows[2].Cells[1].Value.ToString() == "" &&
                dgv.Rows[3].Cells[1].Value.ToString() == "" &&
                dgv.Rows[4].Cells[1].Value.ToString() == "")
            {
                dgv.Rows[0].Cells[1].Value = string.Format(@"{0}\", Application.StartupPath);
                dgv.Rows[1].Cells[1].Value = string.Format(@"{0}\", Application.StartupPath);
                dgv.Rows[2].Cells[1].Value = string.Format(@"{0}\", Application.StartupPath);
                dgv.Rows[3].Cells[1].Value = string.Format(@"{0}\", Application.StartupPath);
                dgv.Rows[4].Cells[1].Value = string.Format(@"{0}\", Application.StartupPath);
            }

            dgv.Columns["BANCO"].Frozen = true;

            #endregion
        }

        #endregion
    }
}
