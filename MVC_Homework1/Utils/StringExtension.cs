using System;
using System.Security.Cryptography;
using System.Text;

namespace MVC_Homework1.Utils
{
    public static class StringExtension
    {
        public static string ToMD5Hash(this string source)
        {
            byte[] Original = Encoding.Default.GetBytes(source); //將字串來源轉為Byte[] 
            MD5 s1 = MD5.Create(); //使用MD5 
            byte[] Change = s1.ComputeHash(Original); //進行加密 
            //var result = Convert.ToBase64String(Change); //將加密後的字串從byte[]轉回string
            var result = BitConverter.ToString(Change).Replace("-", "").ToLower();

            return result;
        }
    }
}