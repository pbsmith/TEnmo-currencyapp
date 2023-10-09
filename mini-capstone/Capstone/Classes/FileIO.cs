using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Capstone.Classes
{
    public class FileIO
    {
        public List<Items> ReadInventory()
        {
            List<Items> inventory = new List<Items>();
            string filePath = @"C:\Store\inventory.csv";
            try
            {
                using (StreamReader stream = new StreamReader(filePath))
                {
                    while (!stream.EndOfStream)
                    {
                        string lineOfInput = stream.ReadLine();
                        string[] category = lineOfInput.Split('|');
                        Items item = new Items();
                        item.ProductType = category[0];
                        item.InventoryId = category[1];
                        item.ProductName = category[2];
                        item.Price = decimal.Parse(category[3]);
                        item.Wrapper = category[4];
                        inventory.Add(item);
                    }
                }
            }
            catch (IOException ex)
            {
                inventory = new List<Items>();
            }
            return inventory;
        }
        public void WriteToAuditMoneyRecieved(string amount, decimal balance)
        {
            string outputFile = @"C:\Store\Log.txt";
            try
            {
                using (StreamWriter dataOutput = new StreamWriter(outputFile, true))
                {
                    dataOutput.WriteLine($"{DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss tt")} MONEY RECIEVED: ${amount} ${balance}");
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("Cannot open the file for writing.");
            }
        }
        public void WriteToAuditChangeGiven(decimal balance)
        {
            string outputFile = @"C:\Store\Log.txt";
            try
            {
                using (StreamWriter dataOutput = new StreamWriter(outputFile, true))
                {
                    dataOutput.WriteLine($"{DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss tt")} CHANGE GIVEN: ${balance} $0.00");
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("Cannot open the file for writing.");
            }
        }
        public void WriteToAuditProductSelection(decimal balance, string selectAmount, string productName, string productID, decimal price)
        {
            string outputFile = @"C:\Store\Log.txt";
            try
            {
                using (StreamWriter dataOutput = new StreamWriter(outputFile, true))
                {
                    dataOutput.WriteLine($"{DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss tt")} {selectAmount} {productName} {productID} ${price} ${balance} ");
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("Cannot open the file for writing.");
            }
        }
    }
}
