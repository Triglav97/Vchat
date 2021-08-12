using Vchat.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using System;
using System.Text;

namespace Vchat.Data{
    public class CryptKey{
        // string KeyId;
        // string UserId;
        // string EncryptKey;
        // string DecryptKey;
        
        public CryptKeysModel CreateCryptKey(string name){
           
            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(2048);
            
            var par_prk = RSA.ExportParameters(true);
            var par_puk = RSA.ExportParameters(false);
            
            string puk = Key_to_string(par_puk); 
            string prk = Key_to_string(par_prk);
            
            CryptKeysModel person = new CryptKeysModel {
                        Id = name, encryptKey = puk, decryptKey = prk};

            // string test_mess = "привет";

            // var t = RSAEncrypt(test_mess, puk);
            // var test = RSADecrypt(t, prk);
            
            // Console.WriteLine("текст закодированный:-" + t +"\n" +
            //                   "текст раскодированный:-" + test);
            
            return person;
        }

        public string Key_to_string(RSAParameters Key){
            var sw = new System.IO.StringWriter();
            var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
            xs.Serialize(sw, Key);
            string KeyString = sw.ToString();
            return KeyString; 
        }

        public string Mess_to_string(byte[] mess){
            var sw = new System.IO.StringWriter();
            var xs = new System.Xml.Serialization.XmlSerializer(typeof(byte[]));
            xs.Serialize(sw, mess);
            string mess_String = sw.ToString();
            return mess_String; 
        }

        public byte[] Mess_to_byte(string mess){
            var sr = new System.IO.StringReader(mess);
            var xs = new System.Xml.Serialization.XmlSerializer(typeof(byte[]));
            byte[] Key_out;
            return Key_out = (byte[])xs.Deserialize(sr);
        }

        public RSAParameters Key_to_byte(string Key){
            var sr = new System.IO.StringReader(Key);
            var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
            RSAParameters Key_out;
            return Key_out = (RSAParameters)xs.Deserialize(sr);
        }

        public string RSAEncrypt(string message, string puk){
            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(2048);
            RSA.ImportParameters(Key_to_byte(puk));
            var text_encrypt = RSA.Encrypt(Encoding.Unicode.GetBytes(message), false);
            return Mess_to_string(text_encrypt);
        }

        public string RSADecrypt(string message, string prk){
            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(2048);
            RSA.ImportParameters(Key_to_byte(prk));
            var text_decrypt = RSA.Decrypt(Mess_to_byte(message), false);
            return Encoding.Unicode.GetString(text_decrypt);
        }
        // string Words = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm";
        // public string GenRandomString(string Alphabet, int Length){
        //     Random rnd = new Random();
        //     StringBuilder sb = new StringBuilder(Length-1);
        //     int Position = 0;
        //     for (int i = 0; i < Length; i++){
        //         Position = rnd.Next(0, Alphabet.Length-1);
        //         sb.Append(Alphabet[Position]);                
        //     }
        //     return sb.ToString();
        // }
        // public string RSAEncrypt(string message, string publickey, bool DoOAEPPadding){
        //     try{
        //         UnicodeEncoding ByteConverter = new UnicodeEncoding();

        //         byte[] mess = Encoding.Unicode.GetBytes(message);
        //         byte[] puk = Encoding.Unicode.GetBytes(publickey);
        //         string message_out;
                
        //         using(RSACryptoServiceProvider RSA = new RSACryptoServiceProvider()){
        //             RSA.ImportRSAPublicKey(puk, out _);
        //             message_out = Encoding.Unicode.GetString(RSA.Encrypt(mess, DoOAEPPadding));
        //         }

        //         return message_out;
        //     }
        //     catch (CryptographicException e){
        //         Console.WriteLine(e.Message);
        //         return null;
        //     }
        // }

        // public string RSADecrypt(string message, string privatekey, bool DoOAEPPadding){
        //     try{
        //         UnicodeEncoding ByteConverter = new UnicodeEncoding();

        //         byte[] mess = Encoding.Unicode.GetBytes(message);
        //         byte[] prk = Encoding.Unicode.GetBytes(privatekey);
        //         string message_out;

        //         using(RSACryptoServiceProvider RSA = new RSACryptoServiceProvider()){
        //             RSA.ImportRSAPrivateKey(prk, out _);
        //             message_out = Encoding.Unicode.GetString(RSA.Decrypt(mess, DoOAEPPadding));
        //         }
        //         return message_out;
        //     }
        //     catch (CryptographicException e){
        //         Console.WriteLine(e.ToString());
        //         return null;
        //     }
        // }
    }
}