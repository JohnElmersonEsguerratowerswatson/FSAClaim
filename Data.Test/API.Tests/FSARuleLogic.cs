using FSA.API.Business;
using FSA.API.Models;
using FSA.API.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FSA.Test.API.Tests
{
    public class APIFSARuleLogic
    {

        FSARuleLogic _fSARuleLogic;
        private int _employeeID = 2;// Change Employee ID and Name accordingly
        private string _employeeName = "Gale Erickson";
        public APIFSARuleLogic()
        {
           // _fSARuleLogic = new FSARuleLogic(_employeeID);
        }

        [Fact]
        public void AddFSARuletoEmployee()
        {
            var rule = new TransactFSARule();
            rule.EmployeeID = _employeeID;
            //rule.EmployeeName = _employeeName;
            rule.FSAAmount = 5000;
            rule.YearCoverage = 2022;
            var result = _fSARuleLogic.AddFSARule(rule);
            Assert.True(result.IsSuccess);
        }
        [Fact]
        public void AddFSARuletoEmployeeFail()
        {
            _employeeID = 3;
            _employeeName = "John Doe";

            ITransactFSARule rule;
           // _fSARuleLogic = new FSARuleLogic();
            try
            {
                rule = _fSARuleLogic.Get();
            }
            catch (Exception ex)
            {
                Assert.Equal(ex.GetType(), new KeyNotFoundException().GetType());
            }


            //rule = new TransactFSARule();
            //rule.EmployeeID = _employeeID;
            //rule.EmployeeName = _employeeName;
            //rule.FSAAmount = 5000;
            //rule.YearCoverage = 2022;
            //var result = _fSARuleLogic.AddFSARule(rule);
            //Assert.False(result.IsSuccess);
        }

    }
}
