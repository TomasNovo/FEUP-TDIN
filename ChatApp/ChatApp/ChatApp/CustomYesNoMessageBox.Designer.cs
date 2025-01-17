﻿namespace ChatApp
{
    partial class CustomYesNoMessageBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomYesNoMessageBox));
            this.BNo = new System.Windows.Forms.Button();
            this.BYes = new System.Windows.Forms.Button();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // BNo
            // 
            this.BNo.BackColor = System.Drawing.Color.RoyalBlue;
            this.BNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BNo.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.BNo.Location = new System.Drawing.Point(219, 241);
            this.BNo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BNo.Name = "BNo";
            this.BNo.Size = new System.Drawing.Size(95, 41);
            this.BNo.TabIndex = 12;
            this.BNo.Text = "No";
            this.BNo.UseVisualStyleBackColor = false;
            this.BNo.Click += new System.EventHandler(this.BNo_Click);
            // 
            // BYes
            // 
            this.BYes.BackColor = System.Drawing.Color.RoyalBlue;
            this.BYes.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BYes.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.BYes.Location = new System.Drawing.Point(53, 241);
            this.BYes.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BYes.Name = "BYes";
            this.BYes.Size = new System.Drawing.Size(95, 41);
            this.BYes.TabIndex = 11;
            this.BYes.Text = "Yes";
            this.BYes.UseVisualStyleBackColor = false;
            this.BYes.Click += new System.EventHandler(this.BYes_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(118, 2);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(100, 100);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox3.TabIndex = 9;
            this.pictureBox3.TabStop = false;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.Gray;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.SystemColors.Window;
            this.textBox1.Location = new System.Drawing.Point(51, 134);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(300, 93);
            this.textBox1.TabIndex = 13;
            this.textBox1.Text = "Text";
            // 
            // CustomYesNoMessageBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(363, 303);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.BNo);
            this.Controls.Add(this.BYes);
            this.Controls.Add(this.pictureBox3);
            this.Name = "CustomYesNoMessageBox";
            this.Text = "CustomMessageBox";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Button BYes;
        private System.Windows.Forms.Button BNo;
        private System.Windows.Forms.TextBox textBox1;
    }
}