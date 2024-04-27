
using CatalogApi.Exceptions;

namespace CatalogApi.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid Id,string Name, List<string> Category, string Description, string ImageFile, decimal Price):ICommand<UpdateProductCommandResult>;
    public record UpdateProductCommandResult(bool IsSuccess) { }

    public class UpdateProdcutCommnadValidator : AbstractValidator<UpdateProductCommand>
    {
       public UpdateProdcutCommnadValidator   () {
            RuleFor(p => p.Id).NotEmpty().WithMessage("Product ID is required");
            RuleFor(p => p.Name).NotEmpty().WithMessage("Name is required")
                .Length(2, 150).WithMessage("Name must be between 2 and 150 characters");
            RuleFor(p => p.Category).NotEmpty().WithMessage("Category is required");
            RuleFor(p => p.ImageFile).NotEmpty().WithMessage("ImageFile is required");
            RuleFor(p => p.Price).GreaterThan(0).WithMessage("Price must be greator than 0");
        }
    }
    internal class UpdateProductCommandHandler(IDocumentSession session) : ICommandHandler<UpdateProductCommand, UpdateProductCommandResult>
    {
        public async Task<UpdateProductCommandResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var existingProduct = await session.LoadAsync<Product>(command.Id, cancellationToken);
            if (existingProduct != null)
            {
                existingProduct.Name = command.Name;
                existingProduct.Category = command.Category;
                existingProduct.Description = command.Description;
                existingProduct.ImageFile = command.ImageFile;
                existingProduct.Price = command.Price;
                session.Update(existingProduct);
                await session.SaveChangesAsync(cancellationToken);
                return new UpdateProductCommandResult(true);

            }
            throw new ProductNotFoundException(command.Id);
        }
    }
}
