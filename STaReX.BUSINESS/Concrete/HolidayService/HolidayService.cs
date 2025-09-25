using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using STaReX.BUSINESS.Abstract.IHolidayService;
using STaReX.ENTITY.Dto;
using STaReX.ENTITY.Models.Holidays;
using STaReX.HELPERS.Abstract;
using STaReX.HELPERS.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STaReX.BUSINESS.Concrete.HolidayService
{
    public class HolidayService: IHolidayService
    {

        private readonly IHelperRepository<HolidayStatus> _repository;
        private readonly HelperOptions _helperOptions;

        public HolidayService(IHelperRepository<HolidayStatus> repository, IOptions<HelperOptions> options)
        {
            _repository = repository;
            _helperOptions = options.Value;
        }

        public async Task<StatusResponse<IEnumerable<HolidayResponse>>> GetList()
        {
            var keyword = _helperOptions.HolidayOptions.HOLIDAY_KEYWORD;

            var result = await _repository.GetAllAsync(keyword, null);

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

    }
}
