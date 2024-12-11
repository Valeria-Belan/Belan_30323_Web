using Belan_30323.Domain.Entities;
using Belan_30323.Domain.Models;

namespace Belan_30323.UI.Services.ApiServices
{
    public interface IApiCategoryService
    {
        public Task<ResponseData<List<Category>>> GetCategoryListAsync();
    }
}
