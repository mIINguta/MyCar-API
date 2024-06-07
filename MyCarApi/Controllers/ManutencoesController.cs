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
    [Route("/auth")]
    public class ManutencoesController : ControllerBase
    {
        
        private readonly MyCarContext _myCarContext;

        public ManutencoesController(MyCarContext myCarContext){
            _myCarContext = myCarContext;
        }

        [HttpPost("RegistrarManutencao")]
        [Authorize]

        public async Task<OkObjectResult> RegistrarManutencao([FromBody] Manutencao manutencao, [FromServices] UserToken token)
        {
    
            manutencao.DataManutencao.ToString("dd/MM/yy : hh:mm");
            _myCarContext.Manutencoes.Add(manutencao);
            _myCarContext.SaveChanges();
            return Ok("Funcionou perfeito!");
        }

        [HttpGet("ConsultarManutencoes")]
        [Authorize]

        public async Task<IQueryable> ConsultarManutencoes (int idCarro){
            var manutencoes = _myCarContext.Manutencoes.Where(x => idCarro == x.IdCarro);
            return manutencoes;
        }
    }
}