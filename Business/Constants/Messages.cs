using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        //Messages of Car
        public static string CarAdded = "Car Added";
        public static string CarDeleted = "Car Deleted";
        public static string CarUpdated = "Car Updated";
        public static string CarListed = "Car Listed";
        public static string CarsListed = "Cars Listed";
        public static string DailyPriceError = "The daily price of the vehicle must be at least 0 dollar";
        public static string CarNameError = "The name of the car must contain at least two characters";

        //Messages of Brand
        public static string BrandAdded = "Brand Added";
        public static string BrandDeleted = "Brand Deleted";
        public static string BrandUpdated = "Brand Updated";
        public static string BrandListed = "Brand Listed";
        public static string BrandsListed = "Brands Listed";
        public static string BrandNameAlreadyExists = "Brand Name Already Exists";

        //Messages of Color
        public static string ColorAdded = "Color Added";
        public static string ColorDeleted = "Color Deleted";
        public static string ColorUpdated = "Color Updated";
        public static string ColorListed = "Color Listed";
        public static string ColorsListed = "Colors Listed";
        public static string ColorNameAlreadyExists = "Color Name Already Exists";

        //Messages of Customer
        public static string CustomerAdded = "Customer Added";
        public static string CustomerDeleted = "Customer Deleted";
        public static string CustomerUpdated = "Customer Updated";
        public static string CustomerListed = "Customer Listed";
        public static string CustomersListed = "Customers Listed";

        //Messages of User
        public static string UserAdded = "User Added";
        public static string UserDeleted = "User Deleted";
        public static string UserUpdated = "User Updated";
        public static string UserListed = "User Listed";
        public static string UsersListed = "Users Listed";


        //Messages of Rental
        public static string RentalAdded = "Rental Added";
        public static string RentalDeleted = "Rental Deleted";
        public static string RentalUpdated = "Rental Updated";
        public static string RentalListed = "Rental Listed";
        public static string RentalsListed = "Rentals Listed";
        public static string CarRented = "Sorry! The Car is Rented";

        //Messages of CarImage
        public static string CarImageAdded = "CarImage Added";
        public static string CarImageDeleted = "CarImage Deleted";
        public static string CarImageUpdated = "CarImage Updated";
        public static string CarImageListed = "CarImage Listed";
        public static string CarImagesListed = "CarImages Listed";
        public static string CarImageLimitExceeded = "CarImage Limit Exceeded";

        //Messages of UserImage
        public static string UserImageAdded = "UserImage Added";
        public static string UserImageDeleted = "UserImage Deleted";
        public static string UserImageUpdated = "UserImage Updated";
        public static string UserImageListed = "UserImage Listed";
        public static string UserImagesListed = "UserImages Listed";
        public static string UserImageLimitExceeded = "UserImage Limit Exceeded";


        //DÜZENLE
        public static string UserNotFound = "Kullanıcı bulunamadı";
        public static string PasswordError = "Şifre hatalı";
        public static string SuccessfulLogin = "Sisteme giriş başarılı";
        public static string UserAlreadyExists = "Bu kullanıcı zaten mevcut";
        public static string UserRegistered = "Kullanıcı başarıyla kaydedildi";
        public static string AccessTokenCreated = "Access token başarıyla oluşturuldu";

        public static string AuthorizationDenied = "Yetkiniz yok";
        public static string ProductNameAlreadyExists = "Ürün ismi zaten mevcut";


    }
}
