using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Security.Cryptography;
using System.Text;

/// <summary>
/// Summary description for Constants
/// </summary>
public class Constants
{
    public static String user_session = "usersession";
    public static String voted_session = "votingsession";
    public static String send_mail_session = "send_mail";
    public static int approval_percentage = 60;
    public static String regex_valid_email = @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+(?:[A-Z]{2}|com|org|net|edu|gov|mil|biz|info|mobi|name|aero|asia|jobs|museum|za|co)\b";

    public static string CalculateSHA1(string text)
    {
        byte[] buffer = System.Text.Encoding.ASCII.GetBytes(text);
        SHA1CryptoServiceProvider cryptoTransformSHA1 = new SHA1CryptoServiceProvider();
        return BitConverter.ToString(cryptoTransformSHA1.ComputeHash(buffer)).Replace("-", "");
    }

    public static string RandomString(int size)
    {
        Random random = new Random((int)DateTime.Now.Ticks);

        StringBuilder builder = new StringBuilder();
        char ch;
        for (int i = 0; i < size; i++)
        {
            ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
            builder.Append(ch);
        }

        return builder.ToString();
    }
}