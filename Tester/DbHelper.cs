using db;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tester
{
    public static class DbHelper
    {

        public static void AddDBDatas(DbTestContext dbTestContext)
        {

            //AddAttributes

            dbTestContext.Attributes.Add(new db.Attribute() { id = 1, name = "color" });
            dbTestContext.Attributes.Add(new db.Attribute() { id = 2, name = "size" });
            dbTestContext.Attributes.Add(new db.Attribute() { id = 3, name = "material" });

            //Add Products
            var ProductId = 0;
            var Name = "Product ";
            var Price = 0;
            var sku = "1000";

            int attributeValueIds = 0;
            for (int i = 0; i < 15; i++)
            {
                ProductId = ProductId+1;
                Price = Price + 2;
                sku = sku + "-" + ProductId;
                dbTestContext.Products.Add(new Product() { id = ProductId, name = Name + " - " + ProductId, price = Price, sku = sku });

                

                if (i % 2 == 0)
                {
                    attributeValueIds = attributeValueIds+1;
                    dbTestContext.AttributeValues.Add(new AttributeValue()
                    {
                        id = Convert.ToInt32(attributeValueIds),
                        attributeId = 1,
                        productId = ProductId,
                        value = "red"
                    });
                    attributeValueIds = attributeValueIds+1;
                    dbTestContext.AttributeValues.Add(new AttributeValue()
                    {
                        id = Convert.ToInt32(attributeValueIds),
                        attributeId = 2,
                        productId = ProductId,
                        value = "large"
                    });
                }
                else
                {
                    attributeValueIds = attributeValueIds+1;
                    dbTestContext.AttributeValues.Add(new AttributeValue()
                    {
                        id = attributeValueIds,
                        attributeId = 1,
                        productId = ProductId,
                        value = "green"
                    });
                    attributeValueIds = attributeValueIds+1;
                    dbTestContext.AttributeValues.Add(new AttributeValue()
                    {
                        id = attributeValueIds,
                        attributeId = 2,
                        productId = ProductId,
                        value = "medium"
                    });
                    attributeValueIds = attributeValueIds+1;
                    dbTestContext.AttributeValues.Add(new AttributeValue()
                    {
                        id = attributeValueIds,
                        attributeId = 3,
                        productId = ProductId,
                        value = "rock"
                    });
                }

            }





            dbTestContext.SaveChanges();
        }

        public static int AddIds(int id)
        {
            return id++;
        }
        public static DbTestContext CreateInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<DbTestContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
               .Options;


            return new DbTestContext(options);
        }

    }
}
