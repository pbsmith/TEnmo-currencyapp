using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection.Metadata;

namespace Capstone.Classes
{
    /// <summary>
    /// Most of the "work" (the data and the methods) of dealing with inventory and money 
    /// should be created or controlled from this class
    /// </summary>
    public class Store
    {
        List<Items> inventory = new List<Items>();
        FileIO fileIO = new FileIO();
       public  List<Items> shoppingCart = new List<Items>();
        List<Items> receipt = new List<Items>();
        List<decimal> quantityList = new List<decimal>();
        public decimal runningTotal = 0;
        public decimal Balance { get; private set; }

        public Store()
        {
            inventory = fileIO.ReadInventory();
        }
        public Items[] ShowItems() 
        {
            return inventory.ToArray();
        }
        public string TakeMoney(string amount)
        {
            int number;
            bool success = int.TryParse(amount, out number);
            if (success)
            {
                number = int.Parse(amount);
                if (Balance + number <= 1000)
                {
                    if (number <= 100 && number > 0)
                    {
                        Balance += number;
                       fileIO.WriteToAuditMoneyRecieved(amount, Balance);
                        return $"Successfully entered ${amount}";
                    }
                    else
                    {
                        return "Inputed amount is invalid. Please enter valid amount between $1 - $100";
                    }
                }
                else
                {
                    return "Inputed total breaks Balance cap of $1000. Please enter valid amount.";
                }
            }
            else
            {
                return "Please enter a whole number.";
            }
        }
        public string SelectProducts(string productID, string selectAmount, decimal balance)
        {
            bool doesExist = ItemExistCheck(productID);
            bool isSoldOut = ItemSoldOutCheck(productID);


            if (doesExist)
            {
                if (!isSoldOut)
                {
                    if(ItemInsufficientStockCheck(productID,selectAmount) == "Success")
                    {
                        if (InsufficientFundsCheck(productID, balance, selectAmount) == "Success")
                        {
                            return $"Successfully bought {selectAmount} of {productID}. ";
                        } else
                        {
                            return "Insufficient Funds";
                        }
                    }
                    else
                    {
                        return $"Insufficient stock of {productID}";
                    }
                } else
                {
                    return $"{productID} is SOLD OUT";
                }
            } else
            {
                return $"{productID} does not exist";
            }
        }
        public bool ItemExistCheck(string productID)
        {
            bool doesExist = false;
            foreach (Items itemId in inventory)
            {
                if (itemId.InventoryId == productID)
                {
                    doesExist = true;
                    return doesExist;
                }
            }
            return doesExist;
        }
        public bool ItemSoldOutCheck(string productID)
        {
            bool soldOut = false;
            foreach (Items itemId in inventory)
            {
                if (itemId.InventoryId == productID)
                {
                    if (itemId.Quantity == "SOLD OUT")
                    {
                        return soldOut = true;
                    }
                }
            }
            return soldOut;
        }
        public string ItemInsufficientStockCheck(string productID, string selectedAmount)
        {
            int number = 0;
            bool success = int.TryParse(selectedAmount, out number);
            if (success)
            {
                number = int.Parse(selectedAmount);
                foreach (Items itemId in inventory)
                {
                    if (itemId.InventoryId == productID)
                    {
                        if (int.Parse(itemId.Quantity) >= number)
                        {
                            return "Success";
                        } else if(int.Parse(itemId.Quantity) < number)
                        {
                            return "Your selected amount was invalid. Please enter a valid amount.";
                        }
                    }
                }
            }
            else
            {
                return "Your selected amount was invalid. Please enter a valid amount.";

            }
            return "Error";
        }
        public string InsufficientFundsCheck(string productID, decimal balance, string selectAmount)
        {
            foreach (Items itemId in inventory)
            {
                if (itemId.InventoryId == productID)
                {
                    int amount = int.Parse(selectAmount);
                    if ((itemId.Price * amount) <= balance)
                    {
                        shoppingCart.Add(itemId);
                        Balance -= (itemId.Price * amount);
                        int itemQuantity = int.Parse(itemId.Quantity);
                        itemQuantity -= amount;
                        itemId.QuantityTotalPrice = amount * itemId.Price;
                        runningTotal += (itemId.Price * amount);
                        fileIO.WriteToAuditProductSelection(balance, selectAmount, itemId.ProductName, itemId.InventoryId, itemId.Price);
                        if (itemQuantity == 0)
                        {
                            itemId.Quantity = "SOLD OUT";
                        }
                        else
                        {
                            itemId.Quantity = itemQuantity.ToString();
                        }
                        return "Success";
                    }
                    return "Insufficent Funds";
                }
            }
            return "Error";
        }
        public int[] GetChange(decimal balance)
        {
            string result = "";
            int twenties = 0;
            int tens = 0;
            int fives = 0;
            int ones = 0;
            int quarters = 0;
            int dimes = 0;
            int nickels = 0;
            int remainder = (int)(balance * 100);

            twenties = (remainder / 2000);
            remainder = remainder % 2000;

            tens = (remainder / 1000);
            remainder = remainder % 1000;

            fives = remainder / 500;
            remainder = remainder % 500;

            ones = remainder / 100;
            remainder = remainder % 100;

            quarters = remainder / 25;
            remainder = remainder % 25;

            dimes = remainder / 10;
            remainder = remainder % 10;

            nickels = remainder / 5;
            remainder = remainder % 5;

            int[] change = { twenties, tens, fives, ones, quarters, dimes, nickels };
            fileIO.WriteToAuditChangeGiven(balance);
            return change;
        }
        public void ResetValues()
        {
            Balance = 0;
            shoppingCart.Clear();
            runningTotal = 0;
        }
        public List<Items> GetReceipt(List<Items> shoppingCart)
        {
            List<Items> receipt = shoppingCart;
            return receipt;
        }
        public void ResetQuantityTotal(Items item)
        {
            item.QuantityTotalPrice = 0;
        }
    }
}
