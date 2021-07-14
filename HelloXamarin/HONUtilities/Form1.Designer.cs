
namespace HONUtilities
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtServiceName = new System.Windows.Forms.TextBox();
            this.cmdStop = new System.Windows.Forms.Button();
            this.cmdStart = new System.Windows.Forms.Button();
            this.lblmessage = new System.Windows.Forms.Label();
            this.dGrid = new System.Windows.Forms.DataGridView();
            this.pBar = new System.Windows.Forms.ProgressBar();
            this.cmdcreatelogfile = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 47);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Service Name";
            // 
            // txtServiceName
            // 
            this.txtServiceName.Location = new System.Drawing.Point(134, 44);
            this.txtServiceName.Name = "txtServiceName";
            this.txtServiceName.Size = new System.Drawing.Size(457, 27);
            this.txtServiceName.TabIndex = 1;
            // 
            // cmdStop
            // 
            this.cmdStop.Location = new System.Drawing.Point(134, 77);
            this.cmdStop.Name = "cmdStop";
            this.cmdStop.Size = new System.Drawing.Size(114, 44);
            this.cmdStop.TabIndex = 2;
            this.cmdStop.Text = "Stop";
            this.cmdStop.UseVisualStyleBackColor = true;
            this.cmdStop.Click += new System.EventHandler(this.cmdStop_Click);
            // 
            // cmdStart
            // 
            this.cmdStart.Location = new System.Drawing.Point(268, 77);
            this.cmdStart.Name = "cmdStart";
            this.cmdStart.Size = new System.Drawing.Size(114, 44);
            this.cmdStart.TabIndex = 3;
            this.cmdStart.Text = "Start";
            this.cmdStart.UseVisualStyleBackColor = true;
            this.cmdStart.Click += new System.EventHandler(this.cmdStart_Click);
            // 
            // lblmessage
            // 
            this.lblmessage.AutoSize = true;
            this.lblmessage.Location = new System.Drawing.Point(13, 148);
            this.lblmessage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblmessage.Name = "lblmessage";
            this.lblmessage.Size = new System.Drawing.Size(0, 20);
            this.lblmessage.TabIndex = 4;
            // 
            // dGrid
            // 
            this.dGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dGrid.Location = new System.Drawing.Point(626, 44);
            this.dGrid.MultiSelect = false;
            this.dGrid.Name = "dGrid";
            this.dGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dGrid.RowTemplate.Height = 24;
            this.dGrid.Size = new System.Drawing.Size(668, 381);
            this.dGrid.TabIndex = 5;
            // 
            // pBar
            // 
            this.pBar.Location = new System.Drawing.Point(134, 127);
            this.pBar.Name = "pBar";
            this.pBar.Size = new System.Drawing.Size(457, 23);
            this.pBar.TabIndex = 6;
            this.pBar.Visible = false;
            // 
            // cmdcreatelogfile
            // 
            this.cmdcreatelogfile.Location = new System.Drawing.Point(406, 77);
            this.cmdcreatelogfile.Name = "cmdcreatelogfile";
            this.cmdcreatelogfile.Size = new System.Drawing.Size(185, 44);
            this.cmdcreatelogfile.TabIndex = 7;
            this.cmdcreatelogfile.Text = "Create Log File";
            this.cmdcreatelogfile.UseVisualStyleBackColor = true;
            this.cmdcreatelogfile.Click += new System.EventHandler(this.cmdcreatelogfile_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1306, 440);
            this.Controls.Add(this.cmdcreatelogfile);
            this.Controls.Add(this.pBar);
            this.Controls.Add(this.dGrid);
            this.Controls.Add(this.lblmessage);
            this.Controls.Add(this.cmdStart);
            this.Controls.Add(this.cmdStop);
            this.Controls.Add(this.txtServiceName);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Diagnostic Tool";
            ((System.ComponentModel.ISupportInitialize)(this.dGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtServiceName;
        private System.Windows.Forms.Button cmdStop;
        private System.Windows.Forms.Button cmdStart;
        private System.Windows.Forms.Label lblmessage;
        private System.Windows.Forms.DataGridView dGrid;
        private System.Windows.Forms.ProgressBar pBar;
        private System.Windows.Forms.Button cmdcreatelogfile;
    }
}

