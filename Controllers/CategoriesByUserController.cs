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
                                                                        c.description
                                                                        )).ToList();

            return Ok(response);
        }
    }
}
