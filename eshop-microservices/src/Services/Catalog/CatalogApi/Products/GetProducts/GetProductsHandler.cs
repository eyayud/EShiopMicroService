

namespace CatalogApi.Products.GetProducts
{
    public record GetProductsQuery(  int? PageNumber = 1, int? pageSize = 10):IQuery<GetProductsResult>;
    public record GetProductsResult(IEnumerable<Product> Products);
    internal class GetProductsQueryHandler(IDocumentSession session) : IQueryHandler<GetProductsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            var products = await session.Query<Product>().ToPagedListAsync(query.PageNumber??1,query.pageSize??10,cancellationToken);
            

            return new GetProductsResult(products);
        }
    }
}
