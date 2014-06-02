using System;
using System.Linq;
using System.Text;

namespace WebUI.Infrastructure
{
    public static class RandomUtil
    {
        private static readonly Random Random = new Random(DateTime.Now.Millisecond);

        public static string GetRandomString(int size)
        {
            var variants = "abcdefghijklmnopqrstuvwxyz0123456789";
            var str = new StringBuilder();
            for (var i = 0; i < size; i++)
            {
                str.Append(variants.ElementAt(Random.Next(variants.Length - 1)));
            }
            return str.ToString();
        }
        public static int GetRandomInt(int max)
        {
            return Random.Next(max);
        }
    }
}