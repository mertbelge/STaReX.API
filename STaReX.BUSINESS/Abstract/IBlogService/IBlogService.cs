using STaReX.ENTITY.Dto;
using STaReX.ENTITY.Models.Blog;
using STaReX.ENTITY.Models.ConnectionTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STaReX.BUSINESS.Abstract.IBlogService
{
    public interface IBlogService
    {
        Task<StatusResponse<IEnumerable<BlogList>>> GetList();
    }
}
