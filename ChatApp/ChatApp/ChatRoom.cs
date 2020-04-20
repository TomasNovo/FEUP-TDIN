using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common;

namespace ChatApp
{
    public partial class ChatRoom : LockedForm
    {
        int message_number = 0;
        private int roomId;
        public List<string> userList;
        public List<Color> colors;
        private Client client;
        private bool hidden = true;

        private string ChatName;

        // MACROS
        public string CHAT_NAME_UPDATE = "UpdatedChatName:";
        public string SEND_FILE = "File:";

        public byte[] tempFile = new byte[0];
        public string tempFileName = "";
        public string extension = "";

        public ChatRoom(int RoomId, List<string>  userList)
        {
            InitializeComponent();

            this.roomId = RoomId;
            this.ChatName = "ChatName" + this.roomId;

            client = MainForm.Instance.client;
            client.MessageReceived += Client_MessageReceived;

            colors = new List<Color>();

            PBFile.MouseClick += new MouseEventHandler(LoadFile);

            //userList.Remove(client.UserName);
            swapToFirst(userList, client.UserName);
            for (int i = 0; i < userList.Count; i++)
            {
                Label temp = new Label();
                temp.DoubleClick += User_DoubleClick;
                temp.Size = new Size(131, 15);
                temp.Location = new Point(0, i * 15);
                temp.BorderStyle = BorderStyle.FixedSingle;
                temp.Text = userList[i];

                if (i == 0)
                {
                    temp.BackColor = Color.MistyRose;
                    colors.Add(Color.MistyRose);
                }
                else
                {
                    temp.BackColor = Color.AntiqueWhite;
                    colors.Add(Color.AntiqueWhite);
                }

                this.PUsers.Controls.Add(temp);

                //TBUserList.Text += $"{userList[i]}{Environment.NewLine}";
            }

            this.userList = userList;

            this.ControlBox = false;

            DrawLogMessages();
        }

        private void DrawLogMessages()
        {
            if (InvokeRequired)
                BeginInvoke((MethodInvoker)delegate { DrawLogMessages(); });
            else
            {
                for (int i = 0; i < client.chatRooms[roomId].log.messageList.Count; i++)
                {
                    var tuple = client.chatRooms[roomId].log.messageList[i];
                    DrawMessage(tuple.Item2, tuple.Item1);
                }
            }
        }

        private void swapToFirst(List<string> list, string user)
        {
            for(int i = 0; i < list.Count; i++)
            {
                if(list[i].Equals(user))
                {
                    string tmp = list[0];
                    list[0] = user;
                    list[i] = tmp;
                }
            }
        }

        private void Client_MessageReceived(object source, MessageReceivedEventArgs e)
        {
            // Ignore event when message is sent by this client
            if (e.sender == client.UserName)
                return;

            // Ignore messages belongin to other rooms
            if (e.roomId != roomId)
                return;

            DrawMessageReceived(e);
        }

        private void DrawMessageReceived(MessageReceivedEventArgs e)
        {
            if (InvokeRequired)
                BeginInvoke((MethodInvoker)delegate { DrawMessageReceived(e); });
            else
            {
                // Chat name update
                if(e.message.Length >= 16 &&  e.message.Substring(0,16).Equals(CHAT_NAME_UPDATE))
                {
                    this.ChatName = e.message.Substring(16);
                    TChatName.Text = this.ChatName;
                    return;
                }

                if(e.message.Length >= 5 && e.message.Substring(0, 5).Equals(SEND_FILE) && e.file != null)
                {
                    tempFile = e.file;
                }

                DrawMessage(e.message, e.sender);
            }
        }

