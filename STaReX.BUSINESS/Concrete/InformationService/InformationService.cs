using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using STaReX.BUSINESS.Abstract.IHolidayService;
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

namespace STaReX.BUSINESS.Concrete.HolidayService
{
    public class InformationService: IInformationService
    {

        private readonly IHelperRepository<HolidayStatus> _repositoryHoliday;
        private readonly IHelperRepository<WeatherStatus> _repositoryWeather;
        private readonly HelperOptions _helperOptions;

        public InformationService(IHelperRepository<HolidayStatus> repositoryHoliday,
            IHelperRepository<WeatherStatus> repositoryWeather,
            IOptions<HelperOptions> options)
        {
            _repositoryHoliday = repositoryHoliday;
            _repositoryWeather = repositoryWeather;
            _helperOptions = options.Value;
        }

        public async Task<StatusResponse<IEnumerable<HolidayResponse>>> GetList()
        {
            var keyword = _helperOptions.HolidayOptions.HOLIDAY_KEYWORD;

            var result = await _repositoryHoliday.GetAllAsyncFromAPI(keyword, null);

            IEnumerable<HolidayResponse> response = result.resmitatiller.Select(x => new HolidayResponse
            {
                NameTR = x.gun,
                NameEN = x.en,
                WeekDayName = x.haftagunu,
                Date = x.tarih,
                LongDate = x.uzuntarih
            }).ToList();
           
            return StatusResponse<IEnumerable<HolidayResponse>>.Success(response);

        }

        public async Task<StatusResponse<WeatherResponse>> GetBy(double latitude, double longitude)
        {
            var keyword = _helperOptions.WeatherOptions.WEATHER_KEYWORD;

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("latitude", latitude);
            parameters.Add("longitude", longitude);

            var result = await _repositoryWeather.GetAllAsyncFromAPI(keyword, parameters);

            WeatherResponse response = new WeatherResponse
            {
                Time = result.current?.time,
                Temperature = result.current.temperature_2m.ToString() + result.current_units.temperature_2m,
                RelativeHumidity = result.current.relative_humidity_2m.ToString() + result.current_units.relative_humidity_2m,
            };

            return StatusResponse<WeatherResponse>.Success(response);

        }

    }
}
