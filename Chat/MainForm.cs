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
    public partial class MainForm : Form
    {
        private Chat chat = null;

        public MainForm()
        {
            InitializeComponent();

            this.Load += MainForm_Load;
            this.FormClosing += MainForm_FormClosing;
        }

        

        private void MainForm_Load(object sender, EventArgs e)
        {
            chat = new Chat();
            chat.Listening();

            // Down -> method registrationLogin

            // TODO: Спросить Логин
            // "Ваш логин: "
            this.Text = "TODO: мой логин";
        }


        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            chat.Close();
        }
    }
}
