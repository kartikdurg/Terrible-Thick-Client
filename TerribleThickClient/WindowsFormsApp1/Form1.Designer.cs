using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Runtime.InteropServices;

namespace WindowsFormsApp1
{
    partial class Form1
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
        ///

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            string random_password = null;
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            random_password = new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());

            return random_password;
        }

        public static string Encryption_AES()
        {
            string StringToEncrypt = RandomString(16);

            // Registry...  
            string keyName = @"HKEY_CURRENT_USER";
            string subKey = @"AESEncrytion";
            string valueName = "AESKeyData";
            string valueName2 = "AppPassword";
            System.Security.Cryptography.DataProtectionScope dpScope = DataProtectionScope.CurrentUser;

            string key;
            int keysize = 128;

            // Create Encryption Key.  
            key = Form1.GenerateKey(keysize);

            // For additional security Pin the key.  
            GCHandle gch = GCHandle.Alloc(key, GCHandleType.Pinned);

            // Store key into registry, using DPAPI.  
            Form1.StoreKey(keyName, subKey, valueName, key, dpScope);

            // Encrypt the string.          
            string EncryptedHexString = Form1.Encrypt(StringToEncrypt, key, keysize);
            Form1.StoreKey(keyName, subKey, valueName2, EncryptedHexString, dpScope);

            // Decrypt the string.  
            string DecryptedHexString = Form1.Decrypt(EncryptedHexString, Form1.ReadKey(keyName, subKey, valueName),keysize);

            // Remove the Key from memory.   
            //ZeroMemory(gch.AddrOfPinnedObject(), key.Length * 2);
            
            return DecryptedHexString;

            gch.Free();
        }

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(220, 107);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Username:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(220, 151);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Password:";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(339, 155);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(270, 22);
            this.textBox2.TabIndex = 3;
            this.textBox2.Text = Form1.Encryption_AES();
            this.textBox2.UseSystemPasswordChar = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.Location = new System.Drawing.Point(334, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 25);
            this.label3.TabIndex = 4;
            this.label3.Text = "iamr00t";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(225, 196);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(384, 28);
            this.richTextBox1.TabIndex = 5;
            this.richTextBox1.Text = "I\'m somewhere in the registry and only you can decrypt it!";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(314, 293);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(172, 31);
            this.button1.TabIndex = 6;
            this.button1.Text = "Submit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(225, 248);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(384, 22);
            this.textBox1.TabIndex = 8;
            this.textBox1.Text = "Enter the password";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label3;
        private RichTextBox richTextBox1;
        private Button button1;
        public TextBox textBox1;
    }
}

