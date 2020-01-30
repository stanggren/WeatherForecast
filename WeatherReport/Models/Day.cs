using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WeatherReport.Models
{
    public class DayRoot
    {
        [JsonProperty("city_name")]
        public string Name { get; set; }

        [JsonProperty("timezone")]
        public string Timezone { get; set; }

        [JsonProperty("data")]
        public Day[] Days { get; set; }
    }
    public class Day
    {
        [JsonProperty("datetime")]
        public string Date { get; set; }

        [JsonProperty("temp")]
        public string Temperature { get; set; }

        [JsonProperty("max_temp")]
        public string MaxTemperature { get; set; }

        [JsonProperty("min_temp")]
        public string MinTemperature { get; set; }

        [JsonProperty("weather")]
        public Weather Weather { get; set; }
    }

    public class Weather
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }
    }
}
