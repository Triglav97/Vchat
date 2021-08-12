using System;
using System.Text;

namespace Vchat.Data{
    public class GenerateId{

        string Words = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm";
        public string GenRandomString(int Length){
            Random rnd = new Random();
            StringBuilder sb = new StringBuilder(Length-1);
            int Position = 0;
            for (int i = 0; i < Length; i++){
                Position = rnd.Next(0, Words.Length-1);
                sb.Append(Words[Position]);                
            }
            return sb.ToString();
        }
    }
}