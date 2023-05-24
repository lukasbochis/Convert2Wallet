using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convert2Wallet.Core
{
    public static class DateCreator
    {
        // Variable 'count' gibt an, wieviele Daten erstellt werden
        public static string[] CreateDates(int count)
        {
            LinkedList<string> dates = new LinkedList<string>();
            DateTime date = new DateTime(1900, 01, 01, 2, 15, 37);
            string tag = "datum;"; // Wird benötigt, um dem Model zu zeigen, um was für ein Feld es sich handelt

            for (int i = 0; i < count; i++)
            {                                            // Folgendes wird ausgegeben:
                dates.AddLast(tag + date.ToString("d")); // "01.01.2000"
                dates.AddLast(tag + date.ToString("g")); // "01.01.2000 02:15"
                dates.AddLast(tag + date.ToString("f")); // "Samstag, 1. Jänner 2000 02:15"
                dates.AddLast(tag + date.ToString("D")); // "Samstag, 1. Jänner 2000"
                dates.AddLast(tag + date.ToString("M")); // "1. Jänner"
                dates.AddLast(tag + date.ToString("R")); // "Sat, 01 Jan 2000 02:15:37 GMT"
                dates.AddLast(tag + date.ToString("t")); // "02:15"
                dates.AddLast(tag + date.ToString("u")); // "2000-01-01 02:15:37Z"
                dates.AddLast(tag + date.ToString("U")); // "Samstag, 1. Jänner 2000 01:15:37"
                dates.AddLast(tag + date.ToString("Y")); // "Jänner 2000"

                date = date.AddSeconds(1);
                date = date.AddMinutes(1);
                date = date.AddHours(1);
                date = date.AddDays(1);
                date = date.AddMonths(1);
                date = date.AddYears(1);

                if (date.Year >= 2100)
                    date = date.AddYears(-120);
            }

            // Schalttage hinzufügen
            dates.AddLast(tag + new DateTime(2004, 2, 29).ToShortDateString());
            dates.AddLast(tag + new DateTime(2008, 2, 29).ToShortDateString());
            dates.AddLast(tag + new DateTime(2024, 2, 29).ToShortDateString());
            dates.AddLast(tag + new DateTime(2016, 2, 29).ToShortDateString());

            dates.AddLast(tag + new DateTime(1996, 2, 29).ToShortDateString());
            dates.AddLast(tag + new DateTime(1988, 2, 29).ToShortDateString());
            dates.AddLast(tag + new DateTime(1976, 2, 29).ToShortDateString());
            dates.AddLast(tag + new DateTime(1960, 2, 29).ToShortDateString());

            return dates.ToArray();
        }
    }
}
