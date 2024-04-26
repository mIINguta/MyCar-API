using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCarApi.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public string Idade { get; set; }
        public string Email { get; set; }        
    }
}