        private void DrawMessage(string message, string sender = "")
        {
            Label temp = new Label();
            this.PMessages.Controls.Add(temp);
            temp.Size = new Size(490, 15);
            temp.AutoSize = false;
            bool left = (sender != "" && sender != client.UserName);

            if (left)
            {
                if (message.Length >= 5 && message.Substring(0, 5).Equals(SEND_FILE))
                {
                    string filename = message.Substring(5);
                    tempFileName = filename;
                    string[] words = filename.Split('.');
                    extension = words[1];
                    temp.DoubleClick += FileDownload;
                    message = $"Sent file with name {filename} (double click to download)";
                }

                temp.BackColor = Color.Yellow;
                temp.Location = new Point(3, 10 + message_number * 20);
                temp.Text = $"{sender}: {message}";

                for (int i = 0; i < userList.Count; i++)
                {
                    if (sender.Equals(userList[i]))
                    {
                        temp.BackColor = colors[i];
                    }
                }
            }
            else
            {
                temp.BackColor = Color.Blue;
                temp.TextAlign = ContentAlignment.MiddleRight;
                temp.Location = new Point(0, 10 + message_number * 20);
                temp.Text = message;
                temp.BackColor = colors[0];
            }

            message_number++;
        }

        private void ChatRoom_Load(object sender, EventArgs e)
        {
            PMessages.AutoScroll = false;
            PMessages.HorizontalScroll.Enabled = false;
            PMessages.HorizontalScroll.Visible = false;
            PMessages.HorizontalScroll.Maximum = 0;
            PMessages.AutoScroll = true;
        }

        //Send textBox Placeholder
        private void TBSend_Enter(object sender, EventArgs e)
        {
            if (TBSend.Text == "Write here your message..")
            {
                TBSend.Text = "";
                TBSend.ForeColor = Color.Black;
            }
        }

        private void TBSend_Leave(object sender, EventArgs e)
        {
            if (TBSend.Text == "")
            {
                TBSend.Text = "Write here your message..";
                TBSend.ForeColor = Color.Black;
            }
        }

        private void BSend_Click(object sender, EventArgs e)
        {
            if (TBSend.Text == "Write here your message..")
                return;

            DrawMessage(TBSend.Text);
         
            MainForm.Instance.client.SendMessage(roomId, TBSend.Text);

            TBSend.Text = "Write here your message..";
            //TBSend.ForeColor = Color.Silver;
        }

        public void ToggleVisibility()
        {
            if (hidden)
                this.Show();
            else
                this.Hide();

            hidden = !hidden; // Swap hidden value
        }

        private void TChatName_TextChanged(object sender, EventArgs e)
        {
            MainForm.Instance.client.SendMessage(roomId, CHAT_NAME_UPDATE + TChatName.Text);
        }

        private void User_DoubleClick(object sender, EventArgs e)
        {
            string username = ((Label)sender).Text;
            
            if (colorDialog1.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            {
                ((Label)sender).BackColor = colorDialog1.Color;
                
                for(int i = 0; i < userList.Count; i++)
                {
                    if(userList[i].Equals(username))
                    {
                        colors[i] = colorDialog1.Color;
                        ResetMessages();
                        break;
                    }
                }

            }
        }

        private void ResetMessages()
        {
            message_number = 0;
            PMessages.Controls.Clear();
            DrawLogMessages();
        }

        //----------Files-------------
        private void LoadFile(object sender, MouseEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            string filename = "";
            string safe = "";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                filename = ofd.FileName;
                safe = ofd.SafeFileName;
            }

            FileInfo fileInfo = new FileInfo(filename);
            byte[] data = new byte[fileInfo.Length];
            using (FileStream fs = fileInfo.OpenRead())
            {
                fs.Read(data, 0, data.Length);
            }

            //byte[] -> string
            //string result = System.Text.Encoding.ASCII.GetString(data);

            DrawMessage($"Sent file {safe}");

            //Format: SEND_FILE:filename:result
            MainForm.Instance.client.SendFile(roomId, SEND_FILE + safe, data);

            // Delete the temporary file
            //fileInfo.Delete();

        }

        private void FileDownload(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = tempFileName;
            sfd.DefaultExt = extension;

            if(sfd.ShowDialog() == DialogResult.OK)
            {
                Stream fs = sfd.OpenFile();
                StreamWriter sw = new StreamWriter(fs);

                fs.Write(tempFile, 0, tempFile.Length);

                sw.Close();
                fs.Close();

                tempFile = new byte[0];
                tempFileName = "";
                extension = "";
            }
        }
    }
}
