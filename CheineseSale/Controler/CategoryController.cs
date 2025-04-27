using CheineseSale.Dal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CheineseSale.Controler
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryDal _categoryDal;

        // קונסטרוקטור, מקבל את הממשק ICategoryDal
        public CategoryController(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        // פעולת Get שמחזירה את כל הקטגוריות
        [HttpGet]
        [Authorize(Roles = "User, Admin")]
        public IActionResult GetAllCategories()
        {
            try
            {
                var categories = _categoryDal.GetAll();
                if (categories == null || categories.Count == 0)
                {
                    return NotFound("No categories found.");
                }

                return Ok(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}

