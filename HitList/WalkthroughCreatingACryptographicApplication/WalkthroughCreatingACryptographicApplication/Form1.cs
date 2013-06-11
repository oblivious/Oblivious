using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;

namespace WalkthroughCreatingACryptographicApplication
{
    public partial class Form1 : Form
    {
        CspParameters cspp = new CspParameters();
        RSACryptoServiceProvider rsa;

        const string EncrFolder = @"D:\Oblivious\HitList\WalkthroughCreatingACryptographicApplication\Encrypt\";
        const string DecrFolder = @"D:\Oblivious\HitList\WalkthroughCreatingACryptographicApplication\Decrypt\";
        const string SrcFolder = @"D:\Oblivious\HitList\WalkthroughCreatingACryptographicApplication\Docs\";

        // Public key file
        const string PubKeyFile = @"D:\Oblivious\HitList\WalkthroughCreatingACryptographicApplication\Encrypt\rsaPublicKey.txt";

        // Key container name for
        // private/public key value pair.
        const string keyName = "Key01";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void buttonCreateAsmKeys_Click(object sender, EventArgs e)
        {
            // Stores a key pair in the key container.
            cspp.KeyContainerName = keyName;
            rsa = new RSACryptoServiceProvider(cspp);
            rsa.PersistKeyInCsp = true;
            if (rsa.PublicOnly)
                label1.Text = String.Format("Key: {0} - Public Only", cspp.KeyContainerName);
            else
                label1.Text = String.Format("Key: {0} - Full Key Pair", cspp.KeyContainerName);
        }

        private void buttonEncryptFile_Click(object sender, EventArgs e)
        {
            if (rsa == null)
            {
                MessageBox.Show("Key not set.");
            }
            else
            {
                // Display a dialog box to select a file to encrypt.
                openFileDialog1.InitialDirectory = SrcFolder;
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string fName = openFileDialog1.FileName;
                    if (fName != null)
                    {
                        FileInfo fInfo = new FileInfo(fName);
                        // Pass the file name without the path.
                        string name = fInfo.FullName;
                        EncryptFile(name);
                    }
                }
            }
        }

        private void buttonDecryptFile_Click(object sender, EventArgs e)
        {
            if (rsa == null)
            {
                MessageBox.Show("Key not set.");
            }
            else
            {
                // Display a dialog box to select the encrypted file.
                openFileDialog2.InitialDirectory = EncrFolder;
                if (openFileDialog2.ShowDialog() == DialogResult.OK)
                {
                    string fName = openFileDialog2.FileName;
                    if (fName != null)
                    {
                        FileInfo fi = new FileInfo(fName);
                        string name = fi.Name;
                        DecryptFile(name);
                    }
                }
            }
        }

        private void buttonGetPrivateKey_Click(object sender, EventArgs e)
        {
            cspp.KeyContainerName = keyName;

            rsa = new RSACryptoServiceProvider(cspp);
            rsa.PersistKeyInCsp = true;

            if (rsa.PublicOnly)
                label1.Text = "Key: " + cspp.KeyContainerName + " - Public Only";
            else
                label1.Text = "Key: " + cspp.KeyContainerName + " - Full Key Pair";
        }

        private void buttonExportPublicKey_Click(object sender, EventArgs e)
        {
            // Save the public key created by the RSA to a file. Caution, persisting the key to a file is a security risk.
            Directory.CreateDirectory(EncrFolder);
            StreamWriter sw = new StreamWriter(PubKeyFile, false);
            sw.Write(rsa.ToXmlString(false));
            sw.Close();
        }

        private void buttonImportPublicKey_Click(object sender, EventArgs e)
        {
            StreamReader sr = new StreamReader(PubKeyFile);
            cspp.KeyContainerName = keyName;
            rsa = new RSACryptoServiceProvider(cspp);
            string keyText = sr.ReadToEnd();
            rsa.FromXmlString(keyText);
            rsa.PersistKeyInCsp = true;
            if (rsa.PublicOnly)
                label1.Text = "Key: " + cspp.KeyContainerName + " - Public Only";
            else
                label1.Text = "Key: " + cspp.KeyContainerName + " - Full Key Pair";
            sr.Close();
        }

