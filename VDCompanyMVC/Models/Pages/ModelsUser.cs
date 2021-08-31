using AFCStudio.Models.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VDCompanyMVC.Models.Objects;

namespace VDCompanyMVC.Models.Pages
{
    public class ModelUserCases
    {
        public List<Case> Cases { get; set; }
    }
    public class ModelUserCase
    {
        public Case Cases { get; set; }
    }
    public class ModelUserBills
    {
        public List<Bill> Bills { get; set; }
    }
    public class ModelUserReport
    {
        public int Id { get; set; }
        public List<Report> Reports { get; set; }
    }
    public class ModelUserPrice
    {
        public int Id { get; set; }
        public List<Price> Price { get; set; }
    }
}
