using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace DOPrint_WD
{
    public partial class MessageForm : Form
    {
        private bool centered = true;

        private string m_message;

        public MessageForm()
        {
            InitializeComponent();
        }

        public MessageForm(string message,string title)
        {
            InitializeComponent();
            m_message = message;
            m_messageBox.Text = m_message;
            this.Text = title;
        }
        public bool CenterFormOnScreen
        {
            get { return centered; }
            set
            {
                centered = value;
                if (centered)
                    CenterWithinScreen();
            }
        }

        private void CenterWithinScreen()
        {
            // Move the position of this form to center it within the
            // working area of the desktop
            int x = (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2;
            int y = (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2;

            this.Location = new Point(x, y);
        }
        private void ok_Button_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void MessageForm_Load(object sender, EventArgs e)
        {
            ok_Button.Focus();
            // Center the form if requested
            if (centered)
            {
                CenterWithinScreen();
            }
        }
    }
}