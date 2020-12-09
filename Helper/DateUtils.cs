using System;

namespace Helper
{
    public static class DateUtils
    {
        public static DateTime NextWorkingDay(DateTime dateIn)
        {
            DateTime dateOut = dateIn;
            bool stop = false;

            while (!stop)
            {
                if (IfDayOff(dateOut))
                {
                    dateOut = dateOut.AddDays(1);
                    stop = false;
                }
                else
                {
                    stop = true;
                }
            }

            return dateOut;
        }

        public static DateTime Easter(int year)
        {
            int day = 0;
            int month = 0;
            int g = year % 19;
            int c = year / 100;
            int h = (c - (int)(c / 4) - (int)((8 * c + 13) / 25) + 19 * g + 15) % 30;
            int i = h - (int)(h / 28) * (1 - (int)(h / 28) * (int)(29 / (h + 1)) * (int)((21 - g) / 11));
            day = i - ((year + (int)(year / 4) + i + 2 - c + (int)(c / 4)) % 7) + 28;
            month = 3;
            if (day > 31)
            {
                month++;
                day -= 31;
            }
            return new DateTime(year, month, day);
        }

        public static bool IfDayOff(DateTime dateIn)
        {
            DateTime date = new DateTime(dateIn.Year, dateIn.Month, dateIn.Day);
            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
            {
                return true;
            }
            else
            {
                switch (date.Month)
                {
                    case 1:
                        if (date.Day == 1 || date.Day == 6)
                        {
                            return true;
                        }
                        break;

                    case 5:
                        if (date.Day == 1 || date.Day == 3)
                        {
                            return true;
                        }
                        break;

                    case 8:
                        if (date.Day == 15)
                        {
                            return true;
                        }
                        break;

                    case 11:
                        if (date.Day == 1 || date.Day == 11)
                        {
                            return true;
                        }
                        break;

                    case 12:
                        if (date.Day == 25 || date.Day == 26)
                        {
                            return true;
                        }
                        break;
                }

                if (date.Month >= 3 || date.Month <= 6)
                {
                    DateTime EasterDate = DateUtils.Easter(date.Year);

                    if (date == EasterDate || date == EasterDate.AddDays(1) || date == EasterDate.AddDays(49) || date == EasterDate.AddDays(60))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
