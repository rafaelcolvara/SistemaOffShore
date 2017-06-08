using SistemaOffShore.Class;
using System;
using System.Windows.Forms;

namespace SistemaOffShore
{
    public partial class frmInicio : Form
    {
        public frmInicio()
        {
            InitializeComponent();
        }

        private void frmInicio_Load(object sender, EventArgs e)
        {
            try
            {
                #region RETORNA USUÁRIO LOGADO 
                string userlog = Environment.UserName;
                string name_machine = Environment.MachineName;

                tsslblUsuario.Text = string.Concat("Login: ", cGlobal.userlogado, " | Setor: ", cGlobal.userSetor);
                tsslblUsuarioDtHr.Text = string.Concat("Data/Hora: ", DateTime.Now);
                tsslblUsuarioRede.Text = string.Concat("Usuário Rede: ", userlog);
                tsslblUsuarioRedeDNS.Text = string.Concat("Terminal: ", name_machine);
                #endregion

                timer1.Start();
                this.WindowState = FormWindowState.Maximized;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                tsslblTime.Text = string.Concat(DateTime.Now.ToLongDateString(), " - ", DateTime.Now.ToLongTimeString()); //.ToString("dd/MM/yyyy HH:MM:SS");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void MnuSair_Click(object sender, EventArgs e)
        {
            try
            {
                #region LOG
                cLog lg = new cLog();
                lg.log = "Finalizou o acesso ao Sistema.";
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
                Application.Exit();
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

        private void mnuSobre_Click(object sender, EventArgs e)
        {
            try
            {
                using (frmAbout frm = new frmAbout())
                {
                    frm.ShowDialog();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void mnuSetor_Click(object sender, EventArgs e)
        {
            try
            {
                using (frmSetor frm = new frmSetor())
                {
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void mnuUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                using (frmUsuario frm = new frmUsuario())
                {
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void mnuLog_Click(object sender, EventArgs e)
        {
            try
            {
                using (frmLog frm = new frmLog())
                {
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void mnuLogoff_Click(object sender, EventArgs e)
        {
            try
            {
                #region LOG
                cLog lg = new cLog();
                lg.log = "Efetuado Logoff no Sistema";
                lg.form = this.Text;
                lg.metodo = sender.ToString();
                lg.dt = DateTime.Now;
                lg.usersistema = cGlobal.userlogado;
                lg.userRede = Environment.UserName;
                lg.terminal = Environment.MachineName;
                lg.tp_flag = true;
                lg.grava_log(lg);
                #endregion
                using (frmLogin frm = new frmLogin())
                {
                    this.Hide();
                    frm.ShowDialog();
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

        private void mnuConfiguracao_Click(object sender, EventArgs e)
        {
            try
            {
                using (frmConfiguracao frm = new frmConfiguracao())
                {
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void mnuDirProcessamento_Click(object sender, EventArgs e)
        {
            try
            {
                using (Processamento.frmDiretorio frm = new Processamento.frmDiretorio())
                {
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void mnuFundo_Click(object sender, EventArgs e)
        {
            try
            {
                using (frmFundo frm = new frmFundo())
                {
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void mnuExecProcessamento_Click(object sender, EventArgs e)
        {
            try
            {
                using (Processamento.frmExecucao frm = new Processamento.frmExecucao())
                {
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void processamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (Processamento.frmRelatorios frm = new Processamento.frmRelatorios())
                {
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
