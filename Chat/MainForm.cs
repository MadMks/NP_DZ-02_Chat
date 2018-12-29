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
            chat = new Chat(this.textBoxChat);
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
