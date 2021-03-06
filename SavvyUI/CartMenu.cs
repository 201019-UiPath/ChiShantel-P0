using System;
using SavvyDB;
using SavvyDB.Entities;
using SavvyDB.Mappers;
using SavvyDB.Models;
using SavvyLib;
using System.Collections.Generic;
namespace SavvyUI
{
    public class CartMenu
    {
        private string userInput;
        private SavvyRepo repo;
        private Order order;
        private LocationTask locationtask;
        private InventoryTask inventorytask;
        private ProductTask producttask;
        private CartTask carttask;
        private CartItemTask cartitemtask;
        private OrderTask ordertask;
        private OrderItemTask orderitemtask;

        public CartMenu()
        {
            SavvyContext context = new SavvyContext();
            DBMapper mapper = new DBMapper();
            this.repo = new SavvyRepo(context, mapper);
            this.locationtask = new LocationTask(repo);
            this.inventorytask = new InventoryTask(repo);
            this.producttask = new ProductTask(repo);
            this.carttask = new CartTask(repo);
            this.cartitemtask = new CartItemTask(repo);
            this.ordertask = new OrderTask(repo);
            this.orderitemtask = new OrderItemTask(repo);
        }
        public void start()
        {
            List<CartItem> items = cartitemtask.GetAllCartItems(1);
            do
            {
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("[1] View Items in Cart");
                Console.WriteLine("[2] Check Out");
                Console.WriteLine("[3] Go Back");
                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        
                        foreach (CartItem Item in items)
                        {
                            int id = Item.ProductId;
                            Product prod = producttask.GetProduct(id);
                            Console.WriteLine("ItemName: " + prod.Name);
                            Console.WriteLine("Quantity: " + Item.Quantity);
                        }
                        break;

                    case "2":

                        Cart cart = carttask.GetCart(1);
                        Order order = new Order();
                        decimal total = 0;
                        order.CustomerId = 1;
                        DateTime orderDate = order.Date = DateTime.Now;
                        ordertask.AddOrder(order);
                        Order neworder = ordertask.GetOrderByDate(orderDate);
                        Console.WriteLine("Thank you for placing an order!");


                        foreach (CartItem cartitem in items)
                        {
                            Product product = producttask.GetProduct(cartitem.ProductId);

                            OrderItem orderitem = new OrderItem();
                            orderitem.ProductId = cartitem.ProductId;
                            orderitem.Quantity = cartitem.Quantity;
                            orderitem.OrderId = neworder.OrderId;

                            decimal itemprice = product.Cost;
                            total += (itemprice * cartitem.Quantity);

                            orderitemtask.AddOrderItem(orderitem);
                            cartitemtask.DeleteCartItem(cartitem);

                        }
                        neworder.Totalprice = total;
                        ordertask.UpdateOrder(neworder);
                        break;

                }
            }
            while (!userInput.Equals("3"));
        }
    }
}