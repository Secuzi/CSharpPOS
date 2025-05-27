using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace FinalOutput
{
    public class Product : IComparable<Product>
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int QtyInStock { get; set; }
        int _productQuantity;

        public int QtyInCart
        {
            get => _productQuantity;
            set
            {
                if (_productQuantity < 999 && value < 999) {
                    _productQuantity = value;
                }
            }           
        }
        public Product(string productName, decimal productPrice, int productQuantity = 0)
        {
            Id = Guid.NewGuid();
            ProductName = productName;
            ProductPrice = productPrice;
            QtyInStock = productQuantity;
            QtyInCart = 1;
        }
        public Product(Guid Guid, string productName, decimal productPrice, int productQuantity = 0, int selectedQuantity = 0)
        {
            Id = Guid;
            ProductName = productName;
            ProductPrice = productPrice;
            QtyInStock = productQuantity;
            QtyInCart = selectedQuantity;
        }


        //Used for sorting price instead of in alphabetical order (depending on the sorting of the folder).
        public int CompareTo(Product other)
        {
            if (this.ProductPrice > other.ProductPrice)
            {
                return 1;
            }
            else if (this.ProductPrice == other.ProductPrice)
            {
                return 0;
            }
            else
            {
                return -1;
            }
            
        }

        public decimal GetTotalPrice()
        {
            return QtyInCart * ProductPrice;
        }
    }


}
