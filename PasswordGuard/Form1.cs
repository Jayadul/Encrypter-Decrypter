using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PasswordGuard
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string hash = "j@y@Du15huV0";

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            if (txtValue.Text == "")
            {
                System.Windows.Forms.MessageBox.Show("Value field can't be emply while trying to encrypt");
                (txtDecrypt.Text) = "";
            }
            else
            {
                byte[] data = UTF8Encoding.UTF8.GetBytes(txtValue.Text);
                using (MD5CryptoServiceProvider MD5 = new MD5CryptoServiceProvider())
                {
                    byte[] keys = MD5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                    using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                    {
                        ICryptoTransform transform = tripDes.CreateEncryptor();
                        byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                        txtEncrypt.Text = Convert.ToBase64String(results, 0, results.Length);
                    }
                }
            }
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            if (txtEncrypt.Text == "")
            {
                System.Windows.Forms.MessageBox.Show("Plese write an encrypted value in encrypt form to decrypt");
            }
            else
            {
                try
                {
                    byte[] data = Convert.FromBase64String(txtEncrypt.Text);
                    using (MD5CryptoServiceProvider MD5 = new MD5CryptoServiceProvider())
                    {
                        byte[] keys = MD5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                        using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                        {
                            ICryptoTransform transform = tripDes.CreateDecryptor();
                            byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                            txtDecrypt.Text = UTF8Encoding.UTF8.GetString(results);
                        }
                    }
                }
                catch
                {
                    System.Windows.Forms.MessageBox.Show("Entered value is not valid encrypted");
                }
                
            }
           
        }


    }
}
