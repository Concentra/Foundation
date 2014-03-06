using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Foundation.Infrastructure.Security
{
    public static class EncryptionService
    {
        private static readonly byte[] Iv = Encoding.UTF8.GetBytes("84fa@3FKas~34GWp");
        private static readonly byte[] Key = Encoding.UTF8.GetBytes("9*%1lfdAKBx~45fekS#2_gfGA&*gj)B4");

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability",
            "CA2000:Dispose objects before losing scope", Justification = "Checked"),
         System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage",
             "CA2202:Do not dispose objects multiple times", Justification = "Checked")]
        public static string Encrypt(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }

            var valueBytes = Encoding.UTF8.GetBytes(value);
            byte[] encryptedBytes;
            RijndaelManaged aesAlg = null;

            try
            {
                aesAlg = new RijndaelManaged {KeySize = 256, Padding = PaddingMode.PKCS7};
                var encryptor = aesAlg.CreateEncryptor(Key, Iv);

                using (var memoryStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(valueBytes, 0, valueBytes.Length);
                    }

                    encryptedBytes = memoryStream.ToArray();
                }
            }
            finally
            {
                if (aesAlg != null)
                {
                    aesAlg.Clear();
                }
            }

            return Convert.ToBase64String(encryptedBytes);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability",
            "CA2000:Dispose objects before losing scope", Justification = "Checked"),
         System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage",
             "CA2202:Do not dispose objects multiple times", Justification = "Checked")]
        public static string Decrypt(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }

            RijndaelManaged aesAlg = null;
            string plaintext;
            var encryptedBytes = Convert.FromBase64String(value);

            try
            {
                aesAlg = new RijndaelManaged {KeySize = 256, Padding = PaddingMode.PKCS7};
                var decryptor = aesAlg.CreateDecryptor(Key, Iv);

                using (var memoryStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(encryptedBytes, 0, encryptedBytes.Length);
                    }

                    plaintext = Encoding.UTF8.GetString(memoryStream.ToArray());
                }
            }
            finally
            {
                if (aesAlg != null)
                {
                    aesAlg.Clear();
                }
            }

            return plaintext;
        }
    }
}

