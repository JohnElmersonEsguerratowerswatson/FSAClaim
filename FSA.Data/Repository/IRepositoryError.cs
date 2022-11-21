using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSA.Data.Repository
{
    public interface IRepositoryError
    {
        public string Message { get; }
        public string Trace { get; }
    }
}
