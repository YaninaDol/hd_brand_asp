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
    public class PostController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public PostController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        [HttpPost]
        [Authorize(Roles = $"{UserRoles.Menager},{UserRoles.Admin}")]
        [Route("AddUkrCity")]
        public IResult AddUkrCity([FromForm] string Name, [FromForm] int RegionId)
        {
            try
            {

                _unitOfWork.UkrCityRep.Create(new UkrCity() { Name = Name,REGION_ID=RegionId });
                _unitOfWork.Commit();
                return Results.Ok();

            }
            catch (Exception ex) { return Results.Ok(ex.Message); }

        }


        [HttpPost]
        [Authorize(Roles = $"{UserRoles.Menager},{UserRoles.Admin}")]
        [Route("UpdateUkrCity")]
        public IResult UpdateUkrCity([FromForm] int id, [FromForm] string newName, [FromForm] int Region)
        {
            try
            {
                var item = _unitOfWork.UkrCityRep.Get(id);
                if (item != null)
                {
                    item.Name = newName;
                    item.REGION_ID= Region;
                    _unitOfWork.UkrCityRep.Update(item);
                    _unitOfWork.Commit();
                    return Results.Ok();
                }
               
                else return Results.Ok("Bad request");


            }
            catch (Exception ex) { return Results.BadRequest(ex.Message); }

        }



        [HttpPost]
        [Authorize(Roles = $"{UserRoles.Menager},{UserRoles.Admin}")]
        [Route("DeleteUkrCity")]
        public IResult DeleteUkrCity([FromForm] int id)
        {
            try
            {
                if (_unitOfWork.UkrCityRep.Delete(id)==true)
                {
                    _unitOfWork.Commit();
                    return Results.Ok();
                }
                return Results.BadRequest("Remove items from category!");

            }
            catch (Exception ex) { return Results.BadRequest(ex.Message); }

        }

        [HttpGet]
        [Route("GetCityById")]

        public IResult GetCityById(int id)
        {

            try
            {

                return Results.Ok(_unitOfWork.UkrCityRep.Get(id));

            }
            catch (Exception ex) { return Results.BadRequest(ex.Message); }

        }

    

        [HttpGet]
        [Route("GetAllCities")]

        public async Task<ActionResult<IEnumerable<UkrCity>>> GetAllCities()
        {
            try
            {
                return _unitOfWork.UkrCityRep.GetAll().ToList();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }

        }


       
    }
}
