using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public interface IResult
    {

        // bunlar void metodlar ucundur.
        bool Success { get; }   //get sadece oxuna bilen metodlar 
        string Message { get; }
    }
}
