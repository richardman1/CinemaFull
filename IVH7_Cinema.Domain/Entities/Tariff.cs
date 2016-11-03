using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.A1.cslib;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IVH7_Cinema.Domain.Entities {
    public class Tariff {

        [Key]
        public Int64 TariffID { get; set; }

        public String Name { get; set; }

        public String EnglishName { get; set; }

        public Double Price { get; set; }

        public Boolean DoesApply = false;

        public Double calculatePrice(Show show) {
            //If the duration of the movie is over 120 minutes, charge an additional fee of €0.50
            if (IsLongerThan120Minutes(show.Movie.Duration)) {
                Price += 0.50;
            }

            //If the show makes use of the normal Tariff
            if (Name.Equals("Normaal")) {
                DoesApply = true;
                return Price;
            }

            //If the show makes use of the child Tariff
            else if (Name.Equals("Kinderkorting")) {
                if (DateTime.Now.Hour < 18) {
                    bool containsKinderkorting = false;

                    IEnumerable<Genre> genres = show.Movie.Genres;
                    foreach (var g in genres)
                    {
                        System.Diagnostics.Debug.WriteLine("Tariff - CalculatePrice - Genres = " + g.Name);
                    }

                    foreach (Genre g in genres)
                    {
                        if (g.Name.Equals("Kinderfilm"))
                        {
                            containsKinderkorting = true;
                            System.Diagnostics.Debug.WriteLine("Tariff - CalculatePrice - Is een kinderfilm");
                        }
                    }

                    if (containsKinderkorting) {
                        bool containsNederlands = false;
                        foreach (Language l in show.Movie.Languages)
                        {
                            System.Diagnostics.Debug.WriteLine("Tariff - CalculatePrice - Languages = " + l.LanguageName);
                            if (l.LanguageName.Equals("Nederlands"))
                            {
                                containsNederlands = true;
                                System.Diagnostics.Debug.WriteLine("Tariff - CalculatePrice - Is nederlandstalig");
                            }
                        }

                        if (containsNederlands)
                        {
                            DoesApply = true;
                        }
                    }
                }
                return Price;
            }
            
            //If the show makes use of the student tariff
            else if (Name.Equals("Studentenkorting")) {
                if (DateTime.Now.DayOfWeek.ToString().Equals("Monday") ||
                    DateTime.Now.DayOfWeek.ToString().Equals("Tuesday") ||
                    DateTime.Now.DayOfWeek.ToString().Equals("Wednesday") ||
                    DateTime.Now.DayOfWeek.ToString().Equals("Thursday")
                    )
                {
                    DoesApply = true;
                }
                return Price;
            }

            //If the show makes use of the senior tariff
            else if (Name.Equals("65+ Reductie")) {
                if (DateTime.Now.DayOfWeek.ToString().Equals("Monday") ||
                    DateTime.Now.DayOfWeek.ToString().Equals("Tuesday") ||
                    DateTime.Now.DayOfWeek.ToString().Equals("Wednesday") ||
                    DateTime.Now.DayOfWeek.ToString().Equals("Thursday")
                    )
                {
                    if (!DateTime.Now.IsHoliday()) {
                        DoesApply = true;
                    }
                }
                return Price;
            }

            //If it's a 3D show, charge an additional fee of €1.50
            else if (Name.Equals("3D Bril"))
            {
                Price = 1.50;
                DoesApply = true;
                return Price;
            }

            //If it's a Popcorn Arrangement tariff
            else if (Name.Equals("Popcorn Arrangement"))
            {
                Price = 6.50;
                DoesApply = true;
                return Price;
            }

            return Price;
        }

        public bool IsLongerThan120Minutes(Int64 duration) {
            if (duration > 120) {
                return true;
            } else {
                return false;
            }
        }
    }
}
