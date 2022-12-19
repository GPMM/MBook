namespace MBook
{
    partial class FInterfaceSerial
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FInterfaceSerial));
            this.btnConnect = new System.Windows.Forms.Button();
            this.cbPort1 = new System.Windows.Forms.ComboBox();
            this.lblPort = new System.Windows.Forms.Label();
            this.cbBaudRate = new System.Windows.Forms.ComboBox();
            this.cbDataBits = new System.Windows.Forms.ComboBox();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.tmrCom1 = new System.Windows.Forms.Timer(this.components);
            this.lblBaudRate = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblDataBit = new System.Windows.Forms.Label();
            this.cbRtsEnable = new System.Windows.Forms.ComboBox();
            this.lblRstEnable = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSmell = new System.Windows.Forms.Button();
            this.btnFan2 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(236, 37);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(105, 23);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "CONNECT";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.cmdConnect_Click);
            // 
            // cbPort1
            // 
            this.cbPort1.FormattingEnabled = true;
            this.cbPort1.Location = new System.Drawing.Point(282, 12);
            this.cbPort1.Name = "cbPort1";
            this.cbPort1.Size = new System.Drawing.Size(59, 21);
            this.cbPort1.TabIndex = 4;
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(236, 15);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(40, 13);
            this.lblPort.TabIndex = 5;
            this.lblPort.Text = "PORT:";
            // 
            // cbBaudRate
            // 
            this.cbBaudRate.FormattingEnabled = true;
            this.cbBaudRate.Items.AddRange(new object[] {
            "9600",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.cbBaudRate.Location = new System.Drawing.Point(104, 12);
            this.cbBaudRate.Name = "cbBaudRate";
            this.cbBaudRate.Size = new System.Drawing.Size(86, 21);
            this.cbBaudRate.TabIndex = 7;
            // 
            // cbDataBits
            // 
            this.cbDataBits.FormattingEnabled = true;
            this.cbDataBits.Items.AddRange(new object[] {
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.cbDataBits.Location = new System.Drawing.Point(104, 39);
            this.cbDataBits.Name = "cbDataBits";
            this.cbDataBits.Size = new System.Drawing.Size(86, 21);
            this.cbDataBits.TabIndex = 10;
            // 
            // tmrCom1
            // 
            this.tmrCom1.Tick += new System.EventHandler(this.tmrCom1_Tick);
            // 
            // lblBaudRate
            // 
            this.lblBaudRate.AutoSize = true;
            this.lblBaudRate.Location = new System.Drawing.Point(23, 15);
            this.lblBaudRate.Name = "lblBaudRate";
            this.lblBaudRate.Size = new System.Drawing.Size(75, 13);
            this.lblBaudRate.TabIndex = 12;
            this.lblBaudRate.Text = "BAUDRATE  :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(322, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 13;
            // 
            // lblDataBit
            // 
            this.lblDataBit.AutoSize = true;
            this.lblDataBit.Location = new System.Drawing.Point(36, 42);
            this.lblDataBit.Name = "lblDataBit";
            this.lblDataBit.Size = new System.Drawing.Size(62, 13);
            this.lblDataBit.TabIndex = 16;
            this.lblDataBit.Text = "DATA BIT :";
            // 
            // cbRtsEnable
            // 
            this.cbRtsEnable.FormattingEnabled = true;
            this.cbRtsEnable.Items.AddRange(new object[] {
            "False",
            "True"});
            this.cbRtsEnable.Location = new System.Drawing.Point(104, 66);
            this.cbRtsEnable.Name = "cbRtsEnable";
            this.cbRtsEnable.Size = new System.Drawing.Size(86, 21);
            this.cbRtsEnable.TabIndex = 18;
            // 
            // lblRstEnable
            // 
            this.lblRstEnable.AutoSize = true;
            this.lblRstEnable.Location = new System.Drawing.Point(18, 69);
            this.lblRstEnable.Name = "lblRstEnable";
            this.lblRstEnable.Size = new System.Drawing.Size(80, 13);
            this.lblRstEnable.TabIndex = 19;
            this.lblRstEnable.Text = "RTS ENABLE :";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(266, 176);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 30;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSmell);
            this.groupBox1.Controls.Add(this.btnFan2);
            this.groupBox1.Location = new System.Drawing.Point(12, 93);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(248, 106);
            this.groupBox1.TabIndex = 31;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // btnSmell
            // 
            this.btnSmell.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSmell.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSmell.BackgroundImage")));
            this.btnSmell.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSmell.Location = new System.Drawing.Point(140, 25);
            this.btnSmell.Name = "btnSmell";
            this.btnSmell.Size = new System.Drawing.Size(75, 75);
            this.btnSmell.TabIndex = 6;
            this.btnSmell.UseVisualStyleBackColor = true;
            this.btnSmell.Click += new System.EventHandler(this.btnSmell_Click);
            // 
            // btnFan2
            // 
            this.btnFan2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFan2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnFan2.BackgroundImage")));
            this.btnFan2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnFan2.Location = new System.Drawing.Point(27, 25);
            this.btnFan2.Name = "btnFan2";
            this.btnFan2.Size = new System.Drawing.Size(75, 75);
            this.btnFan2.TabIndex = 5;
            this.btnFan2.UseVisualStyleBackColor = true;
            this.btnFan2.Click += new System.EventHandler(this.btnFan2_Click);
            // 
            // FInterfaceSerial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(355, 211);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblRstEnable);
            this.Controls.Add(this.cbRtsEnable);
            this.Controls.Add(this.lblDataBit);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblBaudRate);
            this.Controls.Add(this.cbDataBits);
            this.Controls.Add(this.cbBaudRate);
            this.Controls.Add(this.lblPort);
            this.Controls.Add(this.cbPort1);
            this.Controls.Add(this.btnConnect);
            this.Name = "FInterfaceSerial";
            this.ShowIcon = false;
            this.Text = "SERIAL INTERFACE";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.ComboBox cbPort1;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.ComboBox cbBaudRate;
        private System.Windows.Forms.ComboBox cbDataBits;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Timer tmrCom1;
        private System.Windows.Forms.Label lblBaudRate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblDataBit;
        private System.Windows.Forms.ComboBox cbRtsEnable;
        private System.Windows.Forms.Label lblRstEnable;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSmell;
        private System.Windows.Forms.Button btnFan2;
    }
}

