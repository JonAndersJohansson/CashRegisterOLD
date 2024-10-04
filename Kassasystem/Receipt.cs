using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CashRegister
{
    public class Receipt
    {
        public void SaveReceipt(List<CartItem> cart, decimal total)
        {
            string dateString = DateTime.Now.ToString("yyyyMMdd");
            string fileName = $"RECEIPT_{dateString}.txt";
            string directoryPath = "../../../Receipts"; // Ange sökvägen
            string filePath = Path.Combine(directoryPath, fileName); // Kombinera mapp och filnamn
            int receiptNumber = GetNextReceiptNumber(filePath); // Använd filePath!

            // Skapa mappen om den inte finns
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            using (StreamWriter writer = new StreamWriter(filePath, true)) // 'true' för att lägga till i filen
            {
                writer.WriteLine($"KVITTO #{receiptNumber} - {DateTime.Now}");
                foreach (var item in cart)
                {
                    decimal lineTotal = item.Product.Price * item.Quantity;
                    writer.WriteLine($"{item.Product.Name} {item.Quantity} * {item.Product.Price} = {lineTotal}");
                }
                writer.WriteLine($"Total: {total:C}");
                writer.WriteLine(new string('-', 20)); // Separerar
            }
        }

        private int GetNextReceiptNumber(string filePath) // FILEPATH
        {
            int maxNumber = 0;

            // Kolla om filen redan finns
            if (File.Exists(filePath))
            {
                // Läs filen och hämta det högsta kvittonumret?
                var lines = File.ReadAllLines(filePath);
                foreach (var line in lines)
                {
                    if (line.StartsWith("KVITTO #"))
                    {
                        var parts = line.Split('#');
                        if (parts.Length > 1 && int.TryParse(parts[1].Split('-')[0].Trim(), out int receiptNum))
                        {
                            maxNumber = Math.Max(maxNumber, receiptNum);
                        }
                    }
                }
            }

            return maxNumber + 1; // öka med 1 för nästa nummer
        }
        public void DisplayReceipt(List<CartItem> cart, List<Products> products)
        {
            Console.Clear();
            RegisterMethods.MenuGraphics();
            decimal total = 0;
            Console.WriteLine("KÖP GENOMFÖRT\t" + DateTime.Now);

            foreach (var item in cart)
            {
                Products product = item.Product; // hämtar produkten från CartItem
                decimal lineTotal = product.Price * item.Quantity;
                total += lineTotal;
                Console.WriteLine($"{product.Name} {item.Quantity} * {product.Price} = {lineTotal}");
            }

            Console.WriteLine($"Total: {total:C}");
            Console.WriteLine();
            Console.WriteLine("Kvitto har skrivits ut. Tryck enter för att fortsätta.");
   
        }
    }
}
