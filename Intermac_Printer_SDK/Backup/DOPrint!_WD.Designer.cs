namespace DOPrint_WD
{
    partial class DOPrint_WD
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DOPrint_WD));
            this.m_statusTextBox = new System.Windows.Forms.TextBox();
            this.m_printerLanguageComboBox = new System.Windows.Forms.ComboBox();
            this.m_printerLanguage = new System.Windows.Forms.Label();
            this.m_statusLabel = new System.Windows.Forms.Label();
            this.m_printItemsComboBox = new System.Windows.Forms.ComboBox();
            this.m_actionLabel = new System.Windows.Forms.Label();
            this.m_queryRadio = new System.Windows.Forms.RadioButton();
            this.m_printRadio = new System.Windows.Forms.RadioButton();
            this.m_actionLbl = new System.Windows.Forms.Label();
            this.m_portTextBox = new System.Windows.Forms.TextBox();
            this.m_portLabel = new System.Windows.Forms.Label();
            this.m_deviceAddressTextBox = new System.Windows.Forms.TextBox();
            this.m_devAddrLabel = new System.Windows.Forms.Label();
            this.m_connComboBox = new System.Windows.Forms.ComboBox();
            this.connModeLabel = new System.Windows.Forms.Label();
            this.m_performButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.m_browseButton = new System.Windows.Forms.Button();
            this.m_printHeadCmbo = new System.Windows.Forms.ComboBox();
            this.m_printHeadLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // m_statusTextBox
            // 
            this.m_statusTextBox.Location = new System.Drawing.Point(15, 397);
            this.m_statusTextBox.Multiline = true;
            this.m_statusTextBox.Name = "m_statusTextBox";
            this.m_statusTextBox.ReadOnly = true;
            this.m_statusTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.m_statusTextBox.Size = new System.Drawing.Size(423, 108);
            this.m_statusTextBox.TabIndex = 48;
            // 
            // m_printerLanguageComboBox
            // 
            this.m_printerLanguageComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_printerLanguageComboBox.Font = new System.Drawing.Font("Tahoma", 8F);
            this.m_printerLanguageComboBox.Items.AddRange(new object[] {
            "EZ",
            "LP",
            "DPL",
            "ExPCL_LP",
            "ExPCL_PP"});
            this.m_printerLanguageComboBox.Location = new System.Drawing.Point(205, 215);
            this.m_printerLanguageComboBox.Name = "m_printerLanguageComboBox";
            this.m_printerLanguageComboBox.Size = new System.Drawing.Size(153, 21);
            this.m_printerLanguageComboBox.TabIndex = 4;
            this.m_printerLanguageComboBox.SelectedIndexChanged += new System.EventHandler(this.m_printerLanguageComboBox_SelectedIndexChanged);
            // 
            // m_printerLanguage
            // 
            this.m_printerLanguage.Font = new System.Drawing.Font("Tahoma", 8F);
            this.m_printerLanguage.Location = new System.Drawing.Point(12, 216);
            this.m_printerLanguage.Name = "m_printerLanguage";
            this.m_printerLanguage.Size = new System.Drawing.Size(139, 20);
            this.m_printerLanguage.TabIndex = 49;
            this.m_printerLanguage.Text = "Select a printer language:";
            // 
            // m_statusLabel
            // 
            this.m_statusLabel.Font = new System.Drawing.Font("Tahoma", 8F);
            this.m_statusLabel.Location = new System.Drawing.Point(12, 379);
            this.m_statusLabel.Name = "m_statusLabel";
            this.m_statusLabel.Size = new System.Drawing.Size(43, 15);
            this.m_statusLabel.TabIndex = 50;
            this.m_statusLabel.Text = "Status:";
            // 
            // m_printItemsComboBox
            // 
            this.m_printItemsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_printItemsComboBox.Font = new System.Drawing.Font("Tahoma", 8F);
            this.m_printItemsComboBox.Location = new System.Drawing.Point(205, 314);
            this.m_printItemsComboBox.Name = "m_printItemsComboBox";
            this.m_printItemsComboBox.Size = new System.Drawing.Size(154, 21);
            this.m_printItemsComboBox.TabIndex = 8;
            // 
            // m_actionLabel
            // 
            this.m_actionLabel.Font = new System.Drawing.Font("Tahoma", 8F);
            this.m_actionLabel.Location = new System.Drawing.Point(12, 310);
            this.m_actionLabel.Name = "m_actionLabel";
            this.m_actionLabel.Size = new System.Drawing.Size(114, 23);
            this.m_actionLabel.TabIndex = 51;
            this.m_actionLabel.Text = "Select what to print:";
            // 
            // m_queryRadio
            // 
            this.m_queryRadio.Font = new System.Drawing.Font("Tahoma", 8F);
            this.m_queryRadio.Location = new System.Drawing.Point(274, 259);
            this.m_queryRadio.Name = "m_queryRadio";
            this.m_queryRadio.Size = new System.Drawing.Size(67, 20);
            this.m_queryRadio.TabIndex = 6;
            this.m_queryRadio.Text = "Query";
            this.m_queryRadio.CheckedChanged += new System.EventHandler(this.m_queryRadio_CheckedChanged);
            // 
            // m_printRadio
            // 
            this.m_printRadio.Checked = true;
            this.m_printRadio.Font = new System.Drawing.Font("Tahoma", 8F);
            this.m_printRadio.Location = new System.Drawing.Point(204, 259);
            this.m_printRadio.Name = "m_printRadio";
            this.m_printRadio.Size = new System.Drawing.Size(64, 20);
            this.m_printRadio.TabIndex = 5;
            this.m_printRadio.TabStop = true;
            this.m_printRadio.Text = "Print";
            this.m_printRadio.CheckedChanged += new System.EventHandler(this.m_printRadio_CheckedChanged);
            // 
            // m_actionLbl
            // 
            this.m_actionLbl.Font = new System.Drawing.Font("Tahoma", 8F);
            this.m_actionLbl.Location = new System.Drawing.Point(12, 257);
            this.m_actionLbl.Name = "m_actionLbl";
            this.m_actionLbl.Size = new System.Drawing.Size(114, 23);
            this.m_actionLbl.TabIndex = 52;
            this.m_actionLbl.Text = "Select what to do:";
            // 
            // m_portTextBox
            // 
            this.m_portTextBox.Font = new System.Drawing.Font("Tahoma", 8F);
            this.m_portTextBox.Location = new System.Drawing.Point(204, 189);
            this.m_portTextBox.Name = "m_portTextBox";
            this.m_portTextBox.Size = new System.Drawing.Size(154, 20);
            this.m_portTextBox.TabIndex = 3;
            this.m_portTextBox.TextChanged += new System.EventHandler(this.m_portTextBox_TextChanged);
            this.m_portTextBox.Leave += new System.EventHandler(this.m_portTextBox_Leave);
            this.m_portTextBox.Enter += new System.EventHandler(this.m_portTextBox_Enter);
            // 
            // m_portLabel
            // 
            this.m_portLabel.Font = new System.Drawing.Font("Tahoma", 8F);
            this.m_portLabel.Location = new System.Drawing.Point(12, 187);
            this.m_portLabel.Name = "m_portLabel";
            this.m_portLabel.Size = new System.Drawing.Size(82, 23);
            this.m_portLabel.TabIndex = 53;
            this.m_portLabel.Text = "Port:";
            // 
            // m_deviceAddressTextBox
            // 
            this.m_deviceAddressTextBox.Font = new System.Drawing.Font("Tahoma", 8F);
            this.m_deviceAddressTextBox.Location = new System.Drawing.Point(204, 163);
            this.m_deviceAddressTextBox.Name = "m_deviceAddressTextBox";
            this.m_deviceAddressTextBox.Size = new System.Drawing.Size(154, 20);
            this.m_deviceAddressTextBox.TabIndex = 2;
            this.m_deviceAddressTextBox.TextChanged += new System.EventHandler(this.m_deviceAddressTextBox_TextChanged);
            this.m_deviceAddressTextBox.Enter += new System.EventHandler(this.m_deviceAddressTextBox_Enter);
            // 
            // m_devAddrLabel
            // 
            this.m_devAddrLabel.Font = new System.Drawing.Font("Tahoma", 8F);
            this.m_devAddrLabel.Location = new System.Drawing.Point(12, 164);
            this.m_devAddrLabel.Name = "m_devAddrLabel";
            this.m_devAddrLabel.Size = new System.Drawing.Size(99, 23);
            this.m_devAddrLabel.TabIndex = 54;
            this.m_devAddrLabel.Text = "Device Address:";
            // 
            // m_connComboBox
            // 
            this.m_connComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_connComboBox.Font = new System.Drawing.Font("Tahoma", 8F);
            this.m_connComboBox.Items.AddRange(new object[] {
            "Bluetooth",
            "TCP/IP"});
            this.m_connComboBox.Location = new System.Drawing.Point(204, 136);
            this.m_connComboBox.Name = "m_connComboBox";
            this.m_connComboBox.Size = new System.Drawing.Size(154, 21);
            this.m_connComboBox.TabIndex = 1;
            this.m_connComboBox.SelectedIndexChanged += new System.EventHandler(this.m_connComboBox_SelectedIndexChanged);
            // 
            // connModeLabel
            // 
            this.connModeLabel.Font = new System.Drawing.Font("Tahoma", 8F);
            this.connModeLabel.Location = new System.Drawing.Point(12, 136);
            this.connModeLabel.Name = "connModeLabel";
            this.connModeLabel.Size = new System.Drawing.Size(99, 23);
            this.connModeLabel.TabIndex = 55;
            this.connModeLabel.Text = "Connection Mode:";
            // 
            // m_performButton
            // 
            this.m_performButton.Location = new System.Drawing.Point(339, 343);
            this.m_performButton.Name = "m_performButton";
            this.m_performButton.Size = new System.Drawing.Size(99, 33);
            this.m_performButton.TabIndex = 10;
            this.m_performButton.Text = "Print";
            this.m_performButton.Click += new System.EventHandler(this.performButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DOPrint_WD.Properties.Resources.dologo94x85;
            this.pictureBox1.Location = new System.Drawing.Point(174, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(94, 92);
            this.pictureBox1.TabIndex = 56;
            this.pictureBox1.TabStop = false;
            // 
            // m_browseButton
            // 
            this.m_browseButton.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.m_browseButton.Location = new System.Drawing.Point(371, 314);
            this.m_browseButton.Name = "m_browseButton";
            this.m_browseButton.Size = new System.Drawing.Size(67, 23);
            this.m_browseButton.TabIndex = 9;
            this.m_browseButton.Text = "Browse..";
            this.m_browseButton.UseVisualStyleBackColor = false;
            this.m_browseButton.Click += new System.EventHandler(this.m_browseButton_Click);
            // 
            // m_printHeadCmbo
            // 
            this.m_printHeadCmbo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_printHeadCmbo.Font = new System.Drawing.Font("Tahoma", 8F);
            this.m_printHeadCmbo.Items.AddRange(new object[] {
            "384 (2 in.)",
            "576 (3 in.)",
            "832 (4 in.)"});
            this.m_printHeadCmbo.Location = new System.Drawing.Point(205, 287);
            this.m_printHeadCmbo.Name = "m_printHeadCmbo";
            this.m_printHeadCmbo.Size = new System.Drawing.Size(154, 21);
            this.m_printHeadCmbo.TabIndex = 7;
            this.m_printHeadCmbo.SelectedIndexChanged += new System.EventHandler(this.m_printHeadCmbo_SelectedIndexChanged);
            // 
            // m_printHeadLabel
            // 
            this.m_printHeadLabel.Font = new System.Drawing.Font("Tahoma", 8F);
            this.m_printHeadLabel.Location = new System.Drawing.Point(12, 283);
            this.m_printHeadLabel.Name = "m_printHeadLabel";
            this.m_printHeadLabel.Size = new System.Drawing.Size(124, 23);
            this.m_printHeadLabel.TabIndex = 59;
            this.m_printHeadLabel.Text = "Select PrintHead Width:";
            // 
            // DOPrint_WD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(448, 521);
            this.Controls.Add(this.m_printHeadCmbo);
            this.Controls.Add(this.m_printHeadLabel);
            this.Controls.Add(this.m_browseButton);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.m_statusTextBox);
            this.Controls.Add(this.m_printerLanguageComboBox);
            this.Controls.Add(this.m_printerLanguage);
            this.Controls.Add(this.m_statusLabel);
            this.Controls.Add(this.m_printItemsComboBox);
            this.Controls.Add(this.m_actionLabel);
            this.Controls.Add(this.m_queryRadio);
            this.Controls.Add(this.m_printRadio);
            this.Controls.Add(this.m_actionLbl);
            this.Controls.Add(this.m_portTextBox);
            this.Controls.Add(this.m_portLabel);
            this.Controls.Add(this.m_deviceAddressTextBox);
            this.Controls.Add(this.m_devAddrLabel);
            this.Controls.Add(this.m_connComboBox);
            this.Controls.Add(this.connModeLabel);
            this.Controls.Add(this.m_performButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DOPrint_WD";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "D-O Print!";
            this.Load += new System.EventHandler(this.DOPrint_WD_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DOPrint_WD_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox m_statusTextBox;
        private System.Windows.Forms.ComboBox m_printerLanguageComboBox;
        private System.Windows.Forms.Label m_printerLanguage;
        private System.Windows.Forms.Label m_statusLabel;
        private System.Windows.Forms.ComboBox m_printItemsComboBox;
        private System.Windows.Forms.Label m_actionLabel;
        private System.Windows.Forms.RadioButton m_queryRadio;
        private System.Windows.Forms.RadioButton m_printRadio;
        private System.Windows.Forms.Label m_actionLbl;
        private System.Windows.Forms.TextBox m_portTextBox;
        private System.Windows.Forms.Label m_portLabel;
        private System.Windows.Forms.TextBox m_deviceAddressTextBox;
        private System.Windows.Forms.Label m_devAddrLabel;
        private System.Windows.Forms.ComboBox m_connComboBox;
        private System.Windows.Forms.Label connModeLabel;
        private System.Windows.Forms.Button m_performButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button m_browseButton;
        private System.Windows.Forms.ComboBox m_printHeadCmbo;
        private System.Windows.Forms.Label m_printHeadLabel;
    }
}

