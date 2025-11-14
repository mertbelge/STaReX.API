using STaReX.ENTITY.Abstract;

namespace STaReX.ENTITY.Models.Weather
{
    public class CurrentUnits: IEntity
    {
        public string? time { get; set; }
        public string? interval { get; set; }
        public string? temperature_2m { get; set; }
        public string? relative_humidity_2m { get; set; }
    }
}