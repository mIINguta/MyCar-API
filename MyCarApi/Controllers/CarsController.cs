using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyCarApi.Context;
using MyCarApi.Models;

namespace MyCarApi.Controllers
{   
    [ApiController]
    [Route("auth/cars")]
    
        public class CarsController : ControllerBase
    {

        private readonly MyCarContext _myCarContext;

        public CarsController (MyCarContext myCarContext){
            _myCarContext = myCarContext;
        }
        [Authorize]
        [HttpPost("register")]
        public async Task<OkObjectResult> RegistrarCarro([FromBody] Car carInfo, [FromServices] UserToken token){
            
           _myCarContext.Cars.Add(carInfo);
            _myCarContext.SaveChanges();

            return Ok("GG");
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IQueryable> ConsultarCarros(string id)
        {

            var carros = (
            from C in _myCarContext.Cars
            where C.IdUsuario == id
            select new
            {
                id = C.Id,
                modelo = C.Modelo,
                marca = C.Marca,
                placa = C.Placa,
                quilometragemCompra = C.QuilometragemCompra,
                quilometragemAtual = C.QuilometragemAtual,
                usuario = C.IdUsuario,
                anoFabricacao = C.AnoFabricacao,
                manutencoes = C.Manutencoes
            });
            // C.manutencoes recebe as manutencoes como chave

            return carros;
        }
        [Authorize]
        [HttpPut("{id}/quilometragem")]

        public IActionResult AtualizarQuilometragem(int id, int quilometragemAtual){
            Console.Write("Esse é o id do carro " + id + "\n");
            Console.Write("Essa é a quilometragem " + quilometragemAtual + "\n");

            var carro = _myCarContext.Cars.Find(id);

            if(carro == null){
                return NotFound();
            }

            carro.QuilometragemAtual = quilometragemAtual;
            _myCarContext.SaveChanges();
            return Ok("A quilometragem foi atualizada!");
        }

        [Authorize]
        [HttpDelete("{id}")]

        public IActionResult DeletarCarro (int id){
           var carro = _myCarContext.Cars.Find(id);

            if (carro == null){
                return NotFound();
            }
          
                _myCarContext.Cars.Remove(carro);
                _myCarContext.SaveChanges();
                return Ok();
        }

    }
}