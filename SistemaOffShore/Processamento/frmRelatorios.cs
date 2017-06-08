using Microsoft.Reporting.WinForms;
using SistemaOffShore.Class;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace SistemaOffShore.Processamento
{
    public partial class frmRelatorios : Form
    {
        //Microsoft.Office.Interop.Excel.Application XcelApp = new Microsoft.Office.Interop.Excel.Application();
        cDAO dao = new cDAO();
        public frmRelatorios()
        {
            InitializeComponent();
        }

        private void frmRelatorios_Load(object sender, EventArgs e)
        {
            btnExportar.Enabled = false;

            //Popula combo de Status Processamento
            cmbStatusProc.DisplayMember = "Descricao";
            cmbStatusProc.ValueMember = "ID";

            cmbStatusProc.Items.Insert(0, "SAC/DI8 Iguais");
            cmbStatusProc.Items.Insert(1, "SAC/DI8 Divergente");
            cmbStatusProc.Items.Insert(2, "Somente SAC");
            cmbStatusProc.Items.Insert(3, "Somente DI8");
            cmbStatusProc.Refresh();

            //Popula checklistbox com os Ativos
            DataTable dtAtivos = new DataTable();
            string query = "SELECT ID, TABELA, DESCRICAO FROM ARQUIVO_FISICO";
            dtAtivos = dao.retorna_datatable(query, "cnn_proc");
            cmbAtivos.Items.Insert(0, "");
            cmbAtivos.Items.Clear();
            Dictionary<string, string> ativos = new Dictionary<string, string>();


            foreach (DataRow dr in dtAtivos.Rows)
            {
                ativos.Add(dr["TABELA"].ToString(), dr["DESCRICAO"].ToString());
                //cmbAtivos.Items.Insert(Convert.ToInt32(dr["ID"]), dr["DESCRICAO"].ToString());
            }
            
            cmbAtivos.DataSource = new BindingSource(ativos, null);
            cmbAtivos.DisplayMember = "Value";
            cmbAtivos.ValueMember = "Key";
        }

        private void btnCarregar_Click(object sender, EventArgs e)
        {
            try
            {
                //Filtro por Status do Processamento
                string statusProc = cmbStatusProc.SelectedIndex.ToString();
                statusProc = statusProc == "-1" ? "" : " AND FLAG_DIV = " + statusProc;

                //Filtro por Data
                string data = string.Format(" AND DT LIKE '{0}'", dtpData.Value.ToShortDateString());

                //Filtro por Ativo
                string ativo = cmbAtivos.Text;
                string tabela = cmbAtivos.SelectedValue.ToString();
                ativo = string.IsNullOrEmpty(ativo) ? "" : string.Format(" AND AF.DESCRICAO = '{0}'", ativo);

                string query = string.Format("SELECT M.*,AI.TP_ARQ " +
                               "FROM (([{3}] M INNER JOIN [ARQUIVO_IMP] AI ON M.ID_ARQ = AI.ID_ARQ) " +
                               "               INNER JOIN [ARQUIVO_FISICO] AF ON AF.SAC = AI.NM_ARQ OR AF.DI8 = AI.NM_ARQ) " +
                               "WHERE 1=1 " +
                               "{0} {1} {2} " + 
                               "ORDER BY CLCLI_CD,QT,TP_ARQ", statusProc, data, ativo, tabela);

                DataTable dt = new DataTable();

                dt = dao.retorna_datatable(query, "cnn_proc");

                this.dgvRelatorio.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;

                this.dgvRelatorio.DataSource = dt;
                this.dgvRelatorio.Columns["ID"].Visible = false;
                this.dgvRelatorio.Columns["ID_ARQ"].Visible = false;
                this.dgvRelatorio.Columns["FLAG_DIV"].Visible = false;

                lblTotalCount.Text = "Total de " + dt.Rows.Count.ToString() + " registros";

                btnExportar.Enabled = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            SaveFileDialog salvar = new SaveFileDialog(); // novo

            Excel.Application App; // Aplicação Excel
            Excel.Workbook WorkBook; // Pasta
            Excel.Worksheet WorkSheet; // Planilha
            object misValue = System.Reflection.Missing.Value;

            App = new Excel.Application();
            App.DisplayAlerts = false;
            WorkBook = App.Workbooks.Add(misValue);
            WorkSheet = (Excel.Worksheet)WorkBook.Worksheets.get_Item(1);
            
            pbRelatorios.Visible = true;
            pbRelatorios.Value = 0;
            pbRelatorios.Maximum = dgvRelatorio.Rows.Count;
            
            // passa as colunas do DataGridView para a Pasta do Excel
            WorkSheet.Application.Workbooks.Add(Type.Missing);
            int pos = 1;
            for (int i = 1; i < dgvRelatorio.Columns.Count + 1; i++)
            {
                if (dgvRelatorio.Columns[i - 1].Name != "ID" && dgvRelatorio.Columns[i - 1].Name != "ID_ARQ" && dgvRelatorio.Columns[i - 1].Name != "FLAG_DIV")
                {
                    WorkSheet.Cells[1, pos] = dgvRelatorio.Columns[i - 1].HeaderText;
                    WorkSheet.Cells[1, pos].Font.Bold = true;
                    WorkSheet.Cells[1, pos].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    WorkSheet.Cells[1, pos].Interior.Color = ColorTranslator.ToWin32(Color.LightGray);
                    pos++;
                }
            }

            CultureInfo cultura = new CultureInfo("pt-br");

            double x;
            DateTime y;
            // passa as celulas do DataGridView para a Pasta do Excel
            for (int i = 0; i < dgvRelatorio.Rows.Count; i++)
            {
                pos = 0;
                for (int j = 0; j < dgvRelatorio.Columns.Count; j++)
                {
                    if (dgvRelatorio.Columns[j].Name != "ID" && dgvRelatorio.Columns[j].Name != "ID_ARQ" && dgvRelatorio.Columns[j].Name != "FLAG_DIV")
                    {
                        bool EhNumerico = Double.TryParse(dgvRelatorio.Rows[i].Cells[j].Value.ToString(), NumberStyles.Any, cultura, out x);
                        bool EhData = DateTime.TryParse(dgvRelatorio.Rows[i].Cells[j].Value.ToString(), out y);
                        if (EhNumerico)
                        {
                            WorkSheet.Cells[i + 2, pos + 1] = x;
                        }
                        else if (EhData)
                        {
                            WorkSheet.Cells[i + 2, pos + 1] = y.ToShortDateString();
                        }
                        else
                        {
                            WorkSheet.Cells[i + 2, pos + 1] = dgvRelatorio.Rows[i].Cells[j].Value.ToString();
                        }

                        WorkSheet.Cells[i + 2, pos + 1].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                        pos++;
                    }
                }
                pbRelatorios.Value++;
            }
            //Ajusta as colunas
            WorkSheet.Columns.AutoFit();

            // define algumas propriedades da caixa salvar
            salvar.Title = "Exportar para Excel";
            salvar.Filter = "Arquivo do Excel *.xls | *.xls";
            salvar.ShowDialog(); // mostra

            // salva o arquivo
            WorkBook.SaveAs(salvar.FileName, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue,
            Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            WorkBook.Close(true, misValue, misValue);
            App.Quit(); // encerra o excel
            pbRelatorios.Value = 0;
            MessageBox.Show("Exportado com sucesso!","Exportação Excel",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

    }
}
