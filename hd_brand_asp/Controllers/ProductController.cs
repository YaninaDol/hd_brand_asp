using hd_brand_asp.Data;
using hd_brand_asp.Migrations;
using hd_brand_asp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
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
        public IResult Add([FromForm] string Article, [FromForm] string Name, IFormFile Video, IFormFile Image, IFormFile  Image3, IFormFile Image2, [FromForm] string Color,  [FromForm] bool isNew, bool isDiscount, [FromForm] int? SalePrice, [FromForm] string SubCategoryid, [FromForm] int? Categoryid, [FromForm] int? Seasonid, [FromForm] int Materialid, [FromForm] int? Price, [FromForm] string Sizes)
        {
            string imagePath = _unitOfWork.ProductRep.SaveFile(Image);
            string imagePath2 = _unitOfWork.ProductRep.SaveFile(Image2);
            string imagePath3 = _unitOfWork.ProductRep.SaveFile(Image3);

            
            string videoPath = _unitOfWork.ProductRep.SaveFile(Video);
            _unitOfWork.ProductRep.Create(new Product() {Article=Article, Name = Name, Image3 = imagePath3, SubCategoryid = SubCategoryid, Image = imagePath, Image2 = imagePath2, isNew = isNew, isDiscount = isDiscount, SalePrice = SalePrice, Color = Color, Video = videoPath, Categoryid = Categoryid, Seasonid = Seasonid, Materialid = Materialid, Price = Price, Sizes = Sizes });

            _unitOfWork.Commit();

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
                    _unitOfWork.ProductssizeRep.Create(new Productssize() {Article=lastAddedProduct.Article, Productid = lastAddedProduct.Id, Image = lastAddedProduct.Image, Name = lastAddedProduct.Name, Size = "one size", Price = lastAddedProduct.Price });
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

        public IResult Update([FromForm] int id, [FromForm] string Article, [FromForm] string Name,[FromForm] string Color, [FromForm] bool isNew, [FromForm] bool isDiscount, [FromForm] int? SalePrice,  [FromForm] string SubCategoryid, [FromForm] int? Categoryid, [FromForm] int? Seasonid, [FromForm] int Materialid, [FromForm] int Price, [FromForm] string Sizes)
        {

            try
            {
                var item = _unitOfWork.ProductRep.Get(id);
                var listitems = _unitOfWork.ProductRep.updateprocedure(id);

                if (item != null)
                {
                   // _unitOfWork.ProductRep.DeleteFile(item.Image.Replace('/','\\'));
                  //  _unitOfWork.ProductRep.DeleteFile(item.Image.Replace('/', '\\'));
                   // _unitOfWork.ProductRep.DeleteFile(item.Image.Replace('/', '\\'));
                  //  _unitOfWork.ProductRep.DeleteFile(item.Image.Replace('/', '\\'));

                   // string imagePath = _unitOfWork.ProductRep.SaveFile(Image);
                  //  string imagePath2 = _unitOfWork.ProductRep.SaveFile(Image2);
                  //  string imagePath3 = _unitOfWork.ProductRep.SaveFile(Image3);
                   // string videoPath = _unitOfWork.ProductRep.SaveFile(Video);

                    item.Article = Article;
                    item.Name = Name;
                    item.SubCategoryid = SubCategoryid;
                    item.Price = Price;
                    item.Materialid = Materialid;
                    item.Categoryid = Categoryid;
                    item.Seasonid = Seasonid;
                    item.Sizes = Sizes;
                 //   item.Image = imagePath;
                 //   item.Video = videoPath;
                 //   item.Image3 = imagePath3;
                 //   item.Image2 = imagePath2;
                    item.isNew = isNew;
                    item.isDiscount = isDiscount;
                    item.SalePrice = SalePrice;
                    item.Color = Color;
                    _unitOfWork.ProductRep.Update(item);



                    foreach (var iter in listitems)
                    {
                        iter.Name = Name;
                        iter.Article = Article;
                        iter.Price = SalePrice;
                      //  iter.Image = imagePath;
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
        [HttpPost]
       //[Authorize(Roles = $"{UserRoles.Menager},{UserRoles.Admin}")]
        [Route("SetVideoItem")]
        public IResult SetVideoItem(IFormFile Video,[FromForm] int prodId)
        {
            string videoPath = _unitOfWork.ProductRep.SaveFile(Video);
            try {
                _unitOfWork.ContentVideos.Create(new ContentVideo() { URL = videoPath, prodId = prodId });
                _unitOfWork.Commit();
                return Results.Ok();
            }
            catch { return Results.BadRequest(); }

            
        }

        [HttpPost]
        [Authorize(Roles = $"{UserRoles.Menager},{UserRoles.Admin}")]
        [Route("UpdateVideoContent")]
        public IResult UpdateVideoContent([FromForm] int id, IFormFile video)
        {
            string videoPath = _unitOfWork.ProductRep.SaveFile(video);
            try
            {
                var item = _unitOfWork.ContentVideos.Get(id);
                if (item != null)
                {
                    item.URL = videoPath;
                  
                    _unitOfWork.ContentVideos.Update(item);
                    _unitOfWork.Commit();
                    return Results.Ok();
                }

                else return Results.Ok("Bad request");


            }
            catch (Exception ex) { return Results.BadRequest(ex.Message); }

        }
        [HttpPost]
        [Authorize(Roles = $"{UserRoles.Menager},{UserRoles.Admin}")]
        [Route("UpdateProductContent")]
        public IResult UpdateProductContent([FromForm] int id, [FromForm] int prodId)
        {
          
            try
            {
                var item = _unitOfWork.ContentVideos.Get(id);
                if (item != null)
                {
                 
                    item.prodId = prodId;
                    _unitOfWork.ContentVideos.Update(item);
                    _unitOfWork.Commit();
                    return Results.Ok();
                }

                else return Results.Ok("Bad request");


            }
            catch (Exception ex) { return Results.BadRequest(ex.Message); }

        }

        [HttpGet]
        [Route("GetContentVideo")]

        public async Task<ActionResult<IEnumerable<ContentVideo>>> GetContentVideo()
        {
            try
            {
                return _unitOfWork.ContentVideos.GetAll().ToList();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }

        }

    }
}