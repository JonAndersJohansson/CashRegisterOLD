using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CashRegister
{
    public class ProductHandeling
    {
        public List<Products> listOfProducts { get; set; }
        public string myProductsFilePath = "../../../MyProducts.txt";

        public ProductHandeling()
        {
            listOfProducts = new List<Products>();
            LoadProductsToList();
        }

        Menus menu = new Menus();

        public void LoadProductsToList()
        {
            if (!File.Exists(myProductsFilePath))
            {
                Console.WriteLine("Produktfil saknas. Vänligen kontakta tekniker.");
                menu.MainMenu();
            }
            string[] productArray = File.ReadAllLines(myProductsFilePath);

            foreach (string line in productArray)
            {
                string[] parts = line.Split(' ');
                int pluNumber = int.Parse(parts[0]);
                string name = parts[1];
                decimal price = decimal.Parse(parts[2]);
                PriceType priceType = parts[3] == "Styckpris" ? PriceType.Styckpris : PriceType.Kilopris;

                listOfProducts.Add(new Products(pluNumber, name, price, priceType));
            }
        }
        public void ChangeProduct()
        {
            int index = menu.DisplayList(listOfProducts);       //Går igenom listan med produkter

            Products selectedProduct = listOfProducts[index]; // Definierar vald product

            Console.Clear();
            menu.MenuGraphics();

            Console.WriteLine($"Du har valt:\n{selectedProduct}");
            Console.WriteLine();

            // Ändra PLU
            Console.Write("Ange nytt PLU eller tryck Enter för att behålla nuvarande:\n");
            string newPluInput = Console.ReadLine();
            if (!string.IsNullOrEmpty(newPluInput) && int.TryParse(newPluInput, out int newPlu))
            {
                // Kontroll om det nya PLU redan finns
                bool isPluTaken = false; // Anta att PLU inte är upptaget

                foreach (Products product in listOfProducts)
                {
                    if (product.PLU == newPlu)
                    {
                        isPluTaken = true;      // Om vi hittar ett matchande PLU, sätt flaggan till true
                        break;
                    }
                }

                if (isPluTaken == true)         // Felhantering
                {
                    Console.WriteLine("Det angivna PLU-numret är redan upptaget. Försök igen.");
                    Thread.Sleep(2000);  // Pausar kort innan vi rensar
                    ChangeProduct();
                }
                else
                {
                    selectedProduct.PLU = newPlu;       //Skriver över PLU
                }
            }

            // Ändra namn
            Console.Write("Ange nytt namn eller tryck Enter för att behålla nuvarande:\n");
            string newName = Console.ReadLine();
            if (!string.IsNullOrEmpty(newName))
            {
                selectedProduct.Name = newName;
            }

            // Ändra pris
            Console.Write("Ange nytt pris eller tryck Enter för att behålla nuvarande:\n");
            string newPriceInput = Console.ReadLine();
            if (!string.IsNullOrEmpty(newPriceInput) && decimal.TryParse(newPriceInput, out decimal newPrice))
            {
                selectedProduct.Price = newPrice;
            }

            // Spara ändringarna tillbaka till filen
            SaveProductsToFile(listOfProducts);

            Console.WriteLine("Produkten har ändrats!\nÅtergår till huvudmenyn.");


            Thread.Sleep(2000);  // Pausar kort innan vi rensar
            menu.MainMenu();
        }


        public void AddProduct()
        {
            Console.Clear();
            menu.MenuGraphics();

            Products newProduct = new Products(0, "", 0, PriceType.Styckpris); //Skapar ny instans av Product
            int newPlu = 0;
            int lastPlu = listOfProducts.Last().PLU;    //Hämtar sista produktens PLU



            //Nytt PLU
            Console.WriteLine("Skapa ny produkt.");
            Console.Write($"Nuvarade högsta PLU-nummer: {lastPlu}\nAnge önskat PLU och tryck Enter:\n");
            string newPluInput = Console.ReadLine();
            if (!string.IsNullOrEmpty(newPluInput) && int.TryParse(newPluInput, out newPlu))
            {
                // Kontroll om det nya PLU redan finns
                bool isPluTaken = false; // Anta att PLU inte är upptaget

                foreach (Products product in listOfProducts)
                {
                    if (product.PLU == newPlu)
                    {
                        isPluTaken = true;      // Om vi hittar ett matchande PLU, sätt flaggan till true
                        break;
                    }
                }

                if (isPluTaken == true)         // Felhantering
                {
                    Console.WriteLine("Det angivna PLU-numret är redan upptaget. Försök igen.");
                    Thread.Sleep(2000);  // Pausar kort innan vi rensar
                    AddProduct();
                }
                else
                {
                    newProduct.PLU = newPlu;
                }
            }
            else
            {
                Console.WriteLine("Felatigt värde på PLU. Använd heltal, försök igen");
                Thread.Sleep(2000);  // Pausar kort innan vi rensar
                AddProduct();
            }

            // Nytt Namn
            Console.Write("Ange nytt namn och tryck Enter:\n");
            string newName = Console.ReadLine();
            if (!string.IsNullOrEmpty(newName))
            {
                newProduct.Name = newName;
            }
            else
            {
                Console.WriteLine("Du måste ge produkten ett namn, försök igen.");
                Thread.Sleep(2000);  // Pausar kort innan vi rensar
                AddProduct();
            }

            // Nytt pris
            Console.Write("Ange pris. Exempel: 29,90. Tryck därefter Enter:\n");
            string newPriceInput = Console.ReadLine();
            if (!string.IsNullOrEmpty(newPriceInput) && decimal.TryParse(newPriceInput, out decimal newPrice))
            {
                newProduct.Price = newPrice;
            }
            else
            {
                Console.WriteLine("Felaktigt värde, försök igen.");
                Thread.Sleep(2000);  // Pausar kort innan vi rensar
                AddProduct();
            }

            //Ny pristyp
            Console.WriteLine("Ange önskad pristyp. Välj 1 eller 2 och tryck Enter:");
            Console.WriteLine("1. Styckpris\n2. Kilopris");
            string newPriceTypeInput = Console.ReadLine();
            if (newPriceTypeInput == "1" || newPriceTypeInput == "2")
            {
                if (newPriceTypeInput == "1")
                {
                    newProduct.PriceType = PriceType.Styckpris;
                }
                else
                {
                    newProduct.PriceType = PriceType.Kilopris;
                }
            }
            else
            {
                Console.WriteLine("Felaktigt värde, välj 1 eller 2");
                Thread.Sleep(2000);  // Pausar kort innan vi rensar
                AddProduct();
            }

            listOfProducts.Add(newProduct);

            SaveProductsToFile(listOfProducts);

            Console.WriteLine("Produkten har lagts till!\nÅtergår till huvudmenyn.");

            Thread.Sleep(2000);  // Pausar kort innan vi rensar
            menu.MainMenu();

        }
        public void RemoveProduct()
        {
            int selectedIndex = menu.DisplayList(listOfProducts);       //Går igenom listan med produkter

            Console.Clear();
            menu.MenuGraphics();

            listOfProducts.RemoveAt(selectedIndex);
            Console.WriteLine($"Följade produkt är borttagen:\n{selectedIndex}");

            Thread.Sleep(2000);  // Pausar kort innan vi rensar
            menu.MainMenu();



        }
        private void SaveProductsToFile(List<Products> list)
        {
            listOfProducts.Sort(delegate (Products x, Products y) {return x.PLU.CompareTo(y.PLU);});    //Sorterar listan?

            using (StreamWriter writer = new StreamWriter(myProductsFilePath))
            {
                foreach (var product in listOfProducts)
                {
                    writer.WriteLine($"{product.PLU} {product.Name} {product.Price} {product.PriceType}");
                }
            }
        }
    }
}
