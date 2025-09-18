using STaReX.ENTITY.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STaReX.ENTITY.Models.Holidays
{
    public class Holidays : IEntity
    {
        public string? gun { get; set; }
        public string? en { get; set; }
        public string? haftagunu { get; set; }
        public string? tarih { get; set; }
        public string? uzuntarih { get; set; }
    }
}
