﻿using Belan_30323.Domain.Entities;

namespace Belan_30323.Api.Data
{
    public static class DbInitializer
    {
        public static async Task SeedData(WebApplication app)
        {
            // Uri проекта
            var uri = "https://localhost:7005/";

            // Получение контекста БД
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            // Заполнение данными
            if (!context.Categories.Any() && !context.Dishes.Any())
            {
                var categories = new Category[]
                {
                    new Category { Name="Стартеры", NormalizedName="starters"},
                    new Category { Name="Салаты", NormalizedName="salads"},
                    new Category { Name="Супы", NormalizedName="soups"},
                    new Category { Name="Основные блюда", NormalizedName="main-dishes"},
                    new Category { Name="Напитки", NormalizedName="drinks"},
                    new Category { Name="Десерты", NormalizedName="desserts"}
                };
                await context.Categories.AddRangeAsync(categories);
                await context.SaveChangesAsync();

                var dishes = new List<Dish>
                {
                    new Dish
                    {
                        Name= "Суп-харчо",
                        Description = "Очень острый, невкусный",
                        Calories = 200,
                        Category = categories.FirstOrDefault(c => c.NormalizedName.Equals("soups")),
                        Image = uri + "Images/суп-харчо.png"
                    },

                    new Dish
                    {
                        Name = "Борщ",
                        Description = "Много сала, без сметаны",
                        Calories = 330,
                        Category = categories.FirstOrDefault(c => c.NormalizedName.Equals("soups")),
                        Image = uri + "Images/борщ.png"
                    },
                    new Dish
                    {
                        Name = "Щи",
                        Description = "Вкусный",
                        Calories = 180,
                        Category = categories.FirstOrDefault(c => c.NormalizedName.Equals("soups")),
                        Image = uri + "Images/щи.png"
                    },
                     new Dish
                     {
                        Name = "Рассольник",
                        Description = "Вкусный, густой",
                        Calories = 250,
                        Category = categories.FirstOrDefault(c => c.NormalizedName.Equals("soups")),
                        Image = uri + "Images/рассольник.png"
                     },
                };
                await context.AddRangeAsync(dishes);
                await context.SaveChangesAsync();
            }
        }
    }
}
