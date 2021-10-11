using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AFCStudio.Models.DTO
{
    public class GetClientPrice
    {     
        public string Name { get; set; }
      
        public int Id { get; set; }

        public decimal Price { get; set; }
    }
}
