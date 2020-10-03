
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace PlayTube.Controls
{
    public class Functions
    {
        //========================= Variables =========================
        public static Random random = new Random();

        //========================= Functions =========================

        //creat new Random String Session 
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXdsdaawerthklmnbvcxer46gfdsYZ0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        //creat new Random Color
        public static string RandomColor()
        {
            string color = "";
            int b;
            b = random.Next(1, 11);
            switch (b)
            {
                case 5:
                {
                    color = "#314d74"; //dark blue
                }
                    break;
                case 1:
                {

                    color = "#404040"; //dark gray
                }
                    break;
                case 2:
                {
                    color = "#146c7c"; // nele
                }
                    break;
                case 4:
                {
                    color = "#789c74"; //dark green
                }
                    break;
                case 6:
                {
                    color = "#cc8237"; //brown
                }
                    break;
                case 7:
                {
                    color = "#c37887"; //light red
                }
                    break;
                case 8:
                {
                    color = "#f2e032"; //yellow
                }
                    break;
                case 9:
                {
                    color = "#7646ff"; //purple
                }
                    break;
                case 3:
                {
                    color = "#cc635c"; //red
                }
                    break;
                case 10:
                {
                    color = "#20cef5"; //Light blue
                }
                    break;

            }
            return color;
        }

        public static string GetoLettersfromString(string key)
        {
            try
            {
                var string1 = key.Split(' ').First();
                var string2 = key.Split(' ').Last();

                if (string1 != string2)
                {
                    String substring1 = string1.Substring(0, 1);
                    String substring2 = string2.Substring(0, 1);
                    var result = substring1 + substring2;
                    return result.ToUpper();
                }
                else
                {
                    String substring1 = string1.Substring(0, 2);

                    var result = substring1;
                    return result.ToUpper();
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
                return "";
            }
        }

        //SubString Cut Of
        public static string SubStringCutOf(string s, int x)
        {
            try
            {
                String substring = s.Substring(0, x);
                return substring + "...";
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
                return s;
            }
        }

        //Null Remover >> return Empty
        public static string StringNullRemover(string s)
        {
            try
            {
                if (string.IsNullOrEmpty(s))
                {
                    s = "Empty";
                }
                return s;
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
                return s;
            }
        }

        //De code
        public static string DecodeString(string Content)
        {
            try
            {
                var Decoded = System.Net.WebUtility.HtmlDecode(Content);
                var String_formater = Decoded.Replace(":)", "\ud83d\ude0a")
                    .Replace(";)", "\ud83d\ude09")
                    .Replace("0)", "\ud83d\ude07")
                    .Replace("(<", "\ud83d\ude02")
                    .Replace(":D", "\ud83d\ude01")
                    .Replace("*_*", "\ud83d\ude0d")
                    .Replace("(<", "\ud83d\ude02")
                    .Replace("<3", "\ud83d\u2764")
                    .Replace("/_)", "\ud83d\ude0f")
                    .Replace("-_-", "\ud83d\ude11")
                    .Replace(":-/", "\ud83d\ude15")
                    .Replace(":*", "\ud83d\ude18")
                    .Replace(":_p", "\ud83d\ude1b")
                    .Replace(":p", "\ud83d\ude1c")
                    .Replace("x(", "\ud83d\ude20")
                    .Replace("X(", "\ud83d\ude21")
                    .Replace(":_(", "\ud83d\ude22")
                    .Replace("<5", "\ud83d\u2B50")
                    .Replace(":0", "\ud83d\ude31")
                    .Replace("B)", "\ud83d\ude0e")
                    .Replace("o(", "\ud83d\ude27")
                    .Replace("</3", "\uD83D\uDC94")
                    .Replace(":o", "\ud83d\ude26")
                    .Replace("o(", "\ud83d\ude27")
                    .Replace(":__(", "\ud83d\ude2d")
                    .Replace("!_", "\uD83D\u2757")

                    .Replace("<br> <br>", " ")
                    .Replace("<br>", " ")
                    .Replace("<a href=", "")
                    .Replace("target=", "")
                    .Replace("_blank", "")
                    .Replace(@"""", "")
                    .Replace("</a>", "")
                    .Replace("class=hash", "")
                    .Replace("rel=nofollow>", "")
                    .Replace("<p>", "")
                    .Replace("</p>", "")
                    .Replace("</body>", "")
                    .Replace("<body>", "")
                    .Replace("<div>", "")
                    .Replace("<div ", "")
                    .Replace("</div>", "")
                    .Replace("<iframe", "")
                    .Replace("</iframe>", "")
                    .Replace("<table", "")
                    .Replace("</table>", " ")
                    .Replace("\r\n", " ")
                    .Replace("\n", " ")
                    .Replace("\r", " ");

                return String_formater;
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
                return "";
            }
        }

        public static string DecodeStringWithEnter(string Content)
        {
            try
            {
                var Decoded = System.Net.WebUtility.HtmlDecode(Content);
                var String_formater = Decoded.Replace(":)", "\ud83d\ude0a")
                    .Replace(";)", "\ud83d\ude09")
                    .Replace("0)", "\ud83d\ude07")
                    .Replace("(<", "\ud83d\ude02")
                    .Replace(":D", "\ud83d\ude01")
                    .Replace("*_*", "\ud83d\ude0d")
                    .Replace("(<", "\ud83d\ude02")
                    .Replace("<3", "\ud83d\u2764")
                    .Replace("/_)", "\ud83d\ude0f")
                    .Replace("-_-", "\ud83d\ude11")
                    .Replace(":-/", "\ud83d\ude15")
                    .Replace(":*", "\ud83d\ude18")
                    .Replace(":_p", "\ud83d\ude1b")
                    .Replace(":p", "\ud83d\ude1c")
                    .Replace("x(", "\ud83d\ude20")
                    .Replace("X(", "\ud83d\ude21")
                    .Replace(":_(", "\ud83d\ude22")
                    .Replace("<5", "\ud83d\u2B50")
                    .Replace(":0", "\ud83d\ude31")
                    .Replace("B)", "\ud83d\ude0e")
                    .Replace("o(", "\ud83d\ude27")
                    .Replace("</3", "\uD83D\uDC94")
                    .Replace(":o", "\ud83d\ude26")
                    .Replace("o(", "\ud83d\ude27")
                    .Replace(":__(", "\ud83d\ude2d")
                    .Replace("!_", "\uD83D\u2757")

                    .Replace("<br> <br>", "\r\n")
                    .Replace("<br>", "\n")
                    .Replace("<a href=", "")
                    .Replace("target=", "")
                    .Replace("_blank", "")
                    .Replace(@"""", "")
                    .Replace("</a>", "")
                    .Replace("class=hash", "")
                    .Replace("rel=nofollow>", "")
                    .Replace("<p>", "")
                    .Replace("</p>", "")
                    .Replace("</body>", "")
                    .Replace("<body>", "")
                    .Replace("<div>", "")
                    .Replace("<div ", "")
                    .Replace("</div>", "")
                    .Replace("<iframe", "")
                    .Replace("</iframe>", "")
                    .Replace("<table", "")
                    .Replace("</table>", " ")
                    .Replace("/", "\n")
                    .Replace(@"\", "\n");

                return String_formater;
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
                return "";
            }

        }

        //Functions Replace Time
        public static string ReplaceTime(string time)
        {
            if (time.Contains("hours ago"))
            {
                time = time.Replace("hours ago", "H");
            }
            else if (time.Contains("days ago"))
            {
                time = time.Replace("days ago", "D");
            }
            else if (time.Contains("month ago"))
            {
                time = time.Replace("month ago", "M");
            }
            else if (time.Contains("months ago"))
            {
                time = time.Replace("months ago", "M");
            }
            else if (time.Contains("day ago"))
            {
                time = time.Replace("day ago", "D");
            }
            else if (time.Contains("minutes ago"))
            {
                time = time.Replace("minutes ago", "Min");
            }
            else if (time.Contains("seconds ago"))
            {
                time = time.Replace("seconds ago", "Sec");
            }
            else if (time.Contains("hour ago"))
            {
                time = time.Replace("hour ago", "H");
            }
            return time;
        }

        //String format numbers to millions, thousands with rounding
        public static string FormatPriceValue(int num)
        {
            try
            {
                if (num >= 100000000)
                {
                    return ((num >= 10050000 ? num - 500000 : num) / 1000000D).ToString("#M");
                }
                if (num >= 10000000)
                {
                    return ((num >= 10500000 ? num - 50000 : num) / 1000000D).ToString("0.#M");
                }
                if (num >= 1000000)
                {
                    return ((num >= 1005000 ? num - 5000 : num) / 1000000D).ToString("0.##M");
                }
                if (num >= 100000)
                {
                    return ((num >= 100500 ? num - 500 : num) / 1000D).ToString("0.k");
                }
                if (num >= 10000)
                {
                    return ((num >= 10550 ? num - 50 : num) / 1000D).ToString("0.#k");
                }

                return num >= 1000 ? ((num >= 1005 ? num - 5 : num) / 1000D).ToString("0.##k") : num.ToString("#,0");
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
                return num.ToString();
            }
        }


        //########################## End Class Functions Application PlayTube Version 1.0 ##########################
    }
}