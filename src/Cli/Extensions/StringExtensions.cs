using System.Text.Json;
using System.Text.RegularExpressions;

namespace Cli.Extensions
{
    public static class StringExtensions
    {
        private static readonly System.Text.Json.JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public static T FromJson<T>(this string json)
        {
            return JsonSerializer.Deserialize<T>(json, _jsonOptions);
        }
        public static string ToJson<T>(this T obj)
        {
            return JsonSerializer.Serialize(obj, _jsonOptions);
        }

        public static string RemoveAccent(this string txt)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(txt);
            return System.Text.Encoding.ASCII.GetString(bytes);
        }

        public static string GenerateSlug(this string phrase)
        {
            string str = phrase.RemoveAccent().ToLower();
            // invalid chars           
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space   
            str = Regex.Replace(str, @"\s+", " ").Trim();
            // cut and trim 
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
            str = Regex.Replace(str, @"\s", "-"); // hyphens   
            return str;
        }
    }
}
