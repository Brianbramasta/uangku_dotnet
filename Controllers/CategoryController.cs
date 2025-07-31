using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MySimpleApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories([FromQuery] string? type)
        {
            var userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            var query = _context.Categories.Where(c => c.UserId == userId);

            if (!string.IsNullOrEmpty(type))
            {
                query = query.Where(c => c.Type == type);
            }

            var categories = await query.Select(c => new
            {
                c.Id,
                c.Name,
                c.Type,
                c.Icon
            }).ToListAsync();

            return Ok(new { data = categories });
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryRequest request)
        {
            var userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            var category = new Category
            {
                UserId = userId,
                Name = request.Name,
                Type = request.Type,
                Icon = request.Icon
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return Ok(new { data = category });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(long id, CategoryRequest request)
        {
            var userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);

            if (category == null)
                return NotFound();

            category.Name = request.Name;
            category.Type = request.Type;
            category.Icon = request.Icon;
            category.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return Ok(new { data = category });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(long id)
        {
            var userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);

            if (category == null)
                return NotFound();

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Category deleted successfully" });
        }
    }

    public class CategoryRequest
    {
        [Required]
        public string Name { get; set; } = "";

        [Required]
        [RegularExpression("^(income|expense)$")]
        public string Type { get; set; } = "expense";

        public string? Icon { get; set; }
    }
}
