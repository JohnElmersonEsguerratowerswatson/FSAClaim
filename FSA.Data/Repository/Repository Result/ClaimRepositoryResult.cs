using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSA.Data.Repository.FSAClaimRepository
{
    public class ClaimRepositoryResult : IRepositoryResult
    {
        public bool IsSuccess { get; }

        public IRepositoryError? Error { get; }
        public int Rows { get; }

        public ClaimRepositoryResult(bool isSuccess, string message = "", string? trace = "", int rows = 0)
        {
            this.IsSuccess = isSuccess;
            if (!isSuccess)
            {
                trace = trace == null ? "" : trace;
                Error = new RepositoryError(message, trace);
            }
            else
            {
                this.Rows = rows;
            }
        }
    }
}
