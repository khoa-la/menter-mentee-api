using System;

namespace _2MC.BusinessTier.Commons
{
    public static class Utils
    {
        public static DateTime GetCurrentDateTime()
        {
            return DateTime.UtcNow.AddHours(7);
        }
    }
}