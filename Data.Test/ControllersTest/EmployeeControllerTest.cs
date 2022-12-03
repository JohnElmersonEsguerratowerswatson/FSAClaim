using AutoFixture;
using FSA.API.Business.Interfaces;
using FSA.API.Business.Model;
using FSA.API.Business.Services;
using FSA.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FSA.Test.ControllersTest
{
    public class EmployeeControllerTest
    {
        private Mock<IEmployeeService> _service;
        private EmployeeController _controller;

        public EmployeeControllerTest()
        {
            _service = new Mock<IEmployeeService>();
            _controller = new EmployeeController(_service.Object);
            Setup();
        }

        private void Setup()
        {
            _service.Setup(s => s.GetEmployees()).Returns(GenerateEmployeeViewList());
        }

        private List<EmployeeModel> GenerateEmployeeViewList()
        {
            var fixture = new Fixture();
            return fixture.Create<List<EmployeeModel>>();
        }

        [Fact]
        public void GetEmployees_Should_Return_OK()
        {
            var result = _controller.Index();
            Assert.True(typeof(OkObjectResult) == result.Result.GetType());

        }

    }
}
