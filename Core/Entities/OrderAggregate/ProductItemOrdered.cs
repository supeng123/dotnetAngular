using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities.OrderAggregate
{
    public class ProductItemOrdered
    {
        public ProductItemOrdered()
        {
        }

     
        public ProductItemOrdered(int productItemId, string ProductName, string pictureUrl) 
        {
            ProductItemId = productItemId;
            ProductName = ProductName;
            PictureUrl = pictureUrl;
        }
        public int ProductItemId{get; set;}
        public string ProductName{get; set;}
        public string PictureUrl{get; set;}
    }
}