using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyCarApi.Models;

namespace MyCarApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ValuesController : ControllerBase
    {
    
        [HttpGet("ObterCarros")]
        [Authorize]
        public IActionResult ObterCarros([FromServices] UserToken token ){
           var carros = new {
            Marca= "Ford",
            Modelo = "Focus"
           };

           return Ok(carros.ToString());
    
        }
        
    }
}