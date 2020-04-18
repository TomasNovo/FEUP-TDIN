namespace ChatApp
{
    partial class ChatRoom
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChatRoom));
            this.PMessages = new System.Windows.Forms.Panel();
            this.BSend = new System.Windows.Forms.Button();
            this.TBSend = new System.Windows.Forms.TextBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.PBFile = new System.Windows.Forms.PictureBox();
            this.TChatName = new System.Windows.Forms.TextBox();
            this.PUsers = new System.Windows.Forms.Panel();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PBFile)).BeginInit();
            this.SuspendLayout();
            // 
            // PMessages
            // 
            this.PMessages.BackColor = System.Drawing.Color.Silver;
            this.PMessages.Location = new System.Drawing.Point(45, 48);
            this.PMessages.Margin = new System.Windows.Forms.Padding(4);
            this.PMessages.Name = "PMessages";
            this.PMessages.Size = new System.Drawing.Size(653, 340);
            this.PMessages.TabIndex = 0;
            // 
            // BSend
            // 
            this.BSend.BackColor = System.Drawing.Color.RoyalBlue;
            this.BSend.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BSend.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.BSend.Location = new System.Drawing.Point(725, 432);
            this.BSend.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BSend.Name = "BSend";
            this.BSend.Size = new System.Drawing.Size(100, 41);
            this.BSend.TabIndex = 4;
            this.BSend.Text = "Send";
            this.BSend.UseVisualStyleBackColor = false;
            this.BSend.Click += new System.EventHandler(this.BSend_Click);
            // 
            // TBSend
            // 
            this.TBSend.BackColor = System.Drawing.Color.White;
            this.TBSend.ForeColor = System.Drawing.SystemColors.InfoText;
            this.TBSend.Location = new System.Drawing.Point(45, 409);
            this.TBSend.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TBSend.Multiline = true;
            this.TBSend.Name = "TBSend";
            this.TBSend.Size = new System.Drawing.Size(655, 79);
            this.TBSend.TabIndex = 5;
            this.TBSend.Text = "Write here your message..";
            this.TBSend.Enter += new System.EventHandler(this.TBSend_Enter);
            this.TBSend.Leave += new System.EventHandler(this.TBSend_Leave);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(715, 48);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(100, 100);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox3.TabIndex = 9;
            this.pictureBox3.TabStop = false;
            // 
            // PBFile
            // 
            this.PBFile.BackColor = System.Drawing.Color.White;
            this.PBFile.Image = ((System.Drawing.Image)(resources.GetObject("PBFile.Image")));
            this.PBFile.Location = new System.Drawing.Point(663, 447);
            this.PBFile.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PBFile.Name = "PBFile";
            this.PBFile.Size = new System.Drawing.Size(36, 41);
            this.PBFile.TabIndex = 10;
            this.PBFile.TabStop = false;
            // 
            // TChatName
            // 
            this.TChatName.BackColor = System.Drawing.Color.Gray;
            this.TChatName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TChatName.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TChatName.ForeColor = System.Drawing.SystemColors.Menu;
            this.TChatName.Location = new System.Drawing.Point(45, 12);
            this.TChatName.Name = "TChatName";
            this.TChatName.Size = new System.Drawing.Size(271, 29);
            this.TChatName.TabIndex = 12;
            this.TChatName.Text = "Chat Name";
            this.TChatName.TextChanged += new System.EventHandler(this.TChatName_TextChanged);
            // 
            // PUsers
            // 
            this.PUsers.BackColor = System.Drawing.Color.Silver;
            this.PUsers.Location = new System.Drawing.Point(715, 205);
            this.PUsers.Name = "PUsers";
            this.PUsers.Size = new System.Drawing.Size(131, 181);
            this.PUsers.TabIndex = 13;
            // 
            // ChatRoom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(864, 554);
            this.Controls.Add(this.PUsers);
            this.Controls.Add(this.TChatName);
            this.Controls.Add(this.PBFile);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.TBSend);
            this.Controls.Add(this.BSend);
            this.Controls.Add(this.PMessages);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ChatRoom";
            this.Text = "ChatRoom";
            this.Load += new System.EventHandler(this.ChatRoom_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PBFile)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel PMessages;
        private System.Windows.Forms.Button BSend;
        private System.Windows.Forms.TextBox TBSend;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox PBFile;
        private System.Windows.Forms.TextBox TChatName;
        private System.Windows.Forms.Panel PUsers;
        private System.Windows.Forms.ColorDialog colorDialog1;
    }
}