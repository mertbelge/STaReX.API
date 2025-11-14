using STaReX.ENTITY.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STaReX.ENTITY.Models.Weather
{
    public class Weather : IEntity
    {
        public string? time { get; set; }
        public int? interval { get; set; }
        public double? temperature_2m { get; set; }
        public int? relative_humidity_2m { get; set; }
    }
}
