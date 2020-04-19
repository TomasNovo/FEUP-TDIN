namespace ChatApp
{
    partial class Index
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Index));
            this.PUsers = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.LWelcome = new System.Windows.Forms.Label();
            this.PChatRooms = new System.Windows.Forms.Panel();
            this.BGroupChat = new System.Windows.Forms.Button();
            this.BDiscard = new System.Windows.Forms.Button();
            this.LUser = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // PUsers
            // 
            this.PUsers.BackColor = System.Drawing.Color.DarkGray;
            this.PUsers.Dock = System.Windows.Forms.DockStyle.Right;
            this.PUsers.Location = new System.Drawing.Point(641, 0);
            this.PUsers.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PUsers.Name = "PUsers";
            this.PUsers.Size = new System.Drawing.Size(159, 450);
            this.PUsers.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DimGray;
            this.panel2.Location = new System.Drawing.Point(641, 1);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(5, 466);
            this.panel2.TabIndex = 1;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(259, 1);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(100, 100);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox3.TabIndex = 9;
            this.pictureBox3.TabStop = false;
            // 
            // LWelcome
            // 
            this.LWelcome.AutoSize = true;
            this.LWelcome.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LWelcome.Location = new System.Drawing.Point(36, 151);
            this.LWelcome.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LWelcome.Name = "LWelcome";
            this.LWelcome.Size = new System.Drawing.Size(79, 20);
            this.LWelcome.TabIndex = 11;
            this.LWelcome.Text = "Welcome";
            // 
            // PChatRooms
            // 
            this.PChatRooms.Location = new System.Drawing.Point(41, 238);
            this.PChatRooms.Margin = new System.Windows.Forms.Padding(4);
            this.PChatRooms.Name = "PChatRooms";
            this.PChatRooms.Size = new System.Drawing.Size(540, 198);
            this.PChatRooms.TabIndex = 12;
            // 
            // BGroupChat
            // 
            this.BGroupChat.BackColor = System.Drawing.Color.Gray;
            this.BGroupChat.ForeColor = System.Drawing.Color.White;
            this.BGroupChat.Location = new System.Drawing.Point(497, 178);
            this.BGroupChat.Margin = new System.Windows.Forms.Padding(4);
            this.BGroupChat.Name = "BGroupChat";
            this.BGroupChat.Size = new System.Drawing.Size(126, 28);
            this.BGroupChat.TabIndex = 13;
            this.BGroupChat.TabStop = false;
            this.BGroupChat.Text = "Start group chat";
            this.BGroupChat.UseVisualStyleBackColor = false;
            this.BGroupChat.Click += new System.EventHandler(this.BGroupChat_Click);
            // 
            // BDiscard
            // 
            this.BDiscard.BackColor = System.Drawing.Color.Gray;
            this.BDiscard.ForeColor = System.Drawing.Color.White;
            this.BDiscard.Location = new System.Drawing.Point(523, 210);
            this.BDiscard.Margin = new System.Windows.Forms.Padding(4);
            this.BDiscard.Name = "BDiscard";
            this.BDiscard.Size = new System.Drawing.Size(100, 28);
            this.BDiscard.TabIndex = 14;
            this.BDiscard.TabStop = false;
            this.BDiscard.Text = "Discard";
            this.BDiscard.UseVisualStyleBackColor = false;
            this.BDiscard.Visible = false;
            this.BDiscard.Click += new System.EventHandler(this.BDiscard_Click);
            // 
            // LUser
            // 
            this.LUser.AutoSize = true;
            this.LUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LUser.Location = new System.Drawing.Point(37, 125);
            this.LUser.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LUser.Name = "LUser";
            this.LUser.Size = new System.Drawing.Size(79, 20);
            this.LUser.TabIndex = 15;
            this.LUser.Text = "Welcome";
            // 
            // Index
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.LUser);
            this.Controls.Add(this.BDiscard);
            this.Controls.Add(this.BGroupChat);
            this.Controls.Add(this.PChatRooms);
            this.Controls.Add(this.LWelcome);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.PUsers);
            this.ForeColor = System.Drawing.Color.Snow;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Index";
            this.Text = "Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Index_FormClosing);
            this.Load += new System.EventHandler(this.Index_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel PUsers;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label LWelcome;
        private System.Windows.Forms.Panel PChatRooms;
        private System.Windows.Forms.Button BGroupChat;
        private System.Windows.Forms.Button BDiscard;
        private System.Windows.Forms.Label LUser;
    }
}

