using Belan_30323.Domain.Entities;
using Belan_30323.Domain.Models;

namespace Belan_30323.UI.Services
{
    public interface ICategoryService
    {
        /// <summary>
        /// Получение списка всех категорий
        /// </summary>
        /// <returns></returns>
        public Task<ResponseData<List<Category>>> GetCategoryListAsync();
    }
}
