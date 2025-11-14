using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc.Filters;
using STaReX.BUSINESS.Abstract.IConnectionTestingService;

namespace STaReX.API.Filters
{
    public class DBFilter: Attribute, IAuthorizationFilter
    {
        private readonly string _app;
        private readonly string _key;

        public DBFilter(IConfiguration configuration)
        {
            _app = configuration["ApiSettings:APP"];
            _key = configuration["ApiSettings:KEY"];
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                string _APP = context.HttpContext.Request.Headers.FirstOrDefault(x => x.Key.ToUpper() == "APP").Value;
                string _KEY = context.HttpContext.Request.Headers.FirstOrDefault(x => x.Key.ToUpper() == "KEY").Value;

                if (_APP != _app || _KEY != _key)
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
