using RepositoriesLibrary.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoriesLibrary.Models;
using System.Security.Claims;
using RepoLibrary.Interfaces;
using hd_brand_asp.Models;
using RepoLibrary.UnitofWork;
using hd_brand_asp.Migrations;

namespace WebApplication_Atlantis.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpecificationController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public SpecificationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        [HttpPost]
        [Authorize(Roles = $"{UserRoles.Menager},{UserRoles.Admin}")]
        [Route("AddCategory")]
        public IResult AddCategory([FromForm] string Name)
        {
            try
            {

                _unitOfWork.CategoryRep.Create(new Category() { Name = Name });
                _unitOfWork.Commit();
                return Results.Ok();

            }
            catch (Exception ex) { return Results.Ok(ex.Message); }

        }


        [HttpPost]
        [Authorize(Roles = $"{UserRoles.Menager},{UserRoles.Admin}")]
        [Route("UpdateCategory")]
        public IResult UpdateCategory([FromForm] int id, [FromForm] string newName)
        {
            try
            {
                var item = _unitOfWork.CategoryRep.Get(id);
                if (item != null)
                {
                    item.Name = newName;
                    _unitOfWork.CategoryRep.Update(item);
                    _unitOfWork.Commit();
                    return Results.Ok();
                }
               
                else return Results.Ok("Bad request");


            }
            catch (Exception ex) { return Results.BadRequest(ex.Message); }

        }



        [HttpPost]
        [Authorize(Roles = $"{UserRoles.Menager},{UserRoles.Admin}")]
        [Route("DeleteCategory")]
        public IResult DeleteCategory([FromForm] int id)
        {
            try
            {
                if (_unitOfWork.CategoryRep.Delete(id)==true)
                {
                    _unitOfWork.Commit();
                    return Results.Ok();
                }
                return Results.BadRequest("Remove items from category!");

            }
            catch (Exception ex) { return Results.BadRequest(ex.Message); }

        }

        [HttpGet]
        [Route("GetCategoryById")]

        public IResult GetCategoryById(int id)
        {

            try
            {

                return Results.Ok(_unitOfWork.CategoryRep.Get(id));

            }
            catch (Exception ex) { return Results.BadRequest(ex.Message); }

        }

    

        [HttpGet]
        [Route("GetAllCategory")]

        public async Task<ActionResult<IEnumerable<Category>>> GetAllCategory()
        {
            try
            {
                return _unitOfWork.CategoryRep.GetAll().ToList();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }

        }


        [HttpPost]
        [Authorize(Roles = $"{UserRoles.Menager},{UserRoles.Admin}")]
        [Route("AddMaterial")]
        public IResult AddMaterial([FromForm] string Name)
        {
            try
            {

                _unitOfWork.MaterialRep.Create(new Material() { Name = Name });
                _unitOfWork.Commit();
                return Results.Ok();

            }
            catch (Exception ex) { return Results.BadRequest(ex.Message); }

        }


        [HttpPost]
        [Authorize(Roles = $"{UserRoles.Menager},{UserRoles.Admin}")]
        [Route("UpdateMaterial")]
        public IResult UpdateMaterial([FromForm] int id, [FromForm] string newName)
        {
            try
            {
                var item = _unitOfWork.MaterialRep.Get(id);
                if (item != null)
                {
                    item.Name = newName;
                    _unitOfWork.MaterialRep.Update(item);
                    _unitOfWork.Commit();
                    return Results.Ok();
                }

                else return Results.Ok("Bad request");


            }
            catch (Exception ex) { return Results.BadRequest(ex.Message); }

        }



        [HttpPost]
        [Authorize(Roles = $"{UserRoles.Menager},{UserRoles.Admin}")]
        [Route("DeleteMaterial")]
        public IResult DeleteMaterial([FromForm] int id)
        {
            try
            {
                if (_unitOfWork.MaterialRep.Delete(id) == true)
                {
                    _unitOfWork.Commit();
                    return Results.Ok();
                }
                return Results.BadRequest("Remove items from material!");

            }
            catch (Exception ex) { return Results.BadRequest(ex.Message); }

        }




        [HttpGet]
        [Route("GetMaterialById")]

        public IResult GetMaterialById(int id)
        {

            try
            {

                return Results.Ok(_unitOfWork.MaterialRep.Get(id));

            }
            catch (Exception ex) { return Results.BadRequest(ex.Message); }

        }



        [HttpGet]
        [Route("GetAllMaterials")]

        public async Task<ActionResult<IEnumerable<Material>>> GetAllMaterials()
        {
            try
            {
                return _unitOfWork.MaterialRep.GetAll().ToList();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }

        }


        [HttpPost]
        [Authorize(Roles = $"{UserRoles.Menager},{UserRoles.Admin}")]
        [Route("AddSubCategory")]
        public IResult AddSubCategory([FromForm] string Name)
        {
            try
            {

                _unitOfWork.SubCategoryRep.Create(new hd_brand_asp.Models.SubCategory() { Name = Name });
                _unitOfWork.Commit();
                return Results.Ok();

            }
            catch (Exception ex) { return Results.BadRequest(ex.Message); }

        }


        [HttpPost]
        [Authorize(Roles = $"{UserRoles.Menager},{UserRoles.Admin}")]
        [Route("UpdateSubCategory")]
        public IResult UpdateSubCategory([FromForm] int id, [FromForm] string newName)
        {
            try
            {
                var item = _unitOfWork.SubCategoryRep.Get(id);
                if (item != null)
                {
                    item.Name = newName;
                    _unitOfWork.SubCategoryRep.Update(item);
                    _unitOfWork.Commit();
                    return Results.Ok();
                }

                else return Results.BadRequest("Bad request");


            }
            catch (Exception ex) { return Results.BadRequest(ex.Message); }

        }



        [HttpPost]
        [Authorize(Roles = $"{UserRoles.Menager},{UserRoles.Admin}")]
        [Route("DeleteSubCategory")]
        public IResult DeleteSubCategory([FromForm] int id)
        {
            try
            {
                if (_unitOfWork.SubCategoryRep.Delete(id) == true)
                {
                    _unitOfWork.Commit();
                    return Results.Ok();
                }
                return Results.BadRequest("Remove items from sub category!");

            }
            catch (Exception ex) { return Results.BadRequest(ex.Message); }

        }

        [HttpGet]
        [Route("GetSubCategoryRepById")]

        public IResult GetSubCategoryRepById(int id)
        {

            try
            {

                return Results.Ok(_unitOfWork.SubCategoryRep.Get(id));

            }
            catch (Exception ex) { return Results.BadRequest(ex.Message); }

        }



        [HttpGet]
        [Route("GetAllSubCategories")]

        public async Task<ActionResult<IEnumerable<hd_brand_asp.Models.SubCategory>>> GetAllSubCategories()
        {
            try
            {
                return _unitOfWork.SubCategoryRep.GetAll().ToList();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }

        }

        [HttpPost]
        [Authorize(Roles = $"{UserRoles.Menager},{UserRoles.Admin}")]
        [Route("AddSize")]
        public IResult AddSize([FromForm] string Name)
        {
            try
            {

                _unitOfWork.SizeRep.Create(new Size() { Value = Name });
                _unitOfWork.Commit();
                return Results.BadRequest();

            }
            catch (Exception ex) { return Results.BadRequest(ex.Message); }

        }


        [HttpPost]
        [Authorize(Roles = $"{UserRoles.Menager},{UserRoles.Admin}")]
        [Route("UpdateSize")]
        public IResult UpdateSize([FromForm] int id, [FromForm] string newName)
        {
            try
            {
                var item = _unitOfWork.SizeRep.Get(id);
                if (item != null)
                {
                    item.Value = newName;
                    _unitOfWork.SizeRep.Update(item);
                    _unitOfWork.Commit();
                    return Results.Ok();
                }

                else return Results.BadRequest("Bad request");


            }
            catch (Exception ex) { return Results.BadRequest(ex.Message); }

        }



        [HttpPost]
        [Authorize(Roles = $"{UserRoles.Menager},{UserRoles.Admin}")]
        [Route("DeleteSize")]
        public IResult DeleteSize([FromForm] int id)
        {
            try
            {
                if (_unitOfWork.SizeRep.Delete(id) == true)
                {
                    _unitOfWork.Commit();
                    return Results.Ok();
                }
                return Results.BadRequest("Remove items from season !");

            }
            catch (Exception ex) { return Results.BadRequest(ex.Message); }

        }

        [HttpGet]
        [Route("GetSizeById")]

        public IResult GetSizeById(int id)
        {

            try
            {

                return Results.Ok(_unitOfWork.SizeRep.Get(id));

            }
            catch (Exception ex) { return Results.BadRequest(ex.Message); }

        }


        [HttpGet]
        [Route("GetAllSeasons")]

        public async Task<ActionResult<IEnumerable<Season>>> GetAllSeasons()
        {
            try
            {
                return _unitOfWork.SizeRep.GetSeasons;
            }
            catch (Exception ex) { return BadRequest(ex.Message); }

        }

        [HttpGet]
        [Route("GetSeasonById")]

        public  IResult GetSeasonById(int id)
        {
            try
            {

                return Results.Ok(_unitOfWork.SizeRep.getSeasonById(id));

            }
            catch (Exception ex) { return Results.BadRequest(ex.Message); }

        }

        [HttpGet]
        [Route("GetAllSizes")]

        public async Task<ActionResult<IEnumerable<Size>>> GetAllSizes()
        {
            try
            {
                return _unitOfWork.SizeRep.GetAll().ToList();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }

        }
        [HttpGet]
        [Route("GetAllColors")]

        public async Task<ActionResult<IEnumerable<Color>>> GetAllColors()
        {
            try
            {
                return _unitOfWork.ColorRep.GetAll().ToList();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }

        }

        [HttpGet]
        [Route("GetSubCategoryNamesByCategoryId")]

        public async Task<ActionResult<IEnumerable<SubCategory>>> GetSubCategoryNamesByCategoryId(int id)
        {
            try
            {
                return _unitOfWork.CategoryRep.GetSubCategoryNamesByCategoryId(id).ToList();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }

        }
        
        [HttpGet]
        [Route("MaterialNamesByCategoryId")]

        public async Task<ActionResult<IEnumerable<Material>>> MaterialNamesByCategoryId(int id)
        {
            try
            {
                return _unitOfWork.CategoryRep.MaterialNamesByCategoryId(id).ToList();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }

        }
    }
}
