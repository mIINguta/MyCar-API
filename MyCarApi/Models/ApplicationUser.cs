using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MyCarApi.Models
{
    public class ApplicationUser : IdentityUser
    {
       
        [ForeignKey("id_carro_usuario")]
        public virtual ICollection<Car> Cars {get;set;}
    }
}