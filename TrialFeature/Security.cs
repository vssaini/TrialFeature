using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Win32;

namespace TrialFeature
{
    /// <summary>
    /// This class is used to provide features as encrypt and decrypt plus to save dates in registry for license purpose.
    /// </summary>
    public static class Security
    {
        #region Global variables

        /// <summary>
        /// The registry path to save date when website was first used
        /// </summary>
        private const string FirstUseDateKey = @"HKEY_LOCAL_MACHINE\SOFTWARE\ADPhonebook\FirstUseDate";

        /// <summary>
        /// The registry path to save date when website was last used
        /// </summary>
        private const string LastUseDateKey = @"HKEY_LOCAL_MACHINE\SOFTWARE\ADPhonebook\LastUseDate";

        /// <summary>
        /// Pass phrase to be used in generating salt
        /// </summary>
        private const string PassPhrase =
            "MEQCIGVlfwiIYRTY8ipXTui/uQtwKz1rLudKbk9qp/gOJ1HRAiAlAvoqsDsZhMFh6DXCpGqVjqNel9dXoNDvPkqX4s6l5w==";

        /// <summary>
        /// The name of value for reading first use date from registry
        /// </summary>
        private const string FirstUseDateVal = "FirstUseDate";

        /// <summary>
        /// The name of value for reading last use date from registry
        /// </summary>
        private const string LastUseDateVal = "LastUseDate";

        #endregion

        #region Encrypt or Decrypt

        // This constant string is used as a "salt" value.
        // This size of the IV (in bytes) must = (keysize / 8).  Default keysize is 256, so the IV must be
        // 32 bytes long.  Using a 16 character string here gives us 32 bytes when converted to a byte array.
        private static readonly byte[] InitVectorBytes = Encoding.ASCII.GetBytes("tu89geji340t89u2");

        // This constant is used to determine the keysize of the encryption algorithm.
        private const int Keysize = 256;

        /// <summary>
        /// Encrypt plain text.
        /// </summary>
        public static string Encrypt(string plainText)
        {
            string cipherText;

            try
            {
                var plainTextBytes = Encoding.UTF8.GetBytes(plainText);

                using (var password = new Rfc2898DeriveBytes(PassPhrase, InitVectorBytes))
                {
                    var keyBytes = password.GetBytes(Keysize / 8);
                    using (var symmetricKey = new RijndaelManaged())
                    {
                        symmetricKey.Mode = CipherMode.CBC;
                        using (var encryptor = symmetricKey.CreateEncryptor(keyBytes, InitVectorBytes))
                        {
                            using (var memoryStream = new MemoryStream())
                            {
                                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                                {
                                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                                    cryptoStream.FlushFinalBlock();
                                    var cipherTextBytes = memoryStream.ToArray();
                                    cipherText = Convert.ToBase64String(cipherTextBytes);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                cipherText = null;
            }

            return cipherText;
        }

        /// <summary>
        /// Decrypt cipher text.
        /// </summary>
        public static string Decrypt(string cipherText)
        {
            string plainText;

            try
            {
                var cipherTextBytes = Convert.FromBase64String(cipherText);
                using (var password = new Rfc2898DeriveBytes(PassPhrase, InitVectorBytes))
                {
                    var keyBytes = password.GetBytes(Keysize / 8);
                    using (var symmetricKey = new RijndaelManaged())
                    {
                        symmetricKey.Mode = CipherMode.CBC;
                        using (var decryptor = symmetricKey.CreateDecryptor(keyBytes, InitVectorBytes))
                        {
                            using (var memoryStream = new MemoryStream(cipherTextBytes))
                            {
                                using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                                {
                                    var plainTextBytes = new byte[cipherTextBytes.Length];
                                    var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                                    plainText = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                plainText = null;
            }

            return plainText;
        }

        #endregion

        #region Save dates in registry

        /// <summary>
        /// Create registry key to hold first use date
        /// </summary>
        public static void SaveFirstUseDate(out string errorMessage)
        {
            string errMsg = null;
            try
            {
                using (var firstUseDateKey = Registry.CurrentUser.CreateSubKey(FirstUseDateKey))
                {
                    var keyValue = DateTime.Now.ToString("dd-MMM-yyyy");
                    var encryptDate = Encrypt(keyValue);

                    if (firstUseDateKey != null)
                        firstUseDateKey.SetValue("FirstUseDate", encryptDate);
                }
            }
            catch (Exception exc)
            {
                errMsg = exc.ToString();
            }

            errorMessage = errMsg;
        }

        /// <summary>
        /// Create registry key to hold last use date
        /// </summary>
        public static void SaveLastUseDate(out string errorMessage)
        {
            string errMsg = null;
            try
            {
                using (var lastUseDateKey = Registry.CurrentUser.CreateSubKey(LastUseDateKey))
                {
                    var keyValue = DateTime.Now.ToString("dd-MMM-yyyy");
                    var encryptDate = Encrypt(keyValue);

                    if (lastUseDateKey != null)
                        lastUseDateKey.SetValue("LastUseDate", encryptDate);
                }
            }
            catch (Exception exc)
            {
                errMsg = exc.ToString();
            }

            errorMessage = errMsg;
        }

        /// <summary>
        /// Get registry key based on date parameter passed
        /// </summary>
        public static string GetKeyValue(DateType dateUse)
        {
            string keyValue = null;

            switch (dateUse)
            {
                case DateType.FirstUse:
                    using (var registryKey = Registry.CurrentUser.OpenSubKey(FirstUseDateKey))
                    {
                        if (registryKey != null)
                            keyValue = Convert.ToString(registryKey.GetValue(FirstUseDateVal));
                    }
                    break;

                case DateType.LastUse:
                    using (var registryKey = Registry.CurrentUser.OpenSubKey(LastUseDateKey))
                    {
                        if (registryKey != null)
                            keyValue = Convert.ToString(registryKey.GetValue(LastUseDateVal));
                    }
                    break;
            }

            if (!string.IsNullOrEmpty(keyValue))
                keyValue = Decrypt(keyValue);

            return keyValue;
        }

        #endregion
    }

    /// <summary>
    /// Enum represents dates type.
    /// </summary>
    public enum DateType
    {
        /// <summary>
        /// The date on which software was started in use.
        /// </summary>
        FirstUse,
        /// <summary>
        /// The last date on which software was used.
        /// </summary>
        LastUse
    }
}