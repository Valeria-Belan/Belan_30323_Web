using Belan_30323.Domain.Entities;
using Belan_30323.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Belan_30323.UI.Services
{
    public class MemoryProductService : IProductService
    {
        List<Dish> _dishes;
        List<Category> _categories;

        private readonly ICategoryService _categoryService;
        private readonly IConfiguration _config;

        public MemoryProductService([FromServices] IConfiguration config, ICategoryService categoryService)
        {
            _categories = categoryService.GetCategoryListAsync().Result.Data;
           _config = config;
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
            ResponseData<ProductListModel<Dish>> result = new();

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

            // получить размер страницы из конфигурации
            int pageSize = _config.GetSection("ItemsPerPage").Get<int>();

            // получить общее количество страниц
            int totalPages = (int)Math.Ceiling(data.Count / (double)pageSize);

            // получить данные страницы
            var listData = new ProductListModel<Dish>()
            {
                Items = data.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList(),
                CurrentPage = pageNo,
                TotalPages = totalPages
            };

            // поместить данные в объект результата
            result.Data = listData;

            // Если список пустой
            if (data.Count == 0)
            {
                result.Success = false;
                result.ErrorMessage = "Нет объектов в выбраннной категории";
            }

            // Вернуть результат
            return Task.FromResult(result);
        }
    }
}
