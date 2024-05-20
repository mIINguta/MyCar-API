using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCarApi.Models
{
    public class Car
    {
        public int Id{get; set;}
        public string Nome {get; set;}
        public string Marca {get; set;}
        public int AnoFabricacao {get; set;}
        public int Kilometragem {get; set;}

        public  ICollection<Manutencao> IdManutencao {get; set;}
    }
}