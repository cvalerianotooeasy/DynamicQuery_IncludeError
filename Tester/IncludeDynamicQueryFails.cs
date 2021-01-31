using db;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;
using System.Linq;
using PoweredSoft.DynamicQuery;
using db.IncludeStrategies;
using PoweredSoft.DynamicQuery.Core;

namespace Tester
{
    public class IncludeDynamicQueryFails
    {
        private ServiceProvider serviceProvider { get; set; }
        private DbTestContext DbTestContext { get; set; }
        public IncludeDynamicQueryFails()
        {


            DbTestContext = DbHelper.CreateInMemoryContext();
            DbHelper.AddDBDatas(DbTestContext);

        }

        [Fact]
        public void DynamicQuery_PartialSelect_Include_Fails()
        {
            var criteria = new QueryCriteria();
            criteria.Filters.Add(new SimpleFilter() { Value = 15, Path = "id", Type = FilterType.Equal });
            var _handler = new QueryHandler(Enumerable.Empty<IQueryInterceptorProvider>());
            IQueryable<Product> query = DbTestContext.Set<Product>();

            var _articlesStragetyInterceptor = new ProductStragetyInterceptorFail();
            _handler.AddInterceptor(_articlesStragetyInterceptor);

            var toconv = _handler.Execute<Product>(query, criteria);

            if (toconv.TotalRecords > 0)
            {
                Assert.True(toconv.Data.FirstOrDefault().AttributeValues.FirstOrDefault().value == "red");
            }

        }




        [Fact]
        public void DynamicQuery_PartialSelect_Include_Success()
        {
            var criteria = new QueryCriteria();
            criteria.Filters.Add(new SimpleFilter() { Value = 15, Path = "id", Type = FilterType.Equal });
            var _handler = new QueryHandler(Enumerable.Empty<IQueryInterceptorProvider>());
            IQueryable<Product> query = DbTestContext.Set<Product>();

            var _articlesStragetyInterceptor = new ProductStragetyInterceptor();
            _handler.AddInterceptor(_articlesStragetyInterceptor);



            var toconv = _handler.Execute<Product>(query, criteria);

            if (toconv.TotalRecords > 0)
            {
                Assert.True(toconv.Data.FirstOrDefault().AttributeValues.FirstOrDefault().value == "red");
            }
        }




    }
}
