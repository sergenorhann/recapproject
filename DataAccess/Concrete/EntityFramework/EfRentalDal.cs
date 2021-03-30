using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, ReCapProjectContext>, IRentalDal
    {
        public List<RentalDetailDto> GetRentalDetail()
        {
            using (ReCapProjectContext context=new())
            {
                var result = from r in context.Rentals

                             join c in context.Cars
                             on r.CarId equals c.Id

                             join cu in context.Customers
                             on r.CustomerId equals cu.Id

                             join co in context.Colors
                             on c.BrandId equals co.Id

                             join b in context.Brands
                             on c.BrandId equals b.Id

                             join u in context.Users
                             on cu.UserId equals u.Id


                             select new RentalDetailDto
                             {
                                 Id=r.Id,
                                 FirstName=u.FirstName,
                                 LastName=u.LastName,
                                 BrandName = b.Name,
                                 ColorName = co.Name,
                                 DailyPrice = c.DailyPrice
                             };
                return result.ToList();
            }
        }
    }
}
