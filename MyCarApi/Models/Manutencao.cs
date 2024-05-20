using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyCarApi.Models
{
    public class Manutencao
    {
        public int Id{get; set;}
        public string Nome{get; set;}
        public double Valor{get; set;}
        public int KmTroca{get; set;}

        public int KmMax {get;set;}

        [ForeignKey("CarId")]
        public int Car {get; set;}
}
}