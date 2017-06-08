using SistemaOffShore.Class;
using System;
using System.Data;
using System.Windows.Forms;

namespace SistemaOffShore
{
    public partial class frmUsuario : Form
    {
        public frmUsuario()
        {
            InitializeComponent();
        }

        #region EVENTOS
        private void frmUsuario_Load(object sender, EventArgs e)
        {
            try
            {
                inicio();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void mnuExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvUsuarios.SelectedItems.Count == 0)
                {
                    return;
                }

                cUsuario user = new cUsuario();
                user.id_usuario = Convert.ToInt32(lvUsuarios.SelectedItems[0].Text);
                user.usuario = lvUsuarios.SelectedItems[0].SubItems[1].Text.ToUpper();
                DialogResult dlr = (MessageBox.Show(string.Concat("Deseja realmente exclui o usuário:\n", lvUsuarios.SelectedItems[0].SubItems[1].Text, "?"), "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question));
                if (dlr == DialogResult.Yes)
                {
                    user.exclui_usuario(user);
                    MessageBox.Show("Usuário excluído com sucesso.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    #region LOG
                    cLog lg = new cLog();
                    lg.log = string.Concat("Exclusão de Registro.", Environment.NewLine,
                                           "Usuário: ", user.usuario
                                           );
                    lg.form = this.Text;
                    lg.metodo = sender.ToString();
                    lg.dt = DateTime.Now;
                    lg.usersistema = cGlobal.userlogado;
                    lg.userRede = Environment.UserName;
                    lg.terminal = Environment.MachineName;
                    lg.tp_flag = true;
                    lg.grava_log(lg);
                    #endregion

                    inicio();
                }
                else
                {
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
        private void mnuRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                cGlobal.editando = false;
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
        private void tsbtnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                cUsuario user = new cUsuario();
                user.id_usuario = Convert.ToInt32(txtID.Text);
                user.usuario = txtNome.Text.ToUpper();
                user.email = txtEmail.Text.ToLower();
                user.login = txtLogin.Text.ToUpper();
                user.reset_pwd = chkResetaSenha.Checked;
                user.adm = chkAdm.Checked;
                user.exec = chkExec.Checked;
                user.rpt = chkRpt.Checked;
                user.ativo = chkInativar.Checked;
                user.id_setor = Convert.ToInt32(cboArea.SelectedValue);

                if (string.IsNullOrEmpty(cboArea.Text))
                {
                    MessageBox.Show("Área não informada.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (chkResetaSenha.Checked == true)
                {
                    user.senha = Criptografia.Criptografia.executa_cript("1234");
                    user.atualiza_cadastro_usuario(user, true);
                    #region LOG
                    cLog lg = new cLog();
                    lg.log = string.Concat("Resetar senha de Acesso.", Environment.NewLine,
                                           "Usuário: ", txtNome.Text.ToUpper(), Environment.NewLine,
                                           "E-mail: ", txtEmail.Text.ToLower(), Environment.NewLine,
                                           "Login: ", txtLogin.Text.ToUpper()
                                           );
                    lg.form = this.Text;
                    lg.metodo = sender.ToString();
                    lg.dt = DateTime.Now;
                    lg.usersistema = cGlobal.userlogado;
                    lg.userRede = Environment.UserName;
                    lg.terminal = Environment.MachineName;
                    lg.tp_flag = true;
                    lg.grava_log(lg);
                    #endregion
                }
                else
                {
                    user.atualiza_cadastro_usuario(user, false);
                    #region LOG
                    cLog lg = new cLog();
                    lg.log = string.Concat("Alteração de Registro.", Environment.NewLine,
                                           "Usuário: ", txtNome.Text.ToUpper(), Environment.NewLine,
                                           "E-mail: ", txtEmail.Text.ToLower(), Environment.NewLine,
                                           "Login: ", txtLogin.Text.ToUpper()
                                           );
                    lg.form = this.Text;
                    lg.metodo = sender.ToString();
                    lg.dt = DateTime.Now;
                    lg.usersistema = cGlobal.userlogado;
                    lg.userRede = Environment.UserName;
                    lg.terminal = Environment.MachineName;
                    lg.tp_flag = true;
                    lg.grava_log(lg);
                    #endregion
                }

                MessageBox.Show("Os dados do usuário foram\r\nalterados com sucesso.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
        private void chkAdm_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkAdm.Checked == true)
                {
                    chkExec.Checked = true;
                    chkRpt.Checked = true;
                }
                else
                {
                    chkExec.Checked = false;
                    chkRpt.Checked = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region LISTVIEW
        private void lvwUsuarios_Click(object sender, EventArgs e)
        {
            try
            {
                txtID.Text = lvUsuarios.SelectedItems[0].Text;
                txtNome.Text = lvUsuarios.SelectedItems[0].SubItems[1].Text;
                txtEmail.Text = lvUsuarios.SelectedItems[0].SubItems[2].Text;
                txtLogin.Text = lvUsuarios.SelectedItems[0].SubItems[3].Text;
                chkResetaSenha.Checked = Convert.ToBoolean(lvUsuarios.SelectedItems[0].SubItems[5].Text) ? true : false;
                chkAdm.Checked = Convert.ToBoolean(lvUsuarios.SelectedItems[0].SubItems[6].Text) ? true : false;
                chkInativar.Checked = Convert.ToBoolean(lvUsuarios.SelectedItems[0].SubItems[7].Text) ? true : false;
                chkExec.Checked = Convert.ToBoolean(lvUsuarios.SelectedItems[0].SubItems[9].Text) ? true : false;
                chkRpt.Checked = Convert.ToBoolean(lvUsuarios.SelectedItems[0].SubItems[10].Text) ? true : false;

                //MOSTRA SETOR VINCULADO
                cUsuario user = new cUsuario();
                using (DataTable dt = user.retorna_area_usuario(int.Parse(lvUsuarios.SelectedItems[0].Text), Convert.ToInt32(lvUsuarios.SelectedItems[0].SubItems[8].Text)))
                {
                    cboArea.SelectedValue = dt.Rows[0].ItemArray[1].ToString();
                }

                status_form(true);
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
        private void lvUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtID.Text = lvUsuarios.SelectedItems[0].Text;
                txtNome.Text = lvUsuarios.SelectedItems[0].SubItems[1].Text;
                txtEmail.Text = lvUsuarios.SelectedItems[0].SubItems[2].Text;
                txtLogin.Text = lvUsuarios.SelectedItems[0].SubItems[3].Text;
                chkResetaSenha.Checked = Convert.ToBoolean(lvUsuarios.SelectedItems[0].SubItems[5].Text) ? true : false;
                chkAdm.Checked = Convert.ToBoolean(lvUsuarios.SelectedItems[0].SubItems[6].Text) ? true : false;
                chkInativar.Checked = Convert.ToBoolean(lvUsuarios.SelectedItems[0].SubItems[7].Text) ? true : false;
                chkExec.Checked = Convert.ToBoolean(lvUsuarios.SelectedItems[0].SubItems[9].Text) ? true : false;
                chkRpt.Checked = Convert.ToBoolean(lvUsuarios.SelectedItems[0].SubItems[10].Text) ? true : false;

                //MOSTRA SETOR VINCULADO
                cUsuario user = new cUsuario();
                using (DataTable dt = user.retorna_area_usuario(int.Parse(lvUsuarios.SelectedItems[0].Text), Convert.ToInt32(lvUsuarios.SelectedItems[0].SubItems[8].Text)))
                {
                    cboArea.SelectedValue = dt.Rows[0].ItemArray[1].ToString();
                }

                status_form(true);
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
        #endregion

        #endregion

        #region METODOS
        private void inicio()
        {
            try
            {
                limpa_campos();
                cSetor cs = new cSetor();
                using (DataSet dt = cs.retorna_setor())
                {
                    cboArea.DataSource = dt.Tables[0];
                    cboArea.DisplayMember = "SETOR";
                    cboArea.ValueMember = "ID_SETOR";
                    cboArea.Text = string.Empty;
                }

                status_form(false);
                cUsuario user = new cUsuario();
                using (DataTable dt = user.preenche_lista_usuario())
                {
                    #region listview
                    lvUsuarios.Clear();
                    lvUsuarios.View = View.Details;
                    lvUsuarios.LabelEdit = true;
                    lvUsuarios.AllowColumnReorder = true;
                    lvUsuarios.CheckBoxes = false;
                    lvUsuarios.FullRowSelect = true;
                    lvUsuarios.GridLines = true;
                    lvUsuarios.Sorting = SortOrder.Ascending;

                    lvUsuarios.Columns.Add("", 0);
                    lvUsuarios.Columns.Add("Nome", 200, HorizontalAlignment.Left);
                    lvUsuarios.Columns.Add("Email", 200, HorizontalAlignment.Left);
                    lvUsuarios.Columns.Add("Login", 150, HorizontalAlignment.Left);
                    lvUsuarios.Columns.Add("Data Cadastro", 120, HorizontalAlignment.Left);
                    lvUsuarios.Columns.Add("", 0);
                    lvUsuarios.Columns.Add("", 0);
                    lvUsuarios.Columns.Add("", 0);
                    lvUsuarios.Columns.Add("", 0);
                    lvUsuarios.Columns.Add("", 0);
                    lvUsuarios.Columns.Add("", 0);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow drw = dt.Rows[i];
                        ListViewItem lvi = new ListViewItem(drw["ID_USUARIO"].ToString());
                        lvi.SubItems.Add(drw["USUARIO"].ToString());
                        lvi.SubItems.Add(drw["EMAIL"].ToString());
                        lvi.SubItems.Add(drw["LOGIN"].ToString());
                        lvi.SubItems.Add(drw["DTCAD"].ToString());
                        lvi.SubItems.Add(drw["RESET_PWD"].ToString());
                        lvi.SubItems.Add(drw["ADM"].ToString());
                        lvi.SubItems.Add(drw["ATIVO"].ToString());
                        lvi.SubItems.Add(drw["ID_SETOR"].ToString());
                        lvi.SubItems.Add(drw["COLAGEM"].ToString());
                        lvi.SubItems.Add(drw["RPT"].ToString());

                        lvUsuarios.Items.Add(lvi);
                    }
                    lblTotalReg.Text = string.Concat("Total de ", lvUsuarios.Items.Count, " registro(s)");
                    #endregion
                }

                if (lvUsuarios.Items.Count == 0)
                {
                    mnuOculto.Enabled = false;
                }
                else
                {
                    mnuOculto.Enabled = true;
                }

                if (lvUsuarios.Items.Count > 0)
                {
                    lvUsuarios.Items[0].Selected = true;
                    lvwUsuarios_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void limpa_campos()
        {
            try
            {
                txtID.Text = string.Empty;
                txtNome.Text = string.Empty;
                txtEmail.Text = string.Empty;
                txtLogin.Text = string.Empty;
                chkResetaSenha.Checked = false;
                chkAdm.Checked = false;
                chkInativar.Checked = false;
                cboArea.Text = string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void status_form(bool flag)
        {
            gpxDados.Enabled = flag;
            gbxPermissoes.Enabled = flag;
            tsbtnSalvar.Enabled = flag;
        }
        #endregion

    }
}
