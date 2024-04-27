
using CatalogApi.Exceptions;

namespace CatalogApi.Products.DeleteProduct
{
    public record DeleteProductCommand(Guid Id):ICommand<DeleteProductCommandResult>;
    public record DeleteProductCommandResult(bool IsSuccess);

    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator() { 
            RuleFor(p=>p.Id).NotEmpty().WithMessage("Product ID is required");
        }
    }
    internal class DeleteProductCommandHandler(IDocumentSession session) : ICommandHandler<DeleteProductCommand, DeleteProductCommandResult>
    {
        public async Task<DeleteProductCommandResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            var existingProduct = await session.LoadAsync<Product>(command.Id, cancellationToken);
            if (existingProduct != null)
            {
                session.Delete<Product>(command.Id);
                await session.SaveChangesAsync(cancellationToken);
                return new DeleteProductCommandResult(true);

            }
            throw new ProductNotFoundException(command.Id);
        }
    }
}
