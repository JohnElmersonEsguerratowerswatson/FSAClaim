﻿using AutoFixture;
using FSA.API.Business;
using FSA.API.Models;
using FSA.Data.Repository;
using FSA.Data.Repository.FSAClaimRepository;
using FSA.Data.Repository.GenericRepository;
using FSA.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FSA.Test.ServiceTest
{

    public class ClaimsServiceTest
    {
        ClaimsBusinessLogic _logic;
        Mock<IRepository<FSAClaim>> _claimRepository;
        Mock<IJoinRepository<Employee, EmployeeFSA, FSARule>> _employeeFSARepository;
        Mock<IRepository<Employee>> _employeeRepository;
        private int _employeeID = 6;
        private decimal _totalFSA = 0;
        private decimal _timesAddClaim = 6;
        private decimal _fsaLimit = 5000;
        private List<FSAClaim> _fSAClaims = new List<FSAClaim>();

        private Fixture _fixture;

       

        public ClaimsServiceTest()
        {
            _claimRepository = new Mock<IRepository<FSAClaim>>();
            _employeeFSARepository = new Mock<IJoinRepository<Employee, EmployeeFSA, FSARule>>();
            _employeeRepository = new Mock<IRepository<Employee>>();
            _logic = new ClaimsBusinessLogic(_claimRepository.Object, _employeeFSARepository.Object, _employeeRepository.Object);
            _logic.EmployeeID = _employeeID;
            _fixture=new Fixture();
            _fSAClaims = GenerateFSAClaims();
        }

        private List<FSAClaim> GenerateFSAClaims()
        {
            if (_fSAClaims.Count > 0) return _fSAClaims;
            var fixture = new Fixture();
            var result = fixture.Create<List<FSAClaim>>();
            result.ForEach(x =>
            {
                x.EmployeeID = _employeeID;
                x.Status = "Pending";
                x.DateSubmitted = DateTime.Now;
                x.ReceiptDate = DateTime.Now;
                x.ReceiptAmount += x.ClaimAmount;
                x.ClaimAmount = (_fsaLimit / 2) / result.Count;
                x.isCancelled = false;
            });

            _totalFSA = result.Sum(r => r.ClaimAmount);
            _fSAClaims = result;
            return result;
        }

        private Employee GenerateEmployee()
        {
           
            var result = _fixture.Build<Employee>().Do(e => e.ID = _employeeID).Create();
            return result;
        }

        private ClaimRepositoryResult GenerateSuccessClaimResult()
        {

            _fixture.Register<ClaimRepositoryResult>(() => new ClaimRepositoryResult(true));
            var result = _fixture.Create<ClaimRepositoryResult>();
            return result;
        }

        private ClaimRepositoryResult GenerateFailClaimResult()
        {

            _fixture.Register<ClaimRepositoryResult>(() => new ClaimRepositoryResult(false, "Failed transaction."));
            var result = _fixture.Create<ClaimRepositoryResult>();
            return result;
        }

        private FSARule GenerateFSARule()
        {
          
            var result = _fixture.Create<FSARule>();
            result.FSALimit = _fsaLimit;
            return result;
        }
        private List<FSARule> GenerateFSARuleList()
        {
           
            var result = _fixture.Create<List<FSARule>>();
            result.ForEach(r => r.FSALimit += _totalFSA);
            return result;
        }

        private TransactClaim GenerateValidClaimInput()
        {
           
            var claim = _fixture.Create<TransactClaim>();
            claim.ClaimAmount = (_totalFSA / 2) / _timesAddClaim;
            claim.ReceiptAmount += claim.ClaimAmount;
            claim.ReceiptDate = "03/10/2022";
            return claim;
        }

        private TransactClaim GenerateValidClaimUpdateInput()
        {
            
            var fsaClaim = _fSAClaims.First();
            var claim = _fixture.Create<TransactClaim>();
            claim.ReferenceNumber = fsaClaim.ReferenceNumber;
            claim.ClaimAmount = (_totalFSA / 2) / _timesAddClaim;
            claim.ReceiptAmount += claim.ClaimAmount;
            claim.ReceiptDate = "03/10/2022";
            return claim;
        }




        private void AddEditSetup()
        {
           _employeeFSARepository.Setup(r => r.GetList(It.IsAny<Func<Employee, bool>>())).Returns(GenerateFSARuleList());
            _employeeFSARepository.Setup(r => r.Get(It.IsAny<Func<Employee, bool>>())).Returns(GenerateFSARule());
            _claimRepository.Setup(r => r.GetList(It.IsAny<Func<FSAClaim, bool>>())).Returns(GenerateFSAClaims());
        }

        [Fact]
        public void AddClaim_Should_Return_SuccessResult()
        {
            AddEditSetup();
            _claimRepository.Setup(r => r.Add(It.IsAny<FSAClaim>())).Returns(GenerateSuccessClaimResult());
            var result = _logic.AddClaim(GenerateValidClaimInput());
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void EditClaim_Should_Return_SuccessResult()
        {
            AddEditSetup();
            _claimRepository.Setup(r => r.Update(It.IsAny<FSAClaim>(), It.IsAny<Func<FSAClaim, bool>>())).Returns(GenerateSuccessClaimResult());
            var result = _logic.Update(GenerateValidClaimUpdateInput());
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void DeleteClaim_Should_Return_SuccessResult()
        {
            _claimRepository.Setup(r => r.Delete(true, It.IsAny<Func<FSAClaim, bool>>())).Returns(GenerateSuccessClaimResult());
            var result = _logic.Delete(GenerateValidClaimUpdateInput());
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void GetList_Should_Return_NonEmptyList()
        {
            
            _claimRepository.Setup(r => r.GetList(It.IsAny<Func<FSAClaim, bool>>())).Returns(GenerateFSAClaims());
            var claims = _logic.GetClaimList();
            Assert.NotEmpty(claims);
        }

        [Fact]
        public void GetClaim_Should_Return_Claim()
        {
            _claimRepository.Setup(r => r.Get(It.IsAny<Func<FSAClaim, bool>>())).Returns(_fSAClaims.First());
            var claim = _logic.GetClaim(_fSAClaims.First().ReferenceNumber);
            Assert.NotNull(claim);
            Assert.Equal(claim.ReferenceNumber, _fSAClaims.First().ReferenceNumber);
        }
    }
}
