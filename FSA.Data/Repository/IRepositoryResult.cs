using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSA.Data.Repository
{
    public interface IRepositoryResult
    {
        public bool IsSuccess { get; }
        public IRepositoryError? Error { get; }
        public int Rows { get; }

    }
}
