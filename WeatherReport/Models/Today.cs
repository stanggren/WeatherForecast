using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherReport.Models
{
    public class Today
    {
        public string Name { get; set; }

        public string Timezone { get; set; }

        public Day Day { get; set; }
    }
}
