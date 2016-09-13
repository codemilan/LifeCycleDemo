using Android.App;
using Android.OS;
using Android.Widget;
using System;

namespace WeatherApp.Droid
{
    [Activity (Label = "Sample Weather App.", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
            base.OnCreate(bundle);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            Button button = FindViewById<Button>(Resource.Id.weatherBtn);

            button.Click += Button_Click;
        }

        private async void Button_Click(object sender, EventArgs e)
        {
            EditText zipCodeEntry = FindViewById<EditText>(Resource.Id.zipCodeEntry);

            if (!String.IsNullOrEmpty(zipCodeEntry.Text))
            {
                Weather weather = await Core.GetWeather(zipCodeEntry.Text);
                FindViewById<TextView>(Resource.Id.locationText).Text = weather.Title;
                FindViewById<TextView>(Resource.Id.tempText).Text = weather.Temperature;
                FindViewById<TextView>(Resource.Id.windText).Text = weather.Wind;
                FindViewById<TextView>(Resource.Id.visibilityText).Text = weather.Visibility;
                FindViewById<TextView>(Resource.Id.humidityText).Text = weather.Humidity;
                FindViewById<TextView>(Resource.Id.sunriseText).Text = weather.Sunrise;
                FindViewById<TextView>(Resource.Id.sunsetText).Text = weather.Sunset;
            }else
            {

            }
        }
    }
}
