using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TimeTracker.Service;
using TimeTracker.Service.Interface;

namespace TimeTracker
{
    public partial class Login : Form
    {
        IService serviceAccess;
        MainForm mainForm;

        public Login()
        {
            InitializeComponent();
            Init();

        }
        private void Init()
        {
            try
            {
                this.btnLogin.BackColor = Color.FromArgb(0, 212, 255);
                this.btnRegister.BackColor = Color.FromArgb(0, 212, 255);
                this.btnFpass.BackColor = Color.FromArgb(0, 212, 255);
                button3.BackColor = Color.FromArgb(250, 250, 250);
                button4.BackColor = Color.FromArgb(250, 250, 250);

                serviceAccess = new ServiceAccess();

            }
            catch (Exception ex)
            {
                MessageForm.Show(ex.Message);
            }

        }
        private async void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                btnLogin.Text = "waiting .....";
                btnLogin.Enabled = false;
                var userInfo = await serviceAccess.LoginAsync(txtUserName.Text, txtPassword.Text);
                this.Hide();
                mainForm = new MainForm
                {
                    ServiceAccess = this.serviceAccess,
                    userInfo = userInfo,
                };
                mainForm?.Show();
            }
            catch (Exception ex)
            {
                MessageForm.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnLogin.Text = "Login";
                btnLogin.Enabled = true;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            var register = new RegisterForm
            {
                ServiceAccess = this.serviceAccess
            };
            register.ShowDialog();
        }

        private void btnFpass_Click(object sender, EventArgs e)
        {

            var changePassForm = new ChangePassForm
            {
                ServiceAccess = this.serviceAccess
            };
            changePassForm.ShowDialog();
        }
    }
}
