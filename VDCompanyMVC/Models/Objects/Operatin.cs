using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace AFCStudio.Models.Objects
{
    public class Operatin
    {
        public int Id { get; set; }
        public TimelessType TimelessType { get; set; }
        public OperationType OperationType { get; set; }
        public string Coment { get; set; }
        public double Amount { get; set; }
        public DateTime DateAdd { get; set; }
    }
    public enum TimelessType
    {
        [EnumMember(Value = "")]
        None,
        [EnumMember(Value = "Текущие")]
        Current,
        [EnumMember(Value = "Плановые")]
        Planing,
        [EnumMember(Value = "Плановые_в_текущие")]
        PlaningToCurrent
    }
    public enum OperationType
    {
        None = 0,
        Income = 1,
        Сonsumption = 2,
    }
}
