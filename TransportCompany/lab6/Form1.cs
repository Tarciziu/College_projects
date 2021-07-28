using System;
using System.Windows.Forms;
using domain;

namespace client
{
    public partial class Form1 : Form
    {
        private ClientCtrl ctrl;
        public Form1(ClientCtrl ctrl)
        {
            InitializeComponent();
            this.ctrl = ctrl;
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            String user = usernameTxt.Text;
            String pass = passwordTxt.Text;
            try
            {
                ctrl.login(user, pass);
                MainWindow mainWin = new MainWindow(ctrl);
                mainWin.ShowDialog();
                this.Hide();
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(this, "Login error " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
