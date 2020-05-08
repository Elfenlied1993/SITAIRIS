﻿using System;
using System.Collections.Generic;

namespace BSUIR.DAL.Interfaces.Models
{
    public partial class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public string Comment { get; set; }
        public int Amount { get; set; }
        public string CustomerId { get; set; }
        public int DeliveryAddressId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual DeliveryAddress DeliveryAddress { get; set; }
    }
}
