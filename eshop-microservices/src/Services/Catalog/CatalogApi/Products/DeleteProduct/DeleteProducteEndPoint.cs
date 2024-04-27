
using CatalogApi.Products.UpdateProduct;

namespace CatalogApi.Products.DeleteProduct
{
    public record DeleteProductCommandResponse(bool IsSuccess);
    public class DeleteProducteEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/products/{id}", async (Guid id, ISender sender) =>
            {
                var command = new DeleteProductCommand(id);
                var result = await sender.Send(command);
                var response = result.Adapt<DeleteProductCommandResponse>();

                return Results.Ok(response);
            }).WithName("DeleteProduct")
            .Produces<DeleteProductCommandResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Delete Product")
            .WithDescription("Delete Product");
        }
    }
}
