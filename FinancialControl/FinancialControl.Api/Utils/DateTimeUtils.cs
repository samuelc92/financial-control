using System;

namespace FinancialControl.Api.Utils
{
    public static class DateTimeUtils
    {
        public static DateTime GetDateTimeNow() =>
            DateTime.UtcNow;

        public static DateTime GetDateNow() =>
            DateTime.UtcNow.Date;

        public static int GetCurrentYear() =>
            DateTime.UtcNow.Year;
        
        public static int GetCurrentMonth() =>
            DateTime.UtcNow.Month;
    }
}