using STaReX.ENTITY.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STaReX.ENTITY.Models.Blog
{
    public class BlogList: IEntity
    {
        public int BlogId { get; set; }
        public string TitleName { get; set; }
        public string Description { get; set; }
        public string CreateDate { get; set; }
        public string FileURL { get; set; }
    }
}