        private void EncryptFile(string inFile)
        {
            // Create instance of Rijndael for symmetric encryption of the data.
            RijndaelManaged rijndael = new RijndaelManaged();
            rijndael.KeySize = 256;
            rijndael.BlockSize = 256;
            rijndael.Mode = CipherMode.CBC;
            ICryptoTransform transform = rijndael.CreateEncryptor();

            // Use RSACryptoServiceProvider to encrypt the Rijndael key.
            // RSA is previously instantiated: rsa = new RSACryptoServiceProvider(cspp);
            byte[] keyEncrypted = rsa.Encrypt(rijndael.Key, false);

            // Create byte arrays to contain the length values of the key and IV.
            byte[] keyLength = new byte[4];
            byte[] IVLength = new byte[4];

            int lKey = keyEncrypted.Length;
            keyLength = BitConverter.GetBytes(lKey);
            int lIV = rijndael.IV.Length;
            IVLength = BitConverter.GetBytes(lIV);

            // Write the following to the FireStream for the encrypted file (outFS)
            // - length of the key
            // - length of the IV
            // - encrypted key
            // - the IV
            // - the encrypted cipher content

            int startFileName = inFile.LastIndexOf(@"\") + 1;
            // Change the file's extension to ".enc"
            string outFile = EncrFolder + inFile.Substring(startFileName, inFile.LastIndexOf(".") - startFileName) + ".enc";

            using (FileStream outFs = new FileStream(outFile, FileMode.Create))
            {
                outFs.Write(keyLength, 0, 4);
                outFs.Write(IVLength, 0, 4);
                outFs.Write(keyEncrypted, 0, lKey);
                outFs.Write(rijndael.IV, 0, lIV);

                // Now write the cipher text using a CryptoStream for encrypting.
                using (CryptoStream outStreamEncrypted = new CryptoStream(outFs, transform, CryptoStreamMode.Write))
                {
                    // By encrypting a chunk at a time, you can save memory and accommodate large files.
                    int count = 0;
                    int offset = 0;

                    // blockSizeBytes can be any arbitrary size.
                    int blockSizeBytes = rijndael.BlockSize / 8;
                    byte[] data = new byte[blockSizeBytes];
                    int bytesRead = 0;

                    using (FileStream inFs = new FileStream(inFile, FileMode.Open))
                    {
                        do
                        {
                            count = inFs.Read(data, 0, blockSizeBytes);
                            offset += count;
                            outStreamEncrypted.Write(data, 0, count);
                            bytesRead += blockSizeBytes;
                        }
                        while (count > 0);
                        inFs.Close();
                    }
                    outStreamEncrypted.FlushFinalBlock();
                    outStreamEncrypted.Close();
                }
                outFs.Close();
            }
        }

        private void DecryptFile(string inFile)
        {
            // Create instance of Rijndael for symmetric decryption of the data.
            RijndaelManaged rijndael = new RijndaelManaged();
            rijndael.KeySize = 256;
            rijndael.BlockSize = 256;
            rijndael.Mode = CipherMode.CBC;

            // Create byte arrays to get the length of the encrypted key and IV.
            // These values were stored as 4 bytes each at the beginning of the encrypted package.
            byte[] keyLength = new byte[4];
            byte[] IVLength = new byte[4];

            // Construct the file name for the decrypted file.
            string outFile = DecrFolder + inFile.Substring(0, inFile.LastIndexOf(".")) + ".txt";

            // Use FileStream objects to read the encrypted fil (inFs) and save the decrypted file (outFs).
            using (FileStream inFs = new FileStream(EncrFolder + inFile, FileMode.Open))
            {
                //inFs.Seek(0, SeekOrigin.Begin);
                //inFs.Seek(0, SeekOrigin.Begin); // Would reset not do the same? We just opened the file, why do we need to seek?
                inFs.Read(keyLength, 0, 4);
                //inFs.Seek(4, SeekOrigin.Begin);
                inFs.Read(IVLength, 0, 4);

                // Convert the lengths to integer values.
                int lenK = BitConverter.ToInt32(keyLength, 0);
                int lenIV = BitConverter.ToInt32(IVLength, 0);

                // Determine the start position of the cipher text and its length.
                int startC = lenK + lenIV + 8;
                int lenC = (int)inFs.Length - startC;

                // Create the byte arrays for the encrypted Rijndael key, the IV, and the cipher text.
                byte[] KeyEncrypted = new byte[lenK];
                byte[] IV = new byte[lenIV];

                // Extract the key and IV starting from index 8 after the length values.
                //inFs.Seek(8, SeekOrigin.Begin);
                inFs.Read(KeyEncrypted, 0, lenK);
                //inFs.Seek(8 + lenK, SeekOrigin.Begin);
                inFs.Read(IV, 0, lenIV);

                Directory.CreateDirectory(DecrFolder);

                // Use RSACryptoServiceProvider to decrypt the Rijndael key.
                byte[] keyDecrypted = rsa.Decrypt(KeyEncrypted, false);

                // Decrypt the key.
                ICryptoTransform transform = rijndael.CreateDecryptor(keyDecrypted, IV);

                // Decrypt the cipher text from the FileStream of the encrypted
                // file (inFs) into the FileStream for the decrypted file (outFs).
                using (FileStream outFs = new FileStream(outFile, FileMode.Create))
                {
                    int count = 0;
                    int offset = 0;

                    // blockSizeBytes can be any arbitrary size.
                    int blockSizeBytes = rijndael.BlockSize / 8;
                    byte[] data = new byte[blockSizeBytes];

                    // By decrypting a chunk a time, you can save memory and accomodate large files.

                    // Start at the beginning of the cipher text.
                    inFs.Seek(startC, SeekOrigin.Begin);
                    using (CryptoStream outStreamDecrypted = new CryptoStream(outFs, transform, CryptoStreamMode.Write))
                    {
                        do
                        {
                            count = inFs.Read(data, 0, blockSizeBytes);
                            offset += count;
                            outStreamDecrypted.Write(data, 0, count);
                        }
                        while (count > 0);

                        outStreamDecrypted.FlushFinalBlock();
                        outStreamDecrypted.Close();
                    }
                    outFs.Close();
                }
                inFs.Close();
            }
        }
    }
}
