using RepositoriesLibrary.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoriesLibrary.Models;
using System.Security.Claims;
using RepoLibrary.Interfaces;
using hd_brand_asp.Models;

namespace WebApplication_Atlantis.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
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
        public IResult UpdateCategory([FromForm] int id, [FromForm] string categoryName)
        {
            try
            {
                if (_unitOfWork.CategoryRep.UpdateCategory(id, categoryName) == true)

                {
                    _unitOfWork.Commit();
                    return Results.Ok();
                }
                else return Results.Ok("Bad request");


            }
            catch (Exception ex) { return Results.Ok(ex.Message); }

        }



        [HttpPost]
        //[Authorize(Roles = $"{UserRoles.Menager},{UserRoles.Admin}")]
        [Route("Delete")]
        public IResult Delete([FromForm] int id)
        {
            try
            {
                if (_unitOfWork.CategoryRep.DeleteCategory(id)==true)
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
        [Route("GetAll")]

        public async Task<ActionResult<IEnumerable<Category>>> GettAll()
        {
            try
            {
                return _unitOfWork.CategoryRep.GetAll().ToList();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }

        }
        

    }
}
