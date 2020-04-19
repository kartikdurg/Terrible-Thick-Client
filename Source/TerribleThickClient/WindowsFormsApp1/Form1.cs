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
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Win32;
using System.IO;
using System.Globalization;
using System.Runtime.InteropServices;

namespace WindowsFormsApp1
{
    public partial class TTC1 : Form
    {
        public TTC1()
        {
            InitializeComponent();
        }
        public static string ConvertByteToHex(byte[] byteData)
        {
            string hexValues = BitConverter.ToString(byteData).Replace("-", "");

            return hexValues;
        }
        public static byte[] ConvertHexToByteArray(string hexString)
        {
            byte[] byteArray = new byte[hexString.Length / 2];

            for (int index = 0; index < byteArray.Length; index++)
            {
                string byteValue = hexString.Substring(index * 2, 2);
                byteArray[index] = byte.Parse(byteValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            }

            return byteArray;
        }

        private static string GenerateKey(int iKeySize)
        {
            RijndaelManaged aesEncryption = new RijndaelManaged();
            aesEncryption.KeySize = iKeySize;
            aesEncryption.BlockSize = 128;
            aesEncryption.Mode = CipherMode.CBC;
            aesEncryption.Padding = PaddingMode.PKCS7;
            aesEncryption.GenerateIV();
            string ivStr = Convert.ToBase64String(aesEncryption.IV);
            aesEncryption.GenerateKey();
            string keyStr = Convert.ToBase64String(aesEncryption.Key);
            string completeKey = ivStr + "," + keyStr;

            return ConvertByteToHex(ASCIIEncoding.UTF8.GetBytes(completeKey));
        }

        private static string Encrypt(string iPlainStr, string iCompleteEncodedKey, int iKeySize)
        {
            RijndaelManaged aesEncryption = new RijndaelManaged();
            aesEncryption.KeySize = iKeySize;
            aesEncryption.BlockSize = 128;
            aesEncryption.Mode = CipherMode.CBC;
            aesEncryption.Padding = PaddingMode.PKCS7;
            aesEncryption.IV = Convert.FromBase64String(ASCIIEncoding.UTF8.GetString(ConvertHexToByteArray(iCompleteEncodedKey)).Split(',')[0]);
            aesEncryption.Key = Convert.FromBase64String(ASCIIEncoding.UTF8.GetString(ConvertHexToByteArray(iCompleteEncodedKey)).Split(',')[1]);
            byte[] plainText = ASCIIEncoding.UTF8.GetBytes(iPlainStr);
            ICryptoTransform crypto = aesEncryption.CreateEncryptor();
            byte[] cipherText = crypto.TransformFinalBlock(plainText, 0, plainText.Length);
            return ConvertByteToHex(cipherText);
        }
        public static string Decrypt(string iEncryptedText, string iCompleteEncodedKey, int iKeySize)
        {
            RijndaelManaged aesEncryption = new RijndaelManaged();
            aesEncryption.KeySize = iKeySize;
            aesEncryption.BlockSize = 128;
            aesEncryption.Mode = CipherMode.CBC;
            aesEncryption.Padding = PaddingMode.PKCS7;
            aesEncryption.IV = Convert.FromBase64String(ASCIIEncoding.UTF8.GetString(ConvertHexToByteArray(iCompleteEncodedKey)).Split(',')[0]);
            aesEncryption.Key = Convert.FromBase64String(ASCIIEncoding.UTF8.GetString(ConvertHexToByteArray(iCompleteEncodedKey)).Split(',')[1]);
            ICryptoTransform decrypto = aesEncryption.CreateDecryptor();
            byte[] encryptedBytes = ConvertHexToByteArray(iEncryptedText);
            string decrypted = ASCIIEncoding.UTF8.GetString(decrypto.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length));
            return decrypted;
        }


        public static void StoreKey(string keyName, string subKey, string valueName, string key, System.Security.Cryptography.DataProtectionScope dpScope)
        {
            // Turn string key into byte array.   
            byte[] keyAsBytes = UnicodeEncoding.ASCII.GetBytes(key);

            // Store key to protected byte array.  
            byte[] encryptedKeyPair = ProtectedData.Protect(keyAsBytes, null, dpScope);

            // Create a security context.  
            string user = Environment.UserDomainName + "\\" + Environment.UserName;
            RegistrySecurity security = new RegistrySecurity();
            RegistryAccessRule rule = new RegistryAccessRule(user
                                                            , RegistryRights.FullControl
                                                            , InheritanceFlags.ContainerInherit
                                                            , PropagationFlags.None
                                                            , AccessControlType.Allow);
            // Add rule to RegistrySecurity.  
            security.AddAccessRule(rule);

            // Create registry key and apply security context   
            Registry.CurrentUser.CreateSubKey(subKey, RegistryKeyPermissionCheck.ReadWriteSubTree, security);

            // Write the encrypted connection string into the registry  
            Registry.SetValue(keyName + @"\" + subKey, valueName, encryptedKeyPair);
        }

        public static string ReadKey(string keyName, string subKey, string valueName)
        {
            // Retrieve the keypair from the registry, decrypt using DPAPI  
            byte[] encryptedKeyPair = Registry.GetValue(keyName + @"\" + subKey, valueName, null) as byte[];

            // Unprotect data.  
            byte[] keyPairBytes = ProtectedData.Unprotect(encryptedKeyPair, null, DataProtectionScope.CurrentUser);

            // Convert to string.  
            string keyAsBytes = UnicodeEncoding.ASCII.GetString(keyPairBytes);

            // return key.  
            return keyAsBytes;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == textBox2.Text)
            {
                MessageBox.Show(" Humble!! :) ");

                TTC f2 = new TTC();
                f2.ShowDialog();
            }
            else
            {
                MessageBox.Show("Try harder..");
            }
            
        }

    }
}
