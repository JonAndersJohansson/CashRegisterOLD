using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kassasystem
{
    public class InputHandeling
    {
        //public static int TakeInputInMenu()
        //{
        //    string input;
        //    int intInput;
        //    while (true)
        //    {
        //        input = Console.ReadLine();

        //        if (!int.TryParse(input, out intInput))
        //        {
        //            Console.WriteLine("Ogiltigt värde, försök igen..");
        //            continue;
        //        }
        //        if (intInput < 0 || intInput > 2)
        //        {
        //            Console.WriteLine("Ogiltigt värde, försök igen..");
                    
        //            continue;
        //        }
        //        return intInput;
        //    }
        //}
        public static void TakeCommand(string command, Dictionary<int, int> cart, List<Products> products)
        {
            var parts = command.Split(' ');
            if (parts.Length == 2 && int.TryParse(parts[0], out int productId) && int.TryParse(parts[1], out int quantity))
            {
                Products product = products.FirstOrDefault(p => p.PLU == productId);
                if (product != null)
                {
                    RegisterMethods.AddToCart(cart, productId, quantity);
                    Console.WriteLine($"{quantity} x {product.Name} tillagd i varukorgen.");
                }
                else
                {
                    Console.WriteLine("Produkt-ID hittades inte.");
                }
            }
            else
            {
                Console.WriteLine("Ogiltigt kommando. Ange <produktid> <antal>.");
            }
        }
    }
}
