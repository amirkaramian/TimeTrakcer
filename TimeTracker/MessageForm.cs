using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TimeTracker.Properties;

namespace TimeTracker
{
    public partial class MessageForm : Form
    {
        static MessageForm msg;
        private MessageForm()
        {
            InitializeComponent();           
        }
        public static void ShowBox(string subject, string message)
        {
            msg = new MessageForm();
            msg.lblSubject.Text = subject;
            msg.lblMessage.Text = message;          
            msg.ShowDialog();
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            try
            {
                base.OnClosing(e);

                this.Dispose();
            }
            catch (Exception)
            {

            }
        }

        internal static void Show(string message, string subject = "warning", MessageBoxButtons oK = MessageBoxButtons.OK, MessageBoxIcon warning = MessageBoxIcon.Warning)
        {
            msg = new MessageForm();
            msg.lblSubject.Text = subject;
            msg.lblMessage.Text = message;
            msg.lblMessage.MaximumSize = new Size(450, 0);
            msg.lblMessage.AutoSize = true;
            if (subject=="Info")            
                msg.Icon = Resources.Info;           
            msg.ShowDialog();
        }
    }
}
