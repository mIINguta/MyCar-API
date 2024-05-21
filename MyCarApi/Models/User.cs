using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyCarApi.Models
{
    public class User
    {
        public string Email { get; set; }    
        public string Senha {get; set;}


    }
}