using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Emolod6
{
    enum Status
    {
        Pending,
        InProgress,
        Completed
    }
    enum Size
    {
        Small,
        Medium,
        Large
    }
    abstract class OrderItem
    {
        public string name { get; set; }
        public double price { get; set; }
        public abstract void Prepare();
    }
    class Pizza : OrderItem
    {
        public Size size { get; set; }
        public Pizza(double price, Size size)
        {
            this.price = price;
            this.size = size;
        }
        public override void Prepare()
        {
            Console.WriteLine($"Baking {size} pizza");
        }
    }
    class Drink : OrderItem
    {
        public bool isCold;        
        public Drink(double price,bool isCold)
        {
            this.price = price;
            this.isCold = isCold;
        }

        public override void Prepare()
        {
            Console.WriteLine("Pouring drink");
        }
    }

    class Dessert : OrderItem
    {
        public Dessert(double price)
        {
            this.price= price;
        }
        public override void Prepare()
        {
            Console.WriteLine("Making dessert");
        }
    }

    class Order
    {
        private List<OrderItem> items = new List<OrderItem>();
        private Status status {  get; set; }
        public void AddItem( OrderItem item )
        {
            if( item != null)
            {
                items.Add(item);
                Console.WriteLine("Added item");
            }   
            else
            {
                throw new Exception("Item is Null");
            }
        }
        public void ProcessOrder()
        {
            double totalPrice = 0;
            if(items.Count > 0)
            {
                foreach (var item in items)
                {
                    totalPrice += item.price;
                    item.Prepare();
                }
                Console.WriteLine($"Total price of your order - {totalPrice}");
            }
            else
            {
                Console.WriteLine("There is no orders to process");
            }            
        }
    }

    class GetUserInput
    {
        public int Int(string message)
        {
            Console.WriteLine(message);
            int input = Convert.ToInt32(Console.ReadLine());
            return input;
        }
        public string String(string message)
        {
            Console.WriteLine(message);
            string input = Console.ReadLine();
            return input;
        }
    }

    internal class FirstTask
    {
        static GetUserInput getInput = new GetUserInput();     
        static Status status = new Status();
        static void Main()
        {            
            status = Status.Pending;
            Order orders = new Order();
            int i = 0;        
            while(i == 0)
            {
                Console.WriteLine($"\n{status}");
                Console.WriteLine("\n 1 - Add item to your order \n 2 - Process your order");
                int input = getInput.Int("What do you want to do?");
                switch (input)
                {
                    case 1:
                        Console.WriteLine("Processing add...");
                        CreateSampleOrder(orders);
                        status = Status.InProgress;
                        break;
                    case 2:
                        Console.WriteLine("Processing order...");
                        orders.ProcessOrder();
                        status = Status.Completed;
                        i++;
                        break;
                    default:
                        Console.WriteLine("Incorrect index of operation");
                        break;
                }
            }
            Console.WriteLine(status);          
        }
        static void CreateSampleOrder(Order orders)
        {
            double totalPrice = 0.00;
            int input = getInput.Int("What kind of product you want to add to your order? \n 1 - pizza\n 2 - drink \n 3 - dessert");
            switch(input)
            {
                case 1:
                    totalPrice += 9;
                    Size size = new Size();
                    input = getInput.Int("What size you need? \n 1 - small, will cost 9\n 2 - medium, will cost 14\n 3 - large, will cost 16");
                    switch (input)
                    {
                        case 1:
                            totalPrice += 0;
                            size = Size.Small;
                            break;
                        case 2:
                            totalPrice += 5;
                            size = Size.Medium;
                            break;
                        case 3:
                            totalPrice += 7;
                            size = Size.Large;
                            break;
                        default:
                            Console.WriteLine("Incorrect index \n Returning to main page");
                            return;
                    }
                    
                    OrderItem item = new Pizza(totalPrice, size);
                    orders.AddItem(item);
                    break;
                case 2:
                    totalPrice += 5;
                    input = getInput.Int("Do you want it cold? - Will cost 5\n 1 - yes \n 2 - no");
                    switch (input)
                    {
                        case 1:
                            item = new Drink(totalPrice,true);
                            break;
                        case 2:
                            item = new Drink(totalPrice,false);
                            break;
                        default:
                            Console.WriteLine("Incorrect index \n Returning to main page");
                            return;

                    }      
                    orders.AddItem(item);
                    break;
                case 3:
                    totalPrice += 7;
                    Console.WriteLine("Dessert will cost 7");
                    orders.AddItem(new Dessert(totalPrice));
                    break;
                default:
                    Console.WriteLine("Incorrect index of product");
                    break;
            }
        }
    }
}
