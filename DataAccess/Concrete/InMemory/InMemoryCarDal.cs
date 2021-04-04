using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryCarDal : ICarDal
    {
        List<Car> _cars;
        public InMemoryCarDal()
        {
            _cars = new List<Car>
            {
                new Car{Id=1,BrandId=2,ColorId=2,DailyPrice=600,Description="Ford",ModelYear=2015},
                new Car{Id=2,BrandId=1,ColorId=3,DailyPrice=800,Description="Dodge",ModelYear=2010},
                new Car{Id=3,BrandId=3,ColorId=5,DailyPrice=300,Description="Mercedes",ModelYear=2016},
                new Car{Id=4,BrandId=6,ColorId=7,DailyPrice=200,Description="Bugatti",ModelYear=2017},
                new Car{Id=5,BrandId=1,ColorId=9,DailyPrice=700,Description="Citroen",ModelYear=2019}
            };
        }
        public void Add(Car car)
        {
            _cars.Add(car);
        }
        public void Delete(Car car)
        {
            Car carToDelete = _cars.SingleOrDefault(c => c.Id == car.Id);
            _cars.Remove(carToDelete);
        }
        public List<Car> GetAll()
        {
            return _cars;
        }
        public void Update(Car car)
        {
            Car carToUpdate = _cars.SingleOrDefault(c => c.Id == car.Id);
            carToUpdate.ModelYear = car.ModelYear;
            carToUpdate.BrandId = car.BrandId;
            carToUpdate.ColorId = car.ModelYear;
            carToUpdate.DailyPrice = car.DailyPrice;
            carToUpdate.Description = car.Description;
        }
        public List<Car> GetById(Car car)
        {
            return _cars.Where(c => c.Id == car.Id).ToList();
        }
        public List<Car> GetAll(Expression<Func<Car, bool>> filter = null)
        {
            throw new NotImplementedException();
        }
        public Car Get(Expression<Func<Car, bool>> filter)
        {
            throw new NotImplementedException();
        }
        public List<CarDetailDto> GetCarDetail()
        {
            throw new NotImplementedException();
        }
        public List<CarDetailDto> GetCarDetailByCarId(int id)
        {
            throw new NotImplementedException();
        }
    }
}
