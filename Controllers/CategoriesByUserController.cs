using FullStackBrist.Server.Models.Categories;
using Microsoft.AspNetCore.Mvc;
using Slush.DAO.CategoriesDao;
using Slush.Data;
using Slush.Data.Entity;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("[categoriesByUserController]")]
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

            var response = categoriesByUser.Select(c => new CategoryByUser(c.id,
                                                                        c.name,
                                                                        c.description,
                                                                        c.createdAt
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
            _dataContext.dbCategoriesByUsers.AddAsync(result);

            return result;
        }
    }
}
