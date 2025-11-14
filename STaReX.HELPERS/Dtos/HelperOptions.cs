using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STaReX.HELPERS.Dtos
{
    public class HelperOptions
    {
        public EncryptionOptions EncryptionOptions { get; set; } = null!;
        public HolidayOptions HolidayOptions { get; set; } = null!;
        public WeatherOptions WeatherOptions { get; set; } = null!;
    }
    public class EncryptionOptions
    {
        public string KEY { get; set; } = null!;
    }
    public class HolidayOptions
    {
        public string HOLIDAY_KEYWORD { get; set; } = null!;
    }

    public class WeatherOptions
    {
        public string WEATHER_KEYWORD { get; set; } = null!;
    }


    
}
