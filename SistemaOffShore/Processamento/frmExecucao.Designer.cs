namespace SistemaOffShore.Processamento
{
    partial class frmExecucao
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExecucao));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbtnIniciar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.lvArquivos = new System.Windows.Forms.ListView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tslblMsg = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsProgresso = new System.Windows.Forms.ToolStripProgressBar();
            this.chkTodos = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpData = new System.Windows.Forms.DateTimePicker();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnIniciar,
            this.toolStripSeparator3});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(995, 46);
            this.toolStrip1.TabIndex = 162;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbtnIniciar
            // 
            this.tsbtnIniciar.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnIniciar.Image")));
            this.tsbtnIniciar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnIniciar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnIniciar.Name = "tsbtnIniciar";
            this.tsbtnIniciar.Size = new System.Drawing.Size(127, 43);
            this.tsbtnIniciar.Text = "Iniciar Processamento";
            this.tsbtnIniciar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtnIniciar.Click += new System.EventHandler(this.tsbtnIniciar_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 46);
            // 
            // lvArquivos
            // 
            this.lvArquivos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvArquivos.Location = new System.Drawing.Point(5, 80);
            this.lvArquivos.Name = "lvArquivos";
            this.lvArquivos.Size = new System.Drawing.Size(984, 399);
            this.lvArquivos.TabIndex = 164;
            this.lvArquivos.UseCompatibleStateImageBehavior = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslblMsg,
            this.tsProgresso});
            this.statusStrip1.Location = new System.Drawing.Point(0, 484);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(995, 22);
            this.statusStrip1.TabIndex = 165;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tslblMsg
            // 
            this.tslblMsg.AutoSize = false;
            this.tslblMsg.ForeColor = System.Drawing.Color.Red;
            this.tslblMsg.Name = "tslblMsg";
            this.tslblMsg.Size = new System.Drawing.Size(750, 17);
            this.tslblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tsProgresso
            // 
            this.tsProgresso.Name = "tsProgresso";
            this.tsProgresso.Size = new System.Drawing.Size(230, 16);
            // 
            // chkTodos
            // 
            this.chkTodos.AutoSize = true;
            this.chkTodos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkTodos.Location = new System.Drawing.Point(12, 59);
            this.chkTodos.Name = "chkTodos";
            this.chkTodos.Size = new System.Drawing.Size(109, 17);
            this.chkTodos.TabIndex = 166;
            this.chkTodos.Text = "Selecionar Todos";
            this.chkTodos.UseVisualStyleBackColor = true;
            this.chkTodos.CheckedChanged += new System.EventHandler(this.chkTodos_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(787, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 167;
            this.label1.Text = "Data dos Arquivos:";
            // 
            // dtpData
            // 
            this.dtpData.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpData.Location = new System.Drawing.Point(890, 54);
            this.dtpData.Name = "dtpData";
            this.dtpData.Size = new System.Drawing.Size(99, 20);
            this.dtpData.TabIndex = 168;
            // 
            // frmExecucao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(995, 506);
            this.Controls.Add(this.dtpData);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkTodos);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.lvArquivos);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmExecucao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Arquivos (Importação e Batimento)";
            this.Load += new System.EventHandler(this.frmExecucao_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbtnIniciar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ListView lvArquivos;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar tsProgresso;
        private System.Windows.Forms.CheckBox chkTodos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpData;
        private System.Windows.Forms.ToolStripStatusLabel tslblMsg;
    }
}