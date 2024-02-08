using hd_brand_asp.Data;
using hd_brand_asp.Models;
using Microsoft.AspNetCore.Mvc;
using RepoLibrary.Interfaces;

namespace hd_brand_asp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class ProductController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        //private readonly ICacheService _cacheService;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            //  _cacheService = cacheService;

        }

        [HttpGet]
        [Route("GetProducts")]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()

        {
            return _unitOfWork.ProductRep.GetAll().ToList(); 
        }
        [HttpGet]
        [Route("GetProductsBySize")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsBySize(int size)

        {
            return _unitOfWork.ProductRep.getProductBySize(size).ToList();
        }
        [HttpGet]
        [Route("GetSizeofProduct")]
        public async Task<ActionResult<IEnumerable<Productsize>>> GetSizeofProduct(int id)

        {
            return _unitOfWork.ProductRep.getSizes(id).ToList();
        }

    }
}