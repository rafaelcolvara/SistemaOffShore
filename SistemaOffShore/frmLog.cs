using SistemaOffShore.Class;
using System;
using System.Data;
using System.Windows.Forms;

namespace SistemaOffShore
{
    public partial class frmLog : Form
    {

        DataSet ds = new DataSet();

        public frmLog()
        {
            InitializeComponent();
        }

        private void frmLog_Load(object sender, EventArgs e)
        {
            try
            {
                trvLog.ExpandAll();
                tspMenuSuperior.Enabled = false;
                tslblStatus.Text = "0/0";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void inicio(bool flag)
        {
            try
            {
                tspMenuSuperior.Enabled = true;
                cLog cl = new cLog();
                ds = cl.retorna_log(flag);

                if (ds.Tables["TABLE"].Rows.Count == 0)
                {
                    limpa();
                    return;
                }

                #region DATASET
                txtID.DataBindings.Clear();
                txtForm.DataBindings.Clear();
                txtEvento.DataBindings.Clear();
                txtData.DataBindings.Clear();
                txtUsuarioSistema.DataBindings.Clear();
                txtUsuarioRede.DataBindings.Clear();
                txtTerminal.DataBindings.Clear();
                txtLog.DataBindings.Clear();

                txtID.DataBindings.Add("Text", ds, "TABLE.ID_LOG");
                txtForm.DataBindings.Add("Text", ds, "TABLE.FORM");
                txtEvento.DataBindings.Add("Text", ds, "TABLE.METODO");
                txtData.DataBindings.Add("Text", ds, "TABLE.DT");
                txtUsuarioSistema.DataBindings.Add("Text", ds, "TABLE.USER_SISTEMA");
                txtUsuarioRede.DataBindings.Add("Text", ds, "TABLE.USER_REDE");
                txtTerminal.DataBindings.Add("Text", ds, "TABLE.TERMINAL");
                txtLog.DataBindings.Add("Text", ds, "TABLE.LOG");

                //tslblStatus.Text = string.Concat(BindingContext[ds, "TABLE"].Position + 1, "/", BindingContext[ds, "TABLE"].Count);
                BindingContext[ds, "TABLE"].Position = BindingContext[ds, "TABLE"].Count - 1;
                tslblStatus.Text = string.Concat(BindingContext[ds, "TABLE"].Count, "/", BindingContext[ds, "TABLE"].Count);
                #endregion
            }
            catch (Exception ex)
            {
                #region LOG ERRO
                cLog lg = new cLog();
                lg.log = ex.Message.Replace("'", "");
                lg.form = this.Text;
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

        private void tsbtnFirst_Click(object sender, EventArgs e)
        {
            try
            {
                BindingContext[ds, "TABLE"].Position = 0;
                tslblStatus.Text = string.Concat(BindingContext[ds, "TABLE"].Position + 1, "/", BindingContext[ds, "TABLE"].Count);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void tsbtnPrevious_Click(object sender, EventArgs e)
        {
            try
            {
                BindingContext[ds, "TABLE"].Position -= 1;
                tslblStatus.Text = string.Concat(BindingContext[ds, "TABLE"].Position + 1, "/", BindingContext[ds, "TABLE"].Count);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void tsbtnNext_Click(object sender, EventArgs e)
        {
            try
            {
                BindingContext[ds, "TABLE"].Position += 1;
                tslblStatus.Text = string.Concat(BindingContext[ds, "TABLE"].Position + 1, "/", BindingContext[ds, "TABLE"].Count);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void tsbtnLast_Click(object sender, EventArgs e)
        {
            try
            {
                BindingContext[ds, "TABLE"].Position = BindingContext[ds, "TABLE"].Count - 1;
                tslblStatus.Text = string.Concat(BindingContext[ds, "TABLE"].Count, "/", BindingContext[ds, "TABLE"].Count);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void frmLog_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                ds.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            try
            {
                ds.Dispose();
                Dispose();
                Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void trvLog_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                TreeNode trn = e.Node;
                if (trn.Text == "Log de Atenção")
                {
                    inicio(false);
                }
                else if (trn.Text == "Log do Sistema")
                {
                    inicio(true);
                }
                else
                {
                    limpa();
                    return;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void limpa()
        {
            try
            {
                txtID.Text = string.Empty;
                txtForm.Text = string.Empty;
                txtEvento.Text = string.Empty;
                txtData.Text = string.Empty;
                txtUsuarioSistema.Text = string.Empty;
                txtUsuarioRede.Text = string.Empty;
                txtTerminal.Text = string.Empty;
                txtLog.Text = string.Empty;
                tspMenuSuperior.Enabled = false;
                tslblStatus.Text = "0/0";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
