﻿using AutoFixture;
using FSA.API.Business;
using FSA.API.Controllers;
using FSA.API.Models;
using FSA.API.Models.Interface;
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
    public class FSARuleControllerTest
    {
        private Mock<IFSARuleService> _service;
        private FSARuleController _controller;
        public FSARuleControllerTest()
        {
            _service = new Mock<IFSARuleService>();
            _controller = new FSARuleController(_service.Object);
            Setup();
        }

        private void Setup()
        {
            _service.Setup(x => x.AddFSARule(It.IsAny<ITransactFSARule>())).Returns(GenerateAddFSARuleResult());
        }

        private AddFSARuleResult GenerateAddFSARuleResult()
        {
            var fixture = new Fixture();
            return fixture.Create<AddFSARuleResult>();

        }

        private TransactFSARule GenerateValidTransactFSARule()
        {
            var fixture = new Fixture();
            return fixture.Create<TransactFSARule>();
        }

        [Fact]
        public void Add_Should_Return_OK()
        {
            var addResult = _controller.Add(GenerateValidTransactFSARule());
            Assert.True(typeof(OkObjectResult) == addResult.Result.GetType());
        }
    }
}