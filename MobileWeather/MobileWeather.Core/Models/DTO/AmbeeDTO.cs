namespace MobileWeather.Core.Models.DTO
{
    public class AmbeeDTO
    {
        public string message { get; set; }
        public AmbeeCurrent data { get; set; }
    }

    public class AmbeeCurrent
    {
        public int time { get; set; }
        public float temperature { get; set; }
        public float apparentTemperature { get; set; }
        public float dewPoint { get; set; }
        public float humidity { get; set; }
        public float pressure { get; set; }
        public float windSpeed { get; set; }
        public float windGust { get; set; }
        public float windBearing { get; set; }
        public float cloudCover { get; set; }
        public float visibility { get; set; }
        public float ozone { get; set; }
        public float lat { get; set; }
        public float lng { get; set; }
    }

    public class AmbeeForecastDTO
    {
        public string message { get; set; }
        public AmbeeForecastData data { get; set; }
    }

    public class AmbeeForecastData
    {
        public float lat { get; set; }
        public float lng { get; set; }
        public AmbeeForecast[] forecast { get; set; }
    }

    public class AmbeeForecast
    {
        public int time { get; set; }
        public string summary { get; set; }
        public string icon { get; set; }
        public int sunriseTime { get; set; }
        public int sunsetTime { get; set; }
        public float moonPhase { get; set; }
        public float precipIntensity { get; set; }
        public float precipIntensityMax { get; set; }
        public int precipIntensityMaxTime { get; set; }
        public float precipProbability { get; set; }
        public string precipType { get; set; }
        public float temperatureHigh { get; set; }
        public int temperatureHighTime { get; set; }
        public float temperatureLow { get; set; }
        public int temperatureLowTime { get; set; }
        public float apparentTemperatureHigh { get; set; }
        public int apparentTemperatureHighTime { get; set; }
        public float apparentTemperatureLow { get; set; }
        public int apparentTemperatureLowTime { get; set; }
        public float dewPoint { get; set; }
        public float humidity { get; set; }
        public float pressure { get; set; }
        public float windSpeed { get; set; }
        public float windGust { get; set; }
        public int windGustTime { get; set; }
        public float windBearing { get; set; }
        public float cloudCover { get; set; }
        public int uvIndex { get; set; }
        public int uvIndexTime { get; set; }
        public float visibility { get; set; }
        public float ozone { get; set; }
        public float temperatureMin { get; set; }
        public int temperatureMinTime { get; set; }
        public float temperatureMax { get; set; }
        public int temperatureMaxTime { get; set; }
        public float apparentTemperatureMin { get; set; }
        public int apparentTemperatureMinTime { get; set; }
        public float apparentTemperatureMax { get; set; }
        public int apparentTemperatureMaxTime { get; set; }
    }
}
