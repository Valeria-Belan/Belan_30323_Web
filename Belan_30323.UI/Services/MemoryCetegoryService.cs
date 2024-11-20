using Belan_30323.Domain.Entities;
using Belan_30323.Domain.Models;

namespace Belan_30323.UI.Services
{
    public class MemoryCategoryService : ICategoryService
    {
        public Task<ResponseData<List<Category>>>GetCategoryListAsync()
        {
            var categories = new List<Category>
            {
                new Category {Id=1, Name="Стартеры", NormalizedName="starters"},
                new Category {Id=2, Name="Салаты", NormalizedName="salads"},
                new Category {Id=3, Name="Супы", NormalizedName="soups"} //ДОБАВИЛА
            };

            var result = new ResponseData<List<Category>>();
            result.Data = categories;

            return Task.FromResult(result);
        }
    }
}
