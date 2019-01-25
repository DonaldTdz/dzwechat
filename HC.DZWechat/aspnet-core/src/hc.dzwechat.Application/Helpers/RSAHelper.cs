﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace HC.DZWechat.Helpers
{
    /// <summary>
    /// RSA加解密 使用OpenSSL的公钥加密/私钥解密
    /// </summary>
    public class RSAHelper
    {
        private readonly RSA _privateKeyRsaProvider;
        private readonly RSA _publicKeyRsaProvider;
        private readonly HashAlgorithmName _hashAlgorithmName;
        private readonly Encoding _encoding;

        /// <summary>
        /// 实例化RSAHelper
        /// </summary>
        /// <param name="rsaType">加密算法类型 RSA SHA1;RSA2 SHA256 密钥长度至少为2048</param>
        /// <param name="encoding">编码类型</param>
        /// <param name="privateKey">私钥</param>
        /// <param name="publicKey">公钥</param>
        public RSAHelper(RSAType rsaType, Encoding encoding, string privateKey, string publicKey = null)
        {
            _encoding = encoding;
            if (!string.IsNullOrEmpty(privateKey))
            {
                _privateKeyRsaProvider = CreateRsaProviderFromPrivateKey(privateKey);
            }

            if (!string.IsNullOrEmpty(publicKey))
            {
                _publicKeyRsaProvider = CreateRsaProviderFromPublicKey(publicKey);
            }

            _hashAlgorithmName = rsaType == RSAType.RSA ? HashAlgorithmName.SHA1 : HashAlgorithmName.SHA256;
        }

        #region 使用私钥签名

        /// <summary>
        /// 使用私钥签名
        /// </summary>
        /// <param name="data">原始数据</param>
        /// <returns></returns>
        public string Sign(string data)
        {
            byte[] dataBytes = _encoding.GetBytes(data);

            var signatureBytes = _privateKeyRsaProvider.SignData(dataBytes, _hashAlgorithmName, RSASignaturePadding.Pkcs1);

            return Convert.ToBase64String(signatureBytes);
        }

        #endregion

        #region 使用公钥验证签名

        /// <summary>
        /// 使用公钥验证签名
        /// </summary>
        /// <param name="data">原始数据</param>
        /// <param name="sign">签名</param>
        /// <returns></returns>
        public bool Verify(string data, string sign)
        {
            byte[] dataBytes = _encoding.GetBytes(data);
            byte[] signBytes = Convert.FromBase64String(sign);

            var verify = _publicKeyRsaProvider.VerifyData(dataBytes, signBytes, _hashAlgorithmName, RSASignaturePadding.Pkcs1);

            return verify;
        }

        #endregion

        #region 解密
        /// <summary>
        /// 解密
        /// </summary>
        public string Decrypt(string cipherText)
        {
            if (_privateKeyRsaProvider == null)
            {
                throw new Exception("_privateKeyRsaProvider is null");
            }
            return Encoding.UTF8.GetString(_privateKeyRsaProvider.Decrypt(Convert.FromBase64String(cipherText), RSAEncryptionPadding.Pkcs1));
        }

        #endregion

        #region 加密
        /// <summary>
        /// 加密
        /// </summary>
        public string Encrypt(string text)
        {
            if (_publicKeyRsaProvider == null)
            {
                throw new Exception("_publicKeyRsaProvider is null");
            }
            return Convert.ToBase64String(_publicKeyRsaProvider.Encrypt(Encoding.UTF8.GetBytes(text), RSAEncryptionPadding.Pkcs1));
        }

        #endregion

        #region 使用私钥创建RSA实例

        public RSA CreateRsaProviderFromPrivateKey(string privateKey)
        {
            var privateKeyBits = Convert.FromBase64String(privateKey);

            var rsa = RSA.Create();
            var rsaParameters = new RSAParameters();

            using (BinaryReader binr = new BinaryReader(new MemoryStream(privateKeyBits)))
            {
                byte bt = 0;
                ushort twobytes = 0;
                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130)
                    binr.ReadByte();
                else if (twobytes == 0x8230)
                    binr.ReadInt16();
                else
                    throw new Exception("Unexpected value read binr.ReadUInt16()");

                twobytes = binr.ReadUInt16();
                if (twobytes != 0x0102)
                    throw new Exception("Unexpected version");

                bt = binr.ReadByte();
                if (bt != 0x00)
                    throw new Exception("Unexpected value read binr.ReadByte()");

                rsaParameters.Modulus = binr.ReadBytes(GetIntegerSize(binr));
                rsaParameters.Exponent = binr.ReadBytes(GetIntegerSize(binr));
                rsaParameters.D = binr.ReadBytes(GetIntegerSize(binr));
                rsaParameters.P = binr.ReadBytes(GetIntegerSize(binr));
                rsaParameters.Q = binr.ReadBytes(GetIntegerSize(binr));
                rsaParameters.DP = binr.ReadBytes(GetIntegerSize(binr));
                rsaParameters.DQ = binr.ReadBytes(GetIntegerSize(binr));
                rsaParameters.InverseQ = binr.ReadBytes(GetIntegerSize(binr));
            }

            rsa.ImportParameters(rsaParameters);
            return rsa;
        }

        #endregion

        #region 使用公钥创建RSA实例

        public RSA CreateRsaProviderFromPublicKey(string publicKeyString)
        {
            // encoded OID sequence for  PKCS #1 rsaEncryption szOID_RSA_RSA = "1.2.840.113549.1.1.1"
            byte[] seqOid = { 0x30, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01, 0x05, 0x00 };
            byte[] seq = new byte[15];

            var x509Key = Convert.FromBase64String(publicKeyString);

            // ---------  Set up stream to read the asn.1 encoded SubjectPublicKeyInfo blob  ------
            using (MemoryStream mem = new MemoryStream(x509Key))
            {
                using (BinaryReader binr = new BinaryReader(mem))  //wrap Memory Stream with BinaryReader for easy reading
                {
                    byte bt = 0;
                    ushort twobytes = 0;

                    twobytes = binr.ReadUInt16();
                    if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)
                        binr.ReadByte();    //advance 1 byte
                    else if (twobytes == 0x8230)
                        binr.ReadInt16();   //advance 2 bytes
                    else
                        return null;

                    seq = binr.ReadBytes(15);       //read the Sequence OID
                    if (!CompareBytearrays(seq, seqOid))    //make sure Sequence for OID is correct
                        return null;

                    twobytes = binr.ReadUInt16();
                    if (twobytes == 0x8103) //data read as little endian order (actual data order for Bit String is 03 81)
                        binr.ReadByte();    //advance 1 byte
                    else if (twobytes == 0x8203)
                        binr.ReadInt16();   //advance 2 bytes
                    else
                        return null;

                    bt = binr.ReadByte();
                    if (bt != 0x00)     //expect null byte next
                        return null;

                    twobytes = binr.ReadUInt16();
                    if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)
                        binr.ReadByte();    //advance 1 byte
                    else if (twobytes == 0x8230)
                        binr.ReadInt16();   //advance 2 bytes
                    else
                        return null;

                    twobytes = binr.ReadUInt16();
                    byte lowbyte = 0x00;
                    byte highbyte = 0x00;

                    if (twobytes == 0x8102) //data read as little endian order (actual data order for Integer is 02 81)
                        lowbyte = binr.ReadByte();  // read next bytes which is bytes in modulus
                    else if (twobytes == 0x8202)
                    {
                        highbyte = binr.ReadByte(); //advance 2 bytes
                        lowbyte = binr.ReadByte();
                    }
                    else
                        return null;
                    byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };   //reverse byte order since asn.1 key uses big endian order
                    int modsize = BitConverter.ToInt32(modint, 0);

                    int firstbyte = binr.PeekChar();
                    if (firstbyte == 0x00)
                    {   //if first byte (highest order) of modulus is zero, don't include it
                        binr.ReadByte();    //skip this null byte
                        modsize -= 1;   //reduce modulus buffer size by 1
                    }

                    byte[] modulus = binr.ReadBytes(modsize);   //read the modulus bytes

                    if (binr.ReadByte() != 0x02)            //expect an Integer for the exponent data
                        return null;
                    int expbytes = (int)binr.ReadByte();        // should only need one byte for actual exponent data (for all useful values)
                    byte[] exponent = binr.ReadBytes(expbytes);

                    // ------- create RSACryptoServiceProvider instance and initialize with public key -----
                    var rsa = RSA.Create();
                    RSAParameters rsaKeyInfo = new RSAParameters
                    {
                        Modulus = modulus,
                        Exponent = exponent
                    };
                    rsa.ImportParameters(rsaKeyInfo);

                    return rsa;
                }

            }
        }

        #endregion

        #region 导入密钥算法

        private int GetIntegerSize(BinaryReader binr)
        {
            byte bt = 0;
            int count = 0;
            bt = binr.ReadByte();
            if (bt != 0x02)
                return 0;
            bt = binr.ReadByte();

            if (bt == 0x81)
                count = binr.ReadByte();
            else
            if (bt == 0x82)
            {
                var highbyte = binr.ReadByte();
                var lowbyte = binr.ReadByte();
                byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };
                count = BitConverter.ToInt32(modint, 0);
            }
            else
            {
                count = bt;
            }

            while (binr.ReadByte() == 0x00)
            {
                count -= 1;
            }
            binr.BaseStream.Seek(-1, SeekOrigin.Current);
            return count;
        }

        private bool CompareBytearrays(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
                return false;
            int i = 0;
            foreach (byte c in a)
            {
                if (c != b[i])
                    return false;
                i++;
            }
            return true;
        }

        #endregion

        #region 系统默认秘钥

        public static string PrivateKeyRsa2 = "MIIEpAIBAAKCAQEAmRIZQvAXquj7INo9OtXIMX89mzE0zpE8WGSkvQCU6LTOqIA3VvPVpRVO7J3wwxZnW8j8jhkwnsssWgjv49I6y/T+QZp4tbnJHp1noZAKxhNTr4BQhXULpx5ix1juEglhyURF6X+edQIxXL/yaz1jd47EeNFDKsReThFGE2vm+BWN2B9beYTUi9PhGpeVe/TWYfOTzouNbMCJlbVEFNI5FwP58FySq9kuk7Mc3EdWDksnmCBDbJLG8GFded4bEvL42SAtuETB6PUesErmzWVj2tjbW/drPL9plHF/GbYLxOk/99GBHdwIhWs46QG683WWOYZAh/xT1M1PZ2QPmcG6swIDAQABAoIBACyWWwlmil5ccxpEt+U1dJUGbVmRYcJyB9PvrRTo4HdQg4oNnxETAb6OkGjYMNOC6SSRTQ/PQpxerlKjm80O5dWXTaCqcFLvBSiHzTIAlULRSmUqyUm2qGhLr5ZFz0MtvHA2FO9JH00SEHLl7qVSQnEHAy/2NMx2Wsn+uhzaJ3NH0LBMkuYaE/NFWyKfNezwVTVlk2UuYo8KmSIkvJNf18/HADdiyyZu2Cd4wfzznB1ji18TehPbxgklKWLKUYhz8nYZBgyfyO91y3T4nhgE1/RKCUeLRP+IOhq7tIpQoajzXbhIvtW06xQB9Mocg68cZtO8dOw6Y8ZX5pJqVc+Ir2ECgYEAxpfmCDiLBI3EgmU9JTLw1t4WLzpDNyOCOlNVKTgApgUVM++dHre9C0uMZM20Uahx3FI70WAZiinOMrjZ8ci5yJHDi4uKhwrUPK6/GwlonSiPwdZw4/vNomLggenn0FqKSG6g14jOTp25tASjKOEL4dSjfxiy2AYfOFMnI+kvm0sCgYEAxVF6snT3o6rKz89dRjO+Xt1vJpZZxhS2tCW7qO2n/LGOb1ddLRPrPIrXVorI26vT3bsBXoFwY1uZb5NYX24JFmWDygjROItgwa1iBb4Z50SXLe/+rRYfbmDvAQWTHtSlajlrklmK6baZLrDNouSu4AdmU8i3GScA8+L09W1hFTkCgYEAqbAU76VQs71m0XwruONEAnSVRBlmYXDHz4pw0910QGdLbKsq95pLv6D3/xH9J2DkmTryvb59VB7Qf2qPXxcCF6zlBxFednD8VDWEOVfauZ50502R/Fulo0EVUCxK1S7nxWgxqrjMf0Qp7vWfbEiO3JHSecDi386rUndKlFalRO0CgYEAotd5kSmi7fmX4yTZb4RoidXpU0kl0alqlwXE98caqhrG1/CbvwX/TzmuOYfT+Ca4dPdOS/Y7EGSaHlDVz/v5l/gzaVsZf7bXJT+389FNg2VB9vFnluT5D5kD5i35mqoX0bCNrHwlfnfpt3TGYLal89Fni6HMKf/cc2pMive39VECgYBGA6NEcGfJDBGFN6lajTo46TnF4p1ab7/o3q9AWRfjRLIZIL52B3njDNqvD5JvTA113FhikxWLDxd6HoSi5jX5KekkJE717ZX3MNUucbGjP1x8A6qupJWf6PJBQLdtfp2JMlYjmphoppKyHrMXa4s/IZTLjtpuED5cwOE0fDkeVw==";

        public static string PublicKeyRsa2 = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAmRIZQvAXquj7INo9OtXIMX89mzE0zpE8WGSkvQCU6LTOqIA3VvPVpRVO7J3wwxZnW8j8jhkwnsssWgjv49I6y/T+QZp4tbnJHp1noZAKxhNTr4BQhXULpx5ix1juEglhyURF6X+edQIxXL/yaz1jd47EeNFDKsReThFGE2vm+BWN2B9beYTUi9PhGpeVe/TWYfOTzouNbMCJlbVEFNI5FwP58FySq9kuk7Mc3EdWDksnmCBDbJLG8GFded4bEvL42SAtuETB6PUesErmzWVj2tjbW/drPL9plHF/GbYLxOk/99GBHdwIhWs46QG683WWOYZAh/xT1M1PZ2QPmcG6swIDAQAB";

        #endregion

    }

    /// <summary>
    /// RSA算法类型
    /// </summary>
    public enum RSAType
    {
        /// <summary>
        /// SHA1
        /// </summary>
        RSA = 0,
        /// <summary>
        /// RSA2 密钥长度至少为2048 推荐使用
        /// SHA256
        /// </summary>
        RSA2
    }
}
