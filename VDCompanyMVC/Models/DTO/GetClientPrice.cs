using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AFCStudio.Models.DTO
{
    public class GetClientPrice
    {
        [JsonConstructor]
        public GetClientPrice(int id, decimal price , string name)
        {
            Id = id;
            Price = price;
            Name = name;
        }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }
    }
}
