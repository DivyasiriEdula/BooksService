﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksService.Domain.Models
{
    public class RetailPrice
    {
        public double amount { get; set; }
        public string currencyCode { get; set; }
        public int amountInMicros { get; set; }
    }

}
