using Belan_30323.Domain.Entities;
using Belan_30323.Domain.Models;

namespace Belan_30323.UI.Services
{
    public class MemoryProductService : IProductService
    {
        List<Dish> _dishes;
        List<Category> _categories;

        public MemoryProductService(ICategoryService categoryService)
        {
            _categories = categoryService.GetCategoryListAsync()
           .Result
           .Data;
            SetupData();
        }

        private void SetupData()
        {
            _dishes = new List<Dish>
            {
                new Dish
                {
                    Id = 1,
                    Name="Суп-харчо",
                    Description="Очень острый, невкусный",
                    Calories = 200,
                    Image="Images/суп-харчо.png",
                    CategoryId = _categories.Find(c=>c.NormalizedName.Equals("soups")).Id
                },

                new Dish
                {
                    Id = 2,
                    Name="Борщ",
                    Description="Много сала, без сметаны",
                    Calories = 300,
                    Image="Images/борщ.png",
                    CategoryId = _categories.Find(c=>c.NormalizedName.Equals("soups")).Id
                },
                new Dish
                {
                    Id = 3,
                    Name="Щи",
                    Description="Вкусный",
                    Calories = 180,
                    Image="Images/щи.png",
                    CategoryId = _categories.Find(c=>c.NormalizedName.Equals("soups")).Id
                },
                new Dish
                {
                    Id = 4,
                    Name="Рассольник",
                    Description="Вкусный, густой",
                    Calories = 250,
                    Image="Images/рассольник.png",
                    CategoryId = _categories.Find(c=>c.NormalizedName.Equals("soups")).Id
                }
            };
        }

        public Task<ResponseData<ProductListModel<Dish>>> GetProductListAsync(string? categoryNormalizedName, int pageNo = 1)
        {
            // Создать объект результата
            var result = new ResponseData<ProductListModel<Dish>>();

            // Id категории для фильрации
            int? categoryId = null;

            // если требуется фильтрация, то найти Id категории
            // с заданным categoryNormalizedName
            if (categoryNormalizedName != null)
            {
                categoryId = _categories.Find(c => c.NormalizedName.Equals(categoryNormalizedName))?.Id;
            }

            // Выбрать объекты, отфильтрованные по Id категории,
            // если этот Id имеется
            var data = _dishes.Where(d => categoryId == null || d.CategoryId.Equals(categoryId))?.ToList();
            //var data = _dishes.Where(d => categoryNormalizedName == null || d.Category.NormalizedName.Equals(categoryNormalizedName)).ToList();

            // поместить ранные в объект результата
            result.Data = new ProductListModel<Dish>() 
            { 
                Items = data 
            };

            // Если список пустой
            if (data.Count == 0)
            {
                result.Success = false;
                result.ErrorMessage = "Нет объектов в выбраннной  категории";
            }

            // Вернуть результат
            return Task.FromResult(result);
        }
    }
}
