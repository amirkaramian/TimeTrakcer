using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TimeTracker.Model;
using TimeTracker.Service;
using TimeTracker.Service.Interface;

namespace TimeTracker
{
    public partial class RegisterForm : Form
    {
        public IService ServiceAccess { get; set; }
        public RegisterForm()
        {
            InitializeComponent();
            Init();
        }
        private void Init()
        {
            button1.BackColor = Color.FromArgb(250, 250, 250);
            button2.BackColor = Color.FromArgb(250, 250, 250);
            button3.BackColor = Color.FromArgb(250, 250, 250);
            button4.BackColor = Color.FromArgb(250, 250, 250);
            button5.BackColor = Color.FromArgb(250, 250, 250);
            button6.BackColor = Color.FromArgb(250, 250, 250);
        }
        private async void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                btnRegister.Text = "waiting .....";
                btnRegister.Enabled = false;
               await ServiceAccess.RegisterAsync(new UserModel()
               {
                   Email = txtUserName.Text,
                   Family = txtFamily.Text,
                   Name = txtName.Text,
                   Password = txtPassword.Text,
                   ConfirmPassword = txtConfirmPass.Text,
                   MobileNumber = txtMob.Text,
                   RoleId=1,
               });

                MessageForm.Show("Registration successfull", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();

            }
            catch (Exception ex)
            {
                MessageForm.Show(ex.Message);
            }
            finally
            {
                btnRegister.Text = "Register";
                btnRegister.Enabled = true;
            }
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {

        }
    }
}
