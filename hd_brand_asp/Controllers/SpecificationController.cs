using RepositoriesLibrary.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoriesLibrary.Models;
using System.Security.Claims;
using RepoLibrary.Interfaces;
using hd_brand_asp.Models;
using RepoLibrary.UnitofWork;

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
        //[Authorize(Roles = $"{UserRoles.Menager},{UserRoles.Admin}")]
        [Route("AddCategory")]
        public IResult AddCategory([FromForm] string categoryName)
        {
            try
            {

                _unitOfWork.CategoryRep.Create(new Category() { Name = categoryName });
                _unitOfWork.Commit();
                return Results.Ok();

            }
            catch (Exception ex) { return Results.Ok(ex.Message); }

        }


        [HttpPost]
        //[Authorize(Roles = $"{UserRoles.Menager},{UserRoles.Admin}")]
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
        //[Authorize(Roles = $"{UserRoles.Menager},{UserRoles.Admin}")]
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
        //[Authorize(Roles = $"{UserRoles.Menager},{UserRoles.Admin}")]
        [Route("AddMaterial")]
        public IResult AddMaterial([FromForm] string materialName)
        {
            try
            {

                _unitOfWork.MaterialRep.Create(new Material() { Name = materialName });
                _unitOfWork.Commit();
                return Results.Ok();

            }
            catch (Exception ex) { return Results.BadRequest(ex.Message); }

        }


        [HttpPost]
        //[Authorize(Roles = $"{UserRoles.Menager},{UserRoles.Admin}")]
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
        //[Authorize(Roles = $"{UserRoles.Menager},{UserRoles.Admin}")]
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
        //[Authorize(Roles = $"{UserRoles.Menager},{UserRoles.Admin}")]
        [Route("AddShoeType")]
        public IResult AddShoeType([FromForm] string shoeTypeName)
        {
            try
            {

                _unitOfWork.ShoeTypeRep.Create(new ShoeType() { Name = shoeTypeName });
                _unitOfWork.Commit();
                return Results.Ok();

            }
            catch (Exception ex) { return Results.BadRequest(ex.Message); }

        }


        [HttpPost]
        //[Authorize(Roles = $"{UserRoles.Menager},{UserRoles.Admin}")]
        [Route("UpdateShoeType")]
        public IResult UpdateShoeType([FromForm] int id, [FromForm] string newName)
        {
            try
            {
                var item = _unitOfWork.ShoeTypeRep.Get(id);
                if (item != null)
                {
                    item.Name = newName;
                    _unitOfWork.ShoeTypeRep.Update(item);
                    _unitOfWork.Commit();
                    return Results.Ok();
                }

                else return Results.BadRequest("Bad request");


            }
            catch (Exception ex) { return Results.BadRequest(ex.Message); }

        }



        [HttpPost]
        //[Authorize(Roles = $"{UserRoles.Menager},{UserRoles.Admin}")]
        [Route("DeleteShoeType")]
        public IResult DeleteShoeType([FromForm] int id)
        {
            try
            {
                if (_unitOfWork.ShoeTypeRep.Delete(id) == true)
                {
                    _unitOfWork.Commit();
                    return Results.Ok();
                }
                return Results.BadRequest("Remove items from shoe type!");

            }
            catch (Exception ex) { return Results.BadRequest(ex.Message); }

        }

        [HttpGet]
        [Route("GetShoeTypeById")]

        public IResult GetShoeTypeById(int id)
        {

            try
            {

                return Results.Ok(_unitOfWork.ShoeTypeRep.Get(id));

            }
            catch (Exception ex) { return Results.BadRequest(ex.Message); }

        }



        [HttpGet]
        [Route("GetAllShoeTypes")]

        public async Task<ActionResult<IEnumerable<ShoeType>>> GetAllShoeTypes()
        {
            try
            {
                return _unitOfWork.ShoeTypeRep.GetAll().ToList();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }

        }

        [HttpPost]
        //[Authorize(Roles = $"{UserRoles.Menager},{UserRoles.Admin}")]
        [Route("AddSeason")]
        public IResult AddSeason([FromForm] string seasonName)
        {
            try
            {

                _unitOfWork.SeasonRep.Create(new Season() { Name = seasonName });
                _unitOfWork.Commit();
                return Results.BadRequest();

            }
            catch (Exception ex) { return Results.BadRequest(ex.Message); }

        }


        [HttpPost]
        //[Authorize(Roles = $"{UserRoles.Menager},{UserRoles.Admin}")]
        [Route("UpdateSeason")]
        public IResult UpdateSeason([FromForm] int id, [FromForm] string newName)
        {
            try
            {
                var item = _unitOfWork.SeasonRep.Get(id);
                if (item != null)
                {
                    item.Name = newName;
                    _unitOfWork.SeasonRep.Update(item);
                    _unitOfWork.Commit();
                    return Results.Ok();
                }

                else return Results.BadRequest("Bad request");


            }
            catch (Exception ex) { return Results.BadRequest(ex.Message); }

        }



        [HttpPost]
        //[Authorize(Roles = $"{UserRoles.Menager},{UserRoles.Admin}")]
        [Route("DeleteSeason")]
        public IResult DeleteSeason([FromForm] int id)
        {
            try
            {
                if (_unitOfWork.SeasonRep.Delete(id) == true)
                {
                    _unitOfWork.Commit();
                    return Results.Ok();
                }
                return Results.BadRequest("Remove items from season !");

            }
            catch (Exception ex) { return Results.BadRequest(ex.Message); }

        }

        [HttpGet]
        [Route("GetSeasonById")]

        public IResult GetSeasonById(int id)
        {

            try
            {

                return Results.Ok(_unitOfWork.SeasonRep.Get(id));

            }
            catch (Exception ex) { return Results.BadRequest(ex.Message); }

        }



        [HttpGet]
        [Route("GetAllSeasons")]

        public async Task<ActionResult<IEnumerable<Season>>> GetAllSeasons()
        {
            try
            {
                return _unitOfWork.SeasonRep.GetAll().ToList();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }

        }
    }
}
