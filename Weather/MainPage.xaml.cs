using System.Windows.Input;

namespace Weather;

public partial class MainPage : ContentPage
{
	RestService _restService;

	public MainPage()
	{
		InitializeComponent();
		_restService = new RestService();
	}

    private async void OnGetWeatherButtonClicked(object sender, EventArgs e)
    {
		if(!string.IsNullOrWhiteSpace(_cityEntry.Text)) 
		{
			WeatherData weatherData = await 
				_restService.
				GetWeatherData(GenerateRequestURL(Constants.OpenWeatherMapEndpoint));

            weatherData.Weather[0].Icon = Constants.OpenWeatherMapImageEndpoint + weatherData.Weather[0].Icon + ".png";
			weatherData.Dt = weatherData.Dt + weatherData.Timezone;
			weatherData.Sys.Sunrise = weatherData.Sys.Sunrise + weatherData.Timezone;
			weatherData.Sys.Sunset = weatherData.Sys.Sunset + weatherData.Timezone;

            BindingContext = weatherData;

        }
    }

	string GenerateRequestURL(string endPoint)	
	{
		string requestUri = endPoint;
		requestUri += $"?q={_cityEntry.Text}";
		requestUri += "&units=metric";
		requestUri += $"&APPID={Constants.OpenWeatherMapAPIKey}";
		return requestUri;
	}
}

