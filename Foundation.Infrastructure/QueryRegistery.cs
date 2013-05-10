using System.Reflection;
using Foundation.Infrastructure.Query;
using StructureMap.Configuration.DSL;

namespace Foundation.Infrastructure
{
    public class QueryRegistery : Registry , IQueryRegistery
    {
        public QueryRegistery()
        {
            this.For<IQueryContainer>().Use<QueryContainer>();

            this.Scan(x =>
            {
                x.Assembly(Assembly.GetExecutingAssembly().GetName().Name);
                x.With(new QueryRegisterationConventrion());
                x.ConnectImplementationsToTypesClosing(typeof(QueryInterceptor<>));
            });
        }
    }
}
