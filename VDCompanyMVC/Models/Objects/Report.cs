using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VDCompanyMVC.Models.Objects
{
    public enum TypeReport 
    { 
        SEO, Sites, Searches
    }
    public class Report
    {
        //public Report(int id, string name, int caseId, string way, DateTime dateAdd, TypeReport typeReport)
        //{
        //    Id = id;
        //    Name = name;
        //    CaseId = caseId;
        //    Way = way;
        //    DateAdd = dateAdd;
        //    TypeReport = typeReport;
        //}

        public int Id { get; set; } 
        public string Name { get; set; }
        public int CaseId { get; set; }
        public string Way { get; set; }
        public DateTime DateAdd { get; set; }
        [Column(TypeName="nvarchar(25)")]
        public TypeReport TypeReport { get; set; }
    }
}
