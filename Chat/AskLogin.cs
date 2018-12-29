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
        public AskLogin()
        {
            InitializeComponent();

            this.Load += AskLogin_Load;
        }

        private void AskLogin_Load(object sender, EventArgs e)
        {
            this.buttonSave.Click += ButtonSave_Click;
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
