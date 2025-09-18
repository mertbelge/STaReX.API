using STaReX.ENTITY.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STaReX.ENTITY.Models.Weather
{
    public class WeatherStatus : IEntity
    {
        public double? latitude { get; set; }
        public double? longitude { get; set; }
        public double? generationtime_ms { get; set; }
        public int? utc_offset_seconds { get; set; }
        public string? timezone { get; set; }
        public string? timezone_abbreviation { get; set; }
        public double? elevation { get; set; }
        public CurrentUnits current_units { get; set; }   
        public Weather current { get; set; }

    }
}
