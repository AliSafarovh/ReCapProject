using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public interface IDataResult<T> : IResult  //<T> T tipinden data ve IResult interfeysinden dan mesaj qaytaracaq
    {
        T Data { get; }
    }
}
