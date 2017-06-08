using SistemaOffShore.Class;
using System;
using System.Windows.Forms;

namespace SistemaOffShore.Processamento
{
    public partial class frmDiretorio : Form
    {
        public frmDiretorio()
        {
            InitializeComponent();
        }

        #region EVENTOS
        private void frmDiretorio_Load(object sender, EventArgs e)
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
                cConfiguracao cf = new cConfiguracao();
                cf.id_setor = cGlobal.userSetor;
                var retorno = cf.diretorios(cf);

                if (retorno != null)
                {
                    txtSAC.Text = (retorno.Rows[0]).ItemArray[0].ToString();
                    txtDI8.Text = (retorno.Rows[0]).ItemArray[1].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                
                DialogResult dr = (MessageBox.Show("Deseja realmente salvar as alterações?", "Mensagem", MessageBoxButtons.YesNo, MessageBoxIcon.Warning));
                if (dr == DialogResult.Yes)
                {
                    cConfiguracao cf = new cConfiguracao();
                    cf.sac = string.Format(@"{0}", txtSAC.Text);
                    cf.di8 = string.Format(@"{0}", txtDI8.Text);
                    cf.id_setor = cGlobal.userSetor;
                    cf.update_diretorios(cf);

                    MessageBox.Show("Caminho dos arquivos configurado\r\ncom sucesso.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    #region LOG
                    cLog lg = new cLog();
                    lg.log = string.Concat("Configuração de Diretórios",
                                            Environment.NewLine,
                                            "Departamento: ",
                                            cGlobal.userSetor);
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
        private void btnLocalARQSAC_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog fdb = new FolderBrowserDialog();
                if (DialogResult.OK == fdb.ShowDialog())
                {
                    txtSAC.Text = fdb.SelectedPath;
                }
                else
                {
                    inicio();
                    return;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void btnLocalARQDI8_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog fdb = new FolderBrowserDialog();
                if (DialogResult.OK == fdb.ShowDialog())
                {
                    txtDI8.Text = fdb.SelectedPath;
                }
                else
                {
                    inicio();
                    return;
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
