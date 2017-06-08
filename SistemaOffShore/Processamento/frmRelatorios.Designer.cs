namespace SistemaOffShore.Processamento
{
    partial class frmRelatorios
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvRelatorio = new System.Windows.Forms.DataGridView();
            this.btnCarregar = new System.Windows.Forms.Button();
            this.cmbStatusProc = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnExportar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.pbRelatorios = new System.Windows.Forms.ProgressBar();
            this.dtpData = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbAtivos = new System.Windows.Forms.ComboBox();
            this.lblTotalCount = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRelatorio)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvRelatorio
            // 
            this.dgvRelatorio.AllowUserToAddRows = false;
            this.dgvRelatorio.AllowUserToDeleteRows = false;
            this.dgvRelatorio.AllowUserToOrderColumns = true;
            this.dgvRelatorio.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRelatorio.Location = new System.Drawing.Point(-1, 47);
            this.dgvRelatorio.Name = "dgvRelatorio";
            this.dgvRelatorio.ReadOnly = true;
            this.dgvRelatorio.Size = new System.Drawing.Size(763, 351);
            this.dgvRelatorio.TabIndex = 0;
            // 
            // btnCarregar
            // 
            this.btnCarregar.Location = new System.Drawing.Point(523, 5);
            this.btnCarregar.Name = "btnCarregar";
            this.btnCarregar.Size = new System.Drawing.Size(110, 36);
            this.btnCarregar.TabIndex = 1;
            this.btnCarregar.Text = "&Carregar";
            this.btnCarregar.UseVisualStyleBackColor = true;
            this.btnCarregar.Click += new System.EventHandler(this.btnCarregar_Click);
            // 
            // cmbStatusProc
            // 
            this.cmbStatusProc.FormattingEnabled = true;
            this.cmbStatusProc.Location = new System.Drawing.Point(7, 20);
            this.cmbStatusProc.Name = "cmbStatusProc";
            this.cmbStatusProc.Size = new System.Drawing.Size(143, 21);
            this.cmbStatusProc.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Status Processamento";
            // 
            // btnExportar
            // 
            this.btnExportar.Location = new System.Drawing.Point(639, 5);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(110, 36);
            this.btnExportar.TabIndex = 4;
            this.btnExportar.Text = "&Exportar";
            this.btnExportar.UseVisualStyleBackColor = true;
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(405, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Data";
            // 
            // pbRelatorios
            // 
            this.pbRelatorios.Location = new System.Drawing.Point(564, 401);
            this.pbRelatorios.Name = "pbRelatorios";
            this.pbRelatorios.Size = new System.Drawing.Size(193, 23);
            this.pbRelatorios.TabIndex = 6;
            // 
            // dtpData
            // 
            this.dtpData.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpData.Location = new System.Drawing.Point(408, 20);
            this.dtpData.Name = "dtpData";
            this.dtpData.Size = new System.Drawing.Size(92, 20);
            this.dtpData.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(165, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Ativo";
            // 
            // cmbAtivos
            // 
            this.cmbAtivos.FormattingEnabled = true;
            this.cmbAtivos.Location = new System.Drawing.Point(168, 20);
            this.cmbAtivos.Name = "cmbAtivos";
            this.cmbAtivos.Size = new System.Drawing.Size(218, 21);
            this.cmbAtivos.TabIndex = 9;
            // 
            // lblTotalCount
            // 
            this.lblTotalCount.AutoSize = true;
            this.lblTotalCount.Location = new System.Drawing.Point(13, 405);
            this.lblTotalCount.Name = "lblTotalCount";
            this.lblTotalCount.Size = new System.Drawing.Size(0, 13);
            this.lblTotalCount.TabIndex = 10;
            // 
            // frmRelatorios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(761, 428);
            this.Controls.Add(this.lblTotalCount);
            this.Controls.Add(this.cmbAtivos);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtpData);
            this.Controls.Add(this.pbRelatorios);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnExportar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbStatusProc);
            this.Controls.Add(this.btnCarregar);
            this.Controls.Add(this.dgvRelatorio);
            this.Name = "frmRelatorios";
            this.Text = "frmRelatorios";
            this.Load += new System.EventHandler(this.frmRelatorios_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRelatorio)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvRelatorio;
        private System.Windows.Forms.Button btnCarregar;
        private System.Windows.Forms.ComboBox cmbStatusProc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnExportar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar pbRelatorios;
        private System.Windows.Forms.DateTimePicker dtpData;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbAtivos;
        private System.Windows.Forms.Label lblTotalCount;
    }
}