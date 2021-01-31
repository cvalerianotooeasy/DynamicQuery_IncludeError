using Microsoft.EntityFrameworkCore;
using PoweredSoft.DynamicQuery;
using PoweredSoft.DynamicQuery.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic.Core;
using PoweredSoft.DynamicLinq;

namespace db.IncludeStrategies
{
    public class ProductStragetyInterceptorFail : IIncludeStrategyInterceptor<Product>
    {


        public IQueryable<Product> InterceptIncludeStrategy(IQueryCriteria criteria, IQueryable<Product> queryable)
        {
            var fields_filterSelect = new List<String>();



            //my goal is to add the fields I want to filter in the Select clause to save time in very large tables.

            //I Have an function to check witch field i add to fields_filterSelect array
            fields_filterSelect.Add("id");
            fields_filterSelect.Add("name");
            fields_filterSelect.Add("sku");

            //if i add this fields, the library get all fields on AttributeValues With include Attribute and IT Works Great by get all fields
            fields_filterSelect.Add("AttributeValues");


            //if i add this fields for select only the value fields, this string fail
            fields_filterSelect.Add("AttributeValues.value"); // ⚠ -- Value cannot be null ⚠

            var select = queryable.Select(x =>
            {
                x.NullChecking(false);
                x.DestinationType = typeof(Product);

                fields_filterSelect.Distinct().ToList().ForEach(y =>
                {
                    x.Path(y);
                });

            });

            return (IQueryable<Product>)select;
        }
    }
}
