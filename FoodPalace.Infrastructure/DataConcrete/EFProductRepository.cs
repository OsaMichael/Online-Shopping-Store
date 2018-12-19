﻿using FoodPalace.Infrastructure.Entities;
using FoodPalace.Infrastructure.Intaface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPalace.Infrastructure
{
  public  class EFProductRepository : IProductRepository
    {
        private readonly DataContext context = new DataContext();
        public IEnumerable<Product> Products
        {
            get { return context.Products; }
        }

        public Product DeleteProduct(int productId)
        {
            Product dbEntry = context.Products.Find(productId);
            if (dbEntry != null)
            {
                context.Products.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public void SaveProduct(Product product)
        {
            if (product.ProductId == 0)
            {
                context.Products.Add(product);
            }
            else
            {
                Product dbEntry = context.Products.Find(product.ProductId);
                if (dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.Category = product.Category;
                    dbEntry.Location = product.Location;
                    dbEntry.ImageUrl = product.ImageUrl;
                }

            }
            context.SaveChanges();
        }
    }
}
