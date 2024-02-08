using hd_brand_asp.Data;
using hd_brand_asp.Models;
using Microsoft.AspNetCore.Mvc;

namespace hd_brand_asp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class ProductController : ControllerBase
    {

        private readonly HdBrandDboContext _context;

        public ProductController(HdBrandDboContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("GetProducts")]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()

        {
            return _context.Products.ToList();
        }
    }
}