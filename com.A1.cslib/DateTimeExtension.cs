using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace com.A1.cslib
{
    [ExcludeFromCodeCoverage]
    public static class DateTimeExtension
    {
        public static DateTime[] holidays = {
            //******************************************************
            //-- VAKANTIES
            //******************************************************

            //Meivakantie
            //new DateTime(2015, 2, 5),
            //new DateTime(2015, 3, 5),
            //new DateTime(2015, 4, 5),
            //new DateTime(2015, 5, 5),
            //new DateTime(2015, 6, 5),
            //new DateTime(2015, 7, 5),
            //new DateTime(2015, 8, 5),
            //new DateTime(2015, 9, 5),
            //new DateTime(2015, 10, 5),

            //Other Meivakantie format
            new DateTime(2015, 5, 2),
            new DateTime(2015, 5, 3),
            new DateTime(2015, 5, 4),
            new DateTime(2015, 5, 5),
            new DateTime(2015, 5, 6),
            new DateTime(2015, 5, 7),
            new DateTime(2015, 5, 8),
            new DateTime(2015, 5, 9),
            new DateTime(2015, 5, 10),

            //Zomervakantie
            //new DateTime(2015, 18, 7),
            //new DateTime(2015, 19, 7),
            //new DateTime(2015, 20, 7),
            //new DateTime(2015, 21, 7),
            //new DateTime(2015, 22, 7),
            //new DateTime(2015, 23, 7),
            //new DateTime(2015, 24, 7),
            //new DateTime(2015, 25, 7),
            //new DateTime(2015, 26, 7),
            //new DateTime(2015, 27, 7),
            //new DateTime(2015, 28, 7),
            //new DateTime(2015, 29, 7),
            //new DateTime(2015, 30, 7),
            //new DateTime(2015, 1, 8),
            //new DateTime(2015, 2, 8),
            //new DateTime(2015, 3, 8),
            //new DateTime(2015, 4, 8),
            //new DateTime(2015, 5, 8),
            //new DateTime(2015, 6, 8),
            //new DateTime(2015, 7, 8),
            //new DateTime(2015, 8, 8),
            //new DateTime(2015, 9, 8),
            //new DateTime(2015, 10, 8),
            //new DateTime(2015, 11, 8),
            //new DateTime(2015, 12, 8),
            //new DateTime(2015, 13, 8),
            //new DateTime(2015, 14, 8),
            //new DateTime(2015, 15, 8),
            //new DateTime(2015, 16, 8),
            //new DateTime(2015, 17, 8),
            //new DateTime(2015, 18, 8),
            //new DateTime(2015, 19, 8),
            //new DateTime(2015, 20, 8),
            //new DateTime(2015, 21, 8),
            //new DateTime(2015, 22, 8),
            //new DateTime(2015, 23, 8),
            //new DateTime(2015, 24, 8),
            //new DateTime(2015, 25, 8),
            //new DateTime(2015, 26, 8),
            //new DateTime(2015, 27, 8),
            //new DateTime(2015, 28, 8),
            //new DateTime(2015, 29, 8),
            //new DateTime(2015, 30, 8),
                   
            //Other zomervakantie
            new DateTime(2015, 7, 18),
            new DateTime(2015, 7, 19),
            new DateTime(2015, 7, 20),
            new DateTime(2015, 7, 21),
            new DateTime(2015, 7, 22),
            new DateTime(2015, 7, 23),
            new DateTime(2015, 7, 24),
            new DateTime(2015, 7, 25),
            new DateTime(2015, 7, 26),
            new DateTime(2015, 7, 27),
            new DateTime(2015, 7, 28),
            new DateTime(2015, 7, 29),
            new DateTime(2015, 7, 30),
            new DateTime(2015, 8, 1),
            new DateTime(2015, 8, 2),
            new DateTime(2015, 8, 3),
            new DateTime(2015, 8, 4),
            new DateTime(2015, 8, 5),
            new DateTime(2015, 8, 6),
            new DateTime(2015, 8, 7),
            new DateTime(2015, 8, 8),
            new DateTime(2015, 8, 9),
            new DateTime(2015, 8, 10),
            new DateTime(2015, 8, 11),
            new DateTime(2015, 8, 12),
            new DateTime(2015, 8, 13),
            new DateTime(2015, 8, 14),
            new DateTime(2015, 8, 15),
            new DateTime(2015, 8, 16),
            new DateTime(2015, 8, 17),
            new DateTime(2015, 8, 18),
            new DateTime(2015, 8, 19),
            new DateTime(2015, 8, 20),
            new DateTime(2015, 8, 21),
            new DateTime(2015, 8, 22),
            new DateTime(2015, 8, 23),
            new DateTime(2015, 8, 24),
            new DateTime(2015, 8, 25),
            new DateTime(2015, 8, 26),
            new DateTime(2015, 8, 27),
            new DateTime(2015, 8, 28),
            new DateTime(2015, 8, 29),
            new DateTime(2015, 8, 30),

            //Herfstvakantie
            //new DateTime(2015, 24, 10),
            //new DateTime(2015, 25, 10),
            //new DateTime(2015, 26, 10),
            //new DateTime(2015, 27, 10),
            //new DateTime(2015, 28, 10),
            //new DateTime(2015, 29, 10),
            //new DateTime(2015, 30, 10),
            //new DateTime(2015, 31, 10),
            //new DateTime(2015, 1, 11),

            //Other Herfstvakantie
            new DateTime(2015, 10, 24),
            new DateTime(2015, 10, 25),
            new DateTime(2015, 10, 26),
            new DateTime(2015, 10, 27),
            new DateTime(2015, 10, 28),
            new DateTime(2015, 10, 29),
            new DateTime(2015, 10, 30),
            new DateTime(2015, 10, 31),
            new DateTime(2015, 11, 1),

            //Kerstvakantie
            //new DateTime(2015, 19, 12),
            //new DateTime(2015, 20, 12),
            //new DateTime(2015, 21, 12),
            //new DateTime(2015, 22, 12),
            //new DateTime(2015, 23, 12),
            //new DateTime(2015, 24, 12),
            //new DateTime(2015, 25, 12),
            //new DateTime(2015, 26, 12),
            //new DateTime(2015, 27, 12),
            //new DateTime(2015, 28, 12),
            //new DateTime(2015, 29, 12),
            //new DateTime(2015, 30, 12),
            //new DateTime(2015, 31, 12),
            //new DateTime(2016, 1, 1),
            //new DateTime(2016, 2, 1),
            //new DateTime(2016, 3, 1),

            //other Kerstvakantie
            new DateTime(2015, 12, 19),
            new DateTime(2015, 12, 20),
            new DateTime(2015, 12, 21),
            new DateTime(2015, 12, 22),
            new DateTime(2015, 12, 23),
            new DateTime(2015, 12, 24),
            new DateTime(2015, 12, 25),
            new DateTime(2015, 12, 26),
            new DateTime(2015, 12, 27),
            new DateTime(2015, 12, 28),
            new DateTime(2015, 12, 29),
            new DateTime(2015, 12, 30),
            new DateTime(2015, 12, 31),
            new DateTime(2016, 1, 1),
            new DateTime(2016, 1, 2),
            new DateTime(2016, 1, 3),

            //Voorjaarsvakantie
            //new DateTime(2016, 20, 2),
            //new DateTime(2016, 20, 2),
            //new DateTime(2016, 21, 2),
            //new DateTime(2016, 22, 2),
            //new DateTime(2016, 23, 2),
            //new DateTime(2016, 24, 2),
            //new DateTime(2016, 25, 2),
            //new DateTime(2016, 26, 2),
            //new DateTime(2016, 27, 2),
            //new DateTime(2016, 28, 2),

            //Other voorjaarsvakantie
            new DateTime(2016, 2, 20),
            new DateTime(2016, 2, 21),
            new DateTime(2016, 2, 22),
            new DateTime(2016, 2, 23),
            new DateTime(2016, 2, 24),
            new DateTime(2016, 2, 25),
            new DateTime(2016, 2, 26),
            new DateTime(2016, 2, 27),
            new DateTime(2016, 2, 28),

            //******************************************************
            //-- FEESTDAGEN
            //******************************************************

            //Goede Vrijdag
            //new DateTime(2015, 3, 4),
            new DateTime(2015, 4, 3),

            //Pasen
            //new DateTime(2015, 5, 4),
            //new DateTime(2015, 6, 4),
            new DateTime(2015, 4, 5),
            new DateTime(2015, 4, 6),

            //Koningsdag
            //new DateTime(2015, 27, 4),
            new DateTime(2015, 4, 27),

            //Hemelvaartsdag
            //new DateTime(2015, 14, 5),
            new DateTime(2015, 5, 14),

            //Pinksteren
            //new DateTime(2015, 24, 5),
            //new DateTime(2015, 25, 5)
            new DateTime(2015, 5, 24),
            new DateTime(2015, 5, 25)
        };

        public static bool IsHoliday(this DateTime date)
        {
            if(In(date,holidays)) {
                return true;
            }
            return false;
        }

        public static bool In<T>(this T t, params T[] values)
        {
            return values.Contains(t);
        }
    }
}
