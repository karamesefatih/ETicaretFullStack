﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Products
{
    public class VM_Update_Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int stock { get; set; }
        public float price { get; set; }
    }
}
