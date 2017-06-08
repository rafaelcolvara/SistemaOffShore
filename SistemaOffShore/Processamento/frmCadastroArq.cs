using SistemaOffShore.Class;
using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace SistemaOffShore.Processamento
{
    public partial class frmCadastroArq : Form
    {
        OleDbDataAdapter da;
        DataTable dt = new DataTable();
        BindingSource bs;

        public frmCadastroArq()
        {
            InitializeComponent();
        }

        private void frmCadastroArq_Load(object sender, EventArgs e)
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

        private void inicio()
        {
            try
            {
                cDAO dao = new cDAO();
                cGlobal.query = "SELECT * FROM ARQUIVO_FISICO";

                DataGridViewTextBoxColumn campo0 = new DataGridViewTextBoxColumn();
                campo0.Name = "ID";
                campo0.HeaderText = "ID";
                campo0.Width = 100;
                campo0.DataPropertyName = "ID";
                campo0.Visible = true;

                DataGridViewTextBoxColumn campo1 = new DataGridViewTextBoxColumn();
                campo1.Name = "SAC";
                campo1.HeaderText = "SAC";
                campo1.Width = 265;
                campo1.DataPropertyName = "SAC";
                campo1.Visible = true;

                DataGridViewTextBoxColumn campo2 = new DataGridViewTextBoxColumn();
                campo2.Name = "DI8";
                campo2.HeaderText = "DI8";
                campo2.Width = 265;
                campo2.DataPropertyName = "DI8";
                campo2.Visible = true;

                dt = dao.retorna_datatable(cGlobal.query, "cn1");
                
                    bs = new BindingSource();
                    bs.DataSource = dt;

                    dgvArquivos.Columns.Clear();
                    dgvArquivos.AutoGenerateColumns = false;
                    dgvArquivos.Columns.AddRange(new DataGridViewColumn[] { campo0, campo1, campo2 });
                    dgvArquivos.DataSource = dt;
                
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Dispose();
            Close();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            da.Update(dt);
            MessageBox.Show("O registro foi salvo com sucesso.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
            inicio();
        }
    }
}
