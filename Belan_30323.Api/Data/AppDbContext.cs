using Belan_30323.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Belan_30323.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public DbSet<Category> Categories {  get; set; }
        public DbSet<Dish> Dishes { get; set; }
    }
}
