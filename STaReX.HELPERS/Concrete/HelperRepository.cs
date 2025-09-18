using Dapper;
using Microsoft.Extensions.Options;
using STaReX.DB.Abstract;
using STaReX.DB.Dtos;
using STaReX.ENTITY.Abstract;
using STaReX.ENTITY.Dto;
using STaReX.ENTITY.Models.ConnectionTesting;
using STaReX.HELPERS.Abstract;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace STaReX.HELPERS.Concrete
{
    public class HelperRepository<T> : IHelperRepository<T> where T : IEntity
    {

        private readonly ProcedureOptions _procedureOptions;
        private readonly IDatabaseRepository<HelpersAPI> _repository;

        public HelperRepository(IDatabaseRepository<HelpersAPI> repository, IOptions<ProcedureOptions> options)
        {
            _repository = repository;
            _procedureOptions = options.Value;
        }

        public async Task<T?> GetAllAsync(string keyword, DynamicParameters parameters)
        {
            using (var httpClient = new HttpClient())
            {

                var url = await GetURL(keyword);
       
                if (parameters != null)
                {
                    foreach (var paramName in parameters.ParameterNames)
                    {
                        var value = parameters.Get<dynamic>(paramName);
                        url.URL = url.URL.Replace("{" + paramName + "}", Convert.ToString(value, CultureInfo.InvariantCulture));
                    }

                }

                var response = await httpClient.GetAsync(url.URL);

                if (response.IsSuccessStatusCode)
                { 
                
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<T>(jsonResponse, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    return result;
                }
                else
                {
                    // Handle error response
                    return default(T);

                }
            }
        }
        public async Task<HelpersAPI> GetURL(string keyword)
        {
            DynamicParameters? parametes = new DynamicParameters();
            parametes.Add("KeyWord", keyword);
            var procedure = _procedureOptions.HelpersAPIProcedure.GET_BY_KEYWORD;
            var result = await _repository.GetByIdAsync(procedure, parametes);
            return result;

        }
    }
}
