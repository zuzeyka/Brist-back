using FullStackBrist.Server.Models.Categories;
using Microsoft.AspNetCore.Mvc;
using Slush.DAO.CategoriesDao;
using Slush.Data;
using Slush.Data.Entity;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesByUserController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly CategoriesByUserDao _categoriesByUserDao;

        public CategoriesByUserController(DataContext dataContext, CategoriesByUserDao categoriesByUserDao)
        {
            _dataContext = dataContext;
            _categoriesByUserDao = categoriesByUserDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoriesByUserDao>>> GetAllCategoriesByUser()
        {
            var categoriesByUser = await _categoriesByUserDao.GetAllCategoriesByUser();

            var response = categoriesByUser.Select(c => new CategoryByUser(id: c.id,
                                                                           name: c.name,
                                                                           description: c.description,
                                                                           createdAt: c.createdAt
                                                                        )).ToList();

            return Ok(response);
        }
        [HttpPost]
        public async Task<ActionResult<CategoryByUser>> CreateCategoryByUser([FromBody] CategoryByUserModel model)
        {
            var result = new CategoryByUser(Guid.NewGuid(),
                                        model.name,
                                        model.description,
                                        DateTime.Now);
            var response = await _dataContext.dbCategoriesByUsers.AddAsync(result);

            return Ok(response);
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

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCategoryByUser(Guid id, [FromBody] CategoryByUserModel model)
        {
            var result = new CategoryByUser(id, model.name, model.description, DateTime.Now);
            await _categoriesByUserDao.UpdateCategoriesByUser(result);
            return NoContent();
        }
    }
}
