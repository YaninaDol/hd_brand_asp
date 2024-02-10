using hd_brand_asp.Data;
using hd_brand_asp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepoLibrary.Interfaces;
using RepositoriesLibrary.Roles;

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


        [HttpPost]
        //[Authorize(Roles = $"{UserRoles.Menager},{UserRoles.Admin}")]
        [Route("Add")]

        public IResult Add([FromForm] string Name, [FromForm] string ShoeType, [FromForm] int? Categoryid, [FromForm] int? Seasonid, [FromForm] int Materialid,  [FromForm] int Price)
        {
            _unitOfWork.ProductRep.Create(new Product() { Name = Name, ShoeType = ShoeType, Categoryid = Categoryid, Seasonid = Seasonid, Materialid = Materialid, Price = Price });
            _unitOfWork.Commit();

            //  _cacheService.SetData("Products", _unitOfWork.ProductRep.GetAll(), DateTimeOffset.Now.AddDays(1));
            return Results.Ok();


        }
        [HttpPost]
        //[Authorize(Roles = $"{UserRoles.Menager},{UserRoles.Admin}")]
        [Route("Delete")]

        public IResult Delete([FromForm] int Id)
        {
            _unitOfWork.ProductRep.Delete(Id);
            _unitOfWork.Commit();

            //  _cacheService.SetData("Products", _unitOfWork.ProductRep.GetAll(), DateTimeOffset.Now.AddDays(1));
            return Results.Ok();


        }

        [HttpPost]
        [Authorize(Roles = $"{UserRoles.Menager},{UserRoles.Admin}")]
        [Route("Update")]

        public IResult Update([FromForm] int id,[FromForm] string Name, [FromForm] string ShoeType, [FromForm] int? Categoryid, [FromForm] int? Seasonid, [FromForm] int Materialid, [FromForm] int Price)
        {
            try
            {
                var item = _unitOfWork.ProductRep.Get(id);
                if (item != null)
                {
                    item.Name = Name;
                    item.ShoeType = ShoeType;
                    item.Price = Price;
                    item.Materialid = Materialid;
                    item.Categoryid = Categoryid;
                    item.Seasonid = Seasonid;

                    _unitOfWork.ProductRep.Update(item);
                    _unitOfWork.Commit();
                    return Results.Ok();
                }

                else return Results.BadRequest();
            }
            catch (Exception ex) { return Results.BadRequest(ex.Message); }


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