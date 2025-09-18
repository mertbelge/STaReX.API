using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STaReX.ENTITY.Dto;
using STaReX.ENTITY.Models.Holidays;

namespace STaReX.BUSINESS.Abstract.IHolidayService
{
    public interface IHolidayService
    {
        Task<IEnumerable<HolidayResponse>> GetList();
    }
}
