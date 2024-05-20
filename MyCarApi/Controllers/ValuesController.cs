using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyCarApi.Context;
using MyCarApi.Models;

namespace MyCarApi.Controllers
{
    [ApiController]
    [Route("auth")]
    public class ValuesController : ControllerBase
    {
        private readonly MyCarContext _myCarContext;
        private readonly UserManager<ApplicationUser> _userManager;

    
        public ValuesController(MyCarContext myCarContext, UserManager<ApplicationUser> userManager){
            _myCarContext = myCarContext;
            _userManager = userManager;
        }
        [HttpGet("ObterCarros")]
        [Authorize]
        public IActionResult ObterCarros([FromServices] UserToken token){
           var carros = new {
            Marca= "Ford",
            Modelo = "Focus"
           };
           return Ok(carros.ToString());
        }
        
        [HttpGet("RemoveUser")]
        [Authorize]
        public IActionResult RemoveUser(){
            Console.WriteLine("");
            return Ok("Autorizado rapaz!");
        }
    }
}