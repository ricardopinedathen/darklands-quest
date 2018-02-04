 
using System;
using System.Collections.Generic; 

namespace DarklandsFiles
{
    class ListHelper
    {
        /// <summary>
        /// 
        /// </summary>
        public static string GetString(List<byte> data2, int index,int count)
        {
            string str = string.Empty;
            for (int i = index; i < index + count; i++)
            {
                str += data2[i].ToString( "000") + ", ";
            }
            str += Environment.NewLine;
            return str;
        }

        /// <summary>
        /// finds a word in the list of bytes
        /// </summary>
        public static int Find(IList<byte> data, string findData)
        {
            var current = string.Empty;
            for (int i = 0; i < data.Count; i++)
            {
                var b = data[i];
                var c = (char)b;
                if (char.IsLetter(c) || c == ' ')
                {
                    current += c;
                    if (current == findData)
                    {
                        return i - findData.Length + 1;
                    }
                }
                else
                {
                    current = string.Empty;
                }
            }
            return -1;
        }
 
        /// <summary>
        /// reads string in the begging of the list of bytes
        /// </summary>
        public static string Read(IList<byte> data, int amount)
        {
            return Read(data, 0, amount);
        }
        public static string Read(IList<byte> data, int index, int count)
        {
            var str = string.Empty;
            var amount = index + count;
            for (int i = index; i < amount; i++)
            {
                var b = data[i];
                if (b != 0)
                {
                    var c = (char)data[i];
                    if (c == '|' )
                    {
                        c = 'u';
                    }
                    else if (c == '{')
                    {
                        c = 'o';
                    }
                    else if (b == 31)
                    {
                        c = '-';
                    }
                    else if (b == 28)
                    {
                        c = ' ';
                    }
                    if (!char.IsLetter(c) &&
                        !char.IsNumber(c) &&
                        b != 32 &&
                        b != 46 &&
                        b != 45 &&
                        b != 31 &&
                        b != 28 &&
                        b != 13)
                    {
                        throw new Exception("ahhhh");
                    }
                    str += c;
                }
            }
            return str.Trim();
        }
 
    }
}
