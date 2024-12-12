using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Belan_30323.Domain.Entities;
using Belan_30323.UI.Data;

namespace Belan_30323.UI.Areas.Admin.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly Belan_30323.UI.Data.AppDbContext _context;

        public DetailsModel(Belan_30323.UI.Data.AppDbContext context)
        {
            _context = context;
        }

        public Dish Dish { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dish = await _context.Dishes.FirstOrDefaultAsync(m => m.Id == id);
            if (dish == null)
            {
                return NotFound();
            }
            else
            {
                Dish = dish;
            }
            return Page();
        }
    }
}
