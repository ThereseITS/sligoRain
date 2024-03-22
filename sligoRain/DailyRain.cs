using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sligoRain
{
    internal class DailyRain
    {
        DateOnly date;
        double rain;

        public DailyRain(DateOnly date, double rain)
        {
            this.date = date;
            this.rain = rain;
        }

        public override string ToString()
        {
            return $" {date} \t{rain}";
        }
    }
}
