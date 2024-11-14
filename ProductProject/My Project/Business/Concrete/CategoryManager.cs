using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        ICategoryDal _categorydal;
        public CategoryManager(ICategoryDal categoryDal)
        {
            _categorydal = categoryDal;
        }

        public IDataResult <List<Category>>GetAll()
        {
            return new SuccessDataResult <List<Category>>(_categorydal.GetAll());
        }

        public IDataResult <Category> GetById(int categoryid)
        {
            return new SuccessDataResult <Category>(_categorydal.Get(c => c.CategoryId == categoryid));
        }
        
    }
}
