using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFİnalProje.Domain.Utilities.Concretes
{
    public class SuccesDataResult<T>: DataResult<T> where T : class
    {
        public SuccesDataResult() : base(default, true) { }
        public SuccesDataResult(string message) : base(default, true, message) { }
        public SuccesDataResult(T data, string message) : base(data, true, message) { }
    }
}
