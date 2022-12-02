using FSA.Data.Repository;
using FSA.Domain.Entities;
using FSA.Data.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSA.Data.Repository.GenericRepository;
using FSA.Common.Enums;

namespace FSA.Data.Repository.FSAClaimRepository
{

    public class FSAClaimRepository : GenericRepository<FSAClaim>
    {

        public FSAClaimRepository(bool test = false)
        {
            _test = test;
            ClaimContext = new FSAClaimContext();
        }

        protected override FSAClaimContext ClaimContext { get; set; }

        /// <summary>
        /// UPDATE FSACLAIM
        /// </summary>
        /// <param name="fSAClaim"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public override IRepositoryResult Update(FSAClaim fSAClaim, Func<FSAClaim, bool> predicate)
        {
            try
            {
                using (ClaimContext = new FSAClaimContext())
                {
                    var claim = ClaimContext.FSAClaims.SingleOrDefault(predicate);
                    if (claim == null) return new ClaimRepositoryResult(false, "Claim Not Found");

                    claim.ReceiptAmount = fSAClaim.ReceiptAmount;
                    claim.Modified = DateTime.UtcNow;
                    claim.ReceiptDate = fSAClaim.ReceiptDate;
                    claim.ClaimAmount = fSAClaim.ClaimAmount;
                    claim.ReceiptNumber = fSAClaim.ReceiptNumber;

                    int rows = Save(ClaimContext);
                    if (rows <= 0) return new ClaimRepositoryResult(false, "Update Failed");
                    return new ClaimRepositoryResult(true);
                }
            }
            catch (Exception ex)
            {
                return new ClaimRepositoryResult(false, ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// UPDATE APPROVAL {ADMIN ONLY}
        /// </summary>
        /// <param name="status"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IRepositoryResult Update(string status, Func<FSAClaim, bool> predicate)
        {
            try
            {
                using (ClaimContext = new FSAClaimContext())
                {
                    var claim = ClaimContext.FSAClaims.SingleOrDefault(predicate);
                    if (claim == null) return new ClaimRepositoryResult(false, "Claim Not Found");

                    claim.Status = status;

                    int rows = Save(ClaimContext);
                    if (rows <= 0) return new ClaimRepositoryResult(false, "Update Failed");
                    return new ClaimRepositoryResult(true);
                }
            }
            catch (Exception ex)
            {
                return new ClaimRepositoryResult(false, ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// Update Claim Approval(Approve Or Deny)
        /// </summary>
        /// <param name="approve"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IRepositoryResult Update(bool approve, Func<FSAClaim, bool> predicate)
        {
            try
            {
                using (ClaimContext = new FSAClaimContext())
                {
                    var claim = ClaimContext.FSAClaims.SingleOrDefault(predicate);
                    if (claim == null) return new ClaimRepositoryResult(false, "Claim Not Found");

                    claim.Status = approve ? ClaimApprovals.Approved.ToString() : ClaimApprovals.Denied.ToString();
                    claim.ApprovalDate = DateTime.Now;

                    int rows = Save(ClaimContext);
                    if (rows <= 0) return new ClaimRepositoryResult(false, "Update Failed");
                    return new ClaimRepositoryResult(true);
                }
            }
            catch (Exception ex)
            {
                return new ClaimRepositoryResult(false, ex.Message, ex.StackTrace);
            }
        }


        /// <summary>
        /// Update Claim Approval(Approve Or Deny)
        /// </summary>
        /// <param name="approve"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public override IRepositoryResult Delete(bool delete, Func<FSAClaim, bool> predicate)
        {
            try
            {
                using (ClaimContext = new FSAClaimContext())
                {
                    var claim = ClaimContext.FSAClaims.SingleOrDefault(predicate);
                    if (claim == null) return new ClaimRepositoryResult(false, "Claim Not Found");

                    claim.isCancelled = delete;
                    claim.ApprovalDate = DateTime.Now;

                    int rows = Save(ClaimContext);
                    if (rows <= 0) return new ClaimRepositoryResult(false, "Update Failed");
                    return new ClaimRepositoryResult(true);
                }
            }
            catch (Exception ex)
            {
                return new ClaimRepositoryResult(false, ex.Message, ex.StackTrace);
            }
        }

        //public IRepositoryResult Add(FSAClaim entity)
        //{
        //    try
        //    {
        //        using (_dbContext)
        //        {
        //            _dbContext.Add(entity);
        //        }
        //        return new ClaimRepositoryResult(true);
        //    }
        //    catch (Exception ex)
        //    {

        //        return new ClaimRepositoryResult(false, ex.Message, ex.StackTrace);
        //    }
        //}

        //public IRepositoryResult Delete(FSAClaim entity)
        //{
        //    try
        //    {
        //        using (_dbContext)
        //        {

        //            _dbContext.Remove(entity);
        //            return new ClaimRepositoryResult(true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ClaimRepositoryResult(false, ex.Message, ex.StackTrace);
        //    }
        //}

        //public ICollection<FSAClaim> GetList(Func<FSAClaim, bool> criteria)
        //{
        //    try
        //    {
        //        using (_dbContext)
        //        {

        //            return _dbContext.FSAClaims.Where(criteria).ToList();
        //        }
        //    }
        //    catch
        //    {
        //        return new List<FSAClaim>();
        //    }
        //}

        //public ICollection<FSAClaim> GetList()
        //{
        //    try
        //    {
        //        using (_dbContext)
        //        {

        //            return _dbContext.FSAClaims.ToList();
        //        }
        //    }
        //    catch
        //    {
        //        return new List<FSAClaim>();
        //    }
        //}

        //public FSAClaim Get(Func<FSAClaim,bool> predicate)
        //{
        //    try
        //    {
        //        using (_dbContext)
        //        {

        //            return _dbContext.FSAClaims.SingleOrDefault();
        //        }
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        //public IRepositoryResult SaveChanges()
        //{
        //    try
        //    {
        //        using (_dbContext)
        //        {

        //            int currentRows = _dbContext.SaveChanges();
        //            return new ClaimRepositoryResult(true, rows: currentRows);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ClaimRepositoryResult(false, ex.Message, ex.StackTrace);
        //    }
        //}

        //public IRepositoryResult Update(FSAClaim entity, Func<FSAClaim, bool> predicate)
        //{
        //    try
        //    {
        //        using (_dbContext)
        //        {
        //            var claim = _dbContext.FSAClaims.Where(predicate).SingleOrDefault();
        //            if (claim == null) return new ClaimRepositoryResult(false, "Claim Not Found");
        //            claim = entity;
        //        }
        //        return new ClaimRepositoryResult(true);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ClaimRepositoryResult(false, ex.Message, ex.StackTrace);
        //    }

        //}
    }
}
