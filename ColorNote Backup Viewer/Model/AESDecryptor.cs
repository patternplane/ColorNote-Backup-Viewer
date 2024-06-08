using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace ColorNote_Backup_Viewer.Model
{
    public class AESDecryptor
    {
        static readonly byte[] FIXED_IV = new byte[16]; // 00000000....

        static readonly int NOTE_DATA_OFFSET = 28; // or 0 in last version
        static readonly string NOTE_DEFAULT_SALT = "ColorNote Fixed Salt";
        static readonly string NOTE_DEFAULT_PASSWORD = "0000";
        static readonly string NOTE_ENCRYPTED_DATA_KEY = "cDuEwf3DAcxeuX+nk7jAPl87CuQgfUD2rX0h5RGTizU="; // the Key value made by [PKCS12 PBKDF with SHA256 Hashing] from "ColorNote Password"

        static public byte[] decryptBackupFile(byte[] data, string passwordString)
        {
            byte[] salt = Encoding.UTF8.GetBytes(NOTE_DEFAULT_SALT);
            byte[] password = Encoding.UTF8.GetBytes(passwordString);
            byte[] defaultPassword = Encoding.UTF8.GetBytes(NOTE_DEFAULT_PASSWORD);

            byte[] result;
            if ((result = PBEWithMD5And128bitAES(password, salt, FIXED_IV, data, NOTE_DATA_OFFSET)) == null)
                if ((result = PBEWithMD5And128bitAES(password, salt, FIXED_IV, data)) == null)
                    if ((result = PBEWithMD5And128bitAES(defaultPassword, salt, FIXED_IV, data, NOTE_DATA_OFFSET)) == null)
                        if ((result = PBEWithMD5And128bitAES(defaultPassword, salt, FIXED_IV, data)) == null)
                            return null;
            return result;
        }

        static public byte[] decryptEncryptedNote(byte[] data)
        {
            return 
                PBEWithSHA256And256bitAES_needKey(
                    Convert.FromBase64String(NOTE_ENCRYPTED_DATA_KEY),
                    FIXED_IV,
                    data,
                    0);
        }

        /// <summary>
        /// BouncyCastle의 PBEWITHSHA256AND256BITAES-CBC-BC 알고리즘
        /// </summary>

        static private byte[] PBEWithSHA256And256bitAES_needKey(byte[] key, byte[] iv, byte[] encrypted, int offset = 0)
        {
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.KeySize = 256;
            aes.Key = key;
            aes.IV = FIXED_IV;
            aes.Padding = PaddingMode.PKCS7;
            try
            {
                return aes.CreateDecryptor(aes.Key, aes.IV).TransformFinalBlock(encrypted, offset, encrypted.Length - offset);
            }
            catch
            {
                // error - can't decrypt
                return null;
            }
        }

        /// <summary>
        /// BouncyCastle의 PBEWithMD5And128bitAES-CBC-OPENSSL 알고리즘
        /// </summary>
        static readonly int FIXED_INTERATION = 1; // only 1 in this Algorithm

        static private byte[] PBEWithMD5And128bitAES(byte[] password, byte[] salt, byte[] iv, byte[] encrypted, int offset = 0)
        {
            byte[] passwordAndSalt = new byte[password.Length + salt.Length];
            Array.Copy(password, 0, passwordAndSalt, 0, password.Length);
            Array.Copy(salt, 0, passwordAndSalt, password.Length, salt.Length);

            // PBE derived key With MD5
            MD5 hash = MD5.Create();
            for (int i = 0; i < FIXED_INTERATION; i++)
                passwordAndSalt = hash.ComputeHash(passwordAndSalt);

            // Decrypt
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.Key = passwordAndSalt;
            aes.IV = FIXED_IV;
            aes.Padding = PaddingMode.PKCS7;
            try
            {
                return aes.CreateDecryptor(aes.Key, aes.IV).TransformFinalBlock(encrypted, offset, encrypted.Length - offset);
            }
            catch
            {
                // error - can't decrypt
                return null;
            }
        }
    }
}
