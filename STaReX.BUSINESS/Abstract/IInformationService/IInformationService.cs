using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STaReX.ENTITY.Dto;
using STaReX.ENTITY.Models.Holidays;
using STaReX.ENTITY.Models.Weather;

namespace STaReX.BUSINESS.Abstract.IHolidayService
{
    public interface IInformationService
    {
        Task <StatusResponse<IEnumerable<HolidayResponse>>> GetList();
        Task<StatusResponse<WeatherResponse>> GetBy(double latitude, double longitude);
    }
}
