using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyCarApi.Models
{
    public class Manutencao
    {
        [Key]
        public int Id{get; set;}
        public string Descricao{get; set;}
        public double Valor{get; set;}
        public string DataManutencao {get;set;}
        public int QuilometragemAtual {get; set;}

        public int QuilometragemMaxima {get;set;}

        public int IdCarro {get;set;}
        // para que a minha classe disponibilize no JSON receber um id para a chave estrangeira


}
}