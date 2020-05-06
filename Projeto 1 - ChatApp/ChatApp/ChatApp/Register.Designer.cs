namespace ChatApp
{
    partial class Register
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Register));
            this.TBUsername = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.TBPass = new System.Windows.Forms.TextBox();
            this.BLogin = new System.Windows.Forms.Button();
            this.BRegister = new System.Windows.Forms.Button();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.TBPassConfirm = new System.Windows.Forms.TextBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.TBRealname = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.SuspendLayout();
            // 
            // TBUsername
            // 
            this.TBUsername.BackColor = System.Drawing.Color.Gray;
            this.TBUsername.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TBUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TBUsername.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.TBUsername.Location = new System.Drawing.Point(99, 196);
            this.TBUsername.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TBUsername.Name = "TBUsername";
            this.TBUsername.Size = new System.Drawing.Size(199, 21);
            this.TBUsername.TabIndex = 2;
            this.TBUsername.Text = "Enter Username";
            this.TBUsername.Enter += new System.EventHandler(this.TBUsername_Enter);
            this.TBUsername.Leave += new System.EventHandler(this.TBUsername_Leave);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(51, 221);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(256, 5);
            this.panel1.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(51, 185);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(42, 32);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(51, 231);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(43, 38);
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Location = new System.Drawing.Point(51, 273);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(256, 5);
            this.panel2.TabIndex = 4;
            // 
            // TBPass
            // 
            this.TBPass.BackColor = System.Drawing.Color.Gray;
            this.TBPass.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TBPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TBPass.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.TBPass.Location = new System.Drawing.Point(99, 248);
            this.TBPass.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TBPass.Name = "TBPass";
            this.TBPass.Size = new System.Drawing.Size(199, 21);
            this.TBPass.TabIndex = 3;
            this.TBPass.Text = "Enter Password";
            this.TBPass.Enter += new System.EventHandler(this.TBPass_Enter);
            this.TBPass.Leave += new System.EventHandler(this.TBPass_Leave);
            // 
            // BLogin
            // 
            this.BLogin.BackColor = System.Drawing.Color.Blue;
            this.BLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BLogin.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.BLogin.Location = new System.Drawing.Point(212, 398);
            this.BLogin.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BLogin.Name = "BLogin";
            this.BLogin.Size = new System.Drawing.Size(95, 41);
            this.BLogin.TabIndex = 6;
            this.BLogin.Text = "Login";
            this.BLogin.UseVisualStyleBackColor = false;
            this.BLogin.Click += new System.EventHandler(this.BLogin_Click);
            // 
            // BRegister
            // 
            this.BRegister.BackColor = System.Drawing.Color.RoyalBlue;
            this.BRegister.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BRegister.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.BRegister.Location = new System.Drawing.Point(51, 345);
            this.BRegister.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BRegister.Name = "BRegister";
            this.BRegister.Size = new System.Drawing.Size(256, 41);
            this.BRegister.TabIndex = 5;
            this.BRegister.Text = "Register";
            this.BRegister.UseVisualStyleBackColor = false;
            this.BRegister.Click += new System.EventHandler(this.BRegister_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(110, 11);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(100, 100);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox3.TabIndex = 8;
            this.pictureBox3.TabStop = false;
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.Color.Gray;
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox3.ForeColor = System.Drawing.SystemColors.Window;
            this.textBox3.Location = new System.Drawing.Point(51, 412);
            this.textBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(199, 14);
            this.textBox3.TabIndex = 9999;
            this.textBox3.TabStop = false;
            this.textBox3.Text = "Already have an account ?";
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(51, 282);
            this.pictureBox4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(43, 40);
            this.pictureBox4.TabIndex = 13;
            this.pictureBox4.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Location = new System.Drawing.Point(51, 326);
            this.panel3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(256, 5);
            this.panel3.TabIndex = 12;
            // 
            // TBPassConfirm
            // 
            this.TBPassConfirm.BackColor = System.Drawing.Color.Gray;
            this.TBPassConfirm.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TBPassConfirm.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TBPassConfirm.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.TBPassConfirm.Location = new System.Drawing.Point(99, 301);
            this.TBPassConfirm.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TBPassConfirm.Name = "TBPassConfirm";
            this.TBPassConfirm.Size = new System.Drawing.Size(199, 21);
            this.TBPassConfirm.TabIndex = 4;
            this.TBPassConfirm.Text = "Confirm Password";
            this.TBPassConfirm.Enter += new System.EventHandler(this.TBPassConfirm_Enter);
            this.TBPassConfirm.Leave += new System.EventHandler(this.TBPassConfirm_Leave);
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox5.Image")));
            this.pictureBox5.Location = new System.Drawing.Point(51, 137);
            this.pictureBox5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(42, 32);
            this.pictureBox5.TabIndex = 16;
            this.pictureBox5.TabStop = false;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.Location = new System.Drawing.Point(51, 173);
            this.panel4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(256, 5);
            this.panel4.TabIndex = 15;
            // 
            // TBRealname
            // 
            this.TBRealname.BackColor = System.Drawing.Color.Gray;
            this.TBRealname.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TBRealname.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TBRealname.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.TBRealname.Location = new System.Drawing.Point(99, 148);
            this.TBRealname.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TBRealname.Name = "TBRealname";
            this.TBRealname.Size = new System.Drawing.Size(208, 21);
            this.TBRealname.TabIndex = 1;
            this.TBRealname.Text = "Enter Real Name";
            this.TBRealname.Enter += new System.EventHandler(this.TBRealname_Enter);
            this.TBRealname.Leave += new System.EventHandler(this.TBRealname_Leave);
            // 
            // Register
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(340, 450);
            this.Controls.Add(this.pictureBox5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.TBRealname);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.TBPassConfirm);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.BRegister);
            this.Controls.Add(this.BLogin);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.TBPass);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.TBUsername);
            this.Controls.Add(this.textBox3);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Register";
            this.Text = "Register";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Register_FormClosing);
            this.Load += new System.EventHandler(this.Register_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TBUsername;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox TBPass;
        private System.Windows.Forms.Button BLogin;
        private System.Windows.Forms.Button BRegister;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox TBPassConfirm;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox TBRealname;
    }
}