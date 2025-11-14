using STaReX.ENTITY.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STaReX.ENTITY.Models.Holidays
{
    public class HolidayResponse : IEntity
    {
        public string NameTR { get; set; }
        public string NameEN { get; set; }
        public string WeekDayName { get; set; }
        public string Date { get; set; }
        public string LongDate { get; set; }
    }
}
