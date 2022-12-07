using AutoFixture;
using FSA.API.Business.Interfaces;
using FSA.API.Business.Model;
using FSA.API.Business.Services;
using FSA.API.Controllers;
using FSA.API.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
        private Fixture _fixture;

        public EmployeeControllerTest()
        {
            _service = new Mock<IEmployeeService>();
            _controller = new EmployeeController(_service.Object);
            _fixture = new Fixture();
        }



        private List<EmployeeModel> GenerateEmployeeViewList()
        {
           
            return _fixture.Create<List<EmployeeModel>>();
        }

        [Fact]
        public void GetEmployees_Should_Return_OK()
        {
            var employees = GenerateEmployeeViewList(); 
            _service.Setup(s => s.GetEmployees()).Returns(employees); 
            var result = _controller.Index();
            Assert.True(typeof(OkObjectResult) == result.Result.GetType());
            OkObjectResult okResult = (OkObjectResult)result.Result;
            Assert.Equal(okResult.Value, employees); 

        }

        private AddEmployeeModel GenerateEmployeeModel()
        {
            return _fixture.Create<AddEmployeeModel>();
        }

        private AddEmployeeResult GenerateAddEmployeeSuccessResult()
        {
            var result = _fixture.Create<AddEmployeeResult>();
            result.IsSuccess= true;
            return result;
        }

        [Fact]
        public void AddEmployee_Should_Return_OK()
        {
            var model = GenerateEmployeeModel();
            _service.Setup(s => s.Add(model)).Returns(GenerateAddEmployeeSuccessResult());
            var result =_controller.Add(model);
            Assert.True(typeof(OkObjectResult) == result.Result.GetType());
            var okResult = (OkObjectResult) result.Result;
            Assert.True(((AddEmployeeResult)okResult.Value).IsSuccess);
        }

        [Fact]
        public void InvalidEmployeeModel_Should_Return_False()
        {
            var model = GenerateEmployeeModel();
            model.FirstName = "Verylongnamecje8jcn83hf98eh938h98hv98hviuhh9iudshisuhvisehviushvishviwshviwuhviwuhveiu";
            //_service.Setup(s => s.Add(model)).Returns(GenerateAddEmployeeSuccessResult());
            //var result = _controller.Add(model);
            //Assert.True(typeof(BadRequestObjectResult) == result.Result.GetType());
            //var okResult = (BadRequestObjectResult)result.Result;
            //Assert.False(((AddEmployeeResult)okResult.Value).IsSuccess);
                       
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(typeof(AddEmployeeResult), typeof(AddEmployeeResult)), typeof(AddEmployeeResult));

            var isModelStateValid = Validator.TryValidateObject(model, context, results, true);
            Assert.False(isModelStateValid);
        }

    }
}