using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace BoardingHouse.Common.Helper
{
    public class StringHelper
    {
        /// <summary>
        /// Bo cac tag html, tra ve thuan text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        /// <Modified>
        /// Name     Date         Comments
        /// trungtq  2/9/2012   created
        /// </Modified>
        public static string StripHtml(string text)
        {
            string ret = string.Empty;
            try
            {
                string s = RemoveMultipleWhitespace(text.Trim());
                string strip = Regex.Replace(s, @"<(.|\n)*?>", string.Empty);

                if (!string.IsNullOrEmpty(strip))
                {
                    ret = strip;
                }
                else
                {
                    ret = EncodeHTMLTag(text);
                }
            }
            catch { }

            return ret;
        }

        /// <summary>
        /// Removes the script tag.
        /// </summary>
        /// <param name="strData">Doan text can remove</param>
        /// <returns></returns>
        /// <Modified>
        /// Name     Date         Comments
        /// trungtq  2/9/2012   created
        /// </Modified>
        public static string RemoveScriptTag(string strData)
        {
            strData = Regex.Replace(strData, "<script.*?</script>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            return strData;
        }

        /// <summary>
        /// Removes the HTML va javascript tag.
        /// </summary>
        /// <param name="text">Doan text can remove</param>
        /// <returns></returns>
        /// <Modified>
        /// Name     Date         Comments
        /// trungtq  2/9/2012   created
        /// </Modified>
        public static string RemoveHtmlJavascriptTag(string text)
        {
            string ret = "";
            try
            {
                text = HttpContext.Current.Server.HtmlDecode(text);
                Regex re = new Regex(@"<script\s[^>]*>.*?</script>|<\s*(?!/?(?:br?|i|p|u)\b[^>]*>)[^>]*>", RegexOptions.IgnoreCase | RegexOptions.Singleline);

                ret = re.Replace(text, string.Empty);
            }
            catch
            {
                text = "";
            }

            return ret;
        }

        /// <summary>
        /// Kiem tra xem doan text co chua cac the html ko?
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>
        ///   <c>true</c> doan ma co chua the html ; otherwise <c>false</c>.
        /// </returns>
        /// <Modified>
        /// Name     Date         Comments
        /// trungtq  2/9/2012   created
        /// </Modified>
        public static bool IsContainsHTMLTag(string text)
        {
            Regex regex = new Regex(@"<(.|\n)*?>", RegexOptions.IgnoreCase);

            return regex.IsMatch(text);
        }

        /// <summary>
        /// Encodes the HTML tag.
        /// </summary>
        /// <param name="text">Doan text can encode.</param>
        /// <returns></returns>
        /// <Modified>
        /// Name     Date         Comments
        /// trungtq  2/9/2012   created
        /// </Modified>
        public static string EncodeHTMLTag(string text)
        {
            if (text.IndexOf('<') >= 0)
            {
                return HttpUtility.HtmlEncode(text);
            }
            return text;
        }

        /// <summary>
        /// Replaces the single quotes.
        /// fix quotes for SQL insertion...
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        /// <Modified>
        /// Name     Date         Comments
        /// trungtq  15/8/2012   created
        /// </Modified>
        public static string ReplaceSingleQuotes(string text)
        {
            string result = string.Empty;
            if (string.IsNullOrEmpty(text))
            {
                return result;
            }
            result = text.Replace("'", "''").Trim();

            return result;
        }

        /// <summary>
        /// Removes multiple single quote ' characters from a string.
        /// </summary>
        /// <param name="text">
        /// </param>
        /// <returns>
        /// The remove multiple single quotes.
        /// </returns>
        public static string RemoveMultipleSingleQuotes(string text)
        {
            string result = String.Empty;
            if (String.IsNullOrEmpty(text))
            {
                return result;
            }

            var r = new Regex(@"\'");
            return r.Replace(text, @"'");
        }

        /// <summary>
        /// Removes multiple whitespace characters from a string.
        /// </summary>
        /// <param name="text">
        /// </param>
        /// <returns>
        /// The remove multiple whitespace.
        /// </returns>
        public static string RemoveMultipleWhitespace(string text)
        {
            string result = String.Empty;
            if (String.IsNullOrEmpty(text))
            {
                return result;
            }

            var r = new Regex(@"\s+");
            return r.Replace(text, @" ");
        }

        /// <summary>
        /// Bo tat ca khoang trang khoang trang trong chuoi truyen vao
        /// 29R2 7314 -> 29R27314
        /// </summary>
        /// <param name="text">
        /// </param>
        /// <returns>
        /// The remove multiple whitespace.
        /// </returns>
        public static string RemoveWhitespace(string text)
        {
            string result = String.Empty;
            if (String.IsNullOrEmpty(text))
            {
                return result;
            }

            var r = new Regex(@"\s+");
            return r.Replace(text, string.Empty);
        }

        /// <summary>
        /// Truncates a string with the specified limits and adds (...) to the end if truncated
        /// </summary>
        /// <param name="input">
        /// input string
        /// </param>
        /// <param name="limit">
        /// max size of string
        /// </param>
        /// <returns>
        /// truncated string
        /// </returns>
        public static string Truncate(string input, int limit)
        {
            string output = input;

            if (String.IsNullOrEmpty(input))
            {
                return null;
            }

            // Check if the string is longer than the allowed amount
            // otherwise do nothing
            if (output.Length > limit && limit > 0)
            {
                // cut the string down to the maximum number of characters
                output = output.Substring(0, limit);

                // Check if the space right after the truncate point 
                // was a space. if not, we are in the middle of a word and 
                // need to cut out the rest of it
                if (input.Substring(output.Length, 1) != " ")
                {
                    int lastSpace = output.LastIndexOf(" ");

                    // if we found a space then, cut back to that space
                    if (lastSpace != -1)
                    {
                        output = output.Substring(0, lastSpace);
                    }
                }

                // Finally, add the "..."
                output += "...";
            }

            return output;
        }

        /// <summary>
        /// Truncates a string with the specified limits by adding (...) to the middle
        /// </summary>
        /// <param name="input">
        /// input string
        /// </param>
        /// <param name="limit">
        /// max size of string
        /// </param>
        /// <returns>
        /// truncated string
        /// </returns>
        public static string TruncateMiddle(string input, int limit)
        {
            if (String.IsNullOrEmpty(input))
            {
                return null;
            }

            string output = input;
            const string middle = "...";

            // Check if the string is longer than the allowed amount
            // otherwise do nothing
            if (output.Length > limit && limit > 0)
            {
                // figure out how much to make it fit...
                int left = (limit / 2) - (middle.Length / 2);
                int right = limit - left - (middle.Length / 2);

                if ((left + right + middle.Length) < limit)
                {
                    right++;
                }
                else if ((left + right + middle.Length) > limit)
                {
                    right--;
                }

                // cut the left side
                output = input.Substring(0, left);

                // add the middle
                output += middle;

                // add the right side...
                output += input.Substring(input.Length - right, right);
            }

            return output;
        }

        /// <summary>
        /// Bo xung tag <br/> vao chuoi html
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="limit">The limit.</param>
        /// <returns></returns>
        /// <Modified>
        /// Name     Date         Comments
        /// trungtq  16/1/2013   created
        /// </Modified>
        public static string AppendBreak(string input, int limit)
        {
            string output = input;
            if (!string.IsNullOrEmpty(input))
            {
                string[] values = EnumerateByLength(input, limit).ToArray();
                if (values != null && values.Length > 1)
                {
                    return string.Join("<br/>", values);
                }
            }
            return output;
        }

        /// <summary>
        /// Tach chuoi theo do dai
        /// </summary>
        /// <param name="text"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        protected static IEnumerable<string> EnumerateByLength(string text, int length)
        {
            int index = 0;
            while (index < text.Length)
            {
                int charCount = Math.Min(length, text.Length - index);
                yield return text.Substring(index, charCount);
                index += length;
            }
        }

        /// <summary>
        /// Cắt khoảng trắng
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string TrimSpace(string str)
        {
            if (str == null) return string.Empty;
            string sT = str.TrimEnd().TrimStart();

            while (sT.Contains("  "))
            {
                sT = sT.Remove(sT.IndexOf("  "), 1);
            }
            return sT;
        }

        /// <summary>
        /// Lay ra ten va gia tri cua tung thuoc tinh cua doi tuong
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        /// <Modified>
        /// Name     Date         Comments
        /// trungtq  13/9/2012   created
        /// </Modified>
        public static string GetValueProperties<T>(T obj) where T : class
        {
            StringBuilder sb = new StringBuilder();

            PropertyInfo[] pros = obj.GetType().GetProperties();

            sb.AppendLine(string.Format("BEGIN:{0}", obj.GetType().Name.ToUpper()));
            foreach (PropertyInfo p in pros)
            {
                sb.AppendFormat("[{0}:{1}]", p.Name, p.GetValue(obj, null));
                sb.AppendLine();
            }
            sb.AppendLine(string.Format("END:{0}", obj.GetType().Name.ToUpper()));
            return sb.ToString();
        }

        /// <summary>
        /// Lay ra ten va gia tri cua cac thuoc tinh trong object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <returns></returns>
        /// <Modified>
        /// Name     Date         Comments
        /// trungtq  13/9/2012   created
        /// </Modified>
        public static string GetValueProperties<T>(List<T> list) where T : class
        {
            StringBuilder sb = new StringBuilder();
            if (list != null && list.Count > 0)
            {
                foreach (T item in list)
                {
                    sb.Append(GetValueProperties<T>(item));
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// Chuyen 1 chuoi tieng viet co dau thanh khong dau va co gach ngang.
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string ConvertToDashesVn(string input)
        {
            StringBuilder sb = new StringBuilder();

            char[] ca = input.Trim().ToCharArray();


            for (int i = 0; i < ca.Length; ++i)
            {
                char x = ConvertedVNChar(ca[i]);
                if (x != '@')
                    sb.Append(x);
            }

            return Regex.Replace(sb.ToString(), @"-+", "-").Trim('-').ToLower();

        }

        /// <summary>
        /// Chuyển một xâu tiếng Việt có dấu thành không dấu 
        /// longtq
        /// </summary>
        public static string ToVietnameseWithoutAccent(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"\p{IsCombiningDiacriticalMarks}+");
                string strFormD = text.Normalize(System.Text.NormalizationForm.FormD);
                return regex.Replace(strFormD, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
            }
            return text;
        }

        private static char ConvertedVNChar(char x)
        {
            if ((x >= 'a' && x <= 'z') || (x >= '0' && x <= '9') || (x >= 'A' && x <= 'Z'))
            {
                return x;
            }
            String s = x.ToString();

            if ("àáạảãâầấậẩẫăắằặẳẵ".Contains(s)) return 'a';
            if ("èéẹẻẽêềếệểễ".Contains(s)) return 'e';
            if ("ìíịỉĩ".Contains(s)) return 'i';
            if ("đ".Contains(s)) return 'd';
            if ("òóọỏõôồốộổỗơờớợởỡ".Contains(s)) return 'o';
            if ("ùúụủũưừứựửữ".Contains(s)) return 'u';
            if ("ỳýỵỷỹ".Contains(s)) return 'y';
            if ("ÀÁẠẢÃÂẦẤẬẨẪĂẮẰẶẲẴ".Contains(s)) return 'A';
            if ("ÈÉẸẺẼÊỀẾỆỂỄ".Contains(s)) return 'E';
            if ("ÌÍỊỈĨ".Contains(s)) return 'I';
            if ("Đ".Contains(s)) return 'D';
            if ("ÒÓỌỎÕÔỒỐỘỔỖƠỜỚỢỞỠ".Contains(s)) return 'O';
            if ("ÙÚỤỦŨƯỪỨỰỬỮ".Contains(s)) return 'U';
            if ("ỲÝỴỶỸ".Contains(s)) return 'Y';
            if (x == '\t' || x == ' ') return '-';
            if (@"_&*?(){}[]\|/+:'"";,.-".Contains(s)) return '-';

            return '@';
        }

        /// <summary>
        /// Kiểm tra xem chuỗi có phải là unicode ko
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        /// <Modified>
        /// Name     Date         Comments
        /// phongnc  7/27/2015   created
        /// </Modified>
        public static bool IsUnicode(string input)
        {
            var asciiBytesCount = Encoding.ASCII.GetByteCount(input);
            var unicodBytesCount = Encoding.UTF8.GetByteCount(input);
            return asciiBytesCount != unicodBytesCount;
        }

        /// <summary>
        /// Loại bỏ đơn vị trên lưới.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RemoveCurrencyUnit(string str)
        {
            return str.Replace("₫", string.Empty).Replace("$", string.Empty);
        }
    }
}
