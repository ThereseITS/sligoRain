//
//
// This is a basic solution that will:
//
// (i) read in  the rainfall records from a file,
// (ii) count the number in each specified band,
// (iii) if it rained, add to the count for the weekday on which it rained, and to the total rain on that day
// (iv) print reports on the bands and the weekdays.
//
//
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
                            index = (int) date.DayOfWeek;   // day of week is 0..6. 
                            dayCounts[index]++;             // add times it rained on that day
                            dayAmounts[index] += rain;      // add amounts it rained on that day
                        }


                    }
                    else
                    {
                        Console.WriteLine("Invalid record: " + input);
                    }

                }

                // Print Reports

                DayRainReport(dayCounts,dayAmounts);

                OverallRainReport(bandCounts, bandBoundaries);



          
        }

        static void DayRainReport(int[] dayCounts, double[] dayAmounts)
        {
                Console.WriteLine($"\n\n{"Day",-20} {"Times Raining",-20}\t\t{"Amount of Rain",-20}\n");

                // report on rainfall for each day of the week.

                for (int i = 0; i < dayCounts.Length; i++)
                {
                    Console.WriteLine($"{Enum.GetName(typeof(DayOfWeek), i), -20}\t\t{dayCounts[i],-20}\t\t{dayAmounts[i],-20:F2}");

                }

        }
// 
// This method gets the band index. Note that the numbers here are hard coded.
// Think of ways that you could write this method to allow more flexibility in bands.
//
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
//
// This method makes the assumption that the lower band boundary is the lowest value
// that rainfall can take (here 0). How would you alter this for a metric like temperature which
// could have values <0 and also values > the highest band boundary?
//
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
