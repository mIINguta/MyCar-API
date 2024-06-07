using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyCarApi.Models
{
    public class Car
    {
        [Key]
        public int Id{get; set;} // definimos como primary key, porém, na hora de passar o JSON, não preciso passar o ID, pq o EF entende como auto increment.
        public string Nome {get; set;}
        public string Marca {get; set;}
        public int AnoFabricacao {get; set;}
        public int Kilometragem {get; set;}

        public int KilometragemAtual {get;set;} 

        public string IdUsuario {get;set;}
        
        [ForeignKey("IdCarro")] 
        public virtual ICollection<Manutencao> Manutencoes {get; set;}
        // um carro pode ter uma ou mais manutenções, então declaro essa Icollection aqui pois ela irá gerar uma chave estrangeira na tabela manutenções
    }
}