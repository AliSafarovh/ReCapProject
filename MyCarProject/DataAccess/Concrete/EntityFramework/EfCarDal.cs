﻿using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Console.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, CarDataBaseContext>, ICarDal
    {
        public List<CarDetailDto> GetCarDetails()
        {
            using (CarDataBaseContext context=new CarDataBaseContext())
            {
                var result = from c in context.Cars
                             join cl in context.Colors
                             on c.ColorId equals cl.ColorId
                             join b in context.Brands
                             on c.BrandId equals b.BrandId
                             select new CarDetailDto { BrandName=b.BrandName,ColorName=cl.ColorName,
                                 CarName=c.CarName,DailyPrice=c.DailyPrice};
                return result.ToList();
            }

        }
    }
}
