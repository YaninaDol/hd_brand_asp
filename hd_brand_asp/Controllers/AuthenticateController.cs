using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RepositoriesLibrary.Roles;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using RepoLibrary.Interfaces;
using Microsoft.AspNetCore.Authorization;
using static System.Net.WebRequestMethods;
using System.Security.Cryptography.X509Certificates;
using RepositoriesLibrary.Models;
using System.Xml.Linq;
using System.Net.Mail;
using hd_brand_asp;

namespace WebApplication_Atlantis.Controllers
{
    [ApiController]
    [Route("/api/[controller]/")]
  
    public class AuthenticateController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        

        public AuthenticateController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _unitOfWork = unitOfWork;
            
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(Login model)
        {
          
            var user = await _userManager.FindByEmailAsync(model.Email);
         
            var d = _userManager.CheckPasswordAsync(user, model.Password);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userId = user.Id;
                var userRole = await _userManager.GetRolesAsync(user);
                var authClaims = new List<Claim> {
                     new Claim(ClaimTypes.NameIdentifier, userId),
                    new Claim(ClaimTypes.Email, user.Email),
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
                    userRole
                });
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("regUser")]
        public async Task<IActionResult> RegUser(Register model)
        {
            var userEx = await _userManager.FindByEmailAsync(model.Email);
            if (userEx != null) return StatusCode(StatusCodes.Status500InternalServerError, "User in db already");

            IdentityUser user = new()
            {
                UserName= model.Email,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            var res = await _userManager.CreateAsync(user, model.Password);
            if (!res.Succeeded) { return StatusCode(StatusCodes.Status500InternalServerError, "Creation failed!"); }
            if (await _roleManager.RoleExistsAsync(UserRoles.User))
                await _userManager.AddToRoleAsync(user, UserRoles.User);
            _unitOfWork.UserInfo.Create(new Userinfo() { UserId = user.Id, Email = user.Email });
            _unitOfWork.Commit();
            return Ok("User added!");
        }


        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        [Route("regAdmin")]

        public async Task<IActionResult> RegAdmin([FromBody] Register model)
        {
            var userEx = await _userManager.FindByEmailAsync(model.Email);
            if (userEx != null) return StatusCode(StatusCodes.Status500InternalServerError, "Admin in db already");

            IdentityUser user = new()
            {
                UserName = model.Email,
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
            var userEx = await _userManager.FindByEmailAsync(model.Email);
            if (userEx != null) return StatusCode(StatusCodes.Status500InternalServerError, "Menager in db already");

            IdentityUser user = new()
            {UserName = model.Email,
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
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> updateUser([FromForm] string userID, [FromForm] string UserName, [FromForm] string email, [FromForm] string UserSurname, [FromForm] string phonenumber)
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
        [Route("updateUserInfo")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> updateUserInfo([FromForm] string Name, [FromForm] string SurName, [FromForm] string email, [FromForm] string phonenumber)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return BadRequest("Invalid user ID");
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                user.UserName = email;
                user.Email = email;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    try
                    {
                        var item = _unitOfWork.UserInfo.GetUserinfo(user.Id);
                        if (item != null)
                        {
                            item.Name = Name;
                            item.Surname = SurName;
                            item.Phonenumber = phonenumber;
                            item.Email = email;
                            _unitOfWork.UserInfo.Get(item.Id);
                            _unitOfWork.Commit();
                            return Ok();
                        }

                        else return BadRequest("Bad request");


                    }
                    catch (Exception ex) { return BadRequest(ex.Message); }
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Update failed!");
                }
            }
            else
            {
                return NotFound("User not found");
            }

        
        }

        [HttpPost]
        [Route("deleteUser")]
       [Authorize(Roles = UserRoles.Admin)]

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
        [Authorize(Roles = UserRoles.Admin)]

        public async Task<IActionResult> getUsers()
        {
           
                return Ok(_userManager.Users.ToList().Where(x=>x.UserName!= "AdminAdmin01").ToList());
           

        }

        [HttpGet]
        [Route("getUserbyId")]
        [Authorize(Roles = $"{UserRoles.User},{UserRoles.Admin}")]

        public async Task<IActionResult> getUserbyId()
        {


            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return BadRequest("Invalid user ID");
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                
                return Ok(_unitOfWork.UserInfo.GetUserinfo(user.Id));
            }
            else
            {
                return BadRequest("Can't get user!");
            }


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
        [HttpPost]
        [Route("setLike")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> setLike( int prodId, bool like)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return BadRequest("Invalid user ID");
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                if (_unitOfWork.LikedProducts.setProduct(user.Id, prodId, like))
                {
                    _unitOfWork.Commit();
                    return Ok();
                }
                else
                {
                    return BadRequest("Invalid like");
                }
            }
            return BadRequest("Invalid user ID");
        }
     
        [HttpGet]
        [Route("getAllLikes")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> getAllLikes()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return BadRequest("Invalid user ID");
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                
               
                return Ok(_unitOfWork.LikedProducts.GetAllProducts(userId).ToList());
            }
            return BadRequest("Invalid user ID");
        }
        [HttpPost]
        [Route("getlike")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> getlike(int prodId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return BadRequest("Invalid user ID");
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {


                return Ok(_unitOfWork.LikedProducts.getProduct(prodId));
            }
            return BadRequest("Invalid user ID");
        }
        [HttpPost]
        [Route("v1")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> v1(string password)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return BadRequest("Invalid user ID");
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, password);

                if (result.Succeeded)
                {
                    return Ok("Password updated successfully");
                }
                else
                {
                   
                    return BadRequest("Failed to update password");
                }
            }

            return BadRequest("Invalid user ID");
        }
    
        
        }
}
