using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chat
{
    public partial class AskLogin : Form
    {
        public string Login
        {
            get { return this.textBoxLogin.Text; }
        }

        public AskLogin()
        {
            InitializeComponent();

            this.Load += AskLogin_Load;
        }

        private void AskLogin_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();

            this.buttonSave.Click += ButtonSave_Click;
            this.textBoxLogin.KeyDown += TextBoxLogin_KeyDown;
        }

        private void TextBoxLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                this.DialogResult = DialogResult.OK;
            }
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
