
using CatalogApi.Products.CreateProduct;

namespace CatalogApi.Products.UpdateProduct
{
    public record UpdateProductRequest(Guid Id,string Name, List<string> Category, string Description, string ImageFile, decimal Price);

    public record UpdateProductCommandResponse(bool IsSuccess);
    public class UpdateProductEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/products/",async (UpdateProductRequest request,ISender sender) =>
            {
                var command = request.Adapt<UpdateProductCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<UpdateProductCommandResponse>();
                return Results.Ok(response);
            }).WithName("UpdateProduct")
            .Produces<UpdateProductCommandResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Update Product")
            .WithDescription("Update Product");
        }
    }
}
