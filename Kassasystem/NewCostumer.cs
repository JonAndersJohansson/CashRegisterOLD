using CashRegister;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister
{
    public class NewCostumer
    {

        public void Customer()
        {
            Menus menus = new Menus();
            ProductHandeling producthandeling = new ProductHandeling();
            Receipt receipt = new Receipt();
            var products = producthandeling.listOfProducts;
            var cart = new List<CartItem>();
            decimal total = 0;

            while (true)
            {
                Console.Clear();
                menus.MenuGraphics();

                Console.WriteLine("kommandon:\n<productid> <antal>\nPAY\n");
                string command = Console.ReadLine();

                if (command.ToLower() == "pay")
                {
                    // Beräkna totalbeloppet
                    foreach (var item in cart)
                    {
                        total += item.Product.Price * item.Quantity; // Räkna ut totalen
                    }

                    // Spara kvittot
                    receipt.SaveReceipt(cart, total);
                    Console.WriteLine();
                    receipt.DisplayReceipt(cart, products);
                    Console.ReadKey();
                    Menus menu = new Menus();
                    menu.MainMenu();
                    //break;                                //Behövs inte?
                }
                else
                {
                    TakeCommand(command, cart, products);
                    Thread.Sleep(1000);  // Pausar kort innan vi rensar
                    continue;
                }
            }
        }

        public void TakeCommand(string command, List<CartItem> cart, List<Products> products)
        {
            var parts = command.Split(' ');
            if (parts.Length == 2 && int.TryParse(parts[0], out int productId) && int.TryParse(parts[1], out int quantity))
            {
                Products product = products.FirstOrDefault(p => p.PLU == productId);
                if (product != null)
                {
                    AddToCart(cart, product, quantity);
                    Console.WriteLine($"{quantity} x {product.Name} tillagd i varukorgen.");
                    Console.Beep();
                }
                else
                {
                    Console.WriteLine("Produkt-ID hittades inte.");
                    Thread.Sleep(1000);  // Pausar kort innan vi rensar
                }
            }
            else
            {
                Console.WriteLine("Ogiltigt kommando. Ange <produktid> <antal>.");
                Thread.Sleep(1000);  // Pausar kort innan vi rensar
            }
        }

        public void AddToCart(List<CartItem> cart, Products product, int quantity)
        {
            var existingItem = cart.FirstOrDefault(c => c.Product.PLU == product.PLU);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                cart.Add(new CartItem(product, quantity));
            }
        }
    }    
}
