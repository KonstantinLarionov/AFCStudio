using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AFCStudio.Models.Objects
{
    public class Price
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public double MinPrice { get; set; }
        public double MiddlePrice { get; set; } 
        public double MaxPrice { get; set; }
    }
}
