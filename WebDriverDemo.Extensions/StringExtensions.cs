using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Text;

namespace WebDriverDemo.Extensions
{
    public static class StringExtensions
    {
        public static string AppendRandomCharacters(this string s, int length)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            var random = new Random();

            var output = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                output.Append(chars[random.Next(chars.Length)]);
            }

            return s + output.ToString();
        }
    }
}