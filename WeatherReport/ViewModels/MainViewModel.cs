using WeatherReport.Models;
using System.Collections.ObjectModel;
using Windows.System;
using Windows.UI.ViewManagement;
using Windows.UI;
using Windows.Devices.Geolocation;
using System.Threading.Tasks;
using WeatherReport.ViewModels;

namespace WeatherReport.ViewModels
{
    public class MainViewModel
    {
        public ObservableCollection<Day> Days { get; set; } = new ObservableCollection<Day>();
        public ObservableCollection<Today> CurrentDay { get; set; } = new ObservableCollection<Today>();

        public string CoordinatesResult { get; set; }
        public string SearchQuery { get; set; }
        public string SearchResult { get; set; }
        public string SearchString { get; set; }
        public int DaysAmount { get; set; }

        public void SetTitleBarBackground()
        {
            var titleBar = ApplicationView.GetForCurrentView().TitleBar;
            titleBar.BackgroundColor = Color.FromArgb(255, 58, 117, 140);
            titleBar.ForegroundColor = Color.FromArgb(255, 241, 245, 247);
            titleBar.ButtonBackgroundColor = Color.FromArgb(255, 58, 117, 140);
            titleBar.ButtonForegroundColor = Color.FromArgb(255, 158, 187, 198);

            titleBar.ButtonInactiveBackgroundColor = Color.FromArgb(255, 58, 117, 140);
            titleBar.ButtonInactiveForegroundColor = Color.FromArgb(255, 158, 187, 198);
            titleBar.InactiveBackgroundColor = Color.FromArgb(255, 58, 117, 140);
            titleBar.InactiveForegroundColor = Color.FromArgb(255, 241, 245, 247);

        }

    }
}
