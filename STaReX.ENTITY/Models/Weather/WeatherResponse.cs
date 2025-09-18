using STaReX.ENTITY.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STaReX.ENTITY.Models.Weather
{
    public class WeatherResponse : IEntity
    {
        public string? Time { get; set; }
        public string? Temperature { get; set; }
        public string? RelativeHumidity { get; set; }
    }
}
