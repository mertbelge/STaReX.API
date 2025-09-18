using STaReX.ENTITY.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STaReX.ENTITY.Models.Holidays
{
    public class HolidayStatus : IEntity
    {
        public bool? success { get; set; }
        public string? status { get; set; }
        public string? changeLog { get; set; }
        public string? feedbackURL { get; set; }
        public string? pagecreatedate { get; set; }
        public IEnumerable<Holidays>? resmitatiller { get; set; } 
    }
}
