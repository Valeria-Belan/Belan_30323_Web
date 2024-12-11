using Belan_30323.Domain.Entities;
using Belan_30323.Domain.Models;

namespace Belan_30323.UI.Services.ApiServices
{
    public interface IApiProductService
    {
        public Task<ResponseData<ProductListModel<Dish>>> GetProductListAsync(string? categoryNormalizedName, int pageNo = 1);

    }
}
