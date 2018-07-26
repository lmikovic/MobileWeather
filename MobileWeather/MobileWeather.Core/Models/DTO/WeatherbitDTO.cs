namespace MobileWeather.Core.Models.DTO
{
    interface IWeatherbitCity
    {
        string city_name { get; set; }
        float lon { get; set; }
        float lat { get; set; }
        string country_code { get; set; }
    }

    public class WeatherbitDTO
    {
        public WeatherbitData[] data { get; set; }
        public int count { get; set; }
    }

    public class WeatherbitForecastDTO : IWeatherbitCity
    {
        public WeatherbitData[] data { get; set; }
        public string city_name { get; set; }
        public float lon { get; set; }
        public string timezone { get; set; }
        public float lat { get; set; }
        public string country_code { get; set; }
        public string state_code { get; set; }
    }

    public class WeatherbitData : IWeatherbitCity
    {
        public string wind_cdir { get; set; }
        public int rh { get; set; }
        public string pod { get; set; }
        public float lon { get; set; }
        public float pres { get; set; }
        public string timezone { get; set; }
        public string ob_time { get; set; }
        public string country_code { get; set; }
        public int clouds { get; set; }
        public float vis { get; set; }
        public string state_code { get; set; }
        public float wind_spd { get; set; }
        public float lat { get; set; }
        public string wind_cdir_full { get; set; }
        public float slp { get; set; }
        public string datetime { get; set; }
        public float ts { get; set; }
        public string station { get; set; }
        public float h_angle { get; set; }
        public float dewpt { get; set; }
        public float uv { get; set; }
        public float dni { get; set; }
        public float wind_dir { get; set; }
        public float elev_angle { get; set; }
        public float ghi { get; set; }
        public float dhi { get; set; }
        public float precip { get; set; }
        public string city_name { get; set; }
        public Weather weather { get; set; }
        public string sunset { get; set; }
        public float temp { get; set; }
        public string sunrise { get; set; }
        public float app_temp { get; set; }

        public int moonrise_ts { get; set; }
        public int sunset_ts { get; set; }
        public float ozone { get; set; }
        public float moon_phase { get; set; }
        public float wind_gust_spd { get; set; }
        public int snow_depth { get; set; }
        public int sunrise_ts { get; set; }
        public float app_min_temp { get; set; }
        public int pop { get; set; }
        public float app_max_temp { get; set; }
        public int snow { get; set; }
        public string valid_date { get; set; }
        public object max_dhi { get; set; }
        public float clouds_hi { get; set; }
        public float max_temp { get; set; }
        public int moonset_ts { get; set; }
        public float min_temp { get; set; }
        public float clouds_mid { get; set; }
        public float clouds_low { get; set; }
    }

    public class Weather
    {
        public string icon { get; set; }
        public string code { get; set; }
        public string description { get; set; }
    }
}
