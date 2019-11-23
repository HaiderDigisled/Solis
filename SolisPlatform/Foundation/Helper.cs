using Foundation.HelperModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Foundation
{
    public class Helper
    {
        public List<WeekRange> GetWeeklyRange(string StartDate, string EndDate)
        {

            List<WeekRange> dateranges = new List<WeekRange>();
            DateTime start = Convert.ToDateTime(StartDate);
            DateTime end = Convert.ToDateTime(EndDate);
            DateTime ProvidedEnd = end;
            TimeSpan difference = (end - start);

            if (StartDate.Equals(EndDate))
            {
                dateranges.Add(new WeekRange() { StartDate = StartDate, EndDate = EndDate });
                return dateranges;
            }

            if ((end - start).Days.Equals(1))
            {
                dateranges.Add(new WeekRange() { StartDate = StartDate, EndDate = EndDate });
                return dateranges;
            }

            if (end > start)
            {
                if (difference.Days <= 7)
                {
                    dateranges.Add(new WeekRange() { StartDate = start.ToString("yyyy-MM-dd"), EndDate = end.ToString("yyyy-MM-dd") });
                }

                if (difference.Days > 7)
                {
                    double totalWeeks = Math.Ceiling((double)difference.Days / 7);
                    for (int i = 1; i <= totalWeeks; i++)
                    {
                        if (i == 1)
                        {
                            end = start.AddDays(7);
                        }
                        else
                        {
                            start = end.AddDays(1);
                            if ((ProvidedEnd - start).Days < 7)
                            {
                                end = start.AddDays((ProvidedEnd - start).Days);
                            }
                            else
                            {
                                end = start.AddDays(7);
                            }

                        }
                        Console.WriteLine($"StartDate : {start.ToString("yyyy-MM-dd")} --- End Date : {end.ToString("yyyy-MM-dd")}");
                        dateranges.Add(new WeekRange() { StartDate = start.ToString("yyyy-MM-dd"), EndDate = end.ToString("yyyy-MM-dd") });
                    }
                }
            }

            return dateranges;
        }
        
    }
}
