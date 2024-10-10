//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.IO;

//namespace CashRegister
//{
//    public class kladdklass
//    {
//        public List<Products> listOfProducts { get; set; }
//        public string myProductsFilePath = "../../../MyProducts.txt";

//        public kladdklass()
//        {
//            listOfProducts = new List<Products>();
//            LoadProductsToList();
//        }

//        Menus menu = new Menus();

//        public void LoadProductsToList()
//        {
//            if (!File.Exists(myProductsFilePath))
//            {
//                Console.WriteLine("Produktfil saknas. Vänligen kontakta tekniker.");
//                menu.MainMenu();
//            }
//            string[] productArray = File.ReadAllLines(myProductsFilePath);

//            foreach (string line in productArray)
//            {
//                string[] parts = line.Split(' ');
//                int pluNumber = int.Parse(parts[0]);
//                string name = parts[1];
//                decimal price = decimal.Parse(parts[2]);
//                PriceType priceType = parts[3] == "Styckpris" ? PriceType.Styckpris : PriceType.Kilopris;

//                listOfProducts.Add(new Products(pluNumber, name, price, priceType));
//            }
//        }
//        public void ChangeProduct()
//        {
//            Console.Clear();
//            menu.MenuGraphics();
//            Console.WriteLine();
//            Console.WriteLine("Vänligen välj produkt du vill ändra:");
//            Thread.Sleep(2000);  // Pausar kort innan vi rensar
//            int index = menu.DisplayList(listOfProducts);

//            Products selectedProduct = listOfProducts[index];

//            Console.Clear();
//            menu.MenuGraphics();

//            Console.WriteLine($"Du har valt:\n{selectedProduct}");
//            Console.WriteLine();

//            // Ändra PLU
//            Console.Write("Ange nytt PLU (eller tryck Enter för att behålla nuvarande): ");
//            string newPluInput = Console.ReadLine();
//            if (!string.IsNullOrEmpty(newPluInput) && int.TryParse(newPluInput, out int newPlu))
//            {
//                selectedProduct.PLU = newPlu;
//            }

//            // Ändra namn
//            Console.Write("Ange nytt namn (eller tryck Enter för att behålla nuvarande): ");
//            string newName = Console.ReadLine();
//            if (!string.IsNullOrEmpty(newName))
//            {
//                selectedProduct.Name = newName;
//            }

//            // Ändra pris
//            Console.Write("Ange nytt pris (eller tryck Enter för att behålla nuvarande): ");
//            string newPriceInput = Console.ReadLine();
//            if (!string.IsNullOrEmpty(newPriceInput) && decimal.TryParse(newPriceInput, out decimal newPrice))
//            {
//                selectedProduct.Price = newPrice;
//            }

//            // Spara ändringarna tillbaka till filen
//            SaveProductsToFile("../../../MyProducts.txt");

//            Console.WriteLine("Produkten har ändrats.");


//            Thread.Sleep(2000);  // Pausar kort innan vi rensar
//            menu.MainMenu();
//        }
//        private void SaveProductsToFile(string filePath)
//        {
//            using (StreamWriter writer = new StreamWriter(filePath))
//            {
//                foreach (var product in listOfProducts)
//                {
//                    writer.WriteLine($"{product.PLU} {product.Name} {product.Price} {product.PriceType}");
//                }
//            }
//        }
//    }
//}
