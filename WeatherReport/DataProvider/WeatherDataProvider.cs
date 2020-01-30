using WeatherReport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Windows.Web;
using System.Net.Http;
using Windows.UI.Xaml;


namespace WeatherReport.DataProvider
{
    class WeatherDataProvider
    {
        public async Task<DayRoot> GetDays(string SearchString)
        {
            string URL = $"https://api.weatherbit.io/v2.0/forecast/daily?&{SearchString}&key=599311edd49e44b7b6ede9d3facab8bd";
            DayRoot Days = new DayRoot();
            using (HttpResponseMessage response = await APIHelper.ApiClient.GetAsync(URL))
            {

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<DayRoot>(result.Result);
                    foreach(var day in data.Days)
                    {
                        day.Weather.Icon = "Images/" + day.Weather.Icon + ".png";
                    }
                    Days = data;
                }
            }
            return Days;
        }
    }
}
