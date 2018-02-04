using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DarklandsFiles
{
    class ShortHelper
    {
        public static int ParseUInt(List<byte> data, int index)
        {
            return data[index] + data[index + 1] * 256;
        }
        public static int ParseInt(List<byte> data, int index)
        {
            var value = ParseUInt(data, index);
            if(value > Int16.MaxValue )
            {
                value = value - UInt16.MaxValue   -1;
            }
            return value;
        } 

        public static void WriteUInt(List<byte> data, int index, int intValue )
        {
            if(intValue < 0)
            {
                //invert the value to a uint from int
                intValue = intValue + UInt16.MaxValue + 1;
            }

            var second = (byte)(intValue/256);
            data[index + 1] = second;
            data[index] = (byte)(intValue - (second*256));
        }
    }
}
