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


            CustomizeMessagesBox();

            this.Load += MainForm_Load;
            this.FormClosing += MainForm_FormClosing;

            this.buttonSend.Click += ButtonSend_Click;
            this.textBoxMessage.KeyDown += TextBoxMessage_KeyDown;
            this.textBoxChat.TextChanged += TextBoxChat_TextChanged;
        }

        private void CustomizeMessagesBox()
        {
            this.textBoxChat.ReadOnly = true;
            this.textBoxChat.BackColor = Color.White;
            this.textBoxChat.ScrollBars = ScrollBars.Both;
        }

        private void TextBoxChat_TextChanged(object sender, EventArgs e)
        {
            this.textBoxChat.SelectionStart = this.textBoxChat.Text.Length;
            this.textBoxChat.ScrollToCaret();
        }

        private void TextBoxMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                SendingMessage();
            }
        }

        private void ButtonSend_Click(object sender, EventArgs e)
        {
            SendingMessage();
        }

        private void SendingMessage()
        {
            if (this.IsThereAMessage())
            {
                chat.Send(this.textBoxMessage.Text);

                this.textBoxMessage.Clear();
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
