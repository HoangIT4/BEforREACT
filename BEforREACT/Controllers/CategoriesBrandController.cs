using BEforREACT.Data;
using Microsoft.AspNetCore.Mvc;

namespace BEforREACT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesBrandController : ControllerBase
    {
        private readonly DataContext _context;

        public CategoriesBrandController(DataContext context)
        {
            _context = context;
        }
    }

}
