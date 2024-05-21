using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyCarApi.Context;
using MyCarApi.Models;

namespace MyCarApi.Controllers
{   
    [ApiController]
    [Route("auth")]
    
        public class CarsController : ControllerBase
    {

        private readonly MyCarContext _myCarContext;

        public CarsController (MyCarContext myCarContext){
            _myCarContext = myCarContext;
        }
        
        
        [Authorize]
        [HttpPost("RegistrarCarro")]
        public async Task<OkObjectResult> RegistrarCarro([FromBody] Car carInfo, [FromServices] UserToken token){
            
           _myCarContext.Cars.Add(carInfo);
            _myCarContext.SaveChanges();

            return Ok("GG");
        }
        
    }
}