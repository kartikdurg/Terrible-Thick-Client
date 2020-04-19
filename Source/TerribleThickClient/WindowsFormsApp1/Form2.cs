using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using License;
using System.Globalization;

namespace WindowsFormsApp1
{
    public partial class TTC : Form
    {
        public TTC()
        {
            InitializeComponent();
        }

        public DateTime e_date;
        public string new_date;
        public DateTime dt;

        public void button1_Click(object sender, EventArgs e)
        {
            string filepath;
            string read;

            var today = DateTime.Now;
            var tomorrow = today.AddDays(1);
            string expiry = tomorrow.ToString("dd-MM-yyyy");

            OpenFileDialog openfile1 = new OpenFileDialog();
            if (openfile1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filepath = openfile1.FileName;
            }

            read = File.ReadAllText(openfile1.FileName);

            string decrypt = encrypt_n_decrypt(read,expiry);

            var regex = new Regex(@"\b\d{2}\-\d{2}-\d{4}\b");
            foreach (Match m in regex.Matches(decrypt))
            {
                //DateTime dt;
                if (DateTime.TryParseExact(m.Value, "dd-MM-yyyy", null, DateTimeStyles.None, out dt))
                {
                    e_date = dt;
                }
            }

            Console.WriteLine(e_date);
            //MessageBox.Show(decrypt);
            if (dt > DateTime.Now.AddDays(1))
            {
                new_date = e_date.ToString();
                this.textBox1.Text = "Expires on: "+new_date;
                MessageBox.Show("Activated successfuly!!");
            }
            else
                MessageBox.Show("Try harder...");

        }

        public string encrypt_n_decrypt(string fileread, string expiry)
        {
            string key = "!ThisIsTerrible!";
            
            Class1 License1 = new Class1();

            string plaintext = "Username: IamR00t\nExpiry Date:" + expiry;

            var encryptedString = License1.EncryptString(key, plaintext);
            
            var decryptedString = License1.DecryptString(key, fileread);

            return decryptedString;

        }
    }
}
