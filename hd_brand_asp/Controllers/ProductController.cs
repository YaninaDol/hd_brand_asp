using hd_brand_asp.Data;
using hd_brand_asp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepoLibrary.Interfaces;
using RepositoriesLibrary.Roles;
using System.Security.Cryptography;

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

        public IResult Add([FromForm] string Name, [FromForm] string Image, [FromForm] string Video, [FromForm] string SubCategoryid, [FromForm] int? Categoryid, [FromForm] int? Seasonid, [FromForm] int Materialid,  [FromForm] int Price, [FromForm] string Sizes)
        {
            _unitOfWork.ProductRep.Create(new Product() { Name = Name, SubCategoryid = SubCategoryid,Image=Image,Video=Video, Categoryid = Categoryid, Seasonid = Seasonid, Materialid = Materialid, Price = Price, Sizes= Sizes });
            _unitOfWork.Commit();

            //  _cacheService.SetData("Products", _unitOfWork.ProductRep.GetAll(), DateTimeOffset.Now.AddDays(1));
            var lastAddedProduct = _unitOfWork.ProductRep.GetAll().OrderByDescending(p => p.Id).FirstOrDefault();

            // Вернуть последний добавленный продукт
            return Results.Ok(lastAddedProduct);


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
        //[Authorize(Roles = $"{UserRoles.Menager},{UserRoles.Admin}")]
        [Route("Update")]

        public IResult Update([FromForm] int id,[FromForm] string Name, [FromForm] string Image, [FromForm] string Video, [FromForm] string SubCategoryid, [FromForm] int? Categoryid, [FromForm] int? Seasonid, [FromForm] int Materialid, [FromForm] int Price, [FromForm] string Sizes)
        {
            try
            {
                var item = _unitOfWork.ProductRep.Get(id);
                if (item != null)
                {
                    item.Name = Name;
                    item.SubCategoryid = SubCategoryid;
                    item.Price = Price;
                    item.Materialid = Materialid;
                    item.Categoryid = Categoryid;
                    item.Seasonid = Seasonid;
                    item.Sizes = Sizes;
                    item.Image = Image;
                    item.Video = Video;
                    _unitOfWork.ProductRep.Update(item);
                    _unitOfWork.Commit();
                    return Results.Ok(item);
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