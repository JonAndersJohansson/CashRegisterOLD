using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister
{
    public enum PriceType           //per styck eller kg
    {
        Styckpris,
        Kilopris
    }
    public class Products         //Blueprint för produkter
    {
        public int PLU {  get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public PriceType PriceType { get; set; }

        public Products ( int plu, string name, decimal price,  PriceType priceType )  //Konstruktor för nya produkter?
        {
            PLU = plu;
            Name = name;
            Price = price;
            PriceType = priceType;
        }
        public override string ToString() //Metod för att skriva ut specifik produkt
        {
            return $"{PLU} - {Name}, Pris: {Price}, Typ: {PriceType}";
        }
    }
}
