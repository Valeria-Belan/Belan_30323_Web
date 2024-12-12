using Belan_30323.Domain.Entities;
using Belan_30323.Domain.Models;
using Belan_30323.UI.Services.Abstraction;

namespace Belan_30323.UI.Services.ApiServices
{
    public class ApiCategoryService(HttpClient httpClient) : ICategoryService
    {
        public async Task<ResponseData<List<Category>>> GetCategoryListAsync()
        {
            var result = await httpClient.GetAsync(httpClient.BaseAddress);

            if (result.IsSuccessStatusCode)
            {
                return await result.Content
                    .ReadFromJsonAsync<ResponseData<List<Category>>>();
            };

            var response = new ResponseData<List<Category>>
            {
                Success = false,
                ErrorMessage = "Ошибка чтения API"
            };

            return response;
        }
    }
}
