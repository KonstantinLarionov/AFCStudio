using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AFCStudio.Models.Objects
{
    public class Cassa
    {
        public int Id { get; set; } 
        public List<Operatin> Operatins { get; set; }
        public string Name { get; set; }
        public double BalanceCurrent { get; set; }
        public double СonsumptionCurrent { get; set; }
        public double IncomeCurrent { get; set; }
        public double BalancePlaning { get; set; }
        public double СonsumptionPlaning { get; set; }
        public double IncomePlaning { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public int Open { get; set; }
    }
}
