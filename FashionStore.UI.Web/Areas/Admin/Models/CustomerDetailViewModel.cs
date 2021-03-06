﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FashionStore.Entity.Entities;

namespace FashionStore.UI.Web.Areas.Admin.Models
{
    public class CustomerDetailViewModel
    {
        public Customer Customer { get; set; }
        public IEnumerable<Address> Addresses { get; set; }
    }
}