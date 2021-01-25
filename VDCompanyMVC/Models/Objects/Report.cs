using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VDCompanyMVC.Models.Objects
{

    public class Report
    {
        public Report(string name, int caseId, string way, DateTime dateAdd)
        {
            Name = name;
            CaseId = caseId;
            Way = way;
            DateAdd = dateAdd;
        }

        public int Id { get; set; } 
        public string Name { get; set; }
        public int CaseId { get; set; }
        public string Way { get; set; }
        public DateTime DateAdd { get; set; }
    }
}
