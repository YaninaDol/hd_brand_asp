using hd_brand_asp.Data;
using hd_brand_asp.Migrations;
using hd_brand_asp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RepoLibrary.Interfaces;
using RepositoriesLibrary.Roles;
using System.Security.Claims;
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
        [Authorize(Roles = $"{UserRoles.Menager},{UserRoles.Admin}")]
        [Route("Add")]

        public IResult Add([FromForm] string Name, [FromForm] string Image, [FromForm] string Image3, [FromForm] string Color, [FromForm] string Image2, [FromForm] bool isNew, bool isDiscount, [FromForm] int? SalePrice, [FromForm] string Video, [FromForm] string SubCategoryid, [FromForm] int? Categoryid, [FromForm] int? Seasonid, [FromForm] int Materialid, [FromForm] int? Price, [FromForm] string Sizes)
        {

            _unitOfWork.ProductRep.Create(new Product() { Name = Name, Image3 = Image3, SubCategoryid = SubCategoryid, Image = Image, Image2 = Image2, isNew = isNew, isDiscount = isDiscount, SalePrice = SalePrice, Color = Color, Video = Video, Categoryid = Categoryid, Seasonid = Seasonid, Materialid = Materialid, Price = Price, Sizes = Sizes });

            _unitOfWork.Commit();

            //  _cacheService.SetData("Products", _unitOfWork.ProductRep.GetAll(), DateTimeOffset.Now.AddDays(1));
            var lastAddedProduct = _unitOfWork.ProductRep.GetAll().OrderByDescending(p => p.Id).FirstOrDefault();
            if (lastAddedProduct != null)
            {
                if (lastAddedProduct.Sizes != "13")
                {
                    _unitOfWork.ProductRep.procedure(Sizes, lastAddedProduct);
                    _unitOfWork.Commit();
                }
                else
                {
                    _unitOfWork.ProductssizeRep.Create(new Productssize() { Productid = lastAddedProduct.Id, Image = lastAddedProduct.Image, Name = lastAddedProduct.Name, Size = "one size", Price = lastAddedProduct.Price });
                    _unitOfWork.Commit();
                }
            }

            return Results.Ok();


        }
        [HttpPost]
        [Authorize(Roles = $"{UserRoles.Menager},{UserRoles.Admin}")]
        [Route("Delete")]

        public IResult Delete([FromForm] int Id)
        {
            _unitOfWork.ProductssizeRep.DeleteAll(Id);
            _unitOfWork.Commit();
            _unitOfWork.ProductRep.Delete(Id);
            _unitOfWork.Commit();



            //  _cacheService.SetData("Products", _unitOfWork.ProductRep.GetAll(), DateTimeOffset.Now.AddDays(1));
            return Results.Ok();


        }

        [HttpPost]
        [Authorize(Roles = $"{UserRoles.Menager},{UserRoles.Admin}")]
        [Route("Update")]

        public IResult Update([FromForm] int id, [FromForm] string Name, [FromForm] string Image3, [FromForm] string Image2, [FromForm] string Color, [FromForm] bool isNew, [FromForm] bool isDiscount, [FromForm] int? SalePrice, [FromForm] string Image, [FromForm] string Video, [FromForm] string SubCategoryid, [FromForm] int? Categoryid, [FromForm] int? Seasonid, [FromForm] int Materialid, [FromForm] int Price, [FromForm] string Sizes)
        {

            try
            {
                var item = _unitOfWork.ProductRep.Get(id);
                var listitems = _unitOfWork.ProductRep.updateprocedure(id);

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
                    item.Image3 = Image3;
                    item.Image2 = Image2;
                    item.isNew = isNew;
                    item.isDiscount = isDiscount;
                    item.SalePrice = SalePrice;
                    item.Color = Color;
                    _unitOfWork.ProductRep.Update(item);



                    foreach (var iter in listitems)
                    {
                        iter.Name = Name;
                        iter.Price = SalePrice;
                        iter.Image = Image;
                        _unitOfWork.ProductssizeRep.Update(iter);

                    }
                    _unitOfWork.Commit();
                    return Results.Ok();
                }

                else return Results.BadRequest();
            }
            catch (Exception ex) { return Results.BadRequest(ex.Message); }


        }

        [HttpGet]
        [Route("GetProductById")]

        public IResult GetProductById(int id)
        {

            try
            {

                return Results.Ok(_unitOfWork.ProductRep.Get(id));

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
        [Route("GetSizeofProduct")]
        public async Task<ActionResult<IEnumerable<Productssize>>> GetSizeofProduct(int id)

        {
            return _unitOfWork.ProductRep.getSizes(id).ToList();
        }

        [HttpGet]
        [Route("GetProductsByCategory")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategory(int id)

        {
            return _unitOfWork.ProductRep.getByCategory(id).ToList();
        }

        [HttpGet]
        [Route("GetProductsBySubcategory")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsBySubcategory(int id)

        {
            return _unitOfWork.ProductRep.getByShoeType(id).ToList();
        }

        [HttpGet]
        [Authorize(Roles = $"{UserRoles.Menager},{UserRoles.Admin}")]
        [Route("GetAKNv")]

        public string GetCities(int id)

        {
            return "24443d18027301d444ec98b00ef49598";
        }

        [HttpPost]
        [Authorize(Roles = $"{UserRoles.Menager},{UserRoles.Admin}")]
        [Route("WeeklyLook")]
        public IResult WeeklyLook([FromForm] int oldId, [FromForm] int newId)

        {
            try
            {
                var item = _unitOfWork.ProductRep.Get(oldId);
                var item2 = _unitOfWork.ProductRep.Get(newId);
                if (item != null && item2 != null)
                {
                    item.WeeklyLook = false;

                    _unitOfWork.ProductRep.Update(item);
                    item2.WeeklyLook = true;

                    _unitOfWork.ProductRep.Update(item2);
                    _unitOfWork.Commit();


                    return Results.Ok();
                }

                else return Results.Ok("Bad request");


            }
            catch (Exception ex) { return Results.BadRequest(ex.Message); }
        }
        
    }
}