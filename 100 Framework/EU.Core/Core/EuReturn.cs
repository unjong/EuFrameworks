using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EU.Core
{
    public class EuReturn
    {
        public bool IsSuccess { get; set; }
        public IList<dynamic> Parameters { get; set; }
    }
}
