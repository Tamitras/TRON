namespace Tron
{
    partial class TronForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.listBoxProcess = new System.Windows.Forms.ListBox();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxCurrentProcess = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnStartConnect = new System.Windows.Forms.Button();
            this.pictureBoxFoundImage = new System.Windows.Forms.PictureBox();
            this.btnRightClick = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFoundImage)).BeginInit();
            this.SuspendLayout();
            // 
            // listBoxProcess
            // 
            this.listBoxProcess.DataSource = this.bindingSource1;
            this.listBoxProcess.FormattingEnabled = true;
            this.listBoxProcess.Location = new System.Drawing.Point(12, 34);
            this.listBoxProcess.Name = "listBoxProcess";
            this.listBoxProcess.Size = new System.Drawing.Size(186, 394);
            this.listBoxProcess.TabIndex = 0;
            this.listBoxProcess.DoubleClick += new System.EventHandler(this.listBoxProcess_DoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Alle Prozesse";
            // 
            // textBoxCurrentProcess
            // 
            this.textBoxCurrentProcess.Location = new System.Drawing.Point(204, 34);
            this.textBoxCurrentProcess.Multiline = true;
            this.textBoxCurrentProcess.Name = "textBoxCurrentProcess";
            this.textBoxCurrentProcess.Size = new System.Drawing.Size(231, 252);
            this.textBoxCurrentProcess.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(331, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Ausgewählter Prozess";
            // 
            // btnStartConnect
            // 
            this.btnStartConnect.Location = new System.Drawing.Point(204, 292);
            this.btnStartConnect.Name = "btnStartConnect";
            this.btnStartConnect.Size = new System.Drawing.Size(230, 23);
            this.btnStartConnect.TabIndex = 4;
            this.btnStartConnect.Text = "Connect To Process";
            this.btnStartConnect.UseVisualStyleBackColor = true;
            this.btnStartConnect.Click += new System.EventHandler(this.btnStartConnect_Click);
            // 
            // pictureBoxFoundImage
            // 
            this.pictureBoxFoundImage.Location = new System.Drawing.Point(440, 34);
            this.pictureBoxFoundImage.Name = "pictureBoxFoundImage";
            this.pictureBoxFoundImage.Size = new System.Drawing.Size(676, 546);
            this.pictureBoxFoundImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxFoundImage.TabIndex = 8;
            this.pictureBoxFoundImage.TabStop = false;
            // 
            // btnRightClick
            // 
            this.btnRightClick.Location = new System.Drawing.Point(204, 405);
            this.btnRightClick.Name = "btnRightClick";
            this.btnRightClick.Size = new System.Drawing.Size(230, 23);
            this.btnRightClick.TabIndex = 9;
            this.btnRightClick.Text = "Perform Right Click";
            this.btnRightClick.UseVisualStyleBackColor = true;
            this.btnRightClick.Click += new System.EventHandler(this.btnRightClick_Click);
            // 
            // TronForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1267, 592);
            this.Controls.Add(this.btnRightClick);
            this.Controls.Add(this.pictureBoxFoundImage);
            this.Controls.Add(this.btnStartConnect);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxCurrentProcess);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBoxProcess);
            this.Name = "TronForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFoundImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxProcess;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxCurrentProcess;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnStartConnect;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.PictureBox pictureBoxFoundImage;
        private System.Windows.Forms.Button btnRightClick;
    }
}

