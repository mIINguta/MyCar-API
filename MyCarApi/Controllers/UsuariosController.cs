using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyCarApi.Models;

namespace MyCarApi.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class UsuariosController : ControllerBase
    {
        private readonly UserManager<AplicationUser> _userManager;
        private readonly SignInManager<AplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        
        public UsuariosController(UserManager<AplicationUser> userManager, SignInManager<AplicationUser> signInManager, IConfiguration configuration){
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;

        }

        [HttpPost("Registrar")]
        public async Task <ActionResult<UserToken>> CreateUser([FromBody] User userModel){
           
           var user = new AplicationUser{
            UserName = userModel.Email,
            Email = userModel.Email,
           };
           var result = await _userManager.CreateAsync(user, userModel.Senha);
           if (result.Succeeded){
            return BuildToken(userModel);
           }
           else{
                ModelState.AddModelError(string.Empty, "Login inválido!");
                return BadRequest(ModelState);
           }
        }
           private UserToken BuildToken(User userModel){
                var claims = new []{
                    new Claim(JwtRegisteredClaimNames.UniqueName, userModel.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddHours(1);

            JwtSecurityToken token = new JwtSecurityToken (
                issuer: null,
                audience: null,
                claims: claims,
                expires: expiration,
                signingCredentials: creds);
                return new UserToken(){
                    Token = new JwtSecurityTokenHandler().WriteToken(token), Expiration = expiration
                };
           
            

           }


        }
    }
