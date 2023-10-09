using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Channels;

namespace Capstone.Classes
{
    class UserInterface
    {
        private Store store = new Store();

        /// <summary>
        /// Provides all communication with human user.
        /// 
        /// All Console.Readline() and Console.WriteLine() statements belong 
        /// in this class.
        /// 
        /// NO Console.Readline() and Console.WriteLine() statements should be 
        /// in any other class
        /// 
        /// </summary>
        public void Run()
        {
            bool done = false;

            while (!done)
            {
                MainMenu();
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        ShowInventory();
                        Console.WriteLine();
                        break;
                    case "2":
                        SubMenu();
                        break;
                    case "3":
                        done = true;
                        break;
                }
            }
        }
        public void MainMenu()
        {
            Console.WriteLine("(1) Show Inventory");
            Console.WriteLine("(2) Make Sale");
            Console.WriteLine("(3) Quit");
            Console.WriteLine();
        }
        public void SubMenu()
        {
            bool isDone = false;

            while (!isDone)
            {
                Console.Clear();
                Console.WriteLine("(1) Take Money");
                Console.WriteLine("(2) Select Products");
                Console.WriteLine("(3) Complete Sale");
                Console.WriteLine($"Your Current Balance is: ${store.Balance}");
                Console.WriteLine();
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        Console.Write("Please enter whole dollar amount: ");
                        string amount = Console.ReadLine();
                        string response = store.TakeMoney(amount);
                        Console.WriteLine();
                        Console.WriteLine(response);
                        Console.ReadLine();
                        //Console.Clear();

                        break;
                    case "2":
                        ShowInventory();
                        Console.Write("Select product you would like to buy using the products ID: ");
                        string chosenProductID = Console.ReadLine();
                        Console.Write("How much of this product would you like to buy: ");
                        string selectedAmount = Console.ReadLine();
                        string productResponse = store.SelectProducts(chosenProductID, selectedAmount, store.Balance);
                        Console.WriteLine(productResponse);
                        Console.ReadLine();
                        break;
                    case "3":
                        Console.Clear();
                        List<Items> receipt = store.GetReceipt(store.shoppingCart);
                        PrintReceipt(receipt);
                        string balance = store.Balance.ToString();
                        Console.WriteLine($"Total: ${store.runningTotal}");
                        Console.WriteLine();
                        Console.WriteLine($"Change: ${balance}");
                        int[] change = store.GetChange(store.Balance);
                        for (int i = 0; i < change.Length; i++)
                        {
                            if(i == 0)
                            {
                                Console.Write($"Twenties: {change[i]}, ");
                            }else if(i == 1)
                            {
                                Console.Write($"Tens: {change[i]}, ");
                            }
                            else if(i == 2)
                            {
                                Console.Write($"Fives: {change[i]}, ");
                            } else if(i == 3)
                            {
                                Console.Write($"Ones: {change[i]}, ");
                            }else if(i == 4)
                            {
                                Console.Write($"Quarters: {change[i]}, ");
                            } else if(i == 5)
                            {
                                Console.Write($"Dimes: {change[i]}, ");
                            } else if(i == 6)
                            {
                                Console.Write($"Nickels: {change[i]}");
                            } 
                        }
                        store.ResetValues();
                        Console.ReadLine();
                        isDone = true;
                        Console.Clear();
                        break;
                }
            }
            
        }

        public void ShowInventory()
        {
            Items[] temp = store.ShowItems();
            if (temp.Length == 0)
            {
                Console.WriteLine("No items in inventory");
            }
            else
            {
                // TODO SORT LIST 
                char pad = ' ';
                Console.WriteLine("Id".PadRight(8, pad) + "Name".PadRight(20, pad) + "Wrapper".PadRight(9, pad) + "Qty".PadRight(9, pad) + "Price");
                foreach (Items item in temp)
                {
                    Console.WriteLine(item.InventoryId.PadRight(8, pad) + item.ProductName.PadRight(20, pad) + item.Wrapper.PadRight(9, pad) + item.Quantity.ToString().PadRight(9, pad) + item.Price);

                }
            }
        }
        public void PrintReceipt(List<Items> receipt)
        {
            char pad = ' ';
            string ch = "Chocolate Confectionery";
            string sr = "Sour Flavored Candies";
            string hc = "Licorce and Jellies";
            string li = "Chocolate Confectionery";

            foreach (Items item in receipt)
            {
                if(item.ProductType == "CH")
                {
                    int num = 0;
                    int selectedQuantity = 0;
                    //int selectedQuantity = 100 - (int.Parse(item.Quantity));
                    bool success = int.TryParse(item.Quantity, out num);
                    if (success)
                    {
                        selectedQuantity = 100 - (int.Parse(item.Quantity));
                    } else
                    {
                        selectedQuantity = 100;
                    }
                        Console.WriteLine(selectedQuantity.ToString().PadRight(4, pad) +  item.ProductName.PadRight(20, pad) + ch.PadRight(25, pad) + item.Price.ToString().PadRight(6, pad) + item.QuantityTotalPrice);
                    store.ResetQuantityTotal(item);
                } else if(item.ProductType == "SR")
                {
                    int selectedQuantity = 100 - (int.Parse(item.Quantity));
                    Console.WriteLine(selectedQuantity.ToString().PadRight(4, pad) + item.ProductName.PadRight(20, pad) + sr.PadRight(25, pad) + item.Price.ToString().PadRight(6, pad) + item.QuantityTotalPrice);
                    store.ResetQuantityTotal(item);
                }
                else if (item.ProductType == "HC")
                {
                    int selectedQuantity = 100 - (int.Parse(item.Quantity));
                    Console.WriteLine(selectedQuantity.ToString().PadRight(4, pad) + item.ProductName.PadRight(20, pad) + hc.PadRight(25, pad) + item.Price.ToString().PadRight(6, pad) + item.QuantityTotalPrice);
                    store.ResetQuantityTotal(item);
                }
                else if (item.ProductType == "LI")
                {
                    int selectedQuantity = 100 - (int.Parse(item.Quantity));
                    Console.WriteLine(selectedQuantity.ToString().PadRight(4, pad) + item.ProductName.PadRight(20, pad) + li.PadRight(25, pad) + item.Price.ToString().PadRight(6, pad) + item.QuantityTotalPrice);
                    store.ResetQuantityTotal(item);
                }
            }
            Console.WriteLine();
        }
    }
}
