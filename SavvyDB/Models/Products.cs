using System.Collections.Generic;

namespace SavvyDB.Models
{
    public class Products
    {
        public Products(int id, string name, int price, int quantity) 
        {
            this.Id = id;
            this.Name = name;
            this.Price = price;
               
        }
     public int Id {get; set;}
     public string Name {get; set;}
     public int Price {get; set;}

    }
}