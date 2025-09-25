using Dapper;
using Microsoft.Extensions.Options;
using STaReX.BUSINESS.Abstract.IWeatherService;
using STaReX.DB.Dtos;
using STaReX.ENTITY.Dto;
using STaReX.ENTITY.Models.Holidays;
using STaReX.ENTITY.Models.Weather;
using STaReX.HELPERS.Abstract;
using STaReX.HELPERS.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STaReX.BUSINESS.Concrete.WeatherService
{
    public class WeatherService : IWeatherService
    {

        private readonly IHelperRepository<WeatherStatus> _repository;
        private readonly HelperOptions _helperOptions;

        public WeatherService(IHelperRepository<WeatherStatus> repository, IOptions<HelperOptions> options)
        {
            _repository = repository;
            _helperOptions = options.Value;
        }

        public async Task<StatusResponse<WeatherResponse>> GetBy(double latitude, double longitude)
        {
            var keyword = _helperOptions.WeatherOptions.WEATHER_KEYWORD;

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("latitude", latitude);
            parameters.Add("longitude", longitude);

            var result = await _repository.GetAllAsync(keyword , parameters);

            WeatherResponse response = new WeatherResponse
            {
                Time = result.current?.time,
                Temperature = result.current.temperature_2m.ToString() + result.current_units.temperature_2m,
                RelativeHumidity = result.current.relative_humidity_2m.ToString() + result.current_units.relative_humidity_2m,
            };

            return StatusResponse <WeatherResponse>.Success(response);

        }
    }
}
