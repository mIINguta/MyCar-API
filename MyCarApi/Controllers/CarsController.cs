using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
        [Authorize]
        [HttpGet("ConsultarCarrosUsuario")]
        public async Task<IQueryable> ConsultarCarros (string id){
          
            var carros = (
            from M in _myCarContext.Manutencoes 
            from C in _myCarContext.Cars
            where C.IdUsuario == id 
            select new {
                    id = C.Id,
                    nome = C.Nome,
                    marca = C.Marca,
                    kilometragem = C.Kilometragem,
                    usuario = C.IdUsuario,
                    anoFabricacao = C.AnoFabricacao,
                    manutencoes = C.Manutencoes
            }); // C.manutencoes recebe as manutencoes como chave

            return carros;
        }

    }
}