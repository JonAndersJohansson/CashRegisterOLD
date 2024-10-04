using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister
{
    public class CartItem
    {
        public Products Product {  get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }           // Framtida rabatter?

        public CartItem(Products product, int quantity)
        {
            Product = product;
            Quantity = quantity;
            Discount = 0;                               // initialt ingen rabat
        }

        public decimal CalculateToPrice()
        {
            decimal totalPrice = Product.Price * Quantity;
            if (Discount > 0)
            {
                totalPrice -= totalPrice * Discount / 100;
            }
            return totalPrice;
        }




    }
}
