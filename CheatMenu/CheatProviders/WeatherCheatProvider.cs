using System;
using System.Reflection;
using Mafi;
using Mafi.Base;
using Mafi.Collections;
using Mafi.Core;
using Mafi.Core.Environment;
using Mafi.Core.Prototypes;

namespace CaptainOfIndustryMods.CheatMenu.CheatProviders
{
    public class WeatherCheatProvider : ICheatProvider
    {
        private readonly Mafi.Lazy<Lyst<CheatItem>> _lazyCheats;
        private readonly ProtosDb _protosDb;
        private readonly IWeatherManager _weatherManager;
        private PropertyInfo _currentWeatherProperty;
        private FieldInfo _overrideDurationField;

        public WeatherCheatProvider(IWeatherManager weatherManager, ProtosDb protosDb)
        {
            _weatherManager = weatherManager;
            _protosDb = protosDb;
            _lazyCheats = new Mafi.Lazy<Lyst<CheatItem>>(GetCheats);
        }

        public Lyst<CheatItem> Cheats => _lazyCheats.Value;

        private void SetWeatherAccessors()
        {
            if (!(_currentWeatherProperty is null) && !(_overrideDurationField is null)) return;
            var weatherManagerType = typeof(CoreMod).Assembly.GetType("Mafi.Core.Environment.WeatherManager");
            if (weatherManagerType is null)
            {
                Log.Info("*** CheatMenu ERROR *** Unable to fetch the WeatherManager type.");
                throw new Exception("*** CheatMenu ERROR *** Unable to fetch the WeatherManager type.");
            }

            _currentWeatherProperty = weatherManagerType.GetProperty("CurrentWeather");
            _overrideDurationField = weatherManagerType.GetField("m_weatherOverrideDuration",
                BindingFlags.NonPublic | BindingFlags.Instance);
        }

        private Lyst<CheatItem> GetCheats()
        {
            return new Lyst<CheatItem>
            {
                new CheatItem
                {
                    Title = "Reset weather",
                    Action = () =>
                    {
                        SetWeatherAccessors();
                        _currentWeatherProperty.SetValue(_weatherManager,
                            _protosDb.First<WeatherProto>(x => x.Id == Ids.Weather.Sunny).Value);
                        _overrideDurationField.SetValue(_weatherManager, 0);
                    },
                    UsingReflection = true,
                },
                new CheatItem
                {
                    Title = "Sunny weather",
                    Action = () =>
                    {
                        SetWeatherAccessors();
                        _currentWeatherProperty.SetValue(_weatherManager,
                            _protosDb.First<WeatherProto>(x => x.Id == Ids.Weather.Sunny).Value);
                        _overrideDurationField.SetValue(_weatherManager, int.MaxValue);
                    },
                    UsingReflection = true,
                },
                new CheatItem
                {
                    Title = "Cloudy weather",
                    Action = () =>
                    {
                        SetWeatherAccessors();
                        _currentWeatherProperty.SetValue(_weatherManager,
                            _protosDb.First<WeatherProto>(x => x.Id == Ids.Weather.Cloudy).Value);
                        _overrideDurationField.SetValue(_weatherManager, int.MaxValue);
                    },
                    UsingReflection = true,
                },
                new CheatItem
                {
                    Title = "Rainy weather",
                    Action = () =>
                    {
                        SetWeatherAccessors();
                        _currentWeatherProperty.SetValue(_weatherManager,
                            _protosDb.First<WeatherProto>(x => x.Id == Ids.Weather.Rainy).Value);
                        _overrideDurationField.SetValue(_weatherManager, int.MaxValue);
                    },
                    UsingReflection = true,
                },
                new CheatItem
                {
                    Title = "Heavy rain weather",
                    Action = () =>
                    {
                        SetWeatherAccessors();
                        _currentWeatherProperty.SetValue(_weatherManager,
                            _protosDb.First<WeatherProto>(x => x.Id == Ids.Weather.HeavyRain).Value);
                        _overrideDurationField.SetValue(_weatherManager, int.MaxValue);
                    },
                    UsingReflection = true,
                },
            };
        }
    }
}