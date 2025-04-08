using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Console
{
    public class InMemoryCarDal : ICarDal
    {
        List<Car> _cars;

        public InMemoryCarDal()
        {
            _cars = new List<Car>
            {
                new Car { CarId = 1, CarName = "BMW", BrandId = 1, ColorId = 1, ModelYear = "2002", DailyPrice = 21500 },
                new Car { CarId = 2, CarName = "Mercedes", BrandId = 1, ColorId = 2, ModelYear = "2022", DailyPrice = 35500 },
                new Car { CarId = 3, CarName = "Opel", BrandId = 3, ColorId = 3, ModelYear = "2005", DailyPrice = 22500 },
                new Car { CarId = 4, CarName = "Volvo", BrandId = 1, ColorId = 2, ModelYear = "1998", DailyPrice = 15500 },
                new Car { CarId = 5, CarName = "Kia", BrandId = 1, ColorId = 1, ModelYear = "2002", DailyPrice = 21500 }
            };
        }

        public void Add(Car entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Car entity)
        {
            throw new NotImplementedException();
        }

        public Car Get(Expression<Func<Car, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Car> GetAll(Expression<Func<Car, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public List<CarDetailDto> GetCarDetails()
        {
            throw new NotImplementedException();
        }

        public void Update(Car entity)
        {
            throw new NotImplementedException();
        }

        //public void Add(Car car)
        //{
        //    _cars.Add(car);
        //}

        //public void Delete(Car car)
        //{
        //    Car carToDelete = _cars.FirstOrDefault(c => c.CarId == car.CarId);
        //    if (carToDelete != null)
        //    {
        //        _cars.Remove(carToDelete);
        //    }
        //}

        //public List<Car> GetAll()
        //{
        //    return _cars;
        //}

        //public Car GetById(int Id)
        //{
        //    return _cars.SingleOrDefault(c => c.CarId == Id);
        //}

        //public void Update(Car car)
        //{
        //    Car carToUpdate = _cars.FirstOrDefault(c => c.CarId == car.CarId);
        //    if (carToUpdate != null)
        //    {
        //        carToUpdate.CarName = car.CarName;
        //        carToUpdate.ColorId = car.ColorId;
        //        carToUpdate.DailyPrice = car.DailyPrice;
        //        carToUpdate.ModelYear = car.ModelYear;
        //    }
        //}
    }
}
