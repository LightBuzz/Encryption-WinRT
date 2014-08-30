// Copyright (c) LightBuzz, Inc.
// All rights reserved.
//
// http://lightbuzz.com
//
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions
// are met:
//
// 1. Redistributions of source code must retain the above copyright
//    notice, this list of conditions and the following disclaimer.
//
// 2. Redistributions in binary form must reproduce the above copyright
//    notice, this list of conditions and the following disclaimer in the
//    documentation and/or other materials provided with the distribution.
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
// "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
// LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS
// FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE
// COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT,
// INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING,
// BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS
// OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED
// AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT
// LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY
// WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
// POSSIBILITY OF SUCH DAMAGE.

using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Security.Cryptography.DataProtection;
using Windows.Storage.Streams;

namespace LightBuzz.Encryption
{
    /// <summary>
    /// Implements the MD5 encryption algorithm.
    /// </summary>
    public sealed class MD5Encryption : IEncryption
    {
        /// <summary>
        /// The default encryption password.
        /// </summary>
        readonly string DEFAULT_PASSWORD = "Pa$$w0rd";

        /// <summary>
        /// Creates a new instance of the encryption provider.
        /// </summary>
        public MD5Encryption()
        {
            if (Password == null)
            {
                Password = DEFAULT_PASSWORD;
            }
        }

        /// <summary>
        /// The encryption password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Encrypts the specified text.
        /// </summary>
        /// <param name="text">The input string to encrypt.</param>
        /// <returns>The encrypted value of the specified input.</returns>
        public string Encrypt(string text)
        {
            try
            {
                CryptographicKey key = GenerateKey();
                IBuffer buffer = CryptographicBuffer.CreateFromByteArray(Encoding.UTF8.GetBytes(text));

                return CryptographicBuffer.EncodeToBase64String(CryptographicEngine.Encrypt(key, buffer, null));
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Decrypts the specified text.
        /// </summary>
        /// <param name="text">The input string to decrypt.</param>
        /// <returns>The decrypted value of the specified input.</returns>
        public string Decrypt(string text)
        {
            try
            {
                CryptographicKey key = GenerateKey();
                IBuffer buffer = CryptographicBuffer.DecodeFromBase64String(text);

                byte[] decrypted;
                CryptographicBuffer.CopyToByteArray(CryptographicEngine.Decrypt(key, buffer, null), out decrypted);

                return Encoding.UTF8.GetString(decrypted, 0, decrypted.Length);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Generates the cryptographic key.
        /// </summary>
        /// <returns>The cryptographic key.</returns>
        private CryptographicKey GenerateKey()
        {
            HashAlgorithmProvider algorithm = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Md5);
            CryptographicHash cryptographicHash = algorithm.CreateHash();
            SymmetricKeyAlgorithmProvider provider = SymmetricKeyAlgorithmProvider.OpenAlgorithm(SymmetricAlgorithmNames.AesEcbPkcs7);

            byte[] hash = new byte[32];
            cryptographicHash.Append(CryptographicBuffer.CreateFromByteArray(Encoding.UTF8.GetBytes(Password)));

            byte[] temp;
            CryptographicBuffer.CopyToByteArray(cryptographicHash.GetValueAndReset(), out temp);

            Array.Copy(temp, 0, hash, 0, 16);
            Array.Copy(temp, 0, hash, 15, 16);

            CryptographicKey key = provider.CreateSymmetricKey(CryptographicBuffer.CreateFromByteArray(hash));

            return key;
        }
    }
}