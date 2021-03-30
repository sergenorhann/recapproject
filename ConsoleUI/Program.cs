using Business.Concrete;
using Core.Utilities.Helper;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.IO;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            ColorTest();
            BrandTest();
            CarTest();
            UserTest();
            CustomerTest();
            RentalTest();
            CarImageTest();
        }

        private static void ColorTest()
        {
            ColorManager colorManager = new(new EfColorDal());
            //colorManager.Delete(new Color { Id = 1 });
            //colorManager.Add(new Color { Id = 1, Name = "Black" });
            //colorManager.Update(new Color { Id = 2, Name = "White" });

            Console.WriteLine("--- " + colorManager.GetById(1).Message.ToUpper() + " ---\n" + colorManager.GetById(1).Data.Name + "\n");

            Console.WriteLine("\n--- " + colorManager.GetAll().Message.ToUpper() + " ---");
            foreach (var color in colorManager.GetAll().Data)
            {
                Console.WriteLine(color.Name);
            }
        }
        private static void BrandTest()
        {
            BrandManager brandManager = new(new EfBrandDal());
            //brandManager.Delete(new Brand { Id = 1 });
            //brandManager.Add(new Brand { Id = 1, Name = "Honda Motor Company" });
            //brandManager.Update(new Brand { Id = 2, Name = "Stellantis " });
            Console.WriteLine("\n--- " + brandManager.GetById(1).Message.ToUpper() + " ---\n" + brandManager.GetById(1).Data.Name + "\n");

            Console.WriteLine("--- " + brandManager.GetAll().Message.ToUpper() + " ---");
            foreach (var brand in brandManager.GetAll().Data)
            { 
                Console.WriteLine(brand.Name);
            }
        }
        private static void CarTest()
        {
            CarManager carManager = new(new EfCarDal());
            //carManager.Delete(new Car { Id = 2 });
            //carManager.Add(new Car { Id = 2, BrandId = 2, ColorId = 1, DailyPrice = 375, Description = "deneme", ModelYear = 2015 });
            //carManager.Update(new Car { Id =3, BrandId = 3, ColorId = 3, DailyPrice = 100, Description = "deneme2", ModelYear = 2021 });
            
            Console.WriteLine("\n--- " + carManager.GetById(2).Message.ToUpper() + " ---\n" + carManager.GetById(2).Data.Description + "\n");

            Console.WriteLine("--- "+carManager.GetAll().Message.ToUpper() + " ---");
            foreach (var car in carManager.GetAll().Data)
            {
                Console.WriteLine(car.Description);
            }

            Console.WriteLine("\n--- "+carManager.GetCarDetail().Message.ToUpper()+" ---");
            foreach (var car in carManager.GetCarDetail().Data)
            {
                Console.WriteLine(@"{0} - {1} - {2} - {3}",
                car.Id, car.ColorName, car.BrandName, car.DailyPrice);
            }
            
        }
        private static void UserTest()
        {
            UserManager userManager = new(new EfUserDal());
            //userManager.Add(new User {Id=2,FirstName="Veli",LastName="Yılmaz",Email="Veliyılmaz@gmail.com",Password="1412"});
            //userManager.Update(new User {Id=2,FirstName="Veli",LastName="Yılmaz",Email="Veliyılmaz@gmail.com",Password="5468"});
            //userManager.Delete(new User {Id=2});

            Console.WriteLine("\n--- " + userManager.GetById(1).Message.ToUpper() + " ---\n" + userManager.GetById(1).Data.FirstName + "\n");

            Console.WriteLine("--- " + userManager.GetAll().Message.ToUpper() + " ---");
            foreach (var user in userManager.GetAll().Data)
            {
                Console.WriteLine(user.FirstName);
            }
        }
        private static void CustomerTest()
        {
            CustomerManager customerManager = new(new EfCustomerDal());
            //customerManager.Add(new Customer { Id = 2, UserId =2, CompanyName = "CDA" });            //customerManager.Add(new Customer { Id = 2, UserId =2, CompanyName = "CDA" });
            //customerManager.Update(new Customer { Id = 2, UserId =2, CompanyName = "DDDA" });
            //customerManager.Delete(new Customer { Id = 2});

            Console.WriteLine("\n--- " + customerManager.GetById(1).Message.ToUpper() + " ---\n" + customerManager.GetById(1).Data.CompanyName + "\n");

            Console.WriteLine("--- " + customerManager.GetAll().Message.ToUpper() + " ---");
            foreach (var customer in customerManager.GetAll().Data)
            {
                Console.WriteLine(customer.UserId);
                //Tabloları joinle ve customer/user bilgilerini al.
            }
        }
        private static void RentalTest()
        {
            RentalManager rentalManager = new(new EfRentalDal());
            //rentalManager.Add(new Rental { Id = 8, CarId = 2, CustomerId = 1, RentDate = new DateTime(2012, 08, 01)});         
            //rentalManager.Update(new Rental { Id = 7, CarId = 1, CustomerId = 3, RentDate = new DateTime(2012, 07, 26), ReturnDate = new DateTime(2012, 07, 28) });
            //rentalManager.Delete(new Rental { Id = 7});

            Console.WriteLine("\n--- " + rentalManager.GetById(1).Message.ToUpper() + " ---\n" + rentalManager.GetById(1).Data.Id + "\n");

            Console.WriteLine("--- " + rentalManager.GetAll().Message.ToUpper() + " ---");
            foreach (var rental in rentalManager.GetAll().Data)
            {
                Console.WriteLine(rental.Id);
            }

            Console.WriteLine("\n--- " + rentalManager.GetRentalDetail().Message.ToUpper() + " ---");
            foreach (var rental in rentalManager.GetRentalDetail().Data)
            {
                Console.WriteLine(@"{0} - {1} - {2} - {3} - {4}- {5}",
                    rental.Id,
                    rental.FirstName,
                    rental.LastName,
                    rental.BrandName,
                    rental.ColorName,
                    rental.DailyPrice);
            }
        }
        private static void CarImageTest()
        {
            CarImageManager carImageManager = new(new EfCarImageDal());
            //customerManager.Add(new Customer { Id = 2, UserId =2, CompanyName = "CDA" });            
            //customerManager.Update(new Customer { Id = 2, UserId =2, CompanyName = "DDDA" });
            //customerManager.Delete(new Customer { Id = 2});

            
            Console.WriteLine("\n--- " + carImageManager.GetById(1).Message.ToUpper() + " ---\n" + carImageManager.GetById(1).Data.Date + "\n");

            Console.WriteLine("--- " + carImageManager.GetAll().Message.ToUpper() + " ---");
            foreach (var carImage in carImageManager.GetAll().Data)
            {
                Console.WriteLine(carImage.CarId);
                //Tabloları joinle ve customer/user bilgilerini al.
            }
        }
    }
}
