namespace DOPrint_WD
{
    partial class MessageForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.m_messageBox = new System.Windows.Forms.TextBox();
            this.ok_Button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_messageBox
            // 
            this.m_messageBox.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.m_messageBox.Location = new System.Drawing.Point(3, 3);
            this.m_messageBox.Multiline = true;
            this.m_messageBox.Name = "m_messageBox";
            this.m_messageBox.ReadOnly = true;
            this.m_messageBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.m_messageBox.Size = new System.Drawing.Size(342, 165);
            this.m_messageBox.TabIndex = 0;
            this.m_messageBox.TabStop = false;
            // 
            // ok_Button
            // 
            this.ok_Button.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ok_Button.Location = new System.Drawing.Point(115, 174);
            this.ok_Button.Name = "ok_Button";
            this.ok_Button.Size = new System.Drawing.Size(89, 34);
            this.ok_Button.TabIndex = 1;
            this.ok_Button.Text = "OK";
            this.ok_Button.UseVisualStyleBackColor = false;
            this.ok_Button.Click += new System.EventHandler(this.ok_Button_Click);
            // 
            // MessageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(347, 220);
            this.ControlBox = false;
            this.Controls.Add(this.ok_Button);
            this.Controls.Add(this.m_messageBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "MessageForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Message";
            this.Load += new System.EventHandler(this.MessageForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox m_messageBox;
        private System.Windows.Forms.Button ok_Button;
    }
}