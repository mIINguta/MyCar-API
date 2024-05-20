using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCarApi.Models
{
    public class Token
    {
        public string AcessToken { get; set; }
        public string Expiration { get; set; }
    }
}