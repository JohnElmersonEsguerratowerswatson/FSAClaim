using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSA.Common
{
    public static class ObjectStatus
    {
        public static String ObjectNotFound { get { return "Not Found"; } }

        public static String ModelStateInvalid { get { return "Please check your inputs."; } }
    }
}
