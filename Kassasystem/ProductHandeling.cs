using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kassasystem
{
    public class ProductHandeling
    {
        public List<Products> Products { get; set; }

        public ProductHandeling()
        {
            Products = new List<Products>();
            SeedProducts();                     //seeda
        }

        public void SeedProducts()
        {
            if (File.Exists("../../../MyProducts.txt"))
            {
                LoadProductsFromFile();
                return;
            }

            string text = "11 Jordgubbar 39,90 Styckpris\n" +
                          "22 Nutella 19,90 Styckpris\n" +
                          "33 Citron 4,90 Styckpris\n" +
                          "44 Bananer 9,90 Kilopris\n" +
                          "55 Grädde 15,90 Styckpris\n" +
                          "66 Choklad 9,90 Styckpris\n" +
                          "77 Apelsiner 29,90 Kilopris\n";

            File.WriteAllText("../../../MyProducts.txt", text);
            LoadProductsFromFile();
        }

        public void LoadProductsFromFile()
        {
            string[] lines = File.ReadAllLines("../../../MyProducts.txt");

            foreach (string line in lines)
            {
                string[] parts = line.Split(' ');
                int pluNumber = int.Parse(parts[0]);
                string name = parts[1];
                decimal price = decimal.Parse(parts[2]);
                PriceType priceType = parts[3] == "Styckpris" ? PriceType.Styckpris : PriceType.Kilopris;

                Products.Add(new Products(pluNumber, name, price, priceType));
            }
        }
    }   
}
