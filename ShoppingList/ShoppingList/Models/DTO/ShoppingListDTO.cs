using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingList.Models.DTO
{
    public class ShoppingListDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Product> Products { get; set; }
    }
}