namespace sligoRain
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = "../../../sligoRainfall.csv";
            string input = "";

            DateOnly date;
            double rain; 

//data for band report

            int[] bandBoundaries = { 0, 3, 6, 9, 12 };
            int[] bandCounts = new int[bandBoundaries.Length];

// data for day of week report

            int[] dayCounts = new int[7];
            double[] dayAmounts = new double[7];
            string[] days = { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };


            using (StreamReader sr = File.OpenText(path))
            {
                while ((input =sr.ReadLine())!=null)
                {
                    string[] fields = input.Split(',');

                    if((fields.Length ==2) && DateOnly.TryParse(fields[0], out date) && double.TryParse(fields[1], out rain) && rain>=0)
                    {
 
                        Console.WriteLine($"Date: {date}   Rainfall:  {rain}");

                        // add details for band report

                        int index = GetRainBandIndex(rain);
                        bandCounts[index]++;


                        // add details for day of week  report

                        if (rain > 0)  // there has been rain on that day
                        {
                            index = Array.IndexOf(days, date.DayOfWeek.ToString());
                            dayCounts[index]++;        // add times it rained on that day
                            dayAmounts[index] += rain; // add amounts it rained on that day
                        }


                    }
                    else
                    {
                        Console.WriteLine("Invalid record: " + input);
                    }

                }

                // Print Reports

                DayRainReport(dayCounts,days,dayAmounts);

                OverallRainReport(bandCounts, bandBoundaries);



          
        }

        static void DayRainReport(int[] dayCounts, string[] days, double[] dayAmounts)
        {
                Console.WriteLine($"\n\n{"Day",-20} {"Times Raining",-20}\t\t{"Amount of Rain",-20}\n");

                // report on rainfall in each day of week.

                for (int i = 0; i < days.Length; i++)
                {
                    Console.WriteLine($"{days[i],-20}\t\t{dayCounts[i],-20}\t\t{dayAmounts[i],-20:F2}");

                }

        }


            static int GetRainBandIndex(double rain)
            {
                switch (rain)
                {
                    case double r when r >= 0 && r < 3: return 0;
                    case double r when r >= 3 && r < 6: return 1;
                    case double r when r >= 6 && r < 9: return 2;
                    case double r when r >= 0 && r < 12: return 3;
                    case double r when r >= 12: return 4;
                    default: return -1;
                }
            }


            static void OverallRainReport(int[] bandCounts, int[] bandBoundaries)
            {

                //Report on how many days rainfall in each band.
                Console.WriteLine($"\n\nRain Report\n");


                for (int i = 0; i < bandCounts.Length - 1; i++)
                {
                    Console.WriteLine($"{bandBoundaries[i]}-{bandBoundaries[i + 1]}\t{bandCounts[i]}");
                }
                
                Console.WriteLine($" > {bandBoundaries[bandCounts.Length - 1]}\t{bandCounts[bandCounts.Length - 1]}");
            }

        }
 
    }
}
/*
 * 
          
 

     */