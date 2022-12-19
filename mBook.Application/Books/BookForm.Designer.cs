namespace MBook.Books
{
    partial class BookForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BookForm));
            this.principal_splitContainer = new System.Windows.Forms.SplitContainer();
            this.lbTitle = new System.Windows.Forms.Label();
            this.pictureMBook = new System.Windows.Forms.PictureBox();
            this.btExit = new System.Windows.Forms.Button();
            this.secundario_splitContainer = new System.Windows.Forms.SplitContainer();
            this.richTextBox1 = new MBook.Custom.RichTextBoxCustom();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btNext = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btBack = new System.Windows.Forms.Button();
            this.lbPage = new System.Windows.Forms.Label();
            this.behaviorMap1 = new EyeXFramework.Forms.BehaviorMap(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.principal_splitContainer)).BeginInit();
            this.principal_splitContainer.Panel1.SuspendLayout();
            this.principal_splitContainer.Panel2.SuspendLayout();
            this.principal_splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureMBook)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.secundario_splitContainer)).BeginInit();
            this.secundario_splitContainer.Panel1.SuspendLayout();
            this.secundario_splitContainer.Panel2.SuspendLayout();
            this.secundario_splitContainer.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // principal_splitContainer
            // 
            this.principal_splitContainer.BackColor = System.Drawing.Color.Transparent;
            this.principal_splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.principal_splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.principal_splitContainer.Location = new System.Drawing.Point(0, 0);
            this.principal_splitContainer.Name = "principal_splitContainer";
            this.principal_splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // principal_splitContainer.Panel1
            // 
            this.principal_splitContainer.Panel1.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.principal_splitContainer.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.principal_splitContainer.Panel1.Controls.Add(this.lbTitle);
            this.principal_splitContainer.Panel1.Controls.Add(this.pictureMBook);
            this.principal_splitContainer.Panel1.Controls.Add(this.btExit);
            // 
            // principal_splitContainer.Panel2
            // 
            this.principal_splitContainer.Panel2.Controls.Add(this.secundario_splitContainer);
            this.principal_splitContainer.Size = new System.Drawing.Size(850, 500);
            this.principal_splitContainer.SplitterDistance = 53;
            this.principal_splitContainer.TabIndex = 0;
            // 
            // lbTitle
            // 
            this.lbTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbTitle.AutoSize = true;
            this.lbTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTitle.Location = new System.Drawing.Point(385, 18);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(100, 20);
            this.lbTitle.TabIndex = 18;
            this.lbTitle.Text = "Titulo do livro";
            this.lbTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureMBook
            // 
            this.pictureMBook.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureMBook.BackgroundImage")));
            this.pictureMBook.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureMBook.Location = new System.Drawing.Point(12, 11);
            this.pictureMBook.Name = "pictureMBook";
            this.pictureMBook.Size = new System.Drawing.Size(135, 28);
            this.pictureMBook.TabIndex = 17;
            this.pictureMBook.TabStop = false;
            // 
            // btExit
            // 
            this.btExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btExit.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.btExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btExit.BackgroundImage")));
            this.btExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btExit.FlatAppearance.BorderSize = 0;
            this.btExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btExit.ForeColor = System.Drawing.Color.Transparent;
            this.btExit.Location = new System.Drawing.Point(812, 11);
            this.btExit.Margin = new System.Windows.Forms.Padding(2);
            this.btExit.Name = "btExit";
            this.btExit.Size = new System.Drawing.Size(30, 30);
            this.btExit.TabIndex = 16;
            this.btExit.UseVisualStyleBackColor = false;
            this.btExit.Click += new System.EventHandler(this.btExit_Click);
            // 
            // secundario_splitContainer
            // 
            this.secundario_splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.secundario_splitContainer.Location = new System.Drawing.Point(0, 0);
            this.secundario_splitContainer.Name = "secundario_splitContainer";
            this.secundario_splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // secundario_splitContainer.Panel1
            // 
            this.secundario_splitContainer.Panel1.BackColor = System.Drawing.Color.Transparent;
            this.secundario_splitContainer.Panel1.BackgroundImage = global::MBook.Properties.Resources.cover;
            this.secundario_splitContainer.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.secundario_splitContainer.Panel1.Controls.Add(this.richTextBox1);
            this.secundario_splitContainer.Panel1.Controls.Add(this.panel2);
            this.secundario_splitContainer.Panel1.Controls.Add(this.panel1);
            // 
            // secundario_splitContainer.Panel2
            // 
            this.secundario_splitContainer.Panel2.BackColor = System.Drawing.SystemColors.Window;
            this.secundario_splitContainer.Panel2.Controls.Add(this.lbPage);
            this.secundario_splitContainer.Size = new System.Drawing.Size(850, 443);
            this.secundario_splitContainer.SplitterDistance = 396;
            this.secundario_splitContainer.TabIndex = 0;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Font = new System.Drawing.Font("Consolas", 18F);
            this.richTextBox1.Location = new System.Drawing.Point(88, 10);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(670, 375);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.btNext);
            this.panel2.Location = new System.Drawing.Point(777, 6);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(68, 384);
            this.panel2.TabIndex = 1;
            // 
            // btNext
            // 
            this.btNext.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btNext.BackgroundImage = global::MBook.Properties.Resources.setadireita;
            this.btNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btNext.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btNext.FlatAppearance.BorderColor = System.Drawing.SystemColors.Window;
            this.btNext.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Window;
            this.btNext.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Window;
            this.btNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btNext.Location = new System.Drawing.Point(6, 58);
            this.btNext.Name = "btNext";
            this.btNext.Size = new System.Drawing.Size(59, 262);
            this.btNext.TabIndex = 1;
            this.btNext.UseVisualStyleBackColor = true;
            this.btNext.MouseEnter += new System.EventHandler(this.btNext_MouseEnter);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.btBack);
            this.panel1.Location = new System.Drawing.Point(4, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(68, 384);
            this.panel1.TabIndex = 0;
            // 
            // btBack
            // 
            this.btBack.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btBack.BackgroundImage = global::MBook.Properties.Resources.setaesquerda;
            this.btBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btBack.FlatAppearance.BorderColor = System.Drawing.SystemColors.Window;
            this.btBack.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Window;
            this.btBack.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Window;
            this.btBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btBack.Location = new System.Drawing.Point(5, 61);
            this.btBack.Name = "btBack";
            this.btBack.Size = new System.Drawing.Size(59, 256);
            this.btBack.TabIndex = 0;
            this.btBack.UseVisualStyleBackColor = true;
            this.btBack.Click += new System.EventHandler(this.btBack_Click);
            this.btBack.MouseEnter += new System.EventHandler(this.btBack_MouseEnter);
            // 
            // lbPage
            // 
            this.lbPage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbPage.Location = new System.Drawing.Point(385, 13);
            this.lbPage.Name = "lbPage";
            this.lbPage.Size = new System.Drawing.Size(81, 16);
            this.lbPage.TabIndex = 5;
            this.lbPage.Text = "Pagina";
            this.lbPage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BookForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(850, 500);
            this.Controls.Add(this.principal_splitContainer);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "BookForm";
            this.Text = "BookForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.BookForm_Load);
            this.principal_splitContainer.Panel1.ResumeLayout(false);
            this.principal_splitContainer.Panel1.PerformLayout();
            this.principal_splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.principal_splitContainer)).EndInit();
            this.principal_splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureMBook)).EndInit();
            this.secundario_splitContainer.Panel1.ResumeLayout(false);
            this.secundario_splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.secundario_splitContainer)).EndInit();
            this.secundario_splitContainer.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer principal_splitContainer;
        private System.Windows.Forms.SplitContainer secundario_splitContainer;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btExit;
        private System.Windows.Forms.Button btBack;
        private System.Windows.Forms.Button btNext;
        private Custom.RichTextBoxCustom richTextBox1;
        private System.Windows.Forms.PictureBox pictureMBook;
        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.Label lbPage;
        private EyeXFramework.Forms.BehaviorMap behaviorMap1;
    }
}