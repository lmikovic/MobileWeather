using System;

namespace MobileWeather.Core.Models.DTO
{
    public class AccuweatherDTO
    {
        public DateTime LocalObservationDateTime { get; set; }
        public int EpochTime { get; set; }
        public string WeatherText { get; set; }
        public int WeatherIcon { get; set; }
        public bool HasPrecipitation { get; set; }
        public object PrecipitationType { get; set; }
        public bool IsDayTime { get; set; }
        public Value Temperature { get; set; }
        public Value RealFeelTemperature { get; set; }
        public Value RealFeelTemperatureShade { get; set; }
        public int RelativeHumidity { get; set; }
        public int IndoorRelativeHumidity { get; set; }
        public Value DewPoint { get; set; }
        public Wind Wind { get; set; }
        public Windgust WindGust { get; set; }
        public int UVIndex { get; set; }
        public string UVIndexText { get; set; }
        public Value Visibility { get; set; }
        public string ObstructionsToVisibility { get; set; }
        public int CloudCover { get; set; }
        public Value Ceiling { get; set; }
        public Value Pressure { get; set; }
        public Pressuretendency PressureTendency { get; set; }
        public Value Past24HourTemperatureDeparture { get; set; }
        public Value ApparentTemperature { get; set; }
        public Value WindChillTemperature { get; set; }
        public Value WetBulbTemperature { get; set; }
        public Value Precip1hr { get; set; }
        public Precipitationsummary PrecipitationSummary { get; set; }
        public Temperaturesummary TemperatureSummary { get; set; }
        public string MobileLink { get; set; }
        public string Link { get; set; }
    }

    public class Value
    {
        public Metric Metric { get; set; }
        public Imperial Imperial { get; set; }
    }

    public class Metric
    {
        public float Value { get; set; }
        public string Unit { get; set; }
        public int UnitType { get; set; }
    }

    public class Imperial
    {
        public float Value { get; set; }
        public string Unit { get; set; }
        public int UnitType { get; set; }
    }

    public class Wind
    {
        public Direction Direction { get; set; }
        public Value Speed { get; set; }
    }

    public class Direction
    {
        public int Degrees { get; set; }
        public string Localized { get; set; }
        public string English { get; set; }
    }

    public class Windgust
    {
        public Value Speed { get; set; }
    }

    public class Pressuretendency
    {
        public string LocalizedText { get; set; }
        public string Code { get; set; }
    }

    public class Precipitationsummary
    {
        public Value Precipitation { get; set; }
        public Value PastHour { get; set; }
        public Value Past3Hours { get; set; }
        public Value Past6Hours { get; set; }
        public Value Past9Hours { get; set; }
        public Value Past12Hours { get; set; }
        public Value Past18Hours { get; set; }
        public Value Past24Hours { get; set; }
    }

    public class Temperaturesummary
    {
        public Past6hourrange Past6HourRange { get; set; }
        public Past12hourrange Past12HourRange { get; set; }
        public Past24hourrange Past24HourRange { get; set; }
    }

    public class Past6hourrange
    {
        public Value Minimum { get; set; }
        public Value Maximum { get; set; }
    }

    public class Past12hourrange
    {
        public Value Minimum { get; set; }
        public Value Maximum { get; set; }
    }

    public class Past24hourrange
    {
        public Value Minimum { get; set; }
        public Value Maximum { get; set; }
    }

    public class AccuweatherForecastDTO
    {
        public Headline Headline { get; set; }
        public Dailyforecast[] DailyForecasts { get; set; }
    }

    public class Headline
    {
        public DateTime EffectiveDate { get; set; }
        public int EffectiveEpochDate { get; set; }
        public int Severity { get; set; }
        public string Text { get; set; }
        public string Category { get; set; }
        public DateTime EndDate { get; set; }
        public int EndEpochDate { get; set; }
        public string MobileLink { get; set; }
        public string Link { get; set; }
    }

    public class Dailyforecast
    {
        public DateTime Date { get; set; }
        public int EpochDate { get; set; }
        public AccuWeatherTemperature Temperature { get; set; }
        public AccuWeatherDay Day { get; set; }
        public AccuWeatherNight Night { get; set; }
        public string[] Sources { get; set; }
        public string MobileLink { get; set; }
        public string Link { get; set; }
    }

    public class AccuWeatherTemperature
    {
        public Minimum Minimum { get; set; }
        public Maximum Maximum { get; set; }
    }

    public class Minimum
    {
        public float Value { get; set; }
        public string Unit { get; set; }
        public int UnitType { get; set; }
    }

    public class Maximum
    {
        public float Value { get; set; }
        public string Unit { get; set; }
        public int UnitType { get; set; }
    }

    public class AccuWeatherDay
    {
        public int Icon { get; set; }
        public string IconPhrase { get; set; }
        public bool HasPrecipitation { get; set; }
        public string PrecipitationType { get; set; }
        public string PrecipitationIntensity { get; set; }
    }

    public class AccuWeatherNight
    {
        public int Icon { get; set; }
        public string IconPhrase { get; set; }
        public bool HasPrecipitation { get; set; }
        public string PrecipitationType { get; set; }
        public string PrecipitationIntensity { get; set; }
    }

    public class AccuWeatherLocationDTO
    {
        public int Version { get; set; }
        public string Key { get; set; }
        public string Type { get; set; }
        public int Rank { get; set; }
        public string LocalizedName { get; set; }
        public string EnglishName { get; set; }
        public string PrimaryPostalCode { get; set; }
        public Region Region { get; set; }
        public Country Country { get; set; }
        public Administrativearea AdministrativeArea { get; set; }
        public Timezone TimeZone { get; set; }
        public Geoposition GeoPosition { get; set; }
        public bool IsAlias { get; set; }
        public Supplementaladminarea[] SupplementalAdminAreas { get; set; }
        public string[] DataSets { get; set; }
    }

    public class Region
    {
        public string ID { get; set; }
        public string LocalizedName { get; set; }
        public string EnglishName { get; set; }
    }

    public class Country
    {
        public string ID { get; set; }
        public string LocalizedName { get; set; }
        public string EnglishName { get; set; }
    }

    public class Administrativearea
    {
        public string ID { get; set; }
        public string LocalizedName { get; set; }
        public string EnglishName { get; set; }
        public int Level { get; set; }
        public string LocalizedType { get; set; }
        public string EnglishType { get; set; }
        public string CountryID { get; set; }
    }

    public class Timezone
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public float GmtOffset { get; set; }
        public bool IsDaylightSaving { get; set; }
        public DateTime NextOffsetChange { get; set; }
    }

    public class Geoposition
    {
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public Elevation Elevation { get; set; }
    }

    public class Elevation
    {
        public Metric Metric { get; set; }
        public Imperial Imperial { get; set; }
    }

    public class Supplementaladminarea
    {
        public int Level { get; set; }
        public string LocalizedName { get; set; }
        public string EnglishName { get; set; }
    }
}
