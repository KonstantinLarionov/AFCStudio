using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VDCompanyMVC.Models.Objects
{
    public enum TypeReport
    {
        Seo, Groups, Searches
    }
    public class Report
    {
        public Report(string name, int caseId, string type, TypeReport way, DateTime dateAdd)
        {
            Name = name;
            CaseId = caseId;
            Type = type;
            Way = way;
            DateAdd = dateAdd;
        }

        public int Id { get; set; } 
        public string Name { get; set; }
        public int CaseId { get; set; }
        public string Type { get; set; }
        public TypeReport Way { get; set; }
        public DateTime DateAdd { get; set; }
    }
}
