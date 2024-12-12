using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Belan_30323.Domain.Entities;
using Belan_30323.UI.Data;
using Belan_30323.UI.Services.Abstraction;

namespace Belan_30323.UI.Areas.Admin.Pages
{

    public class CreateModel(ICategoryService categoryService, IProductService productService) : PageModel
    {
        public async Task<IActionResult> OnGet()
        {
            var categoryListData = await categoryService.GetCategoryListAsync();
            ViewData["CategoryId"] = new SelectList(categoryListData.Data, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Dish Dish { get; set; } = default!;

        [BindProperty]
        public IFormFile? Image { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await productService.CreateProductAsync(Dish, Image); 

            return RedirectToPage("./Index");
        }
    }

}
