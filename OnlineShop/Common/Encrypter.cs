using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.Common
{
    public static  class Encrypter
    {
        //Mã Hóa MD5
        public static string MD5Hash(string txt)
        {
            String str = "";
            Byte[] buffer = System.Text.Encoding.UTF8.GetBytes(txt);
            System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            buffer = md5.ComputeHash(buffer);
            foreach (Byte b in buffer)
            {
                str += b.ToString("X2");
            }
            return str;
        }
    }
}