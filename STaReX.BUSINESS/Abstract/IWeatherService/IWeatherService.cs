using STaReX.ENTITY.Models.Weather;
using STaReX.ENTITY.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STaReX.BUSINESS.Abstract.IWeatherService
{
    public interface IWeatherService
    {
        Task <StatusResponse<WeatherResponse>> GetBy(double latitude, double longitude);
    }
}
