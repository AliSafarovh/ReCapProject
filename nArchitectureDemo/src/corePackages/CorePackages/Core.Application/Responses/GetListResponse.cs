using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Responses
{
    public class GetListResponse<T>:BasePageableModel
    {
        private IList<T> items;

        public IList<T> Items
        { get => items??=new List<T>();
          set => items = value;
        }
    }


}
