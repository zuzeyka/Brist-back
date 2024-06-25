using FullStackBrist.Server.Models.Categories;
using Microsoft.AspNetCore.Mvc;
using Slush.DAO.CategoriesRepository;
using Slush.Data.Entity;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesByUserController : Controller
    {
        private readonly CategoriesByUserRepository _categoriesByUserDao;

        public CategoriesByUserController( CategoriesByUserRepository categoriesByUserDao)
        {
            _categoriesByUserDao = categoriesByUserDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoriesByUserRepository>>> GetAllCategoriesByUser()
        {
            var categoriesByUser = await _categoriesByUserDao.GetAllCategoriesByUser();

            return Ok(categoriesByUser);
        }
        [HttpPost]
        public async Task<ActionResult<CategoryByUser>> CreateCategoryByUser([FromBody] CategoryByUserModel model)
        {
            var result = new CategoryByUser(Guid.NewGuid(),
                                        model.authorId,
                                        model.name,
                                        model.description,
                                        DateTime.Now);
            await _categoriesByUserDao.Add(result);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryByUser>> GetCategoryByUser(Guid id)
        {
            var response = await _categoriesByUserDao.GetById(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategoryByUser(Guid id)
        {
            await _categoriesByUserDao.DeleteCategoryByUser(id);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateCategoryByUser(Guid id, [FromBody] CategoryByUserModel model)
        {
            var result = await _categoriesByUserDao.UpdateCategoriesByUser(new CategoryByUser(id, model.authorId, model.name, model.description, DateTime.Now));
            return Ok(result);
        }

        [HttpPost("getall")]
        public async Task<ActionResult<List<CategoryByUser>>> GetAllCategoriesByIds([FromBody] List<Guid> guidList)
        {
            var response = await _categoriesByUserDao.GetAllById(guidList);

            return Ok(response);
        }
    }
}
