using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingList.Models.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
}