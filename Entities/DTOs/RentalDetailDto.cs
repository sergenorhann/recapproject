﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class RentalDetailDto
    {
        public int Id { get; set; }
        public int DailyPrice { get; set; }
        public string ColorName { get; set; }
        public string BrandName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
