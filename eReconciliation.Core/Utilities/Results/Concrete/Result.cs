using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eReconciliation.Core.Utilities.Results.Abstract;

namespace eReconciliation.Core.Utilities.Results.Concrete
{
    public class Result : IResult
    {
        public Result(bool success)
        {
            Success = success;
        }

        public Result(bool success, string message) : this(success)
        {
            Message = message;
        }

        public bool Success { get; }
        public string Message { get; }
    }
}