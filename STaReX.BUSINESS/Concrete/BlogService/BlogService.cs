using Microsoft.Extensions.Options;
using STaReX.BUSINESS.Abstract.IBlogService;
using STaReX.DB.Abstract;
using STaReX.DB.Dtos;
using STaReX.ENTITY.Dto;
using STaReX.ENTITY.Models.Blog;
using STaReX.ENTITY.Models.ConnectionTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STaReX.BUSINESS.Concrete.BlogService
{
    public class BlogService: IBlogService
    {
        private readonly ProcedureOptions _procedureOptions;
        private readonly IDatabaseRepository<BlogList> _repositoryList;

        public BlogService(IOptions<ProcedureOptions> options, IDatabaseRepository<BlogList> repositoryList)
        {
            _procedureOptions = options.Value;
            _repositoryList = repositoryList;
        }
        public async Task<StatusResponse<IEnumerable<BlogList>>> GetList()
        {
            var procedure = _procedureOptions.BlogProcedure.GET_LIST;
            var result = await _repositoryList.GetAllAsync(procedure, null);

            return StatusResponse<IEnumerable<BlogList>>.Success(result);
        }

        
    }
}
