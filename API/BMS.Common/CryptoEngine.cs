using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Common
{
    public static class CryptoEngine
    {
        private static string EncryptionPrivateKey = "6B5970337336763979244226452948404D6351665468576D5A7134743777217A";
        public static string Encrypt(string input)
        {
            var tDESalg = new TripleDESCryptoServiceProvider();
            tDESalg.Key = new ASCIIEncoding().GetBytes(EncryptionPrivateKey.Substring(0, 16));
            tDESalg.IV = new ASCIIEncoding().GetBytes(EncryptionPrivateKey.Substring(8, 8));

            byte[] encryptedBinary = EncryptTextToMemory(input, tDESalg.Key, tDESalg.IV);
            return Convert.ToBase64String(encryptedBinary);
        }
        public static string Decrypt(string input)
        {
            var tDESalg = new TripleDESCryptoServiceProvider();
            tDESalg.Key = new ASCIIEncoding().GetBytes(EncryptionPrivateKey.Substring(0, 16));
            tDESalg.IV = new ASCIIEncoding().GetBytes(EncryptionPrivateKey.Substring(8, 8));

            byte[] buffer = Convert.FromBase64String(input);
            return DecryptTextFromMemory(buffer, tDESalg.Key, tDESalg.IV);
        }
        private static byte[] EncryptTextToMemory(string data, byte[] key, byte[] iv)
        {
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, new TripleDESCryptoServiceProvider().CreateEncryptor(key, iv), CryptoStreamMode.Write))
                {
                    byte[] toEncrypt = new UnicodeEncoding().GetBytes(data);
                    cs.Write(toEncrypt, 0, toEncrypt.Length);
                    cs.FlushFinalBlock();
                }

                return ms.ToArray();
            }
        }

        private static string DecryptTextFromMemory(byte[] data, byte[] key, byte[] iv)
        {
            using (var ms = new MemoryStream(data))
            {
                using (var cs = new CryptoStream(ms, new TripleDESCryptoServiceProvider().CreateDecryptor(key, iv), CryptoStreamMode.Read))
                {
                    var sr = new StreamReader(cs, new UnicodeEncoding());
                    return sr.ReadLine();
                }
            }
        }
    }
}
