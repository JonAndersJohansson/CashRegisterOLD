﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kassasystem
{
    public class RegisterMethods
    {
        public static void MainMenu()
        {
            bool menuInput = false;

            while (!menuInput)
            {
                Console.Clear();
                
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("██╗  ██╗ ██████╗  ██████╗ ██████╗ ");
                Console.WriteLine("██║ ██╔╝██╔═══██╗██╔═══██╗██╔══██╗");
                Console.WriteLine("█████╔╝ ██║   ██║██║   ██║██████╔╝");
                Console.WriteLine("██╔═██╗ ██║   ██║██║   ██║██╔═══╝ ");
                Console.WriteLine("██║  ██╗╚██████╔╝╚██████╔╝██║     ");
                Console.WriteLine("╚═╝  ╚═╝ ╚═════╝  ╚═════╝ ╚═╝  ®  ");
                Console.WriteLine("      ~   Örnsköldsvik   ~");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine();
                Console.WriteLine("KASSA");
                Console.WriteLine("1. Ny kund");
                Console.WriteLine("2. Administratörsverktyg");
                Console.WriteLine("0. Avsluta");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        RegisterMethods.NewCustomer();
                        menuInput = true;
                        break;
                    case "2":
                        // Detta gör vi senare.
                        menuInput = true;
                        break;
                    case "0":
                        Environment.Exit(0);
                        return;
                    default:
                        Console.WriteLine("Ogiltigt alternativ, försök igen.");
                        System.Threading.Thread.Sleep(2000);  // Pausar kort innan vi rensar
                        break;
                }
            }
        }
        public static void NewCustomer()
        {
            ProductHandeling producthandeling = new ProductHandeling();
            var products = producthandeling.Products;
            var cart = new Dictionary<int, int>();

            while (true)
            {
                Console.WriteLine("kommandon:\n<productid> <antal>\nPAY\n");
                string command = Console.ReadLine();

                if (command.ToLower() == "pay")
                {
                    PrintReceipt(cart, products);
                    break;
                }
                else
                {
                    InputHandeling.TakeCommand(command, cart, products);
                }
            }
        }
        public static void AddToCart(Dictionary<int, int> cart, int productId, int quantity)
        {
            if (cart.ContainsKey(productId))
            {
                cart[productId] += quantity;
            }
            else
            {
                cart[productId] = quantity;
            }
        }

        public static void PrintReceipt(Dictionary<int, int> cart, List<Products> products)
        {
            decimal total = 0;
            Console.WriteLine("KVITTO\t" + DateTime.Now);

            foreach (var item in cart)
            {
                Products product = products.FirstOrDefault(p => p.PLU == item.Key);
                if (product != null)
                {
                    decimal lineTotal = product.Price * item.Value;
                    total += lineTotal;
                    Console.WriteLine($"{product.Name} {item.Value} * {product.Price} = {lineTotal}");
                }
            }

            Console.WriteLine($"Total: {total:C}");
        }
    }


}