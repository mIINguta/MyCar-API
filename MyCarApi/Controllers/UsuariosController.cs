using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyCarApi.Context;
using MyCarApi.Models;

namespace MyCarApi.Controllers
{
    
    [Route("users")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly MyCarContext _myCarContext;

        private Token _token;
        
        public UsuariosController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration, MyCarContext myCarContext){
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _myCarContext = myCarContext;
        }

        [HttpPost("Registrar")]
        public async Task <ActionResult<UserToken>> CreateUser([FromBody] User userModel){
           
           var user = new ApplicationUser {
            UserName = userModel.Email,
            Email = userModel.Email,
           };
           
           var result = await _userManager.CreateAsync(user, userModel.Senha); // o erro era porque a senha não tinha caracter especial, uma letra maiúscula e minúscula.
           

           if (result.Succeeded){
            return BuildToken(userModel);
           }
           else{
                return BadRequest("Usuário ou senha inválidos!");
           }
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserToken>> Login([FromBody] User userInfo){
            var result = await _signInManager.PasswordSignInAsync(userInfo.Email, userInfo.Senha, isPersistent: false, lockoutOnFailure: false);
            
            if(result.Succeeded){
                // var senha = await _userManager.FindByEmailAsync(userInfo.Email);//pegando id do usuario
                UserToken token = BuildToken(userInfo);
                return Ok(BuildToken(userInfo));

            }
            else
            {
                ModelState.AddModelError(string.Empty, "Login inválido.");
                return BadRequest(ModelState);
            }
        }
           private UserToken BuildToken(User userInfo){
                var claims = new []{
                    new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddHours(1);

            JwtSecurityToken token = new JwtSecurityToken (
                issuer: null,
                audience: null,
                claims: claims,
                expires: expiration,
                signingCredentials: creds);


                return new UserToken(){
                    Token = new JwtSecurityTokenHandler().WriteToken(token), 
                    Expiration = expiration
                };
           }


        }
    }
