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

        [HttpPost("maintenance")]
        [Authorize]

        public async Task<OkObjectResult> RegistrarManutencao([FromBody] Manutencao manutencao)
        {
            _myCarContext.Manutencoes.Add(manutencao);
            _myCarContext.SaveChanges();
            return Ok("Funcionou perfeito!");
        }

        [HttpGet("maintenances")]
        [Authorize]

        public async Task<IQueryable> ConsultarManutencoes (int idCarro){
            var manutencoes = _myCarContext.Manutencoes.Where(x => idCarro == x.IdCarro);
            return manutencoes;
        }
    
        [HttpPut("maintenance")]
        [Authorize]

        public IActionResult EditarManutencao([FromBody] Manutencao manutencao){
            var manutencaoEncontrada = _myCarContext.Manutencoes.Find(manutencao.Id);

            if (manutencaoEncontrada == null){
                return NotFound();
            }

            manutencaoEncontrada.Descricao = manutencao.Descricao;
            manutencaoEncontrada.DataManutencao = manutencao.DataManutencao;
            manutencaoEncontrada.Valor = manutencao.Valor;
            manutencaoEncontrada.QuilometragemAtual = manutencao.QuilometragemAtual;
            manutencaoEncontrada.QuilometragemMaxima = manutencao.QuilometragemMaxima;
            _myCarContext.SaveChanges();

            return Ok("Os dados foram atualizados");

        }


        [HttpDelete("maintenance")]
        [Authorize]

        public IActionResult DeletarManutencao (int id){
            var manutencao = _myCarContext.Manutencoes.Find(id);

            if(manutencao == null){
                return NotFound();
            }

            _myCarContext.Manutencoes.Remove(manutencao);
            _myCarContext.SaveChanges();

            return Ok();



        }
        
     }
}