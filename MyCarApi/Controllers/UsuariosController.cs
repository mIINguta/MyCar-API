using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
    }
}