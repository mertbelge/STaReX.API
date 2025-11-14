using Microsoft.AspNetCore.Mvc.Filters;
using STaReX.BUSINESS.Abstract.IConnectionTestingService;
using STaReX.ENTITY.Dto;

namespace STaReX.API.Filters
{
    public class AuthFilter: Attribute, IAsyncAuthorizationFilter
    {
        private readonly IConnectionService _connectionService;

        public AuthFilter(IConnectionService connectionService)
        {
            _connectionService = connectionService;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            try
            {
                string _Token = context.HttpContext.Request.Headers.FirstOrDefault(x => x.Key.ToUpper() == "TOKEN").Value;

                var response = await _connectionService.Success(_Token);

                if (response.Success == false)
                {
                    throw new UnauthorizedAccessException();
                }
            }

            catch (Exception)
            {
                throw new UnauthorizedAccessException();
            }
        }
    }
}
