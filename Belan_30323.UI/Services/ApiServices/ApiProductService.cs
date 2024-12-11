using Belan_30323.Domain.Entities;
using Belan_30323.Domain.Models;

namespace Belan_30323.UI.Services.ApiServices
{
    public class ApiProductService(HttpClient httpClient) : IProductService
    {
        public async Task<ResponseData<ProductListModel<Dish>>> GetProductListAsync(string? categoryNormalizedName, int pageNo = 1)
        {
            var uri = httpClient.BaseAddress;
            var queryData = new Dictionary<string, string>();

            queryData.Add("pageNo", pageNo.ToString());

            if (!string.IsNullOrEmpty(categoryNormalizedName))
            {
                queryData.Add("category", categoryNormalizedName);
            }

            var query = QueryString.Create(queryData);

            var result = await httpClient.GetAsync(uri + query.Value);

            if (result.IsSuccessStatusCode)
            {
                return await result.Content
               .ReadFromJsonAsync<ResponseData<ProductListModel<Dish>>>();
            };

            var response = new ResponseData<ProductListModel<Dish>>
            {
                Success = false,
                ErrorMessage = "Ошибка чтения API"
            };

            return response;
        }
    }
}
