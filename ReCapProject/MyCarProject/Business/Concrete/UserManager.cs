using Business.Abstract;
using Business.Constants;
using Core.Utilities;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userdal;
        public UserManager(IUserDal userDal)
        {
            _userdal = userDal;
        }

        public IResult Add(User user)
        {
            _userdal.Add(user);
            return new SuccessResult(Messages.ProductAdded);
        }

        public IResult Delete(User user)
        {
            _userdal.Delete(user);
            return new SuccessResult(Messages.ProductDeleted);
        }

        public IDataResult<List<User>> GetAll()
        {
            return new DataResult<List<User>>(_userdal.GetAll(), true,Messages.ProductListed);
        }


        public IResult Update(User user)
        {
            _userdal.Update(user);

            return new SuccessResult(Messages.ProductUpdated);
        }

        IDataResult<User> IUserService.GetById(int userId)
        {
            return new SuccessDataResult<User>(_userdal.Get(r => r.UserId == userId),Messages.ProductListed);
        }

    }
}
