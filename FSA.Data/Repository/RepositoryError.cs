using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSA.Data.Repository
{
    public class RepositoryError : IRepositoryError
    {
        public string Message { get; }

        public string Trace { get; }

        public RepositoryError(string message, string trace)
        {
            Message = message;
            Trace = trace;
        }
    }
}
