﻿using FSA.API.Models.Interface;

namespace FSA.API.Models
{
    public class GetFSARule : IGetFSARule
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public decimal FSAAmount { get; set; }
        public int YearCoverage { get; set; }

    }
}
