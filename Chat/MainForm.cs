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

            this.buttonSend.Click += ButtonSend_Click;
        }

        private void ButtonSend_Click(object sender, EventArgs e)
        {
            if (this.IsThereAMessage())
            {
                // TODO: отправить
                chat.Send(this.textBoxMessage.Text);
            }
        }

        private bool IsThereAMessage()
        {
            if (this.textBoxMessage.Text.Length > 0)
            {
                return true;
            }

            return false;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            string login = "";
            // Down -> method registrationLogin

            AskLogin askLogin = new AskLogin();
            if (askLogin.ShowDialog() == DialogResult.OK)
            {
                login = askLogin.Login;
                this.Text = login;
            }

            // Создание чата.
            chat = new Chat();

            chat.ChatMessages = this.textBoxChat;
            chat.Login = login;

            chat.Listening();
        }


        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            chat.Close();
        }
    }
}
