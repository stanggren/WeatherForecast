using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WeatherReport.DataProvider;
using WeatherReport.Models;
using System.Collections.ObjectModel;
using Windows.System;
using Windows.UI.ViewManagement;
using Windows.UI;
using Windows.Devices.Geolocation;
using System.Threading.Tasks;
using WeatherReport.ViewModels;
using Windows.Graphics.Display;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WeatherReport
{
    public sealed partial class MainPage : Page
    {
        private MainViewModel ViewModel;

        public MainPage()
        {
            ViewModel = new MainViewModel();
            this.InitializeComponent();
            APIHelper.InitilizeClient();
            ViewModel.SetTitleBarBackground();
        }

        private async void ChangedDays(object sender, SelectionChangedEventArgs e)
        {
            await Search();
        }

        private async void PressEnterSearch(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                await Search();
            }
        }

        private async void ClickLocation(object sender, TappedRoutedEventArgs e)
        {
            await getPosition();
        }


        private async Task getDays(string SearchString)
        {
            string stringCheck = "";

            if (SearchString.Length >= 4)
            {
                stringCheck = SearchString.Substring(0, 4);
            }
            else
            {
                stringCheck = SearchString;
            }

            if (stringCheck == "lat=")
            {
                ViewModel.SearchQuery = SearchString;
            }
            else
            {
                ViewModel.SearchQuery = $"city={SearchString}";
            }

            WeatherDataProvider wdp = new WeatherDataProvider();
            var days = await wdp.GetDays(ViewModel.SearchQuery);
            ViewModel.Days.Clear();
            ViewModel.CurrentDay.Clear();
            for (int i = 1; i < ViewModel.DaysAmount; i++)
            {
                ViewModel.Days.Add(days.Days[i]);
            }

            Today today = new Today();
            today.Day = days.Days[0];
            today.Name = days.Name;
            today.Timezone = days.Timezone;
            ViewModel.CurrentDay.Add(today);
            LoadingAnimation.Visibility = Visibility.Collapsed;
            LoadingLocation.Visibility = Visibility.Collapsed;
            WeatherContent.Visibility = Visibility.Visible;
        }

        public async Task Search()
        {

            if (!string.IsNullOrWhiteSpace(ViewModel.SearchString))
            {
                var SelectedDays = ComboBoxDays.SelectedItem as ComboBoxItem;
                ViewModel.DaysAmount = int.Parse(SelectedDays.Content.ToString());

                await WeatherLoading();

                ViewModel.SearchResult = ViewModel.SearchString;
                await getDays(ViewModel.SearchResult);
            } else if (ViewModel.CoordinatesResult != null)
            {
                var SelectedDays = ComboBoxDays.SelectedItem as ComboBoxItem;
                ViewModel.DaysAmount = int.Parse(SelectedDays.Content.ToString());

                await WeatherLoading();

                await getDays(ViewModel.CoordinatesResult);
            }

        }

        private async Task getPosition()
        {
            LocationLoading();

            var accessStatus = await Geolocator.RequestAccessAsync();
            switch (accessStatus)
            {
                case GeolocationAccessStatus.Allowed:
                    Geolocator geolocator = new Geolocator();

                    Geoposition pos = await geolocator.GetGeopositionAsync();

                    Coordinate coordinates = new Coordinate();

                    coordinates.Longitude = pos.Coordinate.Point.Position.Longitude.ToString();
                    coordinates.Latitude = pos.Coordinate.Point.Position.Latitude.ToString();

                    ViewModel.CoordinatesResult = $"lat={coordinates.Latitude}&lon={coordinates.Longitude}";


                    SearchBox.Text = "";

                    var SelectedDays = ComboBoxDays.SelectedItem as ComboBoxItem;
                    ViewModel.DaysAmount = int.Parse(SelectedDays.Content.ToString());

                    await getDays(ViewModel.CoordinatesResult);

                    break;

                case GeolocationAccessStatus.Denied:

                    break;
            }


        }

        private async Task WeatherLoading()
        {
            WelcomeMessage.Visibility = Visibility.Collapsed;
            WeatherContent.Visibility = Visibility.Collapsed;
            LoadingAnimation.Visibility = Visibility.Visible;
            await Task.Delay(1000);

        }

        private void LocationLoading()
        {
            WelcomeMessage.Visibility = Visibility.Collapsed;
            WeatherContent.Visibility = Visibility.Collapsed;
            LoadingLocation.Visibility = Visibility.Visible;
        }

        private void LocationHover(object sender, PointerRoutedEventArgs e)
        {
            LocationButton.Opacity = 0.40;
        }

        private void LocationHoverExited(object sender, PointerRoutedEventArgs e)
        {
            LocationButton.Opacity = 1;

        }

        private void GetHome(object sender, TappedRoutedEventArgs e)
        {
            WeatherContent.Visibility = Visibility.Collapsed;
            WelcomeMessage.Visibility = Visibility.Visible;
            SearchBox.Text = "";
        }

        private void HeaderHover(object sender, PointerRoutedEventArgs e)
        {
            Header.Opacity = 0.40;
        }

        private void HeaderHoverExited(object sender, PointerRoutedEventArgs e)
        {
            Header.Opacity = 1;
        }

        private void HeaderImageHover(object sender, PointerRoutedEventArgs e)
        {
            Logo.Opacity = 0.40;
        }

        private void HeaderImageHoverExited(object sender, PointerRoutedEventArgs e)
        {
            Logo.Opacity = 1;
        }

        private void CollapsedHeader(object sender, SizeChangedEventArgs e)
        {
            var bounds = ApplicationView.GetForCurrentView().VisibleBounds;
            var scaleFactor = DisplayInformation.GetForCurrentView().RawPixelsPerViewPixel;
            var size = new Size(bounds.Width * scaleFactor, bounds.Height * scaleFactor);

            if(size.Width < 970)
            {
                LocationText.Visibility = Visibility.Collapsed;
            }
            else if (size.Width > 970)
            {
                LocationText.Visibility = Visibility.Visible;
            }

            if(size.Width < 805)
            {
                ComboBoxText.Visibility = Visibility.Collapsed;
            }
            else if(size.Width > 805)
            {
                ComboBoxText.Visibility = Visibility.Visible;
            }

            if (size.Width < 755)
            {
                Header.Visibility = Visibility.Collapsed;
            }
            else if (size.Width > 755)
            {
                Header.Visibility = Visibility.Visible;
            }
        }

    }
}   
