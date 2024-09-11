using System;
using System.Security.Cryptography;
using System.Text;

namespace Utilities
{
    public static class IdentityHelper
    {

        public static string HashPassword(string password)
        {
            byte[] staticSalt = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05 };
            byte[] salt;
            byte[] buffer2;
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }

        public static string EncodePasswordMd5(string pass)
        {
            Byte[] originalBytes;
            Byte[] encodedBytes;
            MD5 md5;
            md5 = new MD5CryptoServiceProvider();
            originalBytes = ASCIIEncoding.Default.GetBytes(pass);
            encodedBytes = md5.ComputeHash(originalBytes);
            //Convert encoded bytes back to a 'readable' string   
            var res = BitConverter.ToString(encodedBytes);
            res.Replace('1', '2');
            res.Replace('3', '7');
            res.Replace('a', '0');
            return res;
        }

        public static bool VerifyHashedPasswordMD5(string hashedPassword, string password)
        {
            Byte[] originalBytes;
            Byte[] encodedBytes;
            MD5 md5;
            md5 = new MD5CryptoServiceProvider();
            originalBytes = ASCIIEncoding.Default.GetBytes(password);
            encodedBytes = md5.ComputeHash(originalBytes);
            //Convert encoded bytes back to a 'readable' string   
            var res = BitConverter.ToString(encodedBytes);
            res.Replace('1', '2');
            res.Replace('3', '7');
            res.Replace('a', '0');

            return hashedPassword == res;
        }

        public static bool VerifyHashedPassword(string hashedPassword, string password)
        {
            byte[] buffer4;
            if (hashedPassword == null)
            {
                return false;
            }
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            byte[] src = Convert.FromBase64String(hashedPassword);
            if ((src.Length != 0x31) || (src[0] != 0))
            {
                return false;
            }
            byte[] dst = new byte[0x10];
            Buffer.BlockCopy(src, 1, dst, 0, 0x10);
            byte[] buffer3 = new byte[0x20];
            Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, dst, 0x3e8))
            {
                buffer4 = bytes.GetBytes(0x20);
            }
            return ByteArraysEqual(buffer3, buffer4);
        }


        private static bool ByteArraysEqual(byte[] b1, byte[] b2)
        {
            if (b1 == b2)
                return true;
            if (b1 == null || b2 == null)
                return false;
            if (b1.Length != b2.Length)
                return false;
            for (int i = 0; i < b1.Length; i++)
            {
                if (b1[i] != b2[i])
                    return false;
            }
            return true;
        }

        public static string Encrypt(long plainId)
        {
            //if (plainId < 1)
            //    return "";

            ControlUnit.InitializeProjectKey();
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = ControlUnit.Projectkey;
                aesAlg.IV = new byte[16]; // Set the Initialization Vector (IV) to a new byte array of 16 bytes
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                byte[] encrypted;
                using (System.IO.MemoryStream msEncrypt = new System.IO.MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (System.IO.StreamWriter swEncrypt = new System.IO.StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainId);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
                return Convert.ToBase64String(encrypted);
            }
        }

        public static string Encrypt(string plainText)
        {
            //if (string.IsNullOrEmpty(plainText))
            //    return "";

            ControlUnit.InitializeProjectKey();
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = ControlUnit.Projectkey;
                aesAlg.IV = new byte[16]; // Set the Initialization Vector (IV) to a new byte array of 16 bytes
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                byte[] encrypted;
                using (System.IO.MemoryStream msEncrypt = new System.IO.MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (System.IO.StreamWriter swEncrypt = new System.IO.StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
                return Convert.ToBase64String(encrypted);
            }
        }

        public static long Decrypt(string cipherText)
        {
            long plaintext = -1;

            if (string.IsNullOrEmpty(cipherText))
                return plaintext;
            try
            {
                ControlUnit.InitializeProjectKey();
                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = ControlUnit.Projectkey;
                    aesAlg.IV = new byte[16]; // Set the Initialization Vector (IV) to a new byte array of 16 bytes
                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                    byte[] cipherBytes = Convert.FromBase64String(cipherText);

                    using (System.IO.MemoryStream msDecrypt = new System.IO.MemoryStream(cipherBytes))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (System.IO.StreamReader srDecrypt = new System.IO.StreamReader(csDecrypt))
                            {
                                plaintext = Convert.ToInt64(srDecrypt.ReadToEnd());
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                return plaintext;
            }

            return plaintext;
        }

    }
}
