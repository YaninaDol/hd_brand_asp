﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RepositoriesLibrary.Roles;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.AspNetCore.Authorization;
using static System.Net.WebRequestMethods;
using System.Security.Cryptography.X509Certificates;
using RepositoriesLibrary.Models;


namespace WebApplication_Atlantis.Controllers
{
    [ApiController]
    [Route("/api/[controller]/")]
    public class AuthenticateController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AuthenticateController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(Login model)
        {

            var user = await _userManager.FindByNameAsync(model.UserName);
            var d = _userManager.CheckPasswordAsync(user, model.Password);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRole = await _userManager.GetRolesAsync(user);
                var authClaims = new List<Claim> {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                foreach (var role in userRole)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }

                var token = GetToken(authClaims);

                return Ok(new
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    userId = user.Id,
                    userRole
                });
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("regUser")]
        public async Task<IActionResult> RegUser(Register model)
        {
            var userEx = await _userManager.FindByNameAsync(model.UserName);
            if (userEx != null) return StatusCode(StatusCodes.Status500InternalServerError, "User in db already");

            IdentityUser user = new()
            {
                UserName = model.UserName,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var res = await _userManager.CreateAsync(user, model.Password);
            if (!res.Succeeded) { return StatusCode(StatusCodes.Status500InternalServerError, "Creation failed!"); }
            if (await _roleManager.RoleExistsAsync(UserRoles.User))
                await _userManager.AddToRoleAsync(user, UserRoles.User);
            return Ok("User added!");
        }


        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        [Route("regAdmin")]

        public async Task<IActionResult> RegAdmin([FromBody] Register model)
        {
            var userEx = await _userManager.FindByNameAsync(model.UserName);
            if (userEx != null) return StatusCode(StatusCodes.Status500InternalServerError, "Admin in db already");

            IdentityUser user = new()
            {
                UserName = model.UserName,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var res = await _userManager.CreateAsync(user, model.Password);
            if (!res.Succeeded) { return StatusCode(StatusCodes.Status500InternalServerError, "Creation failed!"); }

            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));


            //Доступно только то, что авторизированно админу !
            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _userManager.AddToRoleAsync(user, UserRoles.Admin);
            // доступны методы и пользователей
            //if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            //    await _userManager.AddToRoleAsync(user, UserRoles.User);

            return Ok("Admin added!");
        }


        [HttpPost]
        [Route("regMenager")]
        [Authorize(Roles = UserRoles.Admin)]

        public async Task<IActionResult> regMenager([FromBody] Register model)
        {
            var userEx = await _userManager.FindByNameAsync(model.UserName);
            if (userEx != null) return StatusCode(StatusCodes.Status500InternalServerError, "Menager in db already");

            IdentityUser user = new()
            {
                UserName = model.UserName,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var res = await _userManager.CreateAsync(user, model.Password);
            if (!res.Succeeded) { return StatusCode(StatusCodes.Status500InternalServerError, "Creation failed!"); }

            if (!await _roleManager.RoleExistsAsync(UserRoles.Menager))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Menager));
            if (!await _roleManager.RoleExistsAsync(UserRoles.Menager))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Menager));


            if (await _roleManager.RoleExistsAsync(UserRoles.Menager))
                await _userManager.AddToRoleAsync(user, UserRoles.Menager);
           
            //if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            //    await _userManager.AddToRoleAsync(user, UserRoles.User);

            return Ok("Menager added!");
        }

        [HttpPost]
        [Route("updateUser")]
        //[Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> updateUser([FromForm] string userID, [FromForm] string UserName, [FromForm] string email )
        {
            var user = await _userManager.FindByIdAsync(userID);

            if (user != null)
            {
                IdentityUser update = new()
                {
                    Id = user.Id,
                    UserName = UserName,
                    Email = email,
                    PasswordHash = user.PasswordHash,
                    SecurityStamp = Guid.NewGuid().ToString()
                };
                await _userManager.DeleteAsync(user);

                var res = await _userManager.CreateAsync(update);
                if (!res.Succeeded) { return StatusCode(StatusCodes.Status500InternalServerError, "Creation failed!"); }
                if (await _roleManager.RoleExistsAsync(UserRoles.User))
                    await _userManager.AddToRoleAsync(update, UserRoles.User);
                return Ok("User update!");
            }
            else return BadRequest("Can't update!");

        }

        [HttpPost]
        [Route("deleteUser")]
       // [Authorize(Roles = UserRoles.Admin)]

        public async Task<IActionResult> deleteUser([FromForm] string userID)
        {
            var user = await _userManager.FindByIdAsync(userID);
            if (user != null)
            {

                var res = await _userManager.DeleteAsync(user);
                     return Ok("User deleted!");
            }
            else return BadRequest("Can't delete!");

        }
       
        [HttpGet]
        [Route("getUsers")]
        //[Authorize(Roles = UserRoles.Admin)]

        public async Task<IActionResult> getUsers()
        {
           
                return Ok(_userManager.Users.ToList().Where(x=>x.UserName!= "AdminAdmin").ToList());
           

        }

        [HttpGet]
        [Route("getUserbyId")]
        [Authorize(Roles = UserRoles.Admin)]

        public async Task<IActionResult> getUserbyId(string IDuser)
        {

            var user = await _userManager.FindByIdAsync(IDuser);
            if (user != null)
            {

               
                return Ok(user);
            }
            else return BadRequest("Can't delete!");


        }


        private JwtSecurityToken GetToken(List<Claim> claimsList)
        {


            var signKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(6),
                    claims: claimsList,
                    signingCredentials: new SigningCredentials(signKey, SecurityAlgorithms.HmacSha256)
                );
          
            return token;
        }
    }
}
