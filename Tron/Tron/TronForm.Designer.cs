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
            this.textBoxLog = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnStartConnect = new System.Windows.Forms.Button();
            this.pictureBoxLiveImage = new System.Windows.Forms.PictureBox();
            this.btnRightClick = new System.Windows.Forms.Button();
            this.labelMousePosX = new System.Windows.Forms.Label();
            this.labelMousePosY = new System.Windows.Forms.Label();
            this.lblCurrentCoeff = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblFoundX = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblFoundY = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblCalcedPosY = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblCalcedPosX = new System.Windows.Forms.Label();
            this.pictureBoxFoundImage = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblCoeff = new System.Windows.Forms.Label();
            this.trackBarEmguType = new System.Windows.Forms.TrackBar();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnEsc = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLiveImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFoundImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarEmguType)).BeginInit();
            this.SuspendLayout();
            // 
            // listBoxProcess
            // 
            this.listBoxProcess.DataSource = this.bindingSource1;
            this.listBoxProcess.FormattingEnabled = true;
            this.listBoxProcess.Location = new System.Drawing.Point(12, 34);
            this.listBoxProcess.Name = "listBoxProcess";
            this.listBoxProcess.Size = new System.Drawing.Size(127, 147);
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
            // textBoxLog
            // 
            this.textBoxLog.Location = new System.Drawing.Point(145, 34);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.Size = new System.Drawing.Size(150, 147);
            this.textBoxLog.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(159, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Ausgewählter Prozess";
            // 
            // btnStartConnect
            // 
            this.btnStartConnect.Enabled = false;
            this.btnStartConnect.Location = new System.Drawing.Point(301, 34);
            this.btnStartConnect.Name = "btnStartConnect";
            this.btnStartConnect.Size = new System.Drawing.Size(150, 23);
            this.btnStartConnect.TabIndex = 4;
            this.btnStartConnect.Text = "Connect To Process";
            this.btnStartConnect.UseVisualStyleBackColor = true;
            this.btnStartConnect.Click += new System.EventHandler(this.BtnStartConnect_Click);
            // 
            // pictureBoxLiveImage
            // 
            this.pictureBoxLiveImage.Location = new System.Drawing.Point(12, 266);
            this.pictureBoxLiveImage.Name = "pictureBoxLiveImage";
            this.pictureBoxLiveImage.Size = new System.Drawing.Size(676, 546);
            this.pictureBoxLiveImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxLiveImage.TabIndex = 8;
            this.pictureBoxLiveImage.TabStop = false;
            // 
            // btnRightClick
            // 
            this.btnRightClick.Location = new System.Drawing.Point(301, 63);
            this.btnRightClick.Name = "btnRightClick";
            this.btnRightClick.Size = new System.Drawing.Size(150, 23);
            this.btnRightClick.TabIndex = 9;
            this.btnRightClick.Text = "Perform Right Click";
            this.btnRightClick.UseVisualStyleBackColor = true;
            this.btnRightClick.Click += new System.EventHandler(this.BtnRightClick_Click);
            // 
            // labelMousePosX
            // 
            this.labelMousePosX.AutoSize = true;
            this.labelMousePosX.Location = new System.Drawing.Point(469, 34);
            this.labelMousePosX.Name = "labelMousePosX";
            this.labelMousePosX.Size = new System.Drawing.Size(67, 13);
            this.labelMousePosX.TabIndex = 10;
            this.labelMousePosX.Text = "MousePosX:";
            // 
            // labelMousePosY
            // 
            this.labelMousePosY.AutoSize = true;
            this.labelMousePosY.Location = new System.Drawing.Point(469, 56);
            this.labelMousePosY.Name = "labelMousePosY";
            this.labelMousePosY.Size = new System.Drawing.Size(67, 13);
            this.labelMousePosY.TabIndex = 11;
            this.labelMousePosY.Text = "MousePosY:";
            // 
            // lblCurrentCoeff
            // 
            this.lblCurrentCoeff.AutoSize = true;
            this.lblCurrentCoeff.Location = new System.Drawing.Point(539, 100);
            this.lblCurrentCoeff.Name = "lblCurrentCoeff";
            this.lblCurrentCoeff.Size = new System.Drawing.Size(59, 13);
            this.lblCurrentCoeff.TabIndex = 12;
            this.lblCurrentCoeff.Text = "CoeffValue";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(467, 100);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "CurrentCoeff:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(274, 250);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Live Bot";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(467, 124);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(109, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "FoundTemplatePosX:";
            // 
            // lblFoundX
            // 
            this.lblFoundX.AutoSize = true;
            this.lblFoundX.Location = new System.Drawing.Point(582, 124);
            this.lblFoundX.Name = "lblFoundX";
            this.lblFoundX.Size = new System.Drawing.Size(41, 13);
            this.lblFoundX.TabIndex = 15;
            this.lblFoundX.Text = "XValue";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(467, 143);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(109, 13);
            this.label9.TabIndex = 18;
            this.label9.Text = "FoundTemplatePosY:";
            // 
            // lblFoundY
            // 
            this.lblFoundY.AutoSize = true;
            this.lblFoundY.Location = new System.Drawing.Point(582, 143);
            this.lblFoundY.Name = "lblFoundY";
            this.lblFoundY.Size = new System.Drawing.Size(41, 13);
            this.lblFoundY.TabIndex = 17;
            this.lblFoundY.Text = "YValue";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(467, 190);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "CalcedTemplatePosY:";
            // 
            // lblCalcedPosY
            // 
            this.lblCalcedPosY.AutoSize = true;
            this.lblCalcedPosY.Location = new System.Drawing.Point(582, 190);
            this.lblCalcedPosY.Name = "lblCalcedPosY";
            this.lblCalcedPosY.Size = new System.Drawing.Size(41, 13);
            this.lblCalcedPosY.TabIndex = 21;
            this.lblCalcedPosY.Text = "YValue";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(467, 171);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(112, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "CalcedTemplatePosX:";
            // 
            // lblCalcedPosX
            // 
            this.lblCalcedPosX.AutoSize = true;
            this.lblCalcedPosX.Location = new System.Drawing.Point(582, 171);
            this.lblCalcedPosX.Name = "lblCalcedPosX";
            this.lblCalcedPosX.Size = new System.Drawing.Size(41, 13);
            this.lblCalcedPosX.TabIndex = 19;
            this.lblCalcedPosX.Text = "XValue";
            // 
            // pictureBoxFoundImage
            // 
            this.pictureBoxFoundImage.Location = new System.Drawing.Point(694, 266);
            this.pictureBoxFoundImage.Name = "pictureBoxFoundImage";
            this.pictureBoxFoundImage.Size = new System.Drawing.Size(676, 546);
            this.pictureBoxFoundImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxFoundImage.TabIndex = 23;
            this.pictureBoxFoundImage.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1011, 250);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 24;
            this.label4.Text = "Found Template";
            // 
            // lblCoeff
            // 
            this.lblCoeff.AutoSize = true;
            this.lblCoeff.Location = new System.Drawing.Point(326, 250);
            this.lblCoeff.Name = "lblCoeff";
            this.lblCoeff.Size = new System.Drawing.Size(56, 13);
            this.lblCoeff.TabIndex = 25;
            this.lblCoeff.Text = "Coeffizient";
            // 
            // trackBarEmguType
            // 
            this.trackBarEmguType.Location = new System.Drawing.Point(12, 190);
            this.trackBarEmguType.Maximum = 6;
            this.trackBarEmguType.Name = "trackBarEmguType";
            this.trackBarEmguType.Size = new System.Drawing.Size(439, 45);
            this.trackBarEmguType.TabIndex = 26;
            this.trackBarEmguType.Scroll += new System.EventHandler(this.TrackBarEmguType_Scroll);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(778, 100);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(238, 23);
            this.btnReset.TabIndex = 27;
            this.btnReset.Text = "Klick Hilfe";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnEsc
            // 
            this.btnEsc.Location = new System.Drawing.Point(778, 76);
            this.btnEsc.Name = "btnEsc";
            this.btnEsc.Size = new System.Drawing.Size(238, 23);
            this.btnEsc.TabIndex = 28;
            this.btnEsc.Text = "Klick ESC";
            this.btnEsc.UseVisualStyleBackColor = true;
            this.btnEsc.Click += new System.EventHandler(this.btnEsc_Click);
            // 
            // TronForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1454, 822);
            this.Controls.Add(this.btnEsc);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.trackBarEmguType);
            this.Controls.Add(this.lblCoeff);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pictureBoxFoundImage);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblCalcedPosY);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lblCalcedPosX);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lblFoundY);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblFoundX);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblCurrentCoeff);
            this.Controls.Add(this.labelMousePosY);
            this.Controls.Add(this.labelMousePosX);
            this.Controls.Add(this.btnRightClick);
            this.Controls.Add(this.pictureBoxLiveImage);
            this.Controls.Add(this.btnStartConnect);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxLog);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBoxProcess);
            this.DoubleBuffered = true;
            this.Name = "TronForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLiveImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFoundImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarEmguType)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxProcess;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxLog;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnStartConnect;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.PictureBox pictureBoxLiveImage;
        private System.Windows.Forms.Button btnRightClick;
        private System.Windows.Forms.Label labelMousePosX;
        private System.Windows.Forms.Label labelMousePosY;
        private System.Windows.Forms.Label lblCurrentCoeff;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblFoundX;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblFoundY;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblCalcedPosY;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblCalcedPosX;
        private System.Windows.Forms.PictureBox pictureBoxFoundImage;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblCoeff;
        private System.Windows.Forms.TrackBar trackBarEmguType;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnEsc;
    }
}

