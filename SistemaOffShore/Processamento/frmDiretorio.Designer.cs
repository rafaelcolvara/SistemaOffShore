namespace SistemaOffShore.Processamento
{
    partial class frmDiretorio
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDiretorio));
            this.label1 = new System.Windows.Forms.Label();
            this.txtSAC = new System.Windows.Forms.TextBox();
            this.btnLocalARQSAC = new System.Windows.Forms.Button();
            this.btnLocalARQDI8 = new System.Windows.Forms.Button();
            this.txtDI8 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.btnFechar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Diretório SAC";
            // 
            // txtSAC
            // 
            this.txtSAC.Location = new System.Drawing.Point(15, 37);
            this.txtSAC.Multiline = true;
            this.txtSAC.Name = "txtSAC";
            this.txtSAC.Size = new System.Drawing.Size(403, 32);
            this.txtSAC.TabIndex = 1;
            // 
            // btnLocalARQSAC
            // 
            this.btnLocalARQSAC.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLocalARQSAC.Image = ((System.Drawing.Image)(resources.GetObject("btnLocalARQSAC.Image")));
            this.btnLocalARQSAC.Location = new System.Drawing.Point(424, 37);
            this.btnLocalARQSAC.Name = "btnLocalARQSAC";
            this.btnLocalARQSAC.Size = new System.Drawing.Size(38, 32);
            this.btnLocalARQSAC.TabIndex = 2;
            this.btnLocalARQSAC.UseVisualStyleBackColor = true;
            this.btnLocalARQSAC.Click += new System.EventHandler(this.btnLocalARQSAC_Click);
            // 
            // btnLocalARQDI8
            // 
            this.btnLocalARQDI8.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLocalARQDI8.Image = ((System.Drawing.Image)(resources.GetObject("btnLocalARQDI8.Image")));
            this.btnLocalARQDI8.Location = new System.Drawing.Point(424, 98);
            this.btnLocalARQDI8.Name = "btnLocalARQDI8";
            this.btnLocalARQDI8.Size = new System.Drawing.Size(38, 32);
            this.btnLocalARQDI8.TabIndex = 5;
            this.btnLocalARQDI8.UseVisualStyleBackColor = true;
            this.btnLocalARQDI8.Click += new System.EventHandler(this.btnLocalARQDI8_Click);
            // 
            // txtDI8
            // 
            this.txtDI8.Location = new System.Drawing.Point(15, 98);
            this.txtDI8.Multiline = true;
            this.txtDI8.Name = "txtDI8";
            this.txtDI8.Size = new System.Drawing.Size(403, 32);
            this.txtDI8.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Diretório DI8";
            // 
            // btnSalvar
            // 
            this.btnSalvar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalvar.Location = new System.Drawing.Point(306, 151);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(75, 23);
            this.btnSalvar.TabIndex = 6;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // btnFechar
            // 
            this.btnFechar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFechar.Location = new System.Drawing.Point(387, 151);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(75, 23);
            this.btnFechar.TabIndex = 7;
            this.btnFechar.Text = "Fechar";
            this.btnFechar.UseVisualStyleBackColor = true;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // frmDiretorio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(479, 186);
            this.Controls.Add(this.btnFechar);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.btnLocalARQDI8);
            this.Controls.Add(this.txtDI8);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnLocalARQSAC);
            this.Controls.Add(this.txtSAC);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDiretorio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuração de Diretório";
            this.Load += new System.EventHandler(this.frmDiretorio_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSAC;
        private System.Windows.Forms.Button btnLocalARQSAC;
        private System.Windows.Forms.Button btnLocalARQDI8;
        private System.Windows.Forms.TextBox txtDI8;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button btnFechar;
    }
}