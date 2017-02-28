namespace ConsoleTestDES
{
    partial class DES
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
            this.textKey = new System.Windows.Forms.TextBox();
            this.labelKey = new System.Windows.Forms.Label();
            this.textMsg = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileReset = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileClose = new System.Windows.Forms.ToolStripMenuItem();
            this.menuType = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTypeTripleSingle = new System.Windows.Forms.ToolStripMenuItem();
            this.textEncDec = new System.Windows.Forms.TextBox();
            this.labelMsg = new System.Windows.Forms.Label();
            this.labelEncDec = new System.Windows.Forms.Label();
            this.btEncDec = new System.Windows.Forms.Button();
            this.errorKey = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorMsg = new System.Windows.Forms.ErrorProvider(this.components);
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorKey)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorMsg)).BeginInit();
            this.SuspendLayout();
            // 
            // textKey
            // 
            this.textKey.Location = new System.Drawing.Point(50, 40);
            this.textKey.Name = "textKey";
            this.textKey.Size = new System.Drawing.Size(461, 20);
            this.textKey.TabIndex = 2;
            this.textKey.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textKey.Enter += new System.EventHandler(this.textKey_Enter);
            this.textKey.Leave += new System.EventHandler(this.textKey_Leave);
            // 
            // labelKey
            // 
            this.labelKey.AutoSize = true;
            this.labelKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelKey.Location = new System.Drawing.Point(15, 43);
            this.labelKey.Name = "labelKey";
            this.labelKey.Size = new System.Drawing.Size(31, 13);
            this.labelKey.TabIndex = 1;
            this.labelKey.Text = "key:";
            // 
            // textMsg
            // 
            this.textMsg.HideSelection = false;
            this.textMsg.Location = new System.Drawing.Point(50, 66);
            this.textMsg.Multiline = true;
            this.textMsg.Name = "textMsg";
            this.textMsg.Size = new System.Drawing.Size(461, 61);
            this.textMsg.TabIndex = 3;
            this.textMsg.Enter += new System.EventHandler(this.textMsg_Enter);
            this.textMsg.Leave += new System.EventHandler(this.textMsg_Leave);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.menuType});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(551, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFileReset,
            this.menuFileClose});
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new System.Drawing.Size(37, 20);
            this.menuFile.Text = "File";
            // 
            // menuFileReset
            // 
            this.menuFileReset.Name = "menuFileReset";
            this.menuFileReset.Size = new System.Drawing.Size(103, 22);
            this.menuFileReset.Text = "Reset";
            this.menuFileReset.Click += new System.EventHandler(this.menuFileReset_Click);
            // 
            // menuFileClose
            // 
            this.menuFileClose.Name = "menuFileClose";
            this.menuFileClose.Size = new System.Drawing.Size(103, 22);
            this.menuFileClose.Text = "Close";
            this.menuFileClose.Click += new System.EventHandler(this.menuFileClose_Click);
            // 
            // menuType
            // 
            this.menuType.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuTypeTripleSingle});
            this.menuType.Name = "menuType";
            this.menuType.Size = new System.Drawing.Size(45, 20);
            this.menuType.Text = "Type";
            // 
            // menuTypeTripleSingle
            // 
            this.menuTypeTripleSingle.Name = "menuTypeTripleSingle";
            this.menuTypeTripleSingle.Size = new System.Drawing.Size(104, 22);
            this.menuTypeTripleSingle.Text = "Triple";
            this.menuTypeTripleSingle.Click += new System.EventHandler(this.menuTypeTripleSingle_Click);
            // 
            // textEncDec
            // 
            this.textEncDec.HideSelection = false;
            this.textEncDec.Location = new System.Drawing.Point(50, 142);
            this.textEncDec.Multiline = true;
            this.textEncDec.Name = "textEncDec";
            this.textEncDec.ReadOnly = true;
            this.textEncDec.Size = new System.Drawing.Size(461, 61);
            this.textEncDec.TabIndex = 0;
            // 
            // labelMsg
            // 
            this.labelMsg.AutoSize = true;
            this.labelMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMsg.Location = new System.Drawing.Point(15, 88);
            this.labelMsg.Name = "labelMsg";
            this.labelMsg.Size = new System.Drawing.Size(33, 13);
            this.labelMsg.TabIndex = 4;
            this.labelMsg.Text = "msg:";
            // 
            // labelEncDec
            // 
            this.labelEncDec.AutoSize = true;
            this.labelEncDec.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelEncDec.Location = new System.Drawing.Point(16, 163);
            this.labelEncDec.Name = "labelEncDec";
            this.labelEncDec.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labelEncDec.Size = new System.Drawing.Size(32, 13);
            this.labelEncDec.TabIndex = 5;
            this.labelEncDec.Text = "enc:";
            // 
            // btEncDec
            // 
            this.btEncDec.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btEncDec.Location = new System.Drawing.Point(87, 223);
            this.btEncDec.Name = "btEncDec";
            this.btEncDec.Size = new System.Drawing.Size(389, 41);
            this.btEncDec.TabIndex = 4;
            this.btEncDec.Text = "Encrypt";
            this.btEncDec.UseVisualStyleBackColor = true;
            this.btEncDec.Click += new System.EventHandler(this.btEncDec_Click);
            // 
            // errorKey
            // 
            this.errorKey.ContainerControl = this;
            // 
            // errorMsg
            // 
            this.errorMsg.ContainerControl = this;
            // 
            // DES
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(551, 287);
            this.Controls.Add(this.btEncDec);
            this.Controls.Add(this.labelEncDec);
            this.Controls.Add(this.labelMsg);
            this.Controls.Add(this.textEncDec);
            this.Controls.Add(this.textMsg);
            this.Controls.Add(this.labelKey);
            this.Controls.Add(this.textKey);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "DES";
            this.Text = "DES";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorKey)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorMsg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textKey;
        private System.Windows.Forms.Label labelKey;
        private System.Windows.Forms.TextBox textMsg;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.ToolStripMenuItem menuFileClose;
        private System.Windows.Forms.ToolStripMenuItem menuType;
        private System.Windows.Forms.ToolStripMenuItem menuTypeTripleSingle;
        private System.Windows.Forms.TextBox textEncDec;
        private System.Windows.Forms.Label labelMsg;
        private System.Windows.Forms.Label labelEncDec;
        private System.Windows.Forms.Button btEncDec;
        private System.Windows.Forms.ToolStripMenuItem menuFileReset;
        private System.Windows.Forms.ErrorProvider errorKey;
        private System.Windows.Forms.ErrorProvider errorMsg;
    }
}

