using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Belan_30323.Api.Data;
using Belan_30323.Domain.Entities;
using Belan_30323.Domain.Models;

namespace Belan_30323.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public DishesController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> SaveImage(int id, IFormFile? image)
        {
            // Найти объект по Id
            var dish = await _context.Dishes.FindAsync(id);
            if (dish == null)
            {
                return NotFound();
            }
            // Путь к папке wwwroot/Images
            var imagesPath = Path.Combine(_env.WebRootPath, "Images");

            // получить случайное имя файла
            var randomName = Path.GetRandomFileName();

            // получить расширение в исходном файле
            var extension = Path.GetExtension(image.FileName);

            // задать в новом имени расширение как в исходном файле
            var fileName = Path.ChangeExtension(randomName, extension);

            // полный путь к файлу
            var filePath = Path.Combine(imagesPath, fileName);

            // создать файл и открыть поток для записи
            using var stream = System.IO.File.OpenWrite(filePath);

            // скопировать файл в поток
            await image.CopyToAsync(stream);

            // получить Url хоста
            var host = "https://" + Request.Host;

            // Url файла изображения
            var url = $"{host}/Images/{fileName}";

            // Сохранить url файла в объекте
            dish.Image = url;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseData<Dish>>> GetDisheById(int id)
        {
            var responseData = new ResponseData<Dish>();

            try
            {
                // Запрос к базе данных
                var dish = await _context.Dishes
                    .FirstOrDefaultAsync(d => d.Id == id);

                if (dish != null)
                {
                    responseData.Data = dish;
                }
                else
                {
                    responseData.Success = false;
                    responseData.ErrorMessage = $"Блюдо с ID {id} не найдено.";
                }
            }
            catch (Exception ex)
            {
                responseData.Success = false;
                responseData.ErrorMessage = $"Ошибка при доступе к данным: {ex.Message}";
            }

            return responseData;
        }

        // GET: api/Dishes
        [HttpGet]
        public async Task<ActionResult<ResponseData<ProductListModel<Dish>>>> GetDishes(
            string? category,
            int pageNo = 1,
            int pageSize = 3)

        {
            // Создать объект результата
            var result = new ResponseData<ProductListModel<Dish>>();

            // Фильтрация по категории загрузка данных категории
            var data = _context.Dishes
                .Include(d => d.Category)
                .Where(d => String.IsNullOrEmpty(category)
                || d.Category.NormalizedName.Equals(category));

            // Подсчет общего количества страниц
            int totalPages = (int)Math.Ceiling(data.Count() / (double)pageSize);
            if (pageNo > totalPages)
                pageNo = totalPages;

            // Создание объекта ProductListModel с нужной страницей данных
            var listData = new ProductListModel<Dish>()
            {
                Items = await data
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(),
                CurrentPage = pageNo,
                TotalPages = totalPages
            };

            // поместить данные в объект результата
            result.Data = listData;

            // Если список пустой
            if (data.Count() == 0)
            {
                result.Success = false;
                result.ErrorMessage = "Нет объектов в выбранной категории";
            }

            return result;
        }
    }
}
