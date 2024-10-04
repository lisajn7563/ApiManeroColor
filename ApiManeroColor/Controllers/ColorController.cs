using ApiManeroColor.Contexts;
using ApiManeroColor.Entites;
using ApiManeroColor.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiManeroColor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorController(DataContext context) : ControllerBase
    {
        private readonly DataContext _context = context;

        [HttpPost]

        public async Task<IActionResult> Create(ColorRegistration model)
        {
            if (ModelState.IsValid)
            {
                var entity = new ColorEntity
                {
                    id = model.id,
                    colorTitle = model.colorTitle,
                };
                _context.Color.Add(entity);
                await _context.SaveChangesAsync();

                return Ok(entity);
            }

            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var colorList = await _context.Color.ToListAsync();

            return Ok(colorList);

        }
    }
}
