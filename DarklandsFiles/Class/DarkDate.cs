using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace DarklandsFiles
{
    public class DarkDate
    {
        private DarkDate()
        {
        }

        #region " static constructors ... "

        /// <summary>
        /// this reads the time, day, month, year
        /// </summary>
        public static DarkDate Create1(List<byte> bytes, int index)
        {
            var date = new DarkDate();
            date.Time = ShortHelper.ParseUInt(bytes, index);
            date.Day = ShortHelper.ParseUInt(bytes, index + 2);
            date.Month = ShortHelper.ParseUInt(bytes, index + 4);
            date.Year = ShortHelper.ParseUInt(bytes, index + 6);
            return date;
        }

        /// <summary>
        /// this reads the year, month, day, time
        /// </summary>
        public static DarkDate Create2(List<byte> bytes, int index)
        {
            var date = new DarkDate();
            date.Year = ShortHelper.ParseUInt(bytes, index);
            date.Month = ShortHelper.ParseUInt(bytes, index + 2);
            date.Day = ShortHelper.ParseUInt(bytes, index + 4);
            date.Time = ShortHelper.ParseUInt(bytes, index + 6);
            return date;
        }

        #endregion

        public int Time { get; private set; }
        public int Day { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public int Month { get; private set; }
        public int Year { get; private set; }

        public static DarkDate Empty = new DarkDate();

        public static bool IsValid(DarkDate date)
        {
            if (date.Time < 0 || date.Time > 24) return false;
            if (date.Day < 1 || date.Day > 31) return false;
            if (date.Month < 0 || date.Month > 12) return false;
            if (date.Year < 0 || date.Year > 2100) return false;
            return true;
        }

        public int MonthValue()
        {
            if (Month == 12) return 1;
            return (Month + 1);
        }

        public string MonthName()
        {
            return DateTimeFormatInfo.CurrentInfo.GetMonthName(MonthValue());
        }
        public override string ToString()
        {
            return Day + " " + MonthName() + " " + Year;
        }
        
        public    DateTime   ToDateTime  (  )
        {
            return new DateTime(Year, MonthValue(), Day);
        }

        public static TimeSpan operator -(DarkDate a, DarkDate b)
        { 
            return a.ToDateTime() - b.ToDateTime();
        }
    }
}  