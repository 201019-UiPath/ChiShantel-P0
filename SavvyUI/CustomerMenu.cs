using System;
using SavvyDB;
using SavvyDB.Mappers;
using SavvyDB.Entities;
using SavvyLib;
using SavvyDB.Models;
using System.Collections.Generic;
using Serilog;

namespace SavvyUI
{
    public class CustomerMenu
    {
        private string userInput;
        private int count = 1;
        private int custID = 1;
        private int productID;
        private SavvyRepo savvyRepo;
        private SavvyContext context;
        private LocationTask locationtask;
        private InventoryTask inventorytask;
        private ProductTask producttask;
        public CustomerMenu(SavvyContext context)
        {
            this.context = context;
        }
        public void start()
        {
            
            do 
            {
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("[1] Buy a Product");
                Console.WriteLine("[2] Check Order History");
                Console.WriteLine("[3] Check Location Inventory");
                Console.WriteLine("[4] Go back");
                userInput = Console.ReadLine();
                savvyRepo = new SavvyRepo(new SavvyContext(), new DBMapper());
                locationtask = new LocationTask(savvyRepo);
                CustomerTask customerTask = new CustomerTask(savvyRepo);
                switch (userInput) 
                {
                    case "1":
                        
                        Console.WriteLine("Select a location!");
                        List<Locations> Location = savvyRepo.GetAllLocations();
                        CartTask cartTask = new CartTask(savvyRepo);
                        SavvyDB.Models.Cart cart = new SavvyDB.Models.Cart();
                        count = 1;
                        foreach (Locations singleLocation in Location) 
                        { 
                            Console.WriteLine("[" + count + "]");
                            Console.WriteLine(singleLocation.Name);
                            count ++;
                        }
                        userInput = Console.ReadLine();
                        int Locationid = Int32.Parse(userInput);
                        InventoryTask inventorytask = new InventoryTask(savvyRepo);
                        Console.WriteLine("Getting items...");
                        List<SavvyDB.Models.Inventory> Items = inventorytask.GetInventory(Locationid);


                        
                        foreach (SavvyDB.Models.Inventory Item in Items)
                        {
                            int id = Item.productid.value;
                            SavvyDB.Models.Products prod = producttask.GetProduct(Item.productid );
                            Console.WriteLine("Item ID: " + Item.InventoryId);
                        }

                        Console.WriteLine ("Select a product!");
                        userInput = Console.ReadLine();
                        cart.productid = Int32.Parse(userInput);
                        Console.WriteLine("How many of this product do you wish to purchase?");
                        userInput = Console.ReadLine();
                        cart.quantity = Int32.Parse(userInput);
                        cart.custid = 1;
                        cartTask.AddToCart(cart);
                        Log.Information("Cart Added!");
                        break;
                    case "2":  
                        List<SavvyDB.Entities.Cart> carthistory = savvyRepo.GetOrderHistory();
                        Log.Information("Order history viewed!");
                        foreach (SavvyDB.Entities.Cart cartitem in carthistory)
                        {
                            Console.WriteLine("CustomerID: " + cartitem.Custid);
                            Console.WriteLine("ProductID: " + cartitem.Productid);
                            Console.WriteLine("LocationID: "+ cartitem.Locationid);
                            Console.WriteLine("Quantity: " + cartitem.Quantity + "\n");
                        }
                        break;
                    case "3":
                        Console.WriteLine("Select a location!");
                        location = savvyRepo.GetLocations();
                        count = 1;
                        foreach (Location singleLocation in location) 
                        { 
                            Console.WriteLine("[" + count + "]");
                            Console.WriteLine(singleLocation.Name);
                            count ++;
                        }
                        userInput = Console.ReadLine();

                        Console.WriteLine("Getting items...");

                        List<SavvyDB.Entities.Inventory> inventory = savvyRepo.GetProducts(Int32.Parse(userInput));
                        foreach (SavvyDB.Entities.Inventory singleProduct in inventory)
                        {
                            Console.WriteLine("Item ID: " + singleProduct.Productid);
                            Console.WriteLine("Item Quantity: " + singleProduct.Quantity);
                        }
                        
                        //Pull up list of available items          
                        break;
                    case "4":
                        Console.WriteLine("Returning to main menu...");
                        break;
                    default:
                        //invalid input message;
                        Console.WriteLine("Make a valid selection!");
                        break;
                    }
            }
            while (!userInput.Equals("4"));
        }
    }
}
