using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class Result : IResult
    {
       
        public Result(bool success, string message):this(success) //hem true qaytarir hem de mesaj qaytaran metod
        {
           Message = message; 
        }
        public Result(bool success) //sadece true qaytaran metod
        {            
            Success = success;
        }

        public bool Success  {get;} 
        public string Message  {get;}
    }
}
