using System;
using System.Collections.Generic;

namespace db
{
    public class Product
    {
        public int id { get; set; }
        public string name { get; set; }
        public string sku { get; set; }
        public double price  {get;set;}
        
        public List<AttributeValue> AttributeValues { get; set; }
    }
    public class AttributeValue
    {
        public int id { get; set; }
        public int productId { get; set; }
        public string value { get; set; }
        public int attributeId { get; set; }
        public Attribute Attribute { get; set; }
        public Product Product { get; set; }
    }
    public class Attribute
    {
        public int id { get; set; }
        public string name { get; set; }
        public List<AttributeValue> Values { get; set; }
    }
}
