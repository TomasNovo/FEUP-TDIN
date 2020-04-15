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
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.circularButton1 = new ChatApp.CircularButton();
            this.Icon = new ChatApp.CircularButton();
            this.label2 = new System.Windows.Forms.Label();
            this.PUsers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // PUsers
            // 
            this.PUsers.BackColor = System.Drawing.Color.DarkGray;
            this.PUsers.Controls.Add(this.label1);
            this.PUsers.Dock = System.Windows.Forms.DockStyle.Right;
            this.PUsers.Location = new System.Drawing.Point(641, 0);
            this.PUsers.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PUsers.Name = "PUsers";
            this.PUsers.Size = new System.Drawing.Size(159, 450);
            this.PUsers.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(56, 21);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Users";
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
            // circularButton1
            // 
            this.circularButton1.BackColor = System.Drawing.Color.Black;
            this.circularButton1.FlatAppearance.BorderSize = 0;
            this.circularButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.circularButton1.ForeColor = System.Drawing.Color.SlateBlue;
            this.circularButton1.Location = new System.Drawing.Point(67, 234);
            this.circularButton1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.circularButton1.Name = "circularButton1";
            this.circularButton1.Size = new System.Drawing.Size(51, 50);
            this.circularButton1.TabIndex = 10;
            this.circularButton1.Text = "Chat1";
            this.circularButton1.UseVisualStyleBackColor = false;
            // 
            // Icon
            // 
            this.Icon.BackColor = System.Drawing.Color.Black;
            this.Icon.FlatAppearance.BorderSize = 0;
            this.Icon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Icon.ForeColor = System.Drawing.Color.SlateBlue;
            this.Icon.Location = new System.Drawing.Point(67, 308);
            this.Icon.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Icon.Name = "Icon";
            this.Icon.Size = new System.Drawing.Size(51, 50);
            this.Icon.TabIndex = 3;
            this.Icon.Text = "Chat1";
            this.Icon.UseVisualStyleBackColor = false;
            this.Icon.Click += new System.EventHandler(this.Icon_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(123, 142);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 25);
            this.label2.TabIndex = 11;
            this.label2.Text = "label2";
            // 
            // Index
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.circularButton1);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.Icon);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.PUsers);
            this.ForeColor = System.Drawing.Color.Snow;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Index";
            this.Text = "Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Index_FormClosing);
            this.Load += new System.EventHandler(this.Index_Load);
            this.PUsers.ResumeLayout(false);
            this.PUsers.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel PUsers;
        private System.Windows.Forms.Panel panel2;
        private CircularButton Icon;
        private System.Windows.Forms.PictureBox pictureBox3;
        private CircularButton circularButton1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